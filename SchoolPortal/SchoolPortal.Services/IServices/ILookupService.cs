using System;
using System.Collections.Generic;
using System.Data;

namespace SchoolPortal.Services.IServices
{
    public class LookupItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public interface ILookupService
    {
        List<LookupItem> GetCountries();
        List<LookupItem> GetStates(Guid countryId);
        List<LookupItem> GetCities(Guid stateId);
        List<LookupItem> GetDepartments();
        List<LookupItem> GetDesignations();
        List<LookupItem> GetCompanies();
        List<LookupItem> GetSchools();
    }
}
