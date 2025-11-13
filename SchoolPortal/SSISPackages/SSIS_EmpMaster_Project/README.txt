SSIS PACKAGE FOR EMPMASTER DATA IMPORT
=====================================

This package imports employee data from an Excel spreadsheet into the EmpMaster SQL Server table using a stored procedure for data processing.

CONTENTS:
---------
1. EmpMaster_Template.csv - Template file for data entry (ID column removed)
2. EmpMaster_Import.dtsx - SSIS package file using stored procedure
3. Create_EmpMaster_Table.sql - SQL script to create the EmpMaster table
4. Upsert_Employee_StoredProc.sql - Stored procedure for data processing
5. Deploy_Package.bat - Deployment script

INSTRUCTIONS:
-------------

1. DATABASE SETUP:
   - Run Create_EmpMaster_Table.sql to create the EmpMaster table
   - Run Upsert_Employee_StoredProc.sql to create the stored procedure

2. TEMPLATE FILE USAGE:
   - Open EmpMaster_Template.csv in Excel or any spreadsheet application
   - Note that the ID column is not present in the template as it's auto-generated
   - Enter employee data in the rows below the header
   - Save the file as Excel (.xlsx) format
   - Ensure all required fields are filled appropriately

3. SSIS PACKAGE CONFIGURATION:
   - Open EmpMaster_Import.dtsx in SQL Server Data Tools (SSDT)
   - Update the Excel Connection Manager:
     * Set the correct path to your Excel file
     * Verify the Excel version (2007 or later)
   - Update the SQL Server Connection Manager:
     * Set the correct server name
     * Set the correct database name
     * Configure authentication (Windows or SQL Server)

4. COLUMN MAPPING:
   The package maps Excel columns to the sp_UpsertEmployeeData stored procedure parameters:
   
   FirstName, LastName, DOB, DOJ, ProbationStartDate, ProbationPeriod, ConfirmationDate, 
   PANNumber, ESICNumber, PFNumeber, CurrentAddress1, CurrentAddress2, CurrentCityId, 
   CurrentStateId, CurrentCountryId, CurrentZipCode, PermanentAddress1, PermanentAddress2, 
   PermanentCityId, PermanentStateId, PermanentCountryId, PermanentZipCode, PhoneNumber, 
   MobileNumber, EmailId, DepartmentId, DesignationId, PaymentModeId, EmployeeTypeId, 
   CategoryId, BankAccountNumber, BankName, GenderId, BloodGroupId, GradeId, Image, 
   EmployeeOldId, FathersName, MothersName, Description, LicenceNumber, LicenceIssueDate, 
   LicenceValidUpto, LicenceDescription, LicenceImage, LicenceType, Salutation, DateOfLeaving, 
   MaritalStatus, YearsOfExperience, PrevioudSchoolCompany, AadhaarNumber, MathUpToClass, 
   EnglishUptoClass, SSTUptoClass, CompanyId, SchoolId, IsActive, IsDeleted, CreatedBy, 
   CreatedDate, ModifiedBy, ModifiedDate, Status, StatusMessage, EmployeeCategoryId

5. EXECUTION:
   - Validate the package in SSDT
   - Execute the package to import data
   - Check the execution results for any errors

NOTES:
------
- The ID column is auto-generated in the database and not required in the source data
- The stored procedure handles both inserts and updates based on matching criteria
- Date fields should be in YYYY-MM-DD format
- Numeric IDs should be integers
- Boolean fields (IsActive, IsDeleted) should be 1 for true, 0 for false
- Large text fields (Description, Address fields) can contain longer text
- Binary data (Image, LicenceImage) should be handled separately or as file paths