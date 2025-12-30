using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SchoolPortal.DBAccess;
using Microsoft.Extensions.Logging;

namespace SchoolPortal.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        protected readonly ILogger<Repository<T>> _logger;
        protected readonly string _tableName;

        public Repository(ILogger<Repository<T>> logger, string tableName)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            using (var p = new Proc($"{_tableName}_GetById"))
            {
                p["@Id"] = id;
                var dt = new DataTable();
                await Task.Run(() => p.Exec(dt));
                return dt.AsEnumerable().Select(Map).FirstOrDefault();
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var p = new Proc($"{_tableName}_GetAll"))
            {
                var dt = new DataTable();
                await Task.Run(() => p.Exec(dt));
                return dt.AsEnumerable().Select(Map);
            }
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var all = await GetAllAsync();
            return all.AsQueryable().Where(predicate);
        }

        public virtual async Task AddAsync(T entity)
        {
            using (var p = new Proc($"{_tableName}_Insert"))
            {
                MapToParameters(entity, p);
                await Task.Run(() => p.ExecNonQuery());
            }
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                await AddAsync(entity);
            }
        }

        public virtual void Update(T entity)
        {
            using (var p = new Proc($"{_tableName}_Update"))
            {
                MapToParameters(entity, p);
                p.ExecNonQuery();
            }
        }

        public virtual void Remove(T entity)
        {
            // Assuming entity has an Id property
            var id = (Guid)entity.GetType().GetProperty("Id").GetValue(entity);
            Remove(id);
        }

        public virtual void Remove(Guid id)
        {
            using (var p = new Proc($"{_tableName}_Delete"))
            {
                p["@Id"] = id;
                p.ExecNonQuery();
            }
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Remove(entity);
            }
        }

        protected virtual T Map(DataRow row)
        {
            var obj = new T();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                if (row.Table.Columns.Contains(property.Name) && row[property.Name] != DBNull.Value)
                {
                    property.SetValue(obj, row[property.Name]);
                }
            }

            return obj;
        }

        protected virtual void MapToParameters(T entity, Proc proc)
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(entity);
                proc[$"@{property.Name}"] = value ?? DBNull.Value;
            }
        }
    }
}
