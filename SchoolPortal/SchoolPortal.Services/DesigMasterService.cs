using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class DesigMasterService : IDesigMasterService
    {
        private DesigMaster Map(DataRow row)
        {
            if (row == null) return null;
            
            return new DesigMaster
            {
                Id = row.Field<Guid?>("Id") ?? Guid.Empty,
                Code = row.Field<string>("Code"),
                Name = row.Field<string>("Name"),
                IsActive = row.Field<bool>("IsActive"),
                SchoolId = row.Field<Guid?>("SchoolId") ?? Guid.Empty,
                CreatedBy = row.Field<Guid>("CreatedBy"),
                CreatedDate = row.Field<DateTime>("CreatedDate"),
                ModifiedBy = row.Field<Guid?>("ModifiedBy"),
                ModifiedDate = row.Field<DateTime?>("ModifiedDate"),
                IsDeleted = row.Field<bool>("IsDeleted"),
                Status = row.Field<string>("Status"),
                StatusMessage = row.Field<string>("StatusMessage")
            };
        }

        public List<DesigMaster> GetAll()
        {
            Proc p = new Proc("DesigMaster_GetAll");
            var dt = new DataTable();
            p.Exec(dt);
            var list = new List<DesigMaster>();
            foreach (DataRow r in dt.Rows)
            {
                list.Add(Map(r));
            }
            return list;
        }

        public DesigMaster? GetById(Guid id)
        {
            Proc p = new Proc("DesigMaster_GetById");
            p["@Id"] = id;
            var dt = new DataTable();
            p.Exec(dt);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public Guid Create(DesigMaster desig)
        {
            if (desig == null)
                throw new ArgumentNullException(nameof(desig));

            desig.Id = Guid.NewGuid();
            desig.CreatedDate = DateTime.UtcNow;
            desig.IsActive = true;
            desig.IsDeleted = false;

            Proc p = new Proc("DesigMaster_Create");
            p["@Id"] = desig.Id;
            p["@Code"] = desig.Code;
            p["@Name"] = desig.Name;
            p["@IsActive"] = desig.IsActive;
            p["@SchoolId"] = desig.SchoolId;
            p["@CreatedBy"] = desig.CreatedBy;
            p["@CreatedDate"] = desig.CreatedDate;
            p["@ModifiedBy"] = desig.ModifiedBy;
            p["@ModifiedDate"] = desig.ModifiedDate;
            p["@Status"] = desig.Status;
            p["@StatusMessage"] = desig.StatusMessage;
            p.Exec();

            return desig.Id;
        }

        public bool Update(DesigMaster desig)
        {
            if (desig == null)
                throw new ArgumentNullException(nameof(desig));

            var existingDesig = GetById(desig.Id);

            if (existingDesig == null)
                return false;

            existingDesig.Code = desig.Code;
            existingDesig.Name = desig.Name;
            existingDesig.IsActive = desig.IsActive;
            existingDesig.SchoolId = desig.SchoolId;
            existingDesig.ModifiedBy = desig.ModifiedBy;
            existingDesig.ModifiedDate = desig.ModifiedDate;

            Proc p = new Proc("DesigMaster_Update");
            p["@Id"] = desig.Id;
            p["@Code"] = desig.Code;
            p["@Name"] = desig.Name;
            p["@IsActive"] = desig.IsActive;
            p["@SchoolId"] = desig.SchoolId;
            p["@CreatedBy"] = desig.CreatedBy;
            p["@CreatedDate"] = desig.CreatedDate;
            p["@ModifiedBy"] = desig.ModifiedBy;
            p["@ModifiedDate"] = desig.ModifiedDate;
            p["@Status"] = desig.Status;
            p["@StatusMessage"] = desig.StatusMessage;
            p.Exec();

            return true;
        }

        public bool Delete(Guid id)
        {
            var desig = GetById(id);

            if (desig == null)
                return false;

            desig.IsDeleted = true;
            desig.ModifiedDate = DateTime.UtcNow;
            
            Proc p = new Proc("DesigMaster_Delete");
            p["@Id"] = id;
            p.Exec();
            return true;
        }
    }
}