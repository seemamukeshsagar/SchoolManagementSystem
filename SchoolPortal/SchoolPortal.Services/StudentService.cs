using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace SchoolPortal.Services
{
    public class StudentService : IStudentService
    {
        private readonly ILookupService _lookupService;

        public StudentService(ILookupService lookupService)
        {
            _lookupService = lookupService ?? throw new ArgumentNullException(nameof(lookupService));
        }

        private static StudentMaster Map(DataRow r)
        {
            var s = new StudentMaster();

            // Identifiers
            if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"]?.ToString(), out var id)) s.Id = id;
            if (r.Table.Columns.Contains("RollNumber") && Guid.TryParse(r["RollNumber"]?.ToString(), out var roll)) s.RollNumber = roll;

            // Core info
            s.FirstName = r.Table.Columns.Contains("FirstName") ? r["FirstName"]?.ToString() ?? string.Empty : string.Empty;
            s.LastName = r.Table.Columns.Contains("LastName") ? r["LastName"]?.ToString() ?? string.Empty : string.Empty;
            s.Email = r.Table.Columns.Contains("Email") ? r["Email"]?.ToString() ?? string.Empty : string.Empty;
            s.Phone = r.Table.Columns.Contains("Phone") ? r["Phone"]?.ToString() ?? string.Empty : string.Empty;

            // Address & contact
            s.Address = r.Table.Columns.Contains("Address") ? r["Address"]?.ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("CityId") && Guid.TryParse(r["CityId"]?.ToString(), out var cityId)) s.CityId = cityId;
            if (r.Table.Columns.Contains("StateId") && Guid.TryParse(r["StateId"]?.ToString(), out var stateId)) s.StateId = stateId;
            if (r.Table.Columns.Contains("CountryId") && Guid.TryParse(r["CountryId"]?.ToString(), out var countryId)) s.CountryId = countryId;
            s.ZipCode = r.Table.Columns.Contains("ZipCode") ? r["ZipCode"]?.ToString() ?? string.Empty : string.Empty;
            s.ContactNumber = r.Table.Columns.Contains("ContactNumber") ? r["ContactNumber"]?.ToString() ?? string.Empty : string.Empty;
            s.EmergencyContactNumber = r.Table.Columns.Contains("EmergencyContactNumber") ? r["EmergencyContactNumber"]?.ToString() ?? string.Empty : string.Empty;

            // Dates & registration
            if (r.Table.Columns.Contains("DOB") && DateTime.TryParse(r["DOB"]?.ToString(), out var dob)) s.DOB = dob;
            if (r.Table.Columns.Contains("DOJ") && DateTime.TryParse(r["DOJ"]?.ToString(), out var doj)) s.DOJ = doj;
            s.RegistrationNumber = r.Table.Columns.Contains("RegistrationNumber") ? r["RegistrationNumber"]?.ToString() ?? string.Empty : string.Empty;

            // Academic
            if (r.Table.Columns.Contains("ClassId") && Guid.TryParse(r["ClassId"]?.ToString(), out var classId)) s.ClassId = classId;
            if (r.Table.Columns.Contains("SectionId") && Guid.TryParse(r["SectionId"]?.ToString(), out var sectionId)) s.SectionId = sectionId;

            // Transport & image
            if (r.Table.Columns.Contains("AvailTransport") && bool.TryParse(r["AvailTransport"]?.ToString(), out var availTransport)) s.AvailTransport = availTransport;
            s.Image = r.Table.Columns.Contains("Image") ? r["Image"]?.ToString() ?? string.Empty : string.Empty;

            // Category & flags
            if (r.Table.Columns.Contains("CategoryId") && Guid.TryParse(r["CategoryId"]?.ToString(), out var categoryId)) s.CategoryId = categoryId;
            if (r.Table.Columns.Contains("SiblingsIfAny") && bool.TryParse(r["SiblingsIfAny"]?.ToString(), out var siblingsIfAny)) s.SiblingsIfAny = siblingsIfAny;
            if (r.Table.Columns.Contains("SiblingClassId") && Guid.TryParse(r["SiblingClassId"]?.ToString(), out var siblingClassId)) s.SiblingClassId = siblingClassId;
            if (r.Table.Columns.Contains("Gender") && Guid.TryParse(r["Gender"]?.ToString(), out var gender)) s.Gender = gender;

            // Medical & birth
            s.DisabilityAny = r.Table.Columns.Contains("DisabilityAny") ? r["DisabilityAny"]?.ToString() ?? string.Empty : string.Empty;
            s.MedicalAlleryAny = r.Table.Columns.Contains("MedicalAlleryAny") ? r["MedicalAlleryAny"]?.ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("BirthCityId") && Guid.TryParse(r["BirthCityId"]?.ToString(), out var birthCityId)) s.BirthCityId = birthCityId;
            if (r.Table.Columns.Contains("BirthStateId") && Guid.TryParse(r["BirthStateId"]?.ToString(), out var birthStateId)) s.BirthStateId = birthStateId;
            if (r.Table.Columns.Contains("BirthCountryId") && Guid.TryParse(r["BirthCountryId"]?.ToString(), out var birthCountryId)) s.BirthCountryId = birthCountryId;

            // Previous school
            s.PreviousSchoolAttended = r.Table.Columns.Contains("PreviousSchoolAttended") ? r["PreviousSchoolAttended"]?.ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("PreviousSchoolClassId") && Guid.TryParse(r["PreviousSchoolClassId"]?.ToString(), out var prevSchoolClassId)) s.PreviousSchoolClassId = prevSchoolClassId;
            if (r.Table.Columns.Contains("PreviousSchoolPercentage") && decimal.TryParse(r["PreviousSchoolPercentage"]?.ToString(), out var prevPct)) s.PreviousSchoolPercentage = prevPct;
            s.PreviousSchoolRank = r.Table.Columns.Contains("PreviousSchoolRank") ? r["PreviousSchoolRank"]?.ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("PreviousSchoolBoardId") && Guid.TryParse(r["PreviousSchoolBoardId"]?.ToString(), out var prevBoardId)) s.PreviousSchoolBoardId = prevBoardId;
            if (r.Table.Columns.Contains("PreviousSchoolFromDate") && DateTime.TryParse(r["PreviousSchoolFromDate"]?.ToString(), out var prevFrom)) s.PreviousSchoolFromDate = prevFrom;
            if (r.Table.Columns.Contains("PreviousSchoolToDate") && DateTime.TryParse(r["PreviousSchoolToDate"]?.ToString(), out var prevTo)) s.PreviousSchoolToDate = prevTo;
            if (r.Table.Columns.Contains("WithdrawnDate") && DateTime.TryParse(r["WithdrawnDate"]?.ToString(), out var withdrawnDate)) s.WithdrawnDate = withdrawnDate;
            s.WithdrawnReason = r.Table.Columns.Contains("WithdrawnReason") ? r["WithdrawnReason"]?.ToString() ?? string.Empty : string.Empty;

            // Other info
            if (r.Table.Columns.Contains("BloodGroupId") && Guid.TryParse(r["BloodGroupId"]?.ToString(), out var bloodGroupId)) s.BloodGroupId = bloodGroupId;
            if (r.Table.Columns.Contains("Nationality") && Guid.TryParse(r["Nationality"]?.ToString(), out var nationality)) s.Nationality = nationality;
            s.Hobbies = r.Table.Columns.Contains("Hobbies") ? r["Hobbies"]?.ToString() ?? string.Empty : string.Empty;
            if (r.Table.Columns.Contains("ReligionId") && Guid.TryParse(r["ReligionId"]?.ToString(), out var religionId)) s.ReligionId = religionId;

            // Transport route details
            if (r.Table.Columns.Contains("RouteId") && Guid.TryParse(r["RouteId"]?.ToString(), out var routeId)) s.RouteId = routeId;
            if (r.Table.Columns.Contains("RouteStopDetailsId") && Guid.TryParse(r["RouteStopDetailsId"]?.ToString(), out var routeStopDetailsId)) s.RouteStopDetailsId = routeStopDetailsId;
            if (r.Table.Columns.Contains("ClassTeacherId") && Guid.TryParse(r["ClassTeacherId"]?.ToString(), out var classTeacherId)) s.ClassTeacherId = classTeacherId;
            if (r.Table.Columns.Contains("RoutePickAndDrop") && bool.TryParse(r["RoutePickAndDrop"]?.ToString(), out var routePickAndDrop)) s.RoutePickAndDrop = routePickAndDrop;

            // Fees
            if (r.Table.Columns.Contains("FeesDiscountCategoryMasterId") && Guid.TryParse(r["FeesDiscountCategoryMasterId"]?.ToString(), out var feesDiscCatId)) s.FeesDiscountCategoryMasterId = feesDiscCatId;
            if (r.Table.Columns.Contains("TutionFees") && decimal.TryParse(r["TutionFees"]?.ToString(), out var tutionFees)) s.TutionFees = tutionFees;
            if (r.Table.Columns.Contains("AnnualFees") && decimal.TryParse(r["AnnualFees"]?.ToString(), out var annualFees)) s.AnnualFees = annualFees;
            if (r.Table.Columns.Contains("TransportFees") && decimal.TryParse(r["TransportFees"]?.ToString(), out var transportFees)) s.TransportFees = transportFees;
            if (r.Table.Columns.Contains("UseTransportFees") && bool.TryParse(r["UseTransportFees"]?.ToString(), out var useTransportFees)) s.UseTransportFees = useTransportFees;

            // Session & ownership
            if (r.Table.Columns.Contains("SessionId") && Guid.TryParse(r["SessionId"]?.ToString(), out var sessionId)) s.SessionId = sessionId;
            if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"]?.ToString(), out var companyId)) s.CompanyId = companyId;
            if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"]?.ToString(), out var schoolId)) s.SchoolId = schoolId;

            // Status & audit
            if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"]?.ToString(), out var isActive)) s.IsActive = isActive;
            if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"]?.ToString(), out var isDeleted)) s.IsDeleted = isDeleted;
            if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"]?.ToString(), out var createdBy)) s.CreatedBy = createdBy;
            if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"]?.ToString(), out var createdDate)) s.CreatedDate = createdDate;
            if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"]?.ToString(), out var modifiedBy)) s.ModifiedBy = modifiedBy;
            if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"]?.ToString(), out var modifiedDate)) s.ModifiedDate = modifiedDate;
            s.Status = r.Table.Columns.Contains("Status") ? r["Status"]?.ToString() ?? string.Empty : string.Empty;
            s.StatusMessage = r.Table.Columns.Contains("StatusMessage") ? r["StatusMessage"]?.ToString() ?? string.Empty : string.Empty;

            // House
            if (r.Table.Columns.Contains("HouseAllotted") && Guid.TryParse(r["HouseAllotted"]?.ToString(), out var houseAllotted)) s.HouseAllotted = houseAllotted;

            return s;
        }

        public async Task<List<StudentMaster>> GetAllAsync(Guid? schoolId = null)
        {
            return await Task.Run(() => 
            {
                var p = new Proc("Student_GetAll");
                if (schoolId.HasValue)
                {
                    p["@SchoolId"] = schoolId.Value;
                }

                DataTable dt = new DataTable();
                p.Exec(dt);
                return dt.Rows.Cast<DataRow>().Select(Map).ToList();
            });
        }

        public async Task<StudentMaster> GetByIdAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                var p = new Proc("Student_GetById");
                p["@Id"] = id;
                DataTable dt = new DataTable();
                p.Exec(dt);
                if (dt.Rows.Count == 0) return null;
                return Map(dt.Rows[0]);
            });
        }

        public async Task<bool> CategoryExistsAsync(Guid categoryId)
        {
            if (categoryId == Guid.Empty) return false;

            try
            {
                var categories = await Task.Run(() => _lookupService.GetCategories());
                return categories.Any(c => c.Id == categoryId);
            }
            catch
            {
                return false;
            }
        }

        public async Task<Guid> CreateAsync(StudentMaster s)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));

            if (s.CompanyId == Guid.Empty || s.SchoolId == Guid.Empty || s.CreatedBy == Guid.Empty)
                throw new ArgumentException("Required fields (CompanyId, SchoolId, or CreatedBy) are missing");

            // Validate category exists asynchronously
            if (s.CategoryId != Guid.Empty && !await CategoryExistsAsync(s.CategoryId))
            {
                throw new ArgumentException("Invalid CategoryId. The specified category does not exist.");
            }

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

        public async Task<bool> UpdateAsync(StudentMaster s)
        {
            if (s == null || s.Id == Guid.Empty)
                throw new ArgumentException("Student or Student ID cannot be null");

            // Validate category exists asynchronously if provided
            if (s.CategoryId != Guid.Empty && !await CategoryExistsAsync(s.CategoryId))
            {
                throw new ArgumentException("Invalid CategoryId. The specified category does not exist.");
            }

            using (var p = new Proc("Student_Update"))
            {
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
                DataTable dt = new DataTable();
                p.Exec(dt);
                if (dt != null)
                {
                    var ret = p.Parameters["@RETURN_VALUE"].Value;
                    int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
                    return code == 1;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Student ID cannot be empty");

            using (var p = new Proc("Student_Delete"))
            {
                p["@Id"] = id;
                var result = await Task.Run(() => p.ExecuteNonQuery());
                return result > 0;
            }
        }

        public async Task<StudentAttendanceDetails?> GetStudentAttendanceByIdAsync(Guid id)
        {
            using (var p = new Proc("Student_GetById"))
            {
                p["@Id"] = id;
                var dt = new DataTable();
                p.Exec(dt);   // If ExecAsync exists, use: await p.ExecAsync(dt);

                if (dt.Rows.Count == 0)
                    return null;

                DataRow row = dt.Rows[0];

                return new StudentAttendanceDetails
                {
                    Id = row.Field<Guid>("Id"),
                    StudentGUID = row.Field<Guid>("StudentId"),
                    AttendenceDate = row.Field<DateTime>("AttendanceDate"),
                    AttendenceStatus = row.Field<bool>("IsPresent"),
                    AttendanceReasonId = row.Field<Guid>("ReasonId"),
                    //Comments = row.Field<string?>("Comments")
                };
            }
        }

        public async Task<Guid> CreateAsync(StudentAttendanceDetails attendance)
        {
            if (attendance == null)
                throw new ArgumentNullException(nameof(attendance));

            return await Task.Run(() =>
            {
                using (Proc p = new Proc("StudentAttendance_Create"))
                {
                    var id = Guid.NewGuid();
                    p["@Id"] = id;
                    p["@StudentId"] = attendance.StudentGUID;
                    p["@AttendanceDate"] = attendance.AttendenceDate;
                    p["@IsPresent"] = attendance.AttendenceStatus;
                    p["@ReasonId"] = attendance.AttendanceReasonId;
                    p["@Comments"] = string.Empty;

                    p.Exec();
                    return id;
                }
            });
        }

        public async Task<bool> UpdateAsync(StudentAttendanceDetails attendance)
        {
            if (attendance == null)
                throw new ArgumentNullException(nameof(attendance));

            return await Task.Run(() =>
            {
                using (Proc p = new Proc("StudentAttendance_Update"))
                {
                    p["@Id"] = attendance.Id;
                    p["@IsPresent"] = attendance.AttendenceStatus;
                    p["@ReasonId"] = attendance.AttendanceReasonId;
                    p["@Comments"] = attendance.Status ?? string.Empty;

                    DataTable dt = new DataTable();
                    p.Exec(dt);

                    if (dt.Rows.Count == 0)
                        return false;

                    return true;
                }
            });
        }

        private StudentAttendanceDetails MapToStudentAttendanceDetails(DataRow row)
        {
            if (row == null)
                return null;

            return new StudentAttendanceDetails
            {
                Id = row["Id"] != DBNull.Value ? (Guid)row["Id"] : Guid.Empty,

                StudentGUID = row["StudentGUID"] != DBNull.Value ? (Guid)row["StudentGUID"] : Guid.Empty,

                ClassId = row["ClassId"] != DBNull.Value ? (Guid)row["ClassId"] : Guid.Empty,

                SectionId = row["SectionId"] != DBNull.Value ? (Guid)row["SectionId"] : Guid.Empty,

                Month = row["Month"] != DBNull.Value ? Convert.ToInt32(row["Month"]) : (int?)null,

                Year = row["Year"] != DBNull.Value ? Convert.ToInt32(row["Year"]) : (int?)null,

                AttendenceDate = row["AttendenceDate"] != DBNull.Value
                    ? Convert.ToDateTime(row["AttendenceDate"])
                    : DateTime.MinValue,

                AttendenceStatus = row["AttendenceStatus"] != DBNull.Value
                    && Convert.ToBoolean(row["AttendenceStatus"]),

                AttendanceReasonId = row["AttendanceReasonId"] != DBNull.Value
                    ? (Guid)row["AttendanceReasonId"]
                    : Guid.Empty,

                AttendenceTime = row["AttendenceTime"] != DBNull.Value
                    ? row["AttendenceTime"].ToString()
                    : string.Empty,

                CompanyId = row["CompanyId"] != DBNull.Value ? (Guid)row["CompanyId"] : Guid.Empty,

                SchoolId = row["SchoolId"] != DBNull.Value ? (Guid)row["SchoolId"] : Guid.Empty,

                IsActive = row["IsActive"] != DBNull.Value && Convert.ToBoolean(row["IsActive"]),

                IsDeleted = row["IsDeleted"] != DBNull.Value && Convert.ToBoolean(row["IsDeleted"]),

                CreatedBy = row["CreatedBy"] != DBNull.Value ? (Guid)row["CreatedBy"] : Guid.Empty,

                CreatedDate = row["CreatedDate"] != DBNull.Value
                    ? Convert.ToDateTime(row["CreatedDate"])
                    : DateTime.MinValue,

                ModifiedBy = row["ModifiedBy"] != DBNull.Value ? (Guid?)row["ModifiedBy"] : null,

                ModifiedDate = row["ModifiedDate"] != DBNull.Value
                    ? Convert.ToDateTime(row["ModifiedDate"])
                    : (DateTime?)null,

                // Status fields provided by model defaults � override only if returned from DB
                Status = row.Table.Columns.Contains("Status") && row["Status"] != DBNull.Value
                    ? row["Status"].ToString()
                    : "INC",

                StatusMessage = row.Table.Columns.Contains("StatusMessage") && row["StatusMessage"] != DBNull.Value
                    ? row["StatusMessage"].ToString()
                    : "In Process...."
            };
        }

        // This method is now implemented as GetByIdAsync

        public async Task<StudentMaster> Create(StudentMaster student)
        {
            try
            {
                if (student == null)
                    throw new ArgumentNullException(nameof(student));
                // Generate new ID if not provided
                if (student.Id == Guid.Empty)
                    student.Id = Guid.NewGuid();
                using (var p = new Proc("Student_Create"))
                {
                    // Map StudentMaster properties to stored procedure parameters
                    p["@Id"] = student.Id;
                    p["@RollNumber"] = student.RollNumber;
                    p["@FirstName"] = student.FirstName;
                    p["@LastName"] = student.LastName;
                    p["@Address"] = student.Address;
                    p["@CityId"] = student.CityId;
                    p["@StateId"] = student.StateId;
                    p["@CountryId"] = student.CountryId;
                    p["@ZipCode"] = student.ZipCode;
                    p["@ContactNumber"] = student.ContactNumber;
                    p["@EmergencyContactNumber"] = student.EmergencyContactNumber;
                    p["@DOB"] = student.DOB;
                    p["@DOJ"] = student.DOJ;
                    p["@RegistrationNumber"] = student.RegistrationNumber;
                    p["@ClassId"] = student.ClassId;
                    p["@SectionId"] = student.SectionId;
                    p["@AvailTransport"] = student.AvailTransport;
                    p["@Image"] = student.Image;
                    p["@Email"] = student.Email;
                    p["@CategoryId"] = student.CategoryId;
                    p["@SiblingsIfAny"] = student.SiblingsIfAny;

                    var dt = new DataTable();
                    p.Exec(dt);

                    if (dt.Rows.Count > 0 && dt.Rows[0]["Id"] != DBNull.Value)
                    {
                        return student;
                    }
                    throw new Exception("Failed to create student.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating student: {ex.Message}", ex);
            }
        }

        public async Task<bool> Update(StudentMaster student)
        {
            try
            {
                using (var p = new Proc("Student_Update"))
                {
                    // Map StudentMaster properties to stored procedure parameters
                    p["@Id"] = student.Id;
                    p["@RollNumber"] = student.RollNumber;
                    p["@FirstName"] = student.FirstName;
                    p["@LastName"] = student.LastName;
                    p["@Address"] = student.Address;
                    p["@CityId"] = student.CityId;
                    p["@StateId"] = student.StateId;
                    p["@CountryId"] = student.CountryId;
                    p["@ZipCode"] = student.ZipCode;
                    p["@ContactNumber"] = student.ContactNumber;
                    p["@EmergencyContactNumber"] = student.EmergencyContactNumber;
                    p["@DOB"] = student.DOB;
                    p["@DOJ"] = student.DOJ;
                    p["@RegistrationNumber"] = student.RegistrationNumber;
                    p["@ClassId"] = student.ClassId;
                    p["@SectionId"] = student.SectionId;
                    p["@AvailTransport"] = student.AvailTransport;
                    p["@Image"] = student.Image;
                    p["@Email"] = student.Email;
                    p["@CategoryId"] = student.CategoryId;
                    p["@SiblingsIfAny"] = student.SiblingsIfAny;

                    DataTable dt = new DataTable();
                    p.Exec(dt);
                    var result = dt.Rows.Count;
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating student: {ex.Message}", ex);
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (var p = new Proc("Student_Delete"))
            {
                p["@Id"] = id;
                DataTable dt = new DataTable();
                await Task.Run(() => p.Exec(dt));
                return dt.Rows.Count > 0;
            }
        }
    }
}
