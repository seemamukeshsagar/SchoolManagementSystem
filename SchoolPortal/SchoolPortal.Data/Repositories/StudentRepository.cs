using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolPortal.Entities.Models;
using SchoolPortal.DBAccess;

namespace SchoolPortal.Data.Repositories
{
    public class StudentRepository : Repository<StudentMaster>, IRepository<StudentMaster>
    {
        public StudentRepository(ILogger<StudentRepository> logger) 
            : base(logger, "StudentMaster")
        {
        }

        public override async Task<StudentMaster> GetByIdAsync(Guid id)
        {
            using (var p = new Proc("StudentMaster_GetById"))
            {
                p["@Id"] = id;
                var dt = new DataTable();
                await Task.Run(() => p.Exec(dt));
                
                if (dt.Rows.Count == 0)
                    return null;
                    
                return MapStudent(dt.Rows[0]);
            }
        }

        protected override StudentMaster Map(DataRow row)
        {
            if (row == null) return null;

            return new StudentMaster
            {
                Id = row["Id"] as Guid? ?? Guid.Empty,
                FirstName = row["FirstName"] as string,
                LastName = row["LastName"] as string,
                Email = row["Email"] as string,
                ContactNumber = row["ContactNumber"] as string,
                Address = row["Address"] as string,
                DOB = row["DOB"] as DateTime? ?? DateTime.MinValue,
                Gender = row["Gender"] as Guid?,
                RegistrationNumber = row["RegistrationNumber"] as string,
                DOJ = row["DOJ"] as DateTime? ?? DateTime.MinValue,
                ClassId = row["ClassId"] as Guid? ?? Guid.Empty,
                SectionId = row["SectionId"] as Guid? ?? Guid.Empty,
                SchoolId = row["SchoolId"] as Guid? ?? Guid.Empty,
                CompanyId = row["CompanyId"] as Guid? ?? Guid.Empty,
                IsActive = row["IsActive"] as bool? ?? false,
                IsDeleted = row["IsDeleted"] as bool? ?? false,
                CreatedBy = row["CreatedBy"] as Guid? ?? Guid.Empty,
                CreatedDate = row["CreatedDate"] as DateTime? ?? DateTime.UtcNow,
                ModifiedBy = row["ModifiedBy"] as Guid?,
                ModifiedDate = row["ModifiedDate"] as DateTime?,
                // Map other properties as needed
            };
        }

        protected override void MapToParameters(StudentMaster entity, Proc proc)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (proc == null) throw new ArgumentNullException(nameof(proc));

            proc["@Id"] = entity.Id;
            proc["@FirstName"] = entity.FirstName;
            proc["@LastName"] = entity.LastName;
            proc["@Email"] = entity.Email ?? (object)DBNull.Value;
            proc["@ContactNumber"] = entity.ContactNumber ?? (object)DBNull.Value;
            proc["@Address"] = entity.Address ?? (object)DBNull.Value;
            proc["@DOB"] = entity.DOB == DateTime.MinValue ? (object)DBNull.Value : entity.DOB;
            proc["@Gender"] = entity.Gender ?? (object)DBNull.Value;
            proc["@RegistrationNumber"] = entity.RegistrationNumber ?? (object)DBNull.Value;
            proc["@DOJ"] = entity.DOJ == DateTime.MinValue ? (object)DBNull.Value : entity.DOJ;
            proc["@ClassId"] = entity.ClassId == Guid.Empty ? (object)DBNull.Value : entity.ClassId;
            proc["@SectionId"] = entity.SectionId == Guid.Empty ? (object)DBNull.Value : entity.SectionId;
            proc["@SchoolId"] = entity.SchoolId;
            proc["@CompanyId"] = entity.CompanyId;
            proc["@IsActive"] = entity.IsActive;
            proc["@IsDeleted"] = entity.IsDeleted;
            proc["@CreatedBy"] = entity.CreatedBy;
            proc["@CreatedDate"] = entity.CreatedDate;
            proc["@ModifiedBy"] = entity.ModifiedBy ?? (object)DBNull.Value;
            proc["@ModifiedDate"] = entity.ModifiedDate ?? (object)DBNull.Value;
        }

        private StudentMaster MapStudent(DataRow row)
        {
            return Map(row);
        }
    }
}
