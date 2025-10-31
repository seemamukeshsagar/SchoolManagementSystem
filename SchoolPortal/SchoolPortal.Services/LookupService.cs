using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class LookupService : ILookupService
    {
        private static List<LookupItem> Map(DataTable dt, string idCol = "Id", string nameCol = "Name")
        {
            var list = new List<LookupItem>();
            foreach (DataRow r in dt.Rows)
            {
                var item = new LookupItem();
                if (dt.Columns.Contains(idCol) && Guid.TryParse(r[idCol]?.ToString(), out var id)) item.Id = id;
                item.Name = dt.Columns.Contains(nameCol) ? (r[nameCol]?.ToString() ?? string.Empty) : string.Empty;
                list.Add(item);
            }
            return list;
        }

        public List<LookupItem> GetCountries()
        {
            try
            {
                Proc p = new Proc("Country_GetAll");
                var dt = new DataTable();
                p.Exec(dt);
                return Map(dt, "Id", "CountryName");
            }
            catch
            {
                // Fallback: return empty list if SP is missing to avoid runtime crash
                return new List<LookupItem>();
            }
        }

        public List<LookupItem> GetStates(Guid countryId)
        {
            Proc p = new Proc("State_GetByCountry");
            p["@CountryId"] = countryId;
            var dt = new DataTable();
            p.Exec(dt);
            return Map(dt, "Id", "StateName");
        }

        public List<LookupItem> GetCities(Guid stateId)
        {
            Proc p = new Proc("City_GetByState");
            p["@StateId"] = stateId;
            var dt = new DataTable();
            p.Exec(dt);
            return Map(dt, "Id", "CityName");
        }

        public List<LookupItem> GetDepartments()
        {
            try
            {
                Proc p = new Proc("Department_GetAll");
                var dt = new DataTable();
                p.Exec(dt);
                return Map(dt, "Id", "DepartmentName");
            }
            catch (Exception)
            {
                // Log the exception if needed
                return new List<LookupItem>();
            }
        }

        public List<LookupItem> GetDesignations()
        {
            try
            {
                Proc p = new Proc("Designation_GetAll");
                var dt = new DataTable();
                p.Exec(dt);
                return Map(dt, "Id", "DesignationName");
            }
            catch (Exception)
            {
                // Log the exception if needed
                return new List<LookupItem>();
            }
        }

        public List<LookupItem> GetCompanies()
        {
            try
            {
                Proc p = new Proc("Company_GetAll");
                var dt = new DataTable();
                p.Exec(dt);
                return Map(dt, "Id", "CompanyName");
            }
            catch (Exception)
            {
                // Log the exception if needed
                return new List<LookupItem>();
            }
        }

        public List<LookupItem> GetSchools()
        {
            try
            {
                Proc p = new Proc("School_GetAll");
                var dt = new DataTable();
                p.Exec(dt);
                return Map(dt, "Id", "SchoolName");
            }
            catch (Exception)
            {
                // Log the exception if needed
                return new List<LookupItem>();
            }
        }

        public List<LookupItem> GetGenders()
        {
            try
            {
                Proc p = new Proc("Gender_GetAll");
                var dt = new DataTable();
                p.Exec(dt);
                return Map(dt, "Id", "Gender");
            }
            catch (Exception)
            {
                // Log the exception if needed
                return new List<LookupItem>();
            }
        }
    }
}
