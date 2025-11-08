using SchoolPortal.Entities.Models;
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
		List<LookupItem> GetQualifications();
		List<LookupItem> GetRelationTypes();
		List<LookupItem> GetCompanies();
		List<LookupItem> GetSchools();
		List<LookupItem> GetGenders();
		List<LookupItem> GetPaymentModes();
		List<LookupItem> GetEmployeeTypes();
		List<LookupItem> GetEmployeeCategories();
		List<LookupItem> GetCategories();
		List<LookupItem> GetGrades();
		List<LookupItem> GetBloodGroups();
		List<LookupItem> GetReligions();
		List<LookupItem> GetSchoolBoards();
		IEnumerable<ClassMaster> GetClasses();
		IEnumerable<SectionMaster> GetSections();
		IEnumerable<LocationMaster> GetLocations();
	}
}
