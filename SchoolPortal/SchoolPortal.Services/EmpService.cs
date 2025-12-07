using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
	public class EmpService : IEmpService
	{
		private new readonly ILogger<EmpService> _logger;
		
		public EmpService(ILogger<EmpService> logger)
		{
			_logger = logger;
		}
		
		private static EmpMaster MapEmp(DataRow r)
		{
			var e = new EmpMaster();
			if (r.Table.Columns.Contains("Id") && Guid.TryParse(r["Id"].ToString(), out var id)) e.Id = id;
			if (r.Table.Columns.Contains("FirstName")) e.FirstName = r["FirstName"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("LastName")) e.LastName = r["LastName"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("DOB") && DateTime.TryParse(r["DOB"].ToString(), out var dob)) e.DOB = dob;
			if (r.Table.Columns.Contains("DOJ") && DateTime.TryParse(r["DOJ"].ToString(), out var doj)) e.DOJ = doj;
			if (r.Table.Columns.Contains("ProbationStartDate") && DateTime.TryParse(r["ProbationStartDate"].ToString(), out var probStart)) e.ProbationStartDate = probStart; else e.ProbationStartDate = null;
			if (r.Table.Columns.Contains("ProbationPeriod") && int.TryParse(r["ProbationPeriod"].ToString(), out var probPeriod)) e.ProbationPeriod = probPeriod; else e.ProbationPeriod = null;
			if (r.Table.Columns.Contains("ConfirmationDate") && DateTime.TryParse(r["ConfirmationDate"].ToString(), out var conf)) e.ConfirmationDate = conf; else e.ConfirmationDate = null;
			if (r.Table.Columns.Contains("PANNumber")) e.PANNumber = r["PANNumber"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("ESICNumber")) e.ESICNumber = r["ESICNumber"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("PFNumeber")) e.PFNumeber = r["PFNumeber"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("CurrentAddress1")) e.CurrentAddress1 = r["CurrentAddress1"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("CurrentAddress2")) e.CurrentAddress2 = r["CurrentAddress2"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("CurrentCityId") && Guid.TryParse(r["CurrentCityId"].ToString(), out var curCity)) e.CurrentCityId = curCity; else e.CurrentCityId = null;
			if (r.Table.Columns.Contains("CurrentStateId") && Guid.TryParse(r["CurrentStateId"].ToString(), out var curState)) e.CurrentStateId = curState; else e.CurrentStateId = null;
			if (r.Table.Columns.Contains("CurrentCountryId") && Guid.TryParse(r["CurrentCountryId"].ToString(), out var curCountry)) e.CurrentCountryId = curCountry; else e.CurrentCountryId = null;
			if (r.Table.Columns.Contains("CurrentZipCode")) e.CurrentZipCode = r["CurrentZipCode"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("PermanentAddress1")) e.PermanentAddress1 = r["PermanentAddress1"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("PermanentAddress2")) e.PermanentAddress2 = r["PermanentAddress2"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("PermanentCityId") && Guid.TryParse(r["PermanentCityId"].ToString(), out var perCity)) e.PermanentCityId = perCity; else e.PermanentCityId = null;
			if (r.Table.Columns.Contains("PermanentStateId") && Guid.TryParse(r["PermanentStateId"].ToString(), out var perState)) e.PermanentStateId = perState; else e.PermanentStateId = null;
			if (r.Table.Columns.Contains("PermanentCountryId") && Guid.TryParse(r["PermanentCountryId"].ToString(), out var perCountry)) e.PermanentCountryId = perCountry; else e.PermanentCountryId = null;
			if (r.Table.Columns.Contains("PermanentZipCode")) e.PermanentZipCode = r["PermanentZipCode"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("PhoneNumber")) e.PhoneNumber = r["PhoneNumber"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("MobileNumber")) e.MobileNumber = r["MobileNumber"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("EmailId")) e.EmailId = r["EmailId"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("DepartmentId") && Guid.TryParse(r["DepartmentId"].ToString(), out var dept)) e.DepartmentId = dept; else e.DepartmentId = null;
			if (r.Table.Columns.Contains("DesignationId") && Guid.TryParse(r["DesignationId"].ToString(), out var desig)) e.DesignationId = desig; else e.DesignationId = null;
			if (r.Table.Columns.Contains("PaymentModeId") && Guid.TryParse(r["PaymentModeId"].ToString(), out var pay)) e.PaymentModeId = pay; else e.PaymentModeId = null;
			if (r.Table.Columns.Contains("EmployeeTypeId") && Guid.TryParse(r["EmployeeTypeId"].ToString(), out var et)) e.EmployeeTypeId = et; else e.EmployeeTypeId = null;
			if (r.Table.Columns.Contains("CategoryId") && Guid.TryParse(r["CategoryId"].ToString(), out var cat)) e.CategoryId = cat; else e.CategoryId = null;
			if (r.Table.Columns.Contains("BankAccountNumber")) e.BankAccountNumber = r["BankAccountNumber"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("BankName")) e.BankName = r["BankName"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("GenderId") && Guid.TryParse(r["GenderId"].ToString(), out var gen)) e.GenderId = gen; else e.GenderId = null;
			if (r.Table.Columns.Contains("BloodGroupId") && Guid.TryParse(r["BloodGroupId"].ToString(), out var bg)) e.BloodGroupId = bg; else e.BloodGroupId = null;
			if (r.Table.Columns.Contains("GradeId") && Guid.TryParse(r["GradeId"].ToString(), out var gr)) e.GradeId = gr; else e.GradeId = null;
			if (r.Table.Columns.Contains("Image")) e.Image = r["Image"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("EmployeeOldId") && Guid.TryParse(r["EmployeeOldId"].ToString(), out var oldId)) e.EmployeeOldId = oldId; else e.EmployeeOldId = null;
			if (r.Table.Columns.Contains("FathersName")) e.FathersName = r["FathersName"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("MothersName")) e.MothersName = r["MothersName"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("Description")) e.Description = r["Description"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("LicenceNumber")) e.LicenceNumber = r["LicenceNumber"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("LicenceIssueDate") && DateTime.TryParse(r["LicenceIssueDate"].ToString(), out var lid)) e.LicenceIssueDate = lid; else e.LicenceIssueDate = null;
			if (r.Table.Columns.Contains("LicenceValidUpto") && DateTime.TryParse(r["LicenceValidUpto"].ToString(), out var lvu)) e.LicenceValidUpto = lvu; else e.LicenceValidUpto = null;
			if (r.Table.Columns.Contains("LicenceDescription")) e.LicenceDescription = r["LicenceDescription"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("LicenceImage")) e.LicenceImage = r["LicenceImage"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("LicenceType")) e.LicenceType = r["LicenceType"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("Salutation")) e.Salutation = r["Salutation"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("DateOfLeaving") && DateTime.TryParse(r["DateOfLeaving"].ToString(), out var dol)) e.DateOfLeaving = dol; else e.DateOfLeaving = null;
			if (r.Table.Columns.Contains("MaritalStatus")) e.MaritalStatus = r["MaritalStatus"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("YearsOfExperience")) e.YearsOfExperience = r["YearsOfExperience"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("PrevioudSchoolCompany")) e.PrevioudSchoolCompany = r["PrevioudSchoolCompany"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("AadhaarNumber")) e.AadhaarNumber = r["AadhaarNumber"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("MathUpToClass") && int.TryParse(r["MathUpToClass"].ToString(), out var mc)) e.MathUpToClass = mc; else e.MathUpToClass = null;
			if (r.Table.Columns.Contains("EnglishUptoClass") && int.TryParse(r["EnglishUptoClass"].ToString(), out var ec)) e.EnglishUptoClass = ec; else e.EnglishUptoClass = null;
			if (r.Table.Columns.Contains("SSTUptoClass") && int.TryParse(r["SSTUptoClass"].ToString(), out var sc)) e.SSTUptoClass = sc; else e.SSTUptoClass = null;
			if (r.Table.Columns.Contains("CompanyId") && Guid.TryParse(r["CompanyId"].ToString(), out var comp)) e.CompanyId = comp;
			if (r.Table.Columns.Contains("SchoolId") && Guid.TryParse(r["SchoolId"].ToString(), out var school)) e.SchoolId = school;
			if (r.Table.Columns.Contains("IsActive") && bool.TryParse(r["IsActive"].ToString(), out var active)) e.IsActive = active;
			if (r.Table.Columns.Contains("IsDeleted") && bool.TryParse(r["IsDeleted"].ToString(), out var deleted)) e.IsDeleted = deleted;
			if (r.Table.Columns.Contains("CreatedBy") && Guid.TryParse(r["CreatedBy"].ToString(), out var createdBy)) e.CreatedBy = createdBy;
			if (r.Table.Columns.Contains("CreatedDate") && DateTime.TryParse(r["CreatedDate"].ToString(), out var createdDate)) e.CreatedDate = createdDate;
			if (r.Table.Columns.Contains("ModifiedBy") && Guid.TryParse(r["ModifiedBy"].ToString(), out var modifiedBy)) e.ModifiedBy = modifiedBy; else e.ModifiedBy = null;
			if (r.Table.Columns.Contains("ModifiedDate") && DateTime.TryParse(r["ModifiedDate"].ToString(), out var modifiedDate)) e.ModifiedDate = modifiedDate; else e.ModifiedDate = null;
			if (r.Table.Columns.Contains("Status")) e.Status = r["Status"].ToString() ?? string.Empty;
			if (r.Table.Columns.Contains("StatusMessage")) e.StatusMessage = r["StatusMessage"].ToString() ?? string.Empty;
			return e;
		}

		public List<EmpMaster> GetAll()
		{
			var list = new List<EmpMaster>();
			Proc p = new Proc("Emp_GetAll");
			var dt = new DataTable();
			p.Exec(dt);
			foreach (DataRow r in dt.Rows)
			{
				list.Add(MapEmp(r));
			}
			return list;
		}

		public EmpMaster? GetById(Guid id)
		{
			Proc p = new Proc("Emp_GetById");
			p["@Id"] = id;
			var dt = new DataTable();
			p.Exec(dt);
			if (dt.Rows.Count == 0) return null;
			return MapEmp(dt.Rows[0]);
		}

		public Guid Create(EmpMaster emp)
		{
			try
			{
				_logger.LogInformation("Creating employee: {FirstName} {LastName}", emp.FirstName, emp.LastName);
				_logger.LogInformation("Employee details - DOB: {DOB}, Email: {Email}, SchoolId: {SchoolId}, CompanyId: {CompanyId}", emp.DOB, emp.EmailId, emp.SchoolId, emp.CompanyId);
				
				Proc p = new Proc("Emp_Create");
				p["@FirstName"] = emp.FirstName ?? string.Empty;
				p["@LastName"] = emp.LastName ?? string.Empty;
				p["@DOB"] = emp.DOB;
				p["@DOJ"] = emp.DOJ;
				p["@ProbationStartDate"] = (object?)emp.ProbationStartDate ?? DBNull.Value;
				p["@ProbationPeriod"] = (object?)emp.ProbationPeriod ?? DBNull.Value;
				p["@ConfirmationDate"] = (object?)emp.ConfirmationDate ?? DBNull.Value;
				p["@PANNumber"] = emp.PANNumber ?? string.Empty;
				p["@ESICNumber"] = emp.ESICNumber ?? string.Empty;
				p["@PFNumeber"] = emp.PFNumeber ?? string.Empty;
				p["@CurrentAddress1"] = emp.CurrentAddress1 ?? string.Empty;
				p["@CurrentAddress2"] = emp.CurrentAddress2 ?? string.Empty;
				p["@CurrentCityId"] = (object?)emp.CurrentCityId ?? DBNull.Value;
				p["@CurrentStateId"] = (object?)emp.CurrentStateId ?? DBNull.Value;
				p["@CurrentCountryId"] = (object?)emp.CurrentCountryId ?? DBNull.Value;
				p["@CurrentZipCode"] = emp.CurrentZipCode ?? string.Empty;
				p["@PermanentAddress1"] = emp.PermanentAddress1 ?? string.Empty;
				p["@PermanentAddress2"] = emp.PermanentAddress2 ?? string.Empty;
				p["@PermanentCityId"] = (object?)emp.PermanentCityId ?? DBNull.Value;
				p["@PermanentStateId"] = (object?)emp.PermanentStateId ?? DBNull.Value;
				p["@PermanentCountryId"] = (object?)emp.PermanentCountryId ?? DBNull.Value;
				p["@PermanentZipCode"] = emp.PermanentZipCode ?? string.Empty;
				p["@PhoneNumber"] = emp.PhoneNumber ?? string.Empty;
				p["@MobileNumber"] = emp.MobileNumber ?? string.Empty;
				p["@EmailId"] = emp.EmailId ?? string.Empty;
				p["@DepartmentId"] = (object?)emp.DepartmentId ?? DBNull.Value;
				p["@DesignationId"] = (object?)emp.DesignationId ?? DBNull.Value;
				p["@PaymentModeId"] = (object?)emp.PaymentModeId ?? DBNull.Value;
				p["@EmployeeTypeId"] = (object?)emp.EmployeeTypeId ?? DBNull.Value;
				p["@CategoryId"] = (object?)emp.CategoryId ?? DBNull.Value;
				p["@BankAccountNumber"] = emp.BankAccountNumber ?? string.Empty;
				p["@BankName"] = emp.BankName ?? string.Empty;
				p["@GenderId"] = (object?)emp.GenderId ?? DBNull.Value;
				p["@BloodGroupId"] = (object?)emp.BloodGroupId ?? DBNull.Value;
				p["@GradeId"] = (object?)emp.GradeId ?? DBNull.Value;
				p["@Image"] = emp.Image ?? string.Empty;
				p["@EmployeeOldId"] = (object?)emp.EmployeeOldId ?? DBNull.Value;
				p["@FathersName"] = emp.FathersName ?? string.Empty;
				p["@MothersName"] = emp.MothersName ?? string.Empty;
				p["@Description"] = emp.Description ?? string.Empty;
				p["@LicenceNumber"] = emp.LicenceNumber ?? string.Empty;
				p["@LicenceIssueDate"] = (object?)emp.LicenceIssueDate ?? DBNull.Value;
				p["@LicenceValidUpto"] = (object?)emp.LicenceValidUpto ?? DBNull.Value;
				p["@LicenceDescription"] = emp.LicenceDescription ?? string.Empty;
				p["@LicenceImage"] = emp.LicenceImage ?? string.Empty;
				p["@LicenceType"] = emp.LicenceType ?? string.Empty;
				p["@Salutation"] = emp.Salutation ?? string.Empty;
				p["@DateOfLeaving"] = (object?)emp.DateOfLeaving ?? DBNull.Value;
				p["@MaritalStatus"] = emp.MaritalStatus ?? string.Empty;
				p["@YearsOfExperience"] = emp.YearsOfExperience ?? string.Empty;
				p["@PrevioudSchoolCompany"] = emp.PrevioudSchoolCompany ?? string.Empty;
				p["@AadhaarNumber"] = emp.AadhaarNumber ?? string.Empty;
				p["@MathUpToClass"] = (object?)emp.MathUpToClass ?? DBNull.Value;
				p["@EnglishUptoClass"] = (object?)emp.EnglishUptoClass ?? DBNull.Value;
				p["@SSTUptoClass"] = (object?)emp.SSTUptoClass ?? DBNull.Value;
				p["@CompanyId"] = emp.CompanyId;
				p["@SchoolId"] = emp.SchoolId;
				p["@IsActive"] = emp.IsActive;
				p["@CreatedBy"] = emp.CreatedBy;
				p["@Status"] = emp.Status ?? string.Empty;
				p["@StatusMessage"] = emp.StatusMessage ?? string.Empty;
				
				_logger.LogInformation("Executing Emp_Create stored procedure");
				var dt = new DataTable();
				p.Exec(dt);
				_logger.LogInformation("Emp_Create stored procedure executed, rows returned: {RowCount}", dt.Rows.Count);
				
				// The Emp_Create stored procedure returns the ID using SELECT Id = @NewId
				if (dt.Rows.Count > 0)
				{
					var idObj = dt.Rows[0]["Id"];
					if (idObj != null && Guid.TryParse(idObj.ToString(), out var newId))
					{
						_logger.LogInformation("Successfully created employee with ID: {EmpId}", newId);
						return newId;
					}
					else
					{
						_logger.LogWarning("Failed to parse ID from stored procedure result");
					}
				}
				else
				{
					_logger.LogWarning("No rows returned from Emp_Create stored procedure");
				}
				return Guid.Empty;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating employee: {Message}", ex.Message);
				// Log the exception for debugging
				throw new Exception($"Error creating employee: {ex.Message}", ex);
			}
		}

		public bool Update(EmpMaster emp)
		{
			Proc p = new Proc("Emp_Update");
			p["@Id"] = emp.Id;
			p["@FirstName"] = emp.FirstName ?? string.Empty;
			p["@LastName"] = emp.LastName ?? string.Empty;
			p["@DOB"] = emp.DOB;
			p["@DOJ"] = emp.DOJ;
			p["@ProbationStartDate"] = (object?)emp.ProbationStartDate ?? DBNull.Value;
			p["@ProbationPeriod"] = (object?)emp.ProbationPeriod ?? DBNull.Value;
			p["@ConfirmationDate"] = (object?)emp.ConfirmationDate ?? DBNull.Value;
			p["@PANNumber"] = emp.PANNumber ?? string.Empty;
			p["@ESICNumber"] = emp.ESICNumber ?? string.Empty;
			p["@PFNumeber"] = emp.PFNumeber ?? string.Empty;
			p["@CurrentAddress1"] = emp.CurrentAddress1 ?? string.Empty;
			p["@CurrentAddress2"] = emp.CurrentAddress2 ?? string.Empty;
			p["@CurrentCityId"] = (object?)emp.CurrentCityId ?? DBNull.Value;
			p["@CurrentStateId"] = (object?)emp.CurrentStateId ?? DBNull.Value;
			p["@CurrentCountryId"] = (object?)emp.CurrentCountryId ?? DBNull.Value;
			p["@CurrentZipCode"] = emp.CurrentZipCode ?? string.Empty;
			p["@PermanentAddress1"] = emp.PermanentAddress1 ?? string.Empty;
			p["@PermanentAddress2"] = emp.PermanentAddress2 ?? string.Empty;
			p["@PermanentCityId"] = (object?)emp.PermanentCityId ?? DBNull.Value;
			p["@PermanentStateId"] = (object?)emp.PermanentStateId ?? DBNull.Value;
			p["@PermanentCountryId"] = (object?)emp.PermanentCountryId ?? DBNull.Value;
			p["@PermanentZipCode"] = emp.PermanentZipCode ?? string.Empty;
			p["@PhoneNumber"] = emp.PhoneNumber ?? string.Empty;
			p["@MobileNumber"] = emp.MobileNumber ?? string.Empty;
			p["@EmailId"] = emp.EmailId ?? string.Empty;
			p["@DepartmentId"] = (object?)emp.DepartmentId ?? DBNull.Value;
			p["@DesignationId"] = (object?)emp.DesignationId ?? DBNull.Value;
			p["@PaymentModeId"] = (object?)emp.PaymentModeId ?? DBNull.Value;
			p["@EmployeeTypeId"] = (object?)emp.EmployeeTypeId ?? DBNull.Value;
			p["@CategoryId"] = (object?)emp.CategoryId ?? DBNull.Value;
			p["@BankAccountNumber"] = emp.BankAccountNumber ?? string.Empty;
			p["@BankName"] = emp.BankName ?? string.Empty;
			p["@GenderId"] = (object?)emp.GenderId ?? DBNull.Value;
			p["@BloodGroupId"] = (object?)emp.BloodGroupId ?? DBNull.Value;
			p["@GradeId"] = (object?)emp.GradeId ?? DBNull.Value;
			p["@Image"] = emp.Image ?? string.Empty;
			p["@EmployeeOldId"] = (object?)emp.EmployeeOldId ?? DBNull.Value;
			p["@FathersName"] = emp.FathersName ?? string.Empty;
			p["@MothersName"] = emp.MothersName ?? string.Empty;
			p["@Description"] = emp.Description ?? string.Empty;
			p["@LicenceNumber"] = emp.LicenceNumber ?? string.Empty;
			p["@LicenceIssueDate"] = (object?)emp.LicenceIssueDate ?? DBNull.Value;
			p["@LicenceValidUpto"] = (object?)emp.LicenceValidUpto ?? DBNull.Value;
			p["@LicenceDescription"] = emp.LicenceDescription ?? string.Empty;
			p["@LicenceImage"] = emp.LicenceImage ?? string.Empty;
			p["@LicenceType"] = emp.LicenceType ?? string.Empty;
			p["@Salutation"] = emp.Salutation ?? string.Empty;
			p["@DateOfLeaving"] = (object?)emp.DateOfLeaving ?? DBNull.Value;
			p["@MaritalStatus"] = emp.MaritalStatus ?? string.Empty;
			p["@YearsOfExperience"] = emp.YearsOfExperience ?? string.Empty;
			p["@PrevioudSchoolCompany"] = emp.PrevioudSchoolCompany ?? string.Empty;
			p["@AadhaarNumber"] = emp.AadhaarNumber ?? string.Empty;
			p["@MathUpToClass"] = (object?)emp.MathUpToClass ?? DBNull.Value;
			p["@EnglishUptoClass"] = (object?)emp.EnglishUptoClass ?? DBNull.Value;
			p["@SSTUptoClass"] = (object?)emp.SSTUptoClass ?? DBNull.Value;
			p["@IsActive"] = emp.IsActive;
			p["@ModifiedBy"] = emp.ModifiedBy ?? Guid.Empty;
			p["@Status"] = emp.Status ?? string.Empty;
			p["@StatusMessage"] = emp.StatusMessage ?? string.Empty;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}

		public bool Delete(Guid id)
		{
			Proc p = new Proc("Emp_Delete");
			p["@Id"] = id;
			p.Exec();
			var ret = p.Parameters["@RETURN_VALUE"].Value;
			int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
			return code == 1;
		}
	}
}
