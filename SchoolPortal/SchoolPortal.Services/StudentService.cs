using System;
using System.Collections.Generic;
using System.Data;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
	public class StudentService : IStudentService
	{
		private static StudentMaster Map(DataRow r)
		{
			var s = new StudentMaster();
			if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) s.Id = id;
			if (r.Table.Columns.Contains("RollNumber") && Guid.TryParse(r["RollNumber"].ToString(), out var roll)) s.RollNumber = roll;
			s.FirstName = r.Table.Columns.Contains("FirstName") ? r["FirstName"].ToString() ?? string.Empty : string.Empty;
			s.LastName = r.Table.Columns.Contains("LastName") ? r["LastName"].ToString() ?? string.Empty : string.Empty;
			s.Email = r.Table.Columns.Contains("Email") ? r["Email"].ToString() ?? string.Empty : string.Empty;
			s.Phone = r.Table.Columns.Contains("Phone") ? r["Phone"].ToString() ?? string.Empty : string.Empty;
			if (r.Table.Columns.Contains("DOB") && DateTime.TryParse(r["DOB"].ToString(), out var dob)) s.DOB = dob;
			if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) s.IsActive = active;
			if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) s.IsDeleted = deleted;
			if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var companyId)) s.CompanyId = companyId;
			if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var schoolId)) s.SchoolId = schoolId;
			if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) s.CreatedBy = createdBy;
			if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) s.CreatedDate = createdDate;
			if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) s.ModifiedBy = modifiedBy;
			if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) s.ModifiedDate = modifiedDate;
			s.Status = r.Table.Columns.Contains("Status") ? r["Status"].ToString() ?? string.Empty : string.Empty;
			s.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"].ToString() ?? string.Empty : string.Empty;
			return s;
		}

		public List<StudentMaster> GetAll()
		{
			var list = new List<StudentMaster>();
			Proc p = new Proc("Student_GetAll");
			var dt = new DataTable();
			p.Exec(dt);
			foreach (DataRow r in dt.Rows)
			{
				list.Add(Map(r));
			}
			return list;
		}

		public StudentMaster? GetById(Guid id)
		{
			Proc p = new Proc("Student_GetById");
			p["@Id"] = id;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count == 0) return null;
			return Map(dt.Rows[0]);
		}

		public Guid Create(StudentMaster s)
		{
			Proc p = new Proc("Student_Create");
			// Optional identifiers
			p["@RollNumber"] = s.RollNumber;

			// Core info
			p["@FirstName"] = s.FirstName;
			p["@LastName"] = s.LastName ?? string.Empty;
			p["@Email"] = s.Email ?? string.Empty;
			p["@Phone"] = s.Phone ?? string.Empty;

			// Address & contact
			p["@Address"] = s.Address ?? string.Empty;
			p["@CityId"] = s.CityId;
			p["@StateId"] = s.StateId;
			p["@CountryId"] = s.CountryId;
			p["@ZipCode"] = s.ZipCode ?? string.Empty;
			p["@ContactNumber"] = s.ContactNumber ?? string.Empty;
			p["@EmergencyContactNumber"] = s.EmergencyContactNumber ?? string.Empty;

			// Dates & registration
			p["@DOB"] = s.DOB;
			p["@DOJ"] = s.DOJ;
			p["@RegistrationNumber"] = s.RegistrationNumber ?? string.Empty;

			// Academic
			p["@ClassId"] = s.ClassId;
			p["@SectionId"] = s.SectionId;

			// Transport & image
			p["@AvailTransport"] = s.AvailTransport;
			p["@Image"] = s.Image ?? string.Empty;

			// Category & flags
			p["@CategoryId"] = s.CategoryId;
			p["@SiblingsIfAny"] = s.SiblingsIfAny;
			p["@SiblingClassId"] = s.SiblingClassId;
			p["@Gender"] = s.Gender;

			// Medical & birth
			p["@DisabilityAny"] = s.DisabilityAny ?? string.Empty;
			p["@MedicalAlleryAny"] = s.MedicalAlleryAny ?? string.Empty;
			p["@BirthCityId"] = s.BirthCityId;
			p["@BirthStateId"] = s.BirthStateId;
			p["@BirthCountryId"] = s.BirthCountryId;

			// Previous school
			p["@PreviousSchoolAttended"] = s.PreviousSchoolAttended ?? string.Empty;
			p["@PreviousSchoolClassId"] = s.PreviousSchoolClassId;
			p["@PreviousSchoolPercentage"] = s.PreviousSchoolPercentage;
			p["@PreviousSchoolRank"] = s.PreviousSchoolRank ?? string.Empty;
			if (s.PreviousSchoolBoardId != Guid.Empty)
			{
				p["@PreviousSchoolBoardId"] = s.PreviousSchoolBoardId;
			}
			else
			{
				p["@PreviousSchoolBoardId"] = new Guid("9C6B72D5-EE6D-48FA-AF3D-05BFF3198617");
			}
			p["@PreviousSchoolFromDate"] = s.PreviousSchoolFromDate;
			p["@PreviousSchoolToDate"] = s.PreviousSchoolToDate;
			p["@WithdrawnDate"] = s.WithdrawnDate;
			p["@WithdrawnReason"] = s.WithdrawnReason ?? string.Empty;

			// Other info
			p["@BloodGroupId"] = s.BloodGroupId;
			p["@Nationality"] = s.Nationality;
			p["@Hobbies"] = s.Hobbies ?? string.Empty;
			p["@ReligionId"] = s.ReligionId;

			// Transport route details
			p["@RouteId"] = s.RouteId;
			p["@RouteStopDetailsId"] = s.RouteStopDetailsId;
			p["@ClassTeacherId"] = s.ClassTeacherId;
			p["@RoutePickAndDrop"] = s.RoutePickAndDrop;

			// Fees
			p["@FeesDiscountCategoryMasterId"] = s.FeesDiscountCategoryMasterId;
			p["@TutionFees"] = s.TutionFees;
			p["@AnnualFees"] = s.AnnualFees;
			p["@TransportFees"] = s.TransportFees;
			p["@UseTransportFees"] = s.UseTransportFees;

			// Session & ownership
			p["@SessionId"] = s.SessionId;
			p["@CompanyId"] = s.CompanyId;
			p["@SchoolId"] = s.SchoolId;

			// Status & audit
			p["@IsActive"] = s.IsActive;
			p["@IsDeleted"] = s.IsDeleted;
			p["@CreatedBy"] = s.CreatedBy;
			p["@CreatedDate"] = s.CreatedDate;
			p["@Status"] = s.Status ?? string.Empty;
			p["@StatusMessage"] = s.StatusMessage ?? string.Empty;

			// House
			p["@HouseAllotted"] = s.HouseAllotted;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count > 0)
			{
				var idObj = dt.Rows[0]["Id"];
				if (idObj != null && Guid.TryParse(idObj.ToString(), out var newId))
				{
					return newId;
				}
			}
			return Guid.Empty;
		}

		public bool Update(StudentMaster s)
		{
			Proc p = new Proc("Student_Update");
			p["@Id"] = s.Id;

			// Optional identifiers
			p["@RollNumber"] = s.RollNumber;

			// Core info
			p["@FirstName"] = s.FirstName;
			p["@LastName"] = s.LastName ?? string.Empty;
			p["@Email"] = s.Email ?? string.Empty;
			p["@Phone"] = s.Phone ?? string.Empty;

			// Address & contact
			p["@Address"] = s.Address ?? string.Empty;
			p["@CityId"] = s.CityId;
			p["@StateId"] = s.StateId;
			p["@CountryId"] = s.CountryId;
			p["@ZipCode"] = s.ZipCode ?? string.Empty;
			p["@ContactNumber"] = s.ContactNumber ?? string.Empty;
			p["@EmergencyContactNumber"] = s.EmergencyContactNumber ?? string.Empty;

			// Dates & registration
			p["@DOB"] = s.DOB;
			p["@DOJ"] = s.DOJ;
			p["@RegistrationNumber"] = s.RegistrationNumber ?? string.Empty;

			// Academic
			p["@ClassId"] = s.ClassId;
			p["@SectionId"] = s.SectionId;

			// Transport & image
			p["@AvailTransport"] = s.AvailTransport;
			p["@Image"] = s.Image ?? string.Empty;

			// Category & flags
			p["@CategoryId"] = s.CategoryId;
			p["@SiblingsIfAny"] = s.SiblingsIfAny;
			p["@SiblingClassId"] = s.SiblingClassId;
			p["@Gender"] = s.Gender;

			// Medical & birth
			p["@DisabilityAny"] = s.DisabilityAny ?? string.Empty;
			p["@MedicalAlleryAny"] = s.MedicalAlleryAny ?? string.Empty;
			p["@BirthCityId"] = s.BirthCityId;
			p["@BirthStateId"] = s.BirthStateId;
			p["@BirthCountryId"] = s.BirthCountryId;

			// Previous school
			p["@PreviousSchoolAttended"] = s.PreviousSchoolAttended ?? string.Empty;
			p["@PreviousSchoolClassId"] = s.PreviousSchoolClassId;
			p["@PreviousSchoolPercentage"] = s.PreviousSchoolPercentage;
			p["@PreviousSchoolRank"] = s.PreviousSchoolRank ?? string.Empty;
			p["@PreviousSchoolBoardId"] = s.PreviousSchoolBoardId;
			p["@PreviousSchoolFromDate"] = s.PreviousSchoolFromDate;
			p["@PreviousSchoolToDate"] = s.PreviousSchoolToDate;
			p["@WithdrawnDate"] = s.WithdrawnDate;
			p["@WithdrawnReason"] = s.WithdrawnReason ?? string.Empty;

			// Other info
			p["@BloodGroupId"] = s.BloodGroupId;
			p["@Nationality"] = s.Nationality;
			p["@Hobbies"] = s.Hobbies ?? string.Empty;
			p["@ReligionId"] = s.ReligionId;

			// Transport route details
			p["@RouteId"] = s.RouteId;
			p["@RouteStopDetailsId"] = s.RouteStopDetailsId;
			p["@ClassTeacherId"] = s.ClassTeacherId;
			p["@RoutePickAndDrop"] = s.RoutePickAndDrop;

			// Fees
			p["@FeesDiscountCategoryMasterId"] = s.FeesDiscountCategoryMasterId;
			p["@TutionFees"] = s.TutionFees;
			p["@AnnualFees"] = s.AnnualFees;
			p["@TransportFees"] = s.TransportFees;
			p["@UseTransportFees"] = s.UseTransportFees;

			// Session & ownership
			p["@SessionId"] = s.SessionId;
			p["@CompanyId"] = s.CompanyId; // included based on proc signature
			p["@SchoolId"] = s.SchoolId;

			// Status & audit
			p["@IsActive"] = s.IsActive;
			p["@IsDeleted"] = s.IsDeleted;
			p["@ModifiedBy"] = s.ModifiedBy ?? Guid.Empty;
			p["@Status"] = s.Status ?? string.Empty;
			p["@StatusMessage"] = s.StatusMessage ?? string.Empty;

			// House
			p["@HouseAllotted"] = s.HouseAllotted;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		public bool Delete(Guid id)
		{
			Proc p = new Proc("Student_Delete");
			p["@Id"] = id;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}
	}
}
