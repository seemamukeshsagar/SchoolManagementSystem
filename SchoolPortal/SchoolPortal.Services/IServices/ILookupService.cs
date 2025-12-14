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
        List<LookupItem> GetSchools(Guid schoolId);
        List<LookupItem> GetGenders();
		List<LookupItem> GetPaymentModes();
		List<LookupItem> GetPaymentModes(Guid schoolId);
		List<LookupItem> GetEmployeeTypes();
		List<LookupItem> GetEmployeeTypes(Guid schoolId);
		List<LookupItem> GetEmployeeCategories();
		List<LookupItem> GetCategories();
		List<LookupItem> GetGrades();
		List<LookupItem> GetGrades(Guid schoolId);
		List<LookupItem> GetBloodGroups();
		List<LookupItem> GetReligions();
		List<LookupItem> GetMaritalStatuses();
		List<LookupItem> GetSchoolBoards();
		List<ClassMaster> GetClasses();
		List<SectionMaster> GetSections();
		List<LocationMaster> GetLocations();
		List<StudentMaster> GetStudents(Guid schoolId);
		List<AttendanceReasonMaster> GetAttendanceReasons(Guid schoolId);
		Task<List<StudentMaster>> GetStudentsAsync(Guid schoolId);
		Task<List<ClassMaster>> GetClassesAsync();
		Task<List<SectionMaster>> GetSectionsAsync();
		Task<List<AttendanceReasonMaster>> GetAttendanceReasonsAsync(Guid schoolId);
	}
}
