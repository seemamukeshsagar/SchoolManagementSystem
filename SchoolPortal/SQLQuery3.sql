USE [SchoolManagementSystem]
GO
/****** Object:  StoredProcedure [dbo].[Visitor_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Visitor_Update]
GO
/****** Object:  StoredProcedure [dbo].[Visitor_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Visitor_GetById]
GO
/****** Object:  StoredProcedure [dbo].[Visitor_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Visitor_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Visitor_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Visitor_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Visitor_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Visitor_Create]
GO
/****** Object:  StoredProcedure [dbo].[Vendor_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Vendor_Update]
GO
/****** Object:  StoredProcedure [dbo].[Vendor_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Vendor_GetById]
GO
/****** Object:  StoredProcedure [dbo].[Vendor_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Vendor_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Vendor_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Vendor_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Vendor_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Vendor_Create]
GO
/****** Object:  StoredProcedure [dbo].[VehicleTypeMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[VehicleTypeMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[VehicleTypeMaster_GetBySchool]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[VehicleTypeMaster_GetBySchool]
GO
/****** Object:  StoredProcedure [dbo].[VehicleTypeMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[VehicleTypeMaster_GetById]
GO
/****** Object:  StoredProcedure [dbo].[VehicleTypeMaster_GetByCompany]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[VehicleTypeMaster_GetByCompany]
GO
/****** Object:  StoredProcedure [dbo].[VehicleTypeMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[VehicleTypeMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[VehicleTypeMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[VehicleTypeMaster_Delete]
GO
/****** Object:  StoredProcedure [dbo].[VehicleTypeMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[VehicleTypeMaster_Create]
GO
/****** Object:  StoredProcedure [dbo].[usp_select_SMSTaskSchedule]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[usp_select_SMSTaskSchedule]
GO
/****** Object:  StoredProcedure [dbo].[UserDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[UserDetails_Update]
GO
/****** Object:  StoredProcedure [dbo].[UserDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[UserDetails_GetById]
GO
/****** Object:  StoredProcedure [dbo].[UserDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[UserDetails_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[UserDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[UserDetails_Delete]
GO
/****** Object:  StoredProcedure [dbo].[UserDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[UserDetails_Create]
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserDetails]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[UpdateUserDetails]
GO
/****** Object:  StoredProcedure [dbo].[TimeTableSetupDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTableSetupDetails_Update]
GO
/****** Object:  StoredProcedure [dbo].[TimeTableSetupDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTableSetupDetails_GetById]
GO
/****** Object:  StoredProcedure [dbo].[TimeTableSetupDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTableSetupDetails_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[TimeTableSetupDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTableSetupDetails_Delete]
GO
/****** Object:  StoredProcedure [dbo].[TimeTableSetupDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTableSetupDetails_Create]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriodMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriodMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriodMaster_GetBySetupId]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriodMaster_GetBySetupId]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriodMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriodMaster_GetById]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriodMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriodMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriodMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriodMaster_Delete]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriodMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriodMaster_Create]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriod_Update]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_Save]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriod_Save]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_IsTeacherAvailable]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriod_IsTeacherAvailable]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_IsClassroomAvailable]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriod_IsClassroomAvailable]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_Insert]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriod_Insert]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_GetByTeacherId]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriod_GetByTeacherId]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_GetBySubjectId]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriod_GetBySubjectId]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_GetBySetupId]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriod_GetBySetupId]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriod_GetById]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_GetByClassSectionAndAcademicYear]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriod_GetByClassSectionAndAcademicYear]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriod_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_DeleteByClassSectionAndAcademicYear]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriod_DeleteByClassSectionAndAcademicYear]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriod_Delete]
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TimeTablePeriod_Create]
GO
/****** Object:  StoredProcedure [dbo].[TeacherSubjectDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherSubjectDetails_Update]
GO
/****** Object:  StoredProcedure [dbo].[TeacherSubjectDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherSubjectDetails_GetById]
GO
/****** Object:  StoredProcedure [dbo].[TeacherSubjectDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherSubjectDetails_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[TeacherSubjectDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherSubjectDetails_Delete]
GO
/****** Object:  StoredProcedure [dbo].[TeacherSubjectDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherSubjectDetails_Create]
GO
/****** Object:  StoredProcedure [dbo].[TeacherSectionDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherSectionDetails_Update]
GO
/****** Object:  StoredProcedure [dbo].[TeacherSectionDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherSectionDetails_GetById]
GO
/****** Object:  StoredProcedure [dbo].[TeacherSectionDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherSectionDetails_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[TeacherSectionDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherSectionDetails_Delete]
GO
/****** Object:  StoredProcedure [dbo].[TeacherSectionDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherSectionDetails_Create]
GO
/****** Object:  StoredProcedure [dbo].[TeacherQualificationDetails_GetByTeacher]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherQualificationDetails_GetByTeacher]
GO
/****** Object:  StoredProcedure [dbo].[TeacherQualificationDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherQualificationDetails_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[TeacherDocumentDetails_GetByTeacher]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherDocumentDetails_GetByTeacher]
GO
/****** Object:  StoredProcedure [dbo].[TeacherDocumentDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherDocumentDetails_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[TeacherClassDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherClassDetails_Update]
GO
/****** Object:  StoredProcedure [dbo].[TeacherClassDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherClassDetails_GetById]
GO
/****** Object:  StoredProcedure [dbo].[TeacherClassDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherClassDetails_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[TeacherClassDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherClassDetails_Delete]
GO
/****** Object:  StoredProcedure [dbo].[TeacherClassDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[TeacherClassDetails_Create]
GO
/****** Object:  StoredProcedure [dbo].[Teacher_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Teacher_Update]
GO
/****** Object:  StoredProcedure [dbo].[Teacher_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Teacher_GetById]
GO
/****** Object:  StoredProcedure [dbo].[Teacher_GetAllActive_BySchool]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Teacher_GetAllActive_BySchool]
GO
/****** Object:  StoredProcedure [dbo].[Teacher_GetAllActive]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Teacher_GetAllActive]
GO
/****** Object:  StoredProcedure [dbo].[Teacher_GetAll_SchoolId]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Teacher_GetAll_SchoolId]
GO
/****** Object:  StoredProcedure [dbo].[Teacher_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Teacher_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Teacher_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Teacher_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Teacher_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Teacher_Create]
GO
/****** Object:  StoredProcedure [dbo].[SystemParameters_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SystemParameters_Update]
GO
/****** Object:  StoredProcedure [dbo].[SystemParameters_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SystemParameters_GetById]
GO
/****** Object:  StoredProcedure [dbo].[SystemParameters_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SystemParameters_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[SystemParameters_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SystemParameters_Delete]
GO
/****** Object:  StoredProcedure [dbo].[SystemParameters_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SystemParameters_Create]
GO
/****** Object:  StoredProcedure [dbo].[Supplier_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Supplier_Update]
GO
/****** Object:  StoredProcedure [dbo].[Supplier_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Supplier_GetById]
GO
/****** Object:  StoredProcedure [dbo].[Supplier_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Supplier_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Supplier_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Supplier_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Supplier_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Supplier_Create]
GO
/****** Object:  StoredProcedure [dbo].[SubjectCategory_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SubjectCategory_Update]
GO
/****** Object:  StoredProcedure [dbo].[SubjectCategory_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SubjectCategory_GetById]
GO
/****** Object:  StoredProcedure [dbo].[SubjectCategory_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SubjectCategory_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[SubjectCategory_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SubjectCategory_Delete]
GO
/****** Object:  StoredProcedure [dbo].[SubjectCategory_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SubjectCategory_Create]
GO
/****** Object:  StoredProcedure [dbo].[Subject_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Subject_Update]
GO
/****** Object:  StoredProcedure [dbo].[Subject_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Subject_GetById]
GO
/****** Object:  StoredProcedure [dbo].[Subject_GetByClassId]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Subject_GetByClassId]
GO
/****** Object:  StoredProcedure [dbo].[Subject_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Subject_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Subject_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Subject_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Subject_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Subject_Create]
GO
/****** Object:  StoredProcedure [dbo].[StudentMaster_GetBySchool]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[StudentMaster_GetBySchool]
GO
/****** Object:  StoredProcedure [dbo].[Student_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Student_Update]
GO
/****** Object:  StoredProcedure [dbo].[Student_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Student_GetById]
GO
/****** Object:  StoredProcedure [dbo].[Student_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Student_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Student_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Student_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Student_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Student_Create]
GO
/****** Object:  StoredProcedure [dbo].[State_GetByCountry]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[State_GetByCountry]
GO
/****** Object:  StoredProcedure [dbo].[sp_SetCurrentAcademicYear]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_SetCurrentAcademicYear]
GO
/****** Object:  StoredProcedure [dbo].[sp_RolePrivilege_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_RolePrivilege_Update]
GO
/****** Object:  StoredProcedure [dbo].[sp_RolePrivilege_GetByRoleId]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_RolePrivilege_GetByRoleId]
GO
/****** Object:  StoredProcedure [dbo].[sp_Privilege_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_Privilege_Update]
GO
/****** Object:  StoredProcedure [dbo].[sp_Privilege_IsInUse]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_Privilege_IsInUse]
GO
/****** Object:  StoredProcedure [dbo].[sp_Privilege_GetByRoleId]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_Privilege_GetByRoleId]
GO
/****** Object:  StoredProcedure [dbo].[sp_Privilege_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_Privilege_GetById]
GO
/****** Object:  StoredProcedure [dbo].[sp_Privilege_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_Privilege_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[sp_Privilege_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_Privilege_Delete]
GO
/****** Object:  StoredProcedure [dbo].[sp_Privilege_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_Privilege_Create]
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeachingDocument_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_NonTeachingDocument_Update]
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeachingDocument_ToggleVerification]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_NonTeachingDocument_ToggleVerification]
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeachingDocument_Insert]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_NonTeachingDocument_Insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeachingDocument_GetByNonTeachingId]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_NonTeachingDocument_GetByNonTeachingId]
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeachingDocument_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_NonTeachingDocument_GetById]
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeachingDocument_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_NonTeachingDocument_Delete]
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeaching_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_NonTeaching_Update]
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeaching_ToggleStatus]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_NonTeaching_ToggleStatus]
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeaching_Insert]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_NonTeaching_Insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeaching_GetBySchoolId]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_NonTeaching_GetBySchoolId]
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeaching_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_NonTeaching_GetById]
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeaching_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_NonTeaching_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeaching_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[sp_NonTeaching_Delete]
GO
/****** Object:  StoredProcedure [dbo].[SessionMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SessionMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[SessionMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SessionMaster_GetById]
GO
/****** Object:  StoredProcedure [dbo].[SessionMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SessionMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[SessionMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SessionMaster_Delete]
GO
/****** Object:  StoredProcedure [dbo].[SessionMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SessionMaster_Create]
GO
/****** Object:  StoredProcedure [dbo].[Section_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Section_Update]
GO
/****** Object:  StoredProcedure [dbo].[Section_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Section_GetById]
GO
/****** Object:  StoredProcedure [dbo].[Section_GetByClassId]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Section_GetByClassId]
GO
/****** Object:  StoredProcedure [dbo].[Section_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Section_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Section_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Section_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Section_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Section_Create]
GO
/****** Object:  StoredProcedure [dbo].[SchoolContact_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SchoolContact_Update]
GO
/****** Object:  StoredProcedure [dbo].[SchoolContact_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SchoolContact_GetById]
GO
/****** Object:  StoredProcedure [dbo].[SchoolContact_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SchoolContact_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[SchoolContact_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SchoolContact_Delete]
GO
/****** Object:  StoredProcedure [dbo].[SchoolContact_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[SchoolContact_Create]
GO
/****** Object:  StoredProcedure [dbo].[School_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[School_Update]
GO
/****** Object:  StoredProcedure [dbo].[School_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[School_GetById]
GO
/****** Object:  StoredProcedure [dbo].[School_GetByCompany]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[School_GetByCompany]
GO
/****** Object:  StoredProcedure [dbo].[School_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[School_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[School_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[School_Delete]
GO
/****** Object:  StoredProcedure [dbo].[School_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[School_Create]
GO
/****** Object:  StoredProcedure [dbo].[RoleMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[RoleMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[RoleMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[RoleMaster_GetById]
GO
/****** Object:  StoredProcedure [dbo].[RoleMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[RoleMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[RoleMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[RoleMaster_Delete]
GO
/****** Object:  StoredProcedure [dbo].[RoleMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[RoleMaster_Create]
GO
/****** Object:  StoredProcedure [dbo].[Report_GetStudents]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Report_GetStudents]
GO
/****** Object:  StoredProcedure [dbo].[Report_GetItemStockMovement]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Report_GetItemStockMovement]
GO
/****** Object:  StoredProcedure [dbo].[Report_GetInventoryItems]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Report_GetInventoryItems]
GO
/****** Object:  StoredProcedure [dbo].[Report_GetFeeCollection]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Report_GetFeeCollection]
GO
/****** Object:  StoredProcedure [dbo].[Report_GetEmployeeLeaves]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Report_GetEmployeeLeaves]
GO
/****** Object:  StoredProcedure [dbo].[Report_ExportEmployeeLeaves]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Report_ExportEmployeeLeaves]
GO
/****** Object:  StoredProcedure [dbo].[Religion_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Religion_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[RelationType_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[RelationType_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[QualificationMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[QualificationMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[QualificationMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[QualificationMaster_GetById]
GO
/****** Object:  StoredProcedure [dbo].[QualificationMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[QualificationMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[QualificationMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[QualificationMaster_Delete]
GO
/****** Object:  StoredProcedure [dbo].[QualificationMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[QualificationMaster_Create]
GO
/****** Object:  StoredProcedure [dbo].[Qualification_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Qualification_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[ProfessionMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[ProfessionMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[ProfessionMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[ProfessionMaster_GetById]
GO
/****** Object:  StoredProcedure [dbo].[ProfessionMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[ProfessionMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[ProfessionMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[ProfessionMaster_Delete]
GO
/****** Object:  StoredProcedure [dbo].[ProfessionMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[ProfessionMaster_Create]
GO
/****** Object:  StoredProcedure [dbo].[PaymentMode_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[PaymentMode_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Parent_GetByStudentId]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Parent_GetByStudentId]
GO
/****** Object:  StoredProcedure [dbo].[Parent_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Parent_Create]
GO
/****** Object:  StoredProcedure [dbo].[MaritalStatus_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[MaritalStatus_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[IsUserExist]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[IsUserExist]
GO
/****** Object:  StoredProcedure [dbo].[HolidayTypeMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[HolidayTypeMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[HolidayTypeMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[HolidayTypeMaster_GetById]
GO
/****** Object:  StoredProcedure [dbo].[HolidayTypeMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[HolidayTypeMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[HolidayTypeMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[HolidayTypeMaster_Delete]
GO
/****** Object:  StoredProcedure [dbo].[HolidayTypeMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[HolidayTypeMaster_Create]
GO
/****** Object:  StoredProcedure [dbo].[HolidayMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[HolidayMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[HolidayMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[HolidayMaster_GetById]
GO
/****** Object:  StoredProcedure [dbo].[HolidayMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[HolidayMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[HolidayMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[HolidayMaster_Delete]
GO
/****** Object:  StoredProcedure [dbo].[HolidayMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[HolidayMaster_Create]
GO
/****** Object:  StoredProcedure [dbo].[Grade_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Grade_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[GetUserRoleName]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[GetUserRoleName]
GO
/****** Object:  StoredProcedure [dbo].[GetUserPrivileges]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[GetUserPrivileges]
GO
/****** Object:  StoredProcedure [dbo].[GetUserNameById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[GetUserNameById]
GO
/****** Object:  StoredProcedure [dbo].[GetUserFullName]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[GetUserFullName]
GO
/****** Object:  StoredProcedure [dbo].[GetSundaysbyyearandmonth]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[GetSundaysbyyearandmonth]
GO
/****** Object:  StoredProcedure [dbo].[GetAllUserDetails]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[GetAllUserDetails]
GO
/****** Object:  StoredProcedure [dbo].[Gender_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Gender_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[FeesCategoryMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[FeesCategoryMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[FeesCategoryMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[FeesCategoryMaster_GetById]
GO
/****** Object:  StoredProcedure [dbo].[FeesCategoryMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[FeesCategoryMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[FeesCategoryMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[FeesCategoryMaster_Delete]
GO
/****** Object:  StoredProcedure [dbo].[FeesCategoryMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[FeesCategoryMaster_Create]
GO
/****** Object:  StoredProcedure [dbo].[EmpTypeMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[EmpTypeMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[EmpTypeMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[EmpTypeMaster_GetById]
GO
/****** Object:  StoredProcedure [dbo].[EmpTypeMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[EmpTypeMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[EmpTypeMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[EmpTypeMaster_Delete]
GO
/****** Object:  StoredProcedure [dbo].[EmpTypeMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[EmpTypeMaster_Create]
GO
/****** Object:  StoredProcedure [dbo].[EmployeeType_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[EmployeeType_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[EmployeeCategory_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[EmployeeCategory_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Emp_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Emp_Update]
GO
/****** Object:  StoredProcedure [dbo].[Emp_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Emp_GetById]
GO
/****** Object:  StoredProcedure [dbo].[Emp_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Emp_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Emp_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Emp_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Emp_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Emp_Create]
GO
/****** Object:  StoredProcedure [dbo].[DriverQualificationDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DriverQualificationDetails_Update]
GO
/****** Object:  StoredProcedure [dbo].[DriverQualificationDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DriverQualificationDetails_GetById]
GO
/****** Object:  StoredProcedure [dbo].[DriverQualificationDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DriverQualificationDetails_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[DriverQualificationDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DriverQualificationDetails_Delete]
GO
/****** Object:  StoredProcedure [dbo].[DriverQualificationDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DriverQualificationDetails_Create]
GO
/****** Object:  StoredProcedure [dbo].[DriverMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DriverMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[DriverMaster_GetByKey]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DriverMaster_GetByKey]
GO
/****** Object:  StoredProcedure [dbo].[DriverMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DriverMaster_GetById]
GO
/****** Object:  StoredProcedure [dbo].[DriverMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DriverMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[DriverMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DriverMaster_Delete]
GO
/****** Object:  StoredProcedure [dbo].[DriverMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DriverMaster_Create]
GO
/****** Object:  StoredProcedure [dbo].[DriverDocumentDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DriverDocumentDetails_Update]
GO
/****** Object:  StoredProcedure [dbo].[DriverDocumentDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DriverDocumentDetails_GetById]
GO
/****** Object:  StoredProcedure [dbo].[DriverDocumentDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DriverDocumentDetails_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[DriverDocumentDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DriverDocumentDetails_Delete]
GO
/****** Object:  StoredProcedure [dbo].[DriverDocumentDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DriverDocumentDetails_Create]
GO
/****** Object:  StoredProcedure [dbo].[Designation_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Designation_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[DesigMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DesigMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[DesigMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DesigMaster_GetById]
GO
/****** Object:  StoredProcedure [dbo].[DesigMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DesigMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[DesigMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DesigMaster_Delete]
GO
/****** Object:  StoredProcedure [dbo].[DesigMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DesigMaster_Create]
GO
/****** Object:  StoredProcedure [dbo].[DeptMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DeptMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[DeptMaster_GetBySchool]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DeptMaster_GetBySchool]
GO
/****** Object:  StoredProcedure [dbo].[DeptMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DeptMaster_GetById]
GO
/****** Object:  StoredProcedure [dbo].[DeptMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DeptMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[DeptMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DeptMaster_Delete]
GO
/****** Object:  StoredProcedure [dbo].[DeptMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DeptMaster_Create]
GO
/****** Object:  StoredProcedure [dbo].[DeptMaster_BulkInsert]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DeptMaster_BulkInsert]
GO
/****** Object:  StoredProcedure [dbo].[DeptDesigDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DeptDesigDetails_Update]
GO
/****** Object:  StoredProcedure [dbo].[DeptDesigDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DeptDesigDetails_GetById]
GO
/****** Object:  StoredProcedure [dbo].[DeptDesigDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DeptDesigDetails_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[DeptDesigDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DeptDesigDetails_Delete]
GO
/****** Object:  StoredProcedure [dbo].[DeptDesigDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DeptDesigDetails_Create]
GO
/****** Object:  StoredProcedure [dbo].[Department_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Department_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[DeleteUserDetails]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[DeleteUserDetails]
GO
/****** Object:  StoredProcedure [dbo].[Country_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Country_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Company_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Company_Update]
GO
/****** Object:  StoredProcedure [dbo].[Company_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Company_GetById]
GO
/****** Object:  StoredProcedure [dbo].[Company_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Company_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Company_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Company_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Company_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Company_Create]
GO
/****** Object:  StoredProcedure [dbo].[CleanerQualificationDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CleanerQualificationDetails_Update]
GO
/****** Object:  StoredProcedure [dbo].[CleanerQualificationDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CleanerQualificationDetails_GetById]
GO
/****** Object:  StoredProcedure [dbo].[CleanerQualificationDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CleanerQualificationDetails_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[CleanerQualificationDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CleanerQualificationDetails_Delete]
GO
/****** Object:  StoredProcedure [dbo].[CleanerQualificationDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CleanerQualificationDetails_Create]
GO
/****** Object:  StoredProcedure [dbo].[CleanerMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CleanerMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[CleanerMaster_GetByKey]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CleanerMaster_GetByKey]
GO
/****** Object:  StoredProcedure [dbo].[CleanerMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CleanerMaster_GetById]
GO
/****** Object:  StoredProcedure [dbo].[CleanerMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CleanerMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[CleanerMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CleanerMaster_Delete]
GO
/****** Object:  StoredProcedure [dbo].[CleanerMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CleanerMaster_Create]
GO
/****** Object:  StoredProcedure [dbo].[CleanerDocumentDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CleanerDocumentDetails_Update]
GO
/****** Object:  StoredProcedure [dbo].[CleanerDocumentDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CleanerDocumentDetails_GetById]
GO
/****** Object:  StoredProcedure [dbo].[CleanerDocumentDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CleanerDocumentDetails_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[CleanerDocumentDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CleanerDocumentDetails_Delete]
GO
/****** Object:  StoredProcedure [dbo].[CleanerDocumentDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CleanerDocumentDetails_Create]
GO
/****** Object:  StoredProcedure [dbo].[ClassRoom_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[ClassRoom_Update]
GO
/****** Object:  StoredProcedure [dbo].[ClassRoom_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[ClassRoom_GetById]
GO
/****** Object:  StoredProcedure [dbo].[ClassRoom_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[ClassRoom_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[ClassRoom_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[ClassRoom_Delete]
GO
/****** Object:  StoredProcedure [dbo].[ClassRoom_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[ClassRoom_Create]
GO
/****** Object:  StoredProcedure [dbo].[ClassMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[ClassMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Class_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Class_Update]
GO
/****** Object:  StoredProcedure [dbo].[Class_GetNameById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Class_GetNameById]
GO
/****** Object:  StoredProcedure [dbo].[Class_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Class_GetById]
GO
/****** Object:  StoredProcedure [dbo].[Class_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Class_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Class_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Class_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Class_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Class_Create]
GO
/****** Object:  StoredProcedure [dbo].[City_GetByState]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[City_GetByState]
GO
/****** Object:  StoredProcedure [dbo].[CheckTeacherAvailability]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[CheckTeacherAvailability]
GO
/****** Object:  StoredProcedure [dbo].[ChangePassword]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[ChangePassword]
GO
/****** Object:  StoredProcedure [dbo].[Category_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Category_Update]
GO
/****** Object:  StoredProcedure [dbo].[Category_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Category_GetById]
GO
/****** Object:  StoredProcedure [dbo].[Category_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Category_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[Category_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Category_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Category_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[Category_Create]
GO
/****** Object:  StoredProcedure [dbo].[BookType_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[BookType_Update]
GO
/****** Object:  StoredProcedure [dbo].[BookType_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[BookType_GetById]
GO
/****** Object:  StoredProcedure [dbo].[BookType_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[BookType_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[BookType_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[BookType_Delete]
GO
/****** Object:  StoredProcedure [dbo].[BookType_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[BookType_Create]
GO
/****** Object:  StoredProcedure [dbo].[BloodGroup_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[BloodGroup_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[AuthenticateUser]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AuthenticateUser]
GO
/****** Object:  StoredProcedure [dbo].[AttendanceReasonMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AttendanceReasonMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[AttendanceReasonMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AttendanceReasonMaster_GetById]
GO
/****** Object:  StoredProcedure [dbo].[AttendanceReasonMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AttendanceReasonMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[AttendanceReasonMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AttendanceReasonMaster_Delete]
GO
/****** Object:  StoredProcedure [dbo].[AttendanceReasonMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AttendanceReasonMaster_Create]
GO
/****** Object:  StoredProcedure [dbo].[AttendanceReason_GetBySchool]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AttendanceReason_GetBySchool]
GO
/****** Object:  StoredProcedure [dbo].[AssesmentMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AssesmentMaster_Update]
GO
/****** Object:  StoredProcedure [dbo].[AssesmentMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AssesmentMaster_GetById]
GO
/****** Object:  StoredProcedure [dbo].[AssesmentMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AssesmentMaster_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[AssesmentMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AssesmentMaster_Delete]
GO
/****** Object:  StoredProcedure [dbo].[AssesmentMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AssesmentMaster_Create]
GO
/****** Object:  StoredProcedure [dbo].[AddUserDetails]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AddUserDetails]
GO
/****** Object:  StoredProcedure [dbo].[AcademicYear_Update]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AcademicYear_Update]
GO
/****** Object:  StoredProcedure [dbo].[AcademicYear_ToggleStatus]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AcademicYear_ToggleStatus]
GO
/****** Object:  StoredProcedure [dbo].[AcademicYear_SetCurrent]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AcademicYear_SetCurrent]
GO
/****** Object:  StoredProcedure [dbo].[AcademicYear_GetById]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AcademicYear_GetById]
GO
/****** Object:  StoredProcedure [dbo].[AcademicYear_GetAllActive]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AcademicYear_GetAllActive]
GO
/****** Object:  StoredProcedure [dbo].[AcademicYear_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AcademicYear_GetAll]
GO
/****** Object:  StoredProcedure [dbo].[AcademicYear_Delete]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AcademicYear_Delete]
GO
/****** Object:  StoredProcedure [dbo].[AcademicYear_Create]    Script Date: 08-01-2026 18:24:18 ******/
DROP PROCEDURE [dbo].[AcademicYear_Create]
GO
/****** Object:  StoredProcedure [dbo].[AcademicYear_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 4. Create Academic Year
CREATE   PROCEDURE [dbo].[AcademicYear_Create]
    @Id UNIQUEIDENTIFIER OUTPUT,
    @AcademicYearName NVARCHAR(100),
    @StartDate DATE,
    @EndDate DATE,
    @IsCurrent BIT,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION;
    
    -- If this is being set as current, unset any existing current year
    IF @IsCurrent = 1
    BEGIN
        UPDATE [dbo].[AcademicYear]
        SET [IsCurrent] = 0,
            [ModifiedBy] = @CreatedBy,
            [ModifiedDate] = GETDATE()
        WHERE [IsCurrent] = 1;
    END;
    
    -- Insert the new academic year
    SET @Id = NEWID();
    
    INSERT INTO [dbo].[AcademicYear] (
        [Id],
        [AcademicYearName],
        [StartDate],
        [EndDate],
        [IsCurrent],
        [IsActive],
        [CreatedBy]
    ) VALUES (
        @Id,
        @AcademicYearName,
        @StartDate,
        @EndDate,
        @IsCurrent,
        1, -- IsActive
        @CreatedBy
    );
    
    COMMIT TRANSACTION;
    
    -- Return the created record
    SELECT 
        [Id],
        [AcademicYearName],
        [StartDate],
        [EndDate],
        [IsCurrent],
        [IsActive],
        [CreatedBy],
        [CreatedDate]
    FROM [dbo].[AcademicYear]
    WHERE [Id] = @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[AcademicYear_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 6. Delete Academic Year (Soft Delete)
CREATE   PROCEDURE [dbo].[AcademicYear_Delete]
    @Id UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Soft delete the academic year
    UPDATE [dbo].[AcademicYear]
    SET 
        [IsActive] = 0,
        [IsDeleted] = 1,
        [ModifiedBy] = @ModifiedBy,
        [ModifiedDate] = GETDATE()
    WHERE [Id] = @Id;
    
    -- Return success indicator
    SELECT 1 AS Success;
END;
GO
/****** Object:  StoredProcedure [dbo].[AcademicYear_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- 1. Get All Academic Years
CREATE   PROCEDURE [dbo].[AcademicYear_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        [Id],
        [AcademicYearName],
        [StartDate],
        [EndDate],
        [IsCurrent],
        [IsActive],
        [IsDeleted],
        [CreatedBy],
        [CreatedDate],
        [ModifiedBy],
        [ModifiedDate]
    FROM [dbo].[AcademicYear]
    WHERE [IsDeleted] = 0
    ORDER BY [StartDate] DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[AcademicYear_GetAllActive]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 3. Get All Active Academic Years
CREATE   PROCEDURE [dbo].[AcademicYear_GetAllActive]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        [Id],
        [AcademicYearName],
        [StartDate],
        [EndDate],
        [IsCurrent],
        [IsActive]
    FROM [dbo].[AcademicYear]
    WHERE [IsActive] = 1 
    AND [IsDeleted] = 0
    ORDER BY [StartDate] DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[AcademicYear_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 2. Get Academic Year By ID
CREATE   PROCEDURE [dbo].[AcademicYear_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        [Id],
        [AcademicYearName],
        [StartDate],
        [EndDate],
        [IsCurrent],
        [IsActive],
        [IsDeleted],
        [CreatedBy],
        [CreatedDate],
        [ModifiedBy],
        [ModifiedDate]
    FROM [dbo].[AcademicYear]
    WHERE [Id] = @Id
    AND [IsDeleted] = 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[AcademicYear_SetCurrent]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 8. Set Current Academic Year
CREATE   PROCEDURE [dbo].[AcademicYear_SetCurrent]
    @Id UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION;
    
    -- First, set all years to not current
    UPDATE [dbo].[AcademicYear]
    SET 
        [IsCurrent] = 0,
        [ModifiedBy] = @ModifiedBy,
        [ModifiedDate] = GETDATE();
    
    -- Then set the specified year as current
    UPDATE [dbo].[AcademicYear]
    SET 
        [IsCurrent] = 1,
        [IsActive] = 1, -- Ensure it's active
        [ModifiedBy] = @ModifiedBy,
        [ModifiedDate] = GETDATE()
    WHERE [Id] = @Id;
    
    -- Return the updated record
    SELECT 
        [Id],
        [AcademicYearName],
        [IsCurrent],
        [IsActive]
    FROM [dbo].[AcademicYear]
    WHERE [Id] = @Id;
    
    COMMIT TRANSACTION;
END;
GO
/****** Object:  StoredProcedure [dbo].[AcademicYear_ToggleStatus]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 7. Toggle Active Status
CREATE   PROCEDURE [dbo].[AcademicYear_ToggleStatus]
    @Id UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @NewStatus BIT;
    
    -- Toggle the IsActive status
    UPDATE [dbo].[AcademicYear]
    SET 
        [IsActive] = ~[IsActive],
        [ModifiedBy] = @ModifiedBy,
        [ModifiedDate] = GETDATE()
    WHERE [Id] = @Id;
    
    -- Return the new status
    SELECT [IsActive] AS NewStatus
    FROM [dbo].[AcademicYear]
    WHERE [Id] = @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[AcademicYear_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 5. Update Academic Year
CREATE   PROCEDURE [dbo].[AcademicYear_Update]
    @Id UNIQUEIDENTIFIER,
    @AcademicYearName NVARCHAR(100),
    @StartDate DATE,
    @EndDate DATE,
    @IsCurrent BIT,
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION;
    
    -- If this is being set as current, unset any existing current year
    IF @IsCurrent = 1
    BEGIN
        UPDATE [dbo].[AcademicYear]
        SET [IsCurrent] = 0,
            [ModifiedBy] = @ModifiedBy,
            [ModifiedDate] = GETDATE()
        WHERE [IsCurrent] = 1
        AND [Id] <> @Id; -- Don't unset the current record if it's the one being updated
    END;
    
    -- Update the academic year
    UPDATE [dbo].[AcademicYear]
    SET 
        [AcademicYearName] = @AcademicYearName,
        [StartDate] = @StartDate,
        [EndDate] = @EndDate,
        [IsCurrent] = @IsCurrent,
        [IsActive] = @IsActive,
        [ModifiedBy] = @ModifiedBy,
        [ModifiedDate] = GETDATE()
    WHERE [Id] = @Id;
    
    -- Return the updated record
    SELECT 
        [Id],
        [AcademicYearName],
        [StartDate],
        [EndDate],
        [IsCurrent],
        [IsActive],
        [ModifiedBy],
        [ModifiedDate]
    FROM [dbo].[AcademicYear]
    WHERE [Id] = @Id;
    
    COMMIT TRANSACTION;
END;
GO
/****** Object:  StoredProcedure [dbo].[AddUserDetails]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create  PROCEDURE [dbo].[AddUserDetails]
    @Id as Uniqueidentifier,
    @UserName as varchar(250),
    @UserPassword as varchar(250),
    @FirstName as varchar(250),
    @LastName as varchar(250),
    @Email as varchar(250),
    @RoleId as uniqueidentifier,
    @DesignationId as uniqueidentifier,
    @CompanyId as uniqueidentifier,
    @SchoolId as uniqueidentifier,
    @IsSuperUser as bit,
    @IsActive as bit,
    @IsDeleted as bit,
    @CreatedBy as uniqueidentifier,
    @CreatedOn as datetime,
    @ModifiedBy as uniqueidentifier,
    @ModifiedOn as datetime
AS
BEGIN
    SET NOCOUNT ON;
    insert into UserDetails values(@Id,@UserName,@UserPassword,@FirstName,@LastName,@Email,@DesignationId,
    @RoleId,@IsSuperUser,@CompanyId,@SchoolId,@IsActive,@IsDeleted,@CreatedBy,@CreatedOn,@ModifiedBy,@ModifiedOn,'INC'
,'User Record Added')
    
END
GO
/****** Object:  StoredProcedure [dbo].[AssesmentMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AssesmentMaster_Create]
    @Name                NVARCHAR(200),
    @Description         NVARCHAR(1000) = N'',
    @PercentageWeightage DECIMAL(5,2) = 0,
    @FromPeriod          DATETIME       = NULL,
    @ToPeriod            DATETIME       = NULL,
    @CompanyId           UNIQUEIDENTIFIER,
    @SchoolId            UNIQUEIDENTIFIER,
    @IsActive            BIT,
    @CreatedBy           UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[AssesmentMaster]
    (
        Id,
        Name,
        Description,
        PercentageWeightage,
        FromPeriod,
        ToPeriod,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @Name,
        @Description,
        @PercentageWeightage,
        @FromPeriod,
        @ToPeriod,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,               -- IsDeleted
        @CreatedBy,
        GETUTCDATE(),    -- CreatedDate
        @CreatedBy,            -- ModifiedBy
        GETUTCDATE(),    -- ModifiedDate (NOT NULL)
        N'INC',
        N'In Process....'
    );

    SELECT @NewId AS Id;
END
GO
/****** Object:  StoredProcedure [dbo].[AssesmentMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AssesmentMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Rows INT;

    UPDATE [dbo].[AssesmentMaster]
    SET
        IsDeleted = 1
    WHERE Id = @Id
      AND IsDeleted = 0;

    SET @Rows = @@ROWCOUNT;

    IF (@Rows = 1)
        RETURN 1;
    ELSE
        RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[AssesmentMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AssesmentMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        Description,
        PercentageWeightage,
        FromPeriod,
        ToPeriod,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM [dbo].[AssesmentMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[AssesmentMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AssesmentMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        Description,
        PercentageWeightage,
        FromPeriod,
        ToPeriod,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM [dbo].[AssesmentMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[AssesmentMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AssesmentMaster_Update]
    @Id                 UNIQUEIDENTIFIER,
    @Name               NVARCHAR(200),
    @Description        NVARCHAR(1000) = N'',
    @PercentageWeightage DECIMAL(5,2) = 0,
    @FromPeriod         DATETIME       = NULL,
    @ToPeriod           DATETIME       = NULL,
    @SchoolId           UNIQUEIDENTIFIER,
    @IsActive           BIT,
    @ModifiedBy         UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Rows INT;

    UPDATE [dbo].[AssesmentMaster]
    SET
        Name               = @Name,
        Description        = @Description,
        PercentageWeightage = @PercentageWeightage,
        FromPeriod         = @FromPeriod,
        ToPeriod           = @ToPeriod,
        SchoolId           = @SchoolId,
        IsActive           = @IsActive,
        ModifiedBy         = @ModifiedBy,
        ModifiedDate       = GETUTCDATE()
    WHERE Id = @Id
      AND IsDeleted = 0;

    SET @Rows = @@ROWCOUNT;

    IF (@Rows = 1)
        RETURN 1;
    ELSE
        RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[AttendanceReason_GetBySchool]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AttendanceReason_GetBySchool]
    @SchoolId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id,
        Name,
        Description,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM 
        AttendanceReasonMaster
    WHERE 
        (SchoolId = @SchoolId OR @SchoolId IS NULL)
        AND IsActive = 1
        AND (IsDeleted = 0 OR IsDeleted IS NULL)
    ORDER BY 
        Name;
END
GO
/****** Object:  StoredProcedure [dbo].[AttendanceReasonMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AttendanceReasonMaster_Create]
    @Code NVARCHAR(50),
    @Name NVARCHAR(200),
    @Description NVARCHAR(500),
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[AttendanceReasonMaster]
    (
        Id,
        Code,
        Name,
        Description,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @Code,
        @Name,
        @Description,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'INC',
        N'In Process....'
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[AttendanceReasonMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AttendanceReasonMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[AttendanceReasonMaster]
    SET
        IsDeleted = 1,
        IsActive = 0
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[AttendanceReasonMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AttendanceReasonMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Code,
        Name,
        Description,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[AttendanceReasonMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[AttendanceReasonMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AttendanceReasonMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Code,
        Name,
        Description,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[AttendanceReasonMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[AttendanceReasonMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AttendanceReasonMaster_Update]
    @Id UNIQUEIDENTIFIER,
    @Code NVARCHAR(50),
    @Name NVARCHAR(200),
    @Description NVARCHAR(500),
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[AttendanceReasonMaster]
    SET
        Code = @Code,
        Name = @Name,
        Description = @Description,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[AuthenticateUser]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AuthenticateUser]
    @userName        NVARCHAR(256),
    @password        NVARCHAR(256),
    @IsAuthenticated BIT OUTPUT,
    @UserId          UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SET @IsAuthenticated = 0;
    SET @UserId = NULL;

    -- Validate username and password
    SELECT TOP (1)
        @UserId = u.Id,
        @IsAuthenticated = CASE WHEN u.IsActive = 1 THEN 1 ELSE 0 END
    FROM dbo.UserDetails u
    WHERE u.UserName = @userName
      AND u.[UserPassword] = @password
      AND u.IsActive = 1;

    -- If authenticated, return user details and privileges
    IF @IsAuthenticated = 1
    BEGIN
        -- Return user details with DesignationName and RoleName
        SELECT 
            u.Id AS UserId,
            u.UserName,
            u.FirstName + ' ' + u.LastName AS FullName,
            u.FirstName,
            u.LastName,
            u.EmailAddress AS Email,
            u.IsActive,
            u.DesignationId,
            d.Name AS DesignationName,
            r.Id AS RoleId,
            r.Name AS RoleName,
			u.CompanyId,
			u.SchoolId
        FROM dbo.UserDetails u
        LEFT JOIN dbo.DesigMaster d ON u.DesignationId = d.Id
        LEFT JOIN dbo.RoleMaster r ON u.UserRoleId = r.Id
        WHERE u.Id = @UserId;

        -- Return user privileges
        SELECT DISTINCT
            p.PrivilegeName
        FROM dbo.UserDetails u
        INNER JOIN dbo.RoleMaster r ON u.UserRoleId = r.Id
        INNER JOIN dbo.RolePrivileges rp ON r.Id = rp.RoleId
        INNER JOIN dbo.[Privileges] p ON rp.PrivilegeId = p.Id
        WHERE u.Id = @UserId
          AND u.IsActive = 1;

        RETURN 1; -- Success
    END
    ELSE
    BEGIN
        RETURN 0; -- Authentication failed
    END
END
GO
/****** Object:  StoredProcedure [dbo].[BloodGroup_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BloodGroup_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name
    FROM dbo.BloodGroupMaster
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[BookType_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookType_Create]
    @Name NVARCHAR(50),
    @Description NVARCHAR(150) = NULL,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[BookTypeMaster]
    (
        Id,
        Name,
        Description,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @Name,
        @Description,
        @IsActive,
        0,
        @CompanyId,
        @SchoolId,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'ACT',
        N'Active'
    );

    -- BookTypeService expects a DataTable with column "Id"
    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[BookType_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookType_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[BookTypeMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    -- BookTypeService checks @RETURN_VALUE == 1
    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[BookType_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookType_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Description,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[BookTypeMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[BookType_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookType_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Description,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[BookTypeMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[BookType_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookType_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(50),
    @Description NVARCHAR(150) = NULL,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[BookTypeMaster]
    SET 
        Name = @Name,
        Description = @Description,
        IsActive = @IsActive,
        CompanyId = @CompanyId,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    -- BookTypeService checks @RETURN_VALUE == 1
    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Category_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Category_Create]
    @Name       NVARCHAR(200),
    @IsActive   BIT,
    @CreatedBy  UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[CategoryMaster]
    (
        Id,
        Name,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        Status,
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @Name,
        @IsActive,
        0,                    -- IsDeleted
        @CreatedBy,
        GETUTCDATE(),         -- CreatedDate
        'ACT',                -- Status
        'Active'              -- StatusMessage
    );

    -- service expects first row with column 'Id'
    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[Category_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Category_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[CategoryMaster]
    SET
        IsDeleted    = 1,
        ModifiedDate = GETUTCDATE()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1
        RETURN 1;
    ELSE
        RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Category_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Category_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM [dbo].[CategoryMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Category_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Category_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM [dbo].[CategoryMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[Category_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Category_Update]
    @Id         UNIQUEIDENTIFIER,
    @Name       NVARCHAR(200),
    @IsActive   BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[CategoryMaster]
    SET
        Name         = @Name,
        IsActive     = @IsActive,
        ModifiedBy   = @ModifiedBy,
        ModifiedDate = GETUTCDATE()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1
        RETURN 1;
    ELSE
        RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[ChangePassword]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ChangePassword]
    @userName    nvarchar(256),
    @oldPassword nvarchar(256),
    @newPassword nvarchar(256)
AS
BEGIN
    SET NOCOUNT ON;

    -- Optional: disallow same new password
    IF @newPassword = @oldPassword
        RETURN 0;

    -- Update only if current password matches and user is active (if you have IsActive)
    UPDATE u
    SET u.[Password] = @newPassword
    FROM dbo.Users u
    WHERE u.UserName = @userName
      AND u.[Password] = @oldPassword
      AND (u.IsActive = 1 OR u.IsActive IS NULL);

    IF @@ROWCOUNT = 1
        RETURN 1;

    RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[CheckTeacherAvailability]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[CheckTeacherAvailability]
    @TeacherId UNIQUEIDENTIFIER,
    @DayOfWeek INT,
    @StartTime TIME,
    @EndTime TIME,
    @ExcludePeriodId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Check if the teacher is available during the specified time
    -- Returns 1 if available, 0 if not
    SELECT CAST(CASE 
        WHEN EXISTS (
            SELECT 1 
            FROM TimeTableClassPeriodDetails t
            INNER JOIN TimeTablePeriodMaster p ON t.PeriodId = p.Id
            WHERE t.TeacherId = @TeacherId
            AND t.DayOfWeek = @DayOfWeek
            AND t.Id <> ISNULL(@ExcludePeriodId, '00000000-0000-0000-0000-000000000000')
            AND (
                (@StartTime >= p.StartTime AND @StartTime < p.EndTime) OR  -- New period starts during existing period
                (@EndTime > p.StartTime AND @EndTime <= p.EndTime) OR      -- New period ends during existing period
                (@StartTime <= p.StartTime AND @EndTime >= p.EndTime)      -- New period completely overlaps existing period
            )
        ) THEN 0 
        ELSE 1 
    END AS BIT) AS IsAvailable;
END
GO
/****** Object:  StoredProcedure [dbo].[City_GetByState]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[City_GetByState]
    @StateId uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        c.[Id],
        c.[CityName]
    FROM dbo.[CityMaster] c
    WHERE c.[CityStateId] = @StateId
    ORDER BY c.[CityName];
END
GO
/****** Object:  StoredProcedure [dbo].[Class_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Class_Create]
    @Name NVARCHAR(200),
    @ExamAssessment NVARCHAR(200) = N'',
    @IsGradePointApplicable BIT = 0,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[ClassMaster]
    (
        Id,
        Name,
        ExamAssessment,
        IsGradePointApplicable,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @Name,
        @ExamAssessment,
        @IsGradePointApplicable,
        @IsActive,
        0,
        @CompanyId,
        @SchoolId,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[Class_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Class_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[ClassMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Class_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Class_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        ExamAssessment,
        IsGradePointApplicable,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [OrderBy],
        [Status],
        StatusMessage
    FROM [dbo].[ClassMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Class_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Class_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        ExamAssessment,
        IsGradePointApplicable,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [OrderBy],
        [Status],
        StatusMessage
    FROM [dbo].[ClassMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[Class_GetNameById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Class_GetNameById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Name
    FROM [dbo].[ClassMaster]
    WHERE Id = @Id 
    AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Class_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Class_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @ExamAssessment NVARCHAR(200) = N'',
    @IsGradePointApplicable BIT = 0,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[ClassMaster]
    SET 
        Name = @Name,
        ExamAssessment = @ExamAssessment,
        IsGradePointApplicable = @IsGradePointApplicable,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[ClassMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ClassMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id,
        Name,
        ExamAssessment,
        IsGradePointApplicable,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        OrderBy,
        Status,
        StatusMessage
    FROM 
        ClassMaster
    WHERE 
        IsDeleted = 0  -- Only non-deleted records
    ORDER BY 
        OrderBy, Name;
END
GO
/****** Object:  StoredProcedure [dbo].[ClassRoom_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ClassRoom_Create]
    @Name NVARCHAR(200),
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[ClassRoomMaster]
    (
        Id,
        Name,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @Name,
        @IsActive,
        0,
        @CompanyId,
        @SchoolId,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[ClassRoom_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ClassRoom_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[ClassRoomMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[ClassRoom_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ClassRoom_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[ClassRoomMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[ClassRoom_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ClassRoom_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[ClassRoomMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[ClassRoom_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ClassRoom_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[ClassRoomMaster]
    SET 
        Name = @Name,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[CleanerDocumentDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[CleanerDocumentDetails_Create]
    @CleanerId UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @Description NVARCHAR(500),
    @FileName NVARCHAR(500),
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER,
    @Status NVARCHAR(50),
    @StatusMessage NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();
    INSERT INTO dbo.CleanerDocumentDetails
    (
        Id, CleanerId, Name, Description, FileName,
        CompanyId, SchoolId, IsActive, IsDeleted,
        CreatedBy, CreatedDate, Status, StatusMessage
    )
    VALUES
    (
        @NewId, @CleanerId, ISNULL(@Name, ''), ISNULL(@Description, ''), ISNULL(@FileName, ''),
        @CompanyId, @SchoolId, ISNULL(@IsActive, 0), 0,
        @CreatedBy, SYSUTCDATETIME(), ISNULL(@Status, ''), ISNULL(@StatusMessage, '')
    );
    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[CleanerDocumentDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[CleanerDocumentDetails_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.CleanerDocumentDetails
    SET IsDeleted = 1,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[CleanerDocumentDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[CleanerDocumentDetails_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,
           CleanerId,
           Name,
           Description,
           FileName,
           CompanyId,
           SchoolId,
           IsActive,
           IsDeleted,
           CreatedBy,
           CreatedDate,
           ModifiedBy,
           ModifiedDate,
           Status,
           StatusMessage
    FROM dbo.CleanerDocumentDetails WITH (NOLOCK)
    WHERE ISNULL(IsDeleted, 0) = 0
    ORDER BY CreatedDate DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[CleanerDocumentDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[CleanerDocumentDetails_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,
           CleanerId,
           Name,
           Description,
           FileName,
           CompanyId,
           SchoolId,
           IsActive,
           IsDeleted,
           CreatedBy,
           CreatedDate,
           ModifiedBy,
           ModifiedDate,
           Status,
           StatusMessage
    FROM dbo.CleanerDocumentDetails WITH (NOLOCK)
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[CleanerDocumentDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[CleanerDocumentDetails_Update]
    @Id UNIQUEIDENTIFIER,
    @CleanerId UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @Description NVARCHAR(500),
    @FileName NVARCHAR(500),
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.CleanerDocumentDetails
    SET CleanerId = @CleanerId,
        Name = ISNULL(@Name, ''),
        Description = ISNULL(@Description, ''),
        FileName = ISNULL(@FileName, ''),
        IsActive = ISNULL(@IsActive, 0),
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[CleanerMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[CleanerMaster_Create]
    @Name           nvarchar(200),
    @Image          nvarchar(500),
    @FatherName     nvarchar(200),
    @Description    nvarchar(max),
    @IsActive       bit,
    @IsDeleted      bit,
    @CompanyId      uniqueidentifier,
    @SchoolId       uniqueidentifier,
    @CreatedBy      uniqueidentifier,
    @CreatedDate    datetime2(0),
    @Status         nvarchar(50),
    @StatusMessage  nvarchar(200)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Id uniqueidentifier = NEWID();

    INSERT INTO dbo.CleanerMaster
    (
        Id,
        Name,
        Image,
        FatherName,
        Description,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    )
    VALUES
    (
        @Id,
        @Name,
        @Image,
        @FatherName,
        @Description,
        @IsActive,
        @IsDeleted,
        @CompanyId,
        @SchoolId,
        @CreatedBy,
        @CreatedDate,
        NULL,               -- ModifiedBy
        NULL,               -- ModifiedDate
        @Status,
        @StatusMessage
    );

    SELECT @Id AS Id;
END
GO
/****** Object:  StoredProcedure [dbo].[CleanerMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[CleanerMaster_Delete]
    @Id uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.CleanerMaster
    SET IsDeleted    = 1,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN CASE WHEN @@ROWCOUNT = 1 THEN 1 ELSE 0 END;
END
GO
/****** Object:  StoredProcedure [dbo].[CleanerMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CleanerMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Image,
        FatherName,
        Description,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[CleanerMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[CleanerMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CleanerMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Image,
        FatherName,
        Description,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[CleanerMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[CleanerMaster_GetByKey]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[CleanerMaster_GetByKey]
    @CompanyId uniqueidentifier,
    @SchoolId  uniqueidentifier,
    @Name      nvarchar(200)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (1)
        Id,
        Name,
        Image,
        FatherName,
        Description,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM dbo.CleanerMaster WITH (NOLOCK)
    WHERE CompanyId = @CompanyId
      AND SchoolId  = @SchoolId
      AND Name      = @Name
      AND (IsDeleted = 0 OR IsDeleted IS NULL);
END
GO
/****** Object:  StoredProcedure [dbo].[CleanerMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[CleanerMaster_Update]
    @Id            uniqueidentifier,
    @Name          nvarchar(200),
    @Image         nvarchar(500),
    @FatherName    nvarchar(200),
    @Description   nvarchar(max),
    @IsActive      bit,
    @IsDeleted     bit,
    @ModifiedBy    uniqueidentifier,
    @ModifiedDate  datetime2(0),
    @Status        nvarchar(50),
    @StatusMessage nvarchar(200)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.CleanerMaster
    SET Name          = @Name,
        Image         = @Image,
        FatherName    = @FatherName,
        Description   = @Description,
        IsActive      = @IsActive,
        IsDeleted     = @IsDeleted,
        ModifiedBy    = @ModifiedBy,
        ModifiedDate  = @ModifiedDate,
        Status        = @Status,
        StatusMessage = @StatusMessage
    WHERE Id = @Id;

    RETURN CASE WHEN @@ROWCOUNT = 1 THEN 1 ELSE 0 END;
END
GO
/****** Object:  StoredProcedure [dbo].[CleanerQualificationDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[CleanerQualificationDetails_Create]
    @CleanerId UNIQUEIDENTIFIER,
    @QualificationId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER,
    @Status NVARCHAR(50),
    @StatusMessage NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();
    INSERT INTO dbo.CleanerQualificationDetails
    (
        Id, CleanerId, QualificationId,
        CompanyId, SchoolId, IsActive, IsDeleted,
        CreatedBy, CreatedDate, Status, StatusMessage
    )
    VALUES
    (
        @NewId, @CleanerId, @QualificationId,
        @CompanyId, @SchoolId, ISNULL(@IsActive, 0), 0,
        @CreatedBy, SYSUTCDATETIME(), ISNULL(@Status, ''), ISNULL(@StatusMessage, '')
    );
    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[CleanerQualificationDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[CleanerQualificationDetails_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.CleanerQualificationDetails
    SET IsDeleted = 1,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[CleanerQualificationDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[CleanerQualificationDetails_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,
           CleanerId,
           QualificationId,
           SchoolId,
           CompanyId,
           IsActive,
           IsDeleted,
           CreatedBy,
           CreatedDate,
           ModifiedBy,
           ModifiedDate,
           Status,
           StatusMessage
    FROM dbo.CleanerQualificationDetails WITH (NOLOCK)
    WHERE ISNULL(IsDeleted, 0) = 0
    ORDER BY CreatedDate DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[CleanerQualificationDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[CleanerQualificationDetails_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,
           CleanerId,
           QualificationId,
           SchoolId,
           CompanyId,
           IsActive,
           IsDeleted,
           CreatedBy,
           CreatedDate,
           ModifiedBy,
           ModifiedDate,
           Status,
           StatusMessage
    FROM dbo.CleanerQualificationDetails WITH (NOLOCK)
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[CleanerQualificationDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[CleanerQualificationDetails_Update]
    @Id UNIQUEIDENTIFIER,
    @CleanerId UNIQUEIDENTIFIER,
    @QualificationId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.CleanerQualificationDetails
    SET CleanerId = @CleanerId,
        QualificationId = @QualificationId,
        IsActive = ISNULL(@IsActive, 0),
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Company_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Company_Create]
    @CompanyName        NVARCHAR(200),
    @Description        NVARCHAR(1000) = NULL,
    @Address            NVARCHAR(1000) = NULL,
    @CityId             UNIQUEIDENTIFIER,
    @StateId            UNIQUEIDENTIFIER,
    @CountryId          UNIQUEIDENTIFIER,
    @ZipCode            NVARCHAR(50) = NULL,
    @Email              NVARCHAR(320) = NULL,
    @IsActive           BIT,
    @CreatedBy          UNIQUEIDENTIFIER,
    @EstablishmentYear  NVARCHAR(20) = NULL,
    @JudistrictionArea  UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO dbo.CompanyMaster
    (
        Id,
        CompanyName,
        Description,
        Address,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        Email,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        EstablishmentYear,
        JudistrictionArea,
        Status,
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @CompanyName,
        @Description,
        @Address,
        @CityId,
        @StateId,
        @CountryId,
        @ZipCode,
        @Email,
        @IsActive,
        0,                    -- IsDeleted
        @CreatedBy,
        SYSUTCDATETIME(),     -- CreatedDate
        @EstablishmentYear,
        @JudistrictionArea,
        NULL,                 -- Status
        NULL                  -- StatusMessage
    );

    -- Return code and the new Id as a result set
    SELECT @NewId AS Id;
    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Company_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Company_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.CompanyMaster WITH (NOLOCK) WHERE Id = @Id AND IsDeleted = 0)
    BEGIN
        RETURN 0;
    END

    UPDATE dbo.CompanyMaster
    SET
        IsDeleted = 1,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Company_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Company_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        CompanyName,
        Description,
        Address,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        Email,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        EstablishmentYear,
        JudistrictionArea,
        Status,
        StatusMessage
    FROM dbo.CompanyMaster WITH (NOLOCK)
    WHERE IsDeleted = 0
    ORDER BY CompanyName;
END
GO
/****** Object:  StoredProcedure [dbo].[Company_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Company_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        CompanyName,
        Description,
        Address,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        Email,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        EstablishmentYear,
        JudistrictionArea,
        [Status],
        StatusMessage
    FROM [dbo].[CompanyMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[Company_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Company_Update]
    @Id                 UNIQUEIDENTIFIER,
    @CompanyName        NVARCHAR(200),
    @Description        NVARCHAR(1000) = NULL,
    @Address            NVARCHAR(1000) = NULL,
    @CityId             UNIQUEIDENTIFIER,
    @StateId            UNIQUEIDENTIFIER,
    @CountryId          UNIQUEIDENTIFIER,
    @ZipCode            NVARCHAR(50) = NULL,
    @Email              NVARCHAR(320) = NULL,
    @IsActive           BIT,
    @ModifiedBy         UNIQUEIDENTIFIER = NULL,
    @EstablishmentYear  NVARCHAR(20) = NULL,
    @JudistrictionArea  UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.CompanyMaster WITH (NOLOCK) WHERE Id = @Id AND IsDeleted = 0)
    BEGIN
        RETURN 0;
    END

    UPDATE dbo.CompanyMaster
    SET
        CompanyName        = @CompanyName,
        Description        = @Description,
        Address            = @Address,
        CityId             = @CityId,
        StateId            = @StateId,
        CountryId          = @CountryId,
        ZipCode            = @ZipCode,
        Email              = @Email,
        IsActive           = @IsActive,
        ModifiedBy         = @ModifiedBy,
        ModifiedDate       = SYSUTCDATETIME(),
        EstablishmentYear  = @EstablishmentYear,
        JudistrictionArea  = @JudistrictionArea
    WHERE Id = @Id AND IsDeleted = 0;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Country_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Country_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        c.[Id],
        c.[CountryName]
    FROM dbo.[CountryMaster] c
    ORDER BY c.[CountryName];
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteUserDetails]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create  PROCEDURE [dbo].[DeleteUserDetails]
    @Id as Uniqueidentifier,
    @UserId as uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;
    update UserDetails 
        set 
        IsActive = 0,
        IsDeleted = 1,
        ModifiedBy = @UserId,
        ModifiedDate = getdate(),
        Status = 'INC',
        StatusMessage = 'User Deleted Successfully'
    where Id = @Id
    
END
GO
/****** Object:  StoredProcedure [dbo].[Department_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Department_GetAll]
    @SchoolId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        DeptName AS DepartmentName
    FROM DeptMaster
    WHERE IsActive = 1
      AND (@SchoolId IS NULL OR SchoolId = @SchoolId)
    ORDER BY DeptName;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeptDesigDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- DeptDesigDetails_Create
CREATE   PROCEDURE [dbo].[DeptDesigDetails_Create]
    @DepartmentId UNIQUEIDENTIFIER,
    @DesignationId UNIQUEIDENTIFIER,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CreatedBy UNIQUEIDENTIFIER,
    @Status VARCHAR(10) = NULL,
    @StatusMessage NVARCHAR(255) = NULL
AS
BEGIN
    DECLARE @Id UNIQUEIDENTIFIER = NEWID()
    
    INSERT INTO DeptDesigDetails (
        Id, DepartmentId, DesignationId, CompanyId, SchoolId, 
        IsActive, IsDeleted, CreatedBy, CreatedDate, 
        Status, StatusMessage
    ) VALUES (
        @Id, @DepartmentId, @DesignationId, @CompanyId, @SchoolId,
        @IsActive, 0, @CreatedBy, GETUTCDATE(),
        @Status, @StatusMessage
    )
    
    SELECT @Id AS Id
    RETURN 1
END
GO
/****** Object:  StoredProcedure [dbo].[DeptDesigDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- DeptDesigDetails_Delete
CREATE   PROCEDURE [dbo].[DeptDesigDetails_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE DeptDesigDetails 
    SET IsDeleted = 1,
        ModifiedDate = GETUTCDATE()
    WHERE Id = @Id
    
    RETURN 1
END
GO
/****** Object:  StoredProcedure [dbo].[DeptDesigDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- DeptDesigDetails_GetAll
CREATE   PROCEDURE [dbo].[DeptDesigDetails_GetAll]
AS
BEGIN
    SELECT * FROM DeptDesigDetails WHERE IsDeleted = 0
END
GO
/****** Object:  StoredProcedure [dbo].[DeptDesigDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- DeptDesigDetails_GetById
CREATE   PROCEDURE [dbo].[DeptDesigDetails_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * FROM DeptDesigDetails WHERE Id = @Id AND IsDeleted = 0
END
GO
/****** Object:  StoredProcedure [dbo].[DeptDesigDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- DeptDesigDetails_Update
CREATE   PROCEDURE [dbo].[DeptDesigDetails_Update]
    @Id UNIQUEIDENTIFIER,
    @DepartmentId UNIQUEIDENTIFIER,
    @DesignationId UNIQUEIDENTIFIER,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER,
    @Status VARCHAR(10) = NULL,
    @StatusMessage NVARCHAR(255) = NULL
AS
BEGIN
    UPDATE DeptDesigDetails SET
        DepartmentId = @DepartmentId,
        DesignationId = @DesignationId,
        CompanyId = @CompanyId,
        SchoolId = @SchoolId,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = GETUTCDATE(),
        Status = @Status,
        StatusMessage = @StatusMessage
    WHERE Id = @Id
    
    RETURN 1
END
GO
/****** Object:  StoredProcedure [dbo].[DeptMaster_BulkInsert]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[DeptMaster_BulkInsert]
    @Departments DeptMasterType READONLY
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO DeptMasters (
        Id,
        DeptCode,
        DeptName,
        SchoolId,
        IsActive,
        CreatedBy,
        CreatedOn,
        CompanyId
    )
    SELECT 
        Id,
        DeptCode,
        DeptName,
        SchoolId,
        IsActive,
        CreatedBy,
        CreatedOn,
        CompanyId
    FROM @Departments
END

GO
/****** Object:  StoredProcedure [dbo].[DeptMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- DeptMaster_Create
CREATE   PROCEDURE [dbo].[DeptMaster_Create]
    @DeptCode NVARCHAR(50),
    @DeptName NVARCHAR(100),
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();
    
    INSERT INTO DeptMaster (
        Id, DeptCode, DeptName, IsActive, CompanyId, SchoolId,
        CreatedBy, CreatedDate, IsDeleted
    ) VALUES (
        @NewId, @DeptCode, @DeptName, @IsActive, @CompanyId, @SchoolId,
        @CreatedBy, GETUTCDATE(), 0
    );
    
    SELECT @NewId AS Id;
    
    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[DeptMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- DeptMaster_Delete
CREATE   PROCEDURE [dbo].[DeptMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Soft delete
    UPDATE DeptMaster
    SET 
        IsDeleted = 1,
        ModifiedDate = GETUTCDATE()
    WHERE 
        Id = @Id;
    
    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[DeptMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- DeptMaster_GetAll
CREATE   PROCEDURE [dbo].[DeptMaster_GetAll]
AS
BEGIN
    SELECT 
        d.Id, d.DeptCode, d.DeptName, d.IsActive, d.CompanyId, d.SchoolId,
        d.CreatedBy, d.CreatedDate, d.ModifiedBy, d.ModifiedDate,
        d.Status, d.StatusMessage
    FROM DeptMaster d
    WHERE d.IsDeleted = 0
    ORDER BY d.DeptName
END
GO
/****** Object:  StoredProcedure [dbo].[DeptMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- DeptMaster_GetById
CREATE   PROCEDURE [dbo].[DeptMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT 
        d.Id, d.DeptCode, d.DeptName, d.IsActive, d.CompanyId, d.SchoolId,
        d.CreatedBy, d.CreatedDate, d.ModifiedBy, d.ModifiedDate,
        d.Status, d.StatusMessage
    FROM DeptMaster d
    WHERE d.Id = @Id AND d.IsDeleted = 0
END
GO
/****** Object:  StoredProcedure [dbo].[DeptMaster_GetBySchool]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[DeptMaster_GetBySchool]
    @SchoolId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        DeptCode,
        DeptName,
        IsActive,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM DeptMaster
    WHERE SchoolId = @SchoolId
    ORDER BY DeptName;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeptMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- DeptMaster_Update
CREATE   PROCEDURE [dbo].[DeptMaster_Update]
    @Id UNIQUEIDENTIFIER,
    @DeptCode NVARCHAR(50),
    @DeptName NVARCHAR(100),
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE DeptMaster
    SET 
        DeptCode = @DeptCode,
        DeptName = @DeptName,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = GETUTCDATE()
    WHERE 
        Id = @Id;
    
    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[DesigMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create a new designation
CREATE   PROCEDURE [dbo].[DesigMaster_Create]
    @Id UNIQUEIDENTIFIER,
    @Code NVARCHAR(50),
    @Name NVARCHAR(100),
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @IsDeleted BIT,
    @CreatedBy UNIQUEIDENTIFIER,
    @CreatedDate DATETIME,
    @ModifiedBy UNIQUEIDENTIFIER = NULL,
    @ModifiedDate DATETIME = NULL,
    @Status NVARCHAR(50) = NULL,
    @StatusMessage NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO DesigMaster (
        Id,
        Code,
        Name,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    ) VALUES (
        @Id,
        @Code,
        @Name,
        @CompanyId,
        @SchoolId,
        @IsActive,
        @IsDeleted,
        @CreatedBy,
        @CreatedDate,
        @ModifiedBy,
        @ModifiedDate,
        @Status,
        @StatusMessage
    );
    
    SELECT @Id AS Id;
END
GO
/****** Object:  StoredProcedure [dbo].[DesigMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Soft delete a designation
CREATE   PROCEDURE [dbo].[DesigMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE DesigMaster
    SET 
        IsDeleted = 1,
        ModifiedDate = GETUTCDATE()
    WHERE 
        Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[DesigMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Get all designations
CREATE   PROCEDURE [dbo].[DesigMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id,
        Code,
        Name,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM 
        DesigMaster
    WHERE 
        IsDeleted = 0
    ORDER BY 
        Name;
END
GO
/****** Object:  StoredProcedure [dbo].[DesigMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Get designation by ID
CREATE   PROCEDURE [dbo].[DesigMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id,
        Code,
        Name,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM 
        DesigMaster
    WHERE 
        Id = @Id
        AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[DesigMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Update an existing designation
CREATE   PROCEDURE [dbo].[DesigMaster_Update]
    @Id UNIQUEIDENTIFIER,
    @Code NVARCHAR(50),
    @Name NVARCHAR(100),
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @IsDeleted BIT,
    @CreatedBy UNIQUEIDENTIFIER,
    @CreatedDate DATETIME,
    @ModifiedBy UNIQUEIDENTIFIER = NULL,
    @ModifiedDate DATETIME = NULL,
    @Status NVARCHAR(50) = NULL,
    @StatusMessage NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE DesigMaster
    SET 
        Code = @Code,
        Name = @Name,
        CompanyId = @CompanyId,
        SchoolId = @SchoolId,
        IsActive = @IsActive,
        IsDeleted = @IsDeleted,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = @ModifiedDate,
        Status = @Status,
        StatusMessage = @StatusMessage
    WHERE 
        Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[Designation_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Designation_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        d.[Id],
        d.[Name]
    FROM dbo.[DesigMaster] d
    ORDER BY d.[Name];
END
GO
/****** Object:  StoredProcedure [dbo].[DriverDocumentDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[DriverDocumentDetails_Create]
    @DriverId UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @Description NVARCHAR(500),
    @FileName NVARCHAR(500),
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER,
    @Status NVARCHAR(50),
    @StatusMessage NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();
    INSERT INTO dbo.DriverDocumentDetails
    (
        Id, DriverId, Name, Description, FileName,
        CompanyId, SchoolId, IsActive, IsDeleted,
        CreatedBy, CreatedDate, Status, StatusMessage
    )
    VALUES
    (
        @NewId, @DriverId, ISNULL(@Name, ''), ISNULL(@Description, ''), ISNULL(@FileName, ''),
        @CompanyId, @SchoolId, ISNULL(@IsActive, 0), 0,
        @CreatedBy, SYSUTCDATETIME(), ISNULL(@Status, ''), ISNULL(@StatusMessage, '')
    );
    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[DriverDocumentDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[DriverDocumentDetails_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.DriverDocumentDetails
    SET IsDeleted = 1,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[DriverDocumentDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[DriverDocumentDetails_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,
           DriverId,
           Name,
           Description,
           FileName,
           CompanyId,
           SchoolId,
           IsActive,
           IsDeleted,
           CreatedBy,
           CreatedDate,
           ModifiedBy,
           ModifiedDate,
           Status,
           StatusMessage
    FROM dbo.DriverDocumentDetails WITH (NOLOCK)
    WHERE ISNULL(IsDeleted, 0) = 0
    ORDER BY CreatedDate DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[DriverDocumentDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[DriverDocumentDetails_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,
           DriverId,
           Name,
           Description,
           FileName,
           CompanyId,
           SchoolId,
           IsActive,
           IsDeleted,
           CreatedBy,
           CreatedDate,
           ModifiedBy,
           ModifiedDate,
           Status,
           StatusMessage
    FROM dbo.DriverDocumentDetails WITH (NOLOCK)
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[DriverDocumentDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[DriverDocumentDetails_Update]
    @Id UNIQUEIDENTIFIER,
    @DriverId UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @Description NVARCHAR(500),
    @FileName NVARCHAR(500),
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.DriverDocumentDetails
    SET DriverId = @DriverId,
        Name = ISNULL(@Name, ''),
        Description = ISNULL(@Description, ''),
        FileName = ISNULL(@FileName, ''),
        IsActive = ISNULL(@IsActive, 0),
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[DriverMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[DriverMaster_Create]
    @FirstName               nvarchar(200),
    @LastName                nvarchar(200),
    @DateOfBirth             datetime2(0) = NULL,
    @FathersName             nvarchar(200),
    @MothersName             nvarchar(200),
    @QualificationId         uniqueidentifier = NULL,
    @Address1                nvarchar(300),
    @Address2                nvarchar(300),
    @CityId                  uniqueidentifier = NULL,
    @StateId                 uniqueidentifier = NULL,
    @CountryId               uniqueidentifier = NULL,
    @ZipCode                 nvarchar(20),
    @MobileNumber            nvarchar(20),
    @PhoneNumber             nvarchar(20),
    @DriverImage             nvarchar(500),
    @LicenceNumber           nvarchar(100),
    @LicenceIssueDate        datetime2(0) = NULL,
    @LicenceValidUptoDate    datetime2(0) = NULL,
    @LicenceDescription      nvarchar(max),
    @LicenceImage            nvarchar(500),
    @LicenceType             nvarchar(100),
    @CompanyId               uniqueidentifier,
    @SchoolId                uniqueidentifier,
    @IsActive                bit,
    @IsDeleted               bit,
    @CreatedBy               uniqueidentifier,
    @CreatedDate             datetime2(0),
    @Status                  nvarchar(50),
    @StatusMessage           nvarchar(200)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Id uniqueidentifier = NEWID();

    INSERT INTO dbo.DriverMaster
    (
        Id, FirstName, LastName, DateOfBirth, FathersName, MothersName,
        QualificationId, Address1, Address2, CityId, StateId, CountryId, ZipCode,
        MobileNumber, PhoneNumber, DriverImage, LicenceNumber, LicenceIssueDate,
        LicenceValidUptoDate, LicenceDescription, LicenceImage, LicenceType,
        CompanyId, SchoolId, IsActive, IsDeleted, CreatedBy, CreatedDate, Status, StatusMessage
    )
    VALUES
    (
        @Id, @FirstName, @LastName, @DateOfBirth, @FathersName, @MothersName,
        ISNULL(@QualificationId, '00000000-0000-0000-0000-000000000000'),
        @Address1, @Address2, ISNULL(@CityId, '00000000-0000-0000-0000-000000000000'),
        ISNULL(@StateId, '00000000-0000-0000-0000-000000000000'),
        ISNULL(@CountryId, '00000000-0000-0000-0000-000000000000'),
        @ZipCode, @MobileNumber, @PhoneNumber, @DriverImage, @LicenceNumber,
        @LicenceIssueDate, @LicenceValidUptoDate, @LicenceDescription, @LicenceImage,
        @LicenceType, @CompanyId, @SchoolId, @IsActive, @IsDeleted, @CreatedBy, @CreatedDate,
        @Status, @StatusMessage
    );

    SELECT @Id AS Id;
END
GO
/****** Object:  StoredProcedure [dbo].[DriverMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[DriverMaster_Delete]
    @Id uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.DriverMaster
    SET IsDeleted    = 1,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN CASE WHEN @@ROWCOUNT = 1 THEN 1 ELSE 0 END;
END
GO
/****** Object:  StoredProcedure [dbo].[DriverMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DriverMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        FirstName,
        LastName,
        DateOfBirth,
        FathersName,
        MothersName,
        QualificationId,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        MobileNumber,
        PhoneNumber,
        DriverImage,
        LicenceNumber,
        LicenceIssueDate,
        LicenceValidUptoDate,
        LicenceDescription,
        LicenceImage,
        LicenceType,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[DriverMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[DriverMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DriverMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        FirstName,
        LastName,
        DateOfBirth,
        FathersName,
        MothersName,
        QualificationId,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        MobileNumber,
        PhoneNumber,
        DriverImage,
        LicenceNumber,
        LicenceIssueDate,
        LicenceValidUptoDate,
        LicenceDescription,
        LicenceImage,
        LicenceType,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[DriverMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[DriverMaster_GetByKey]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[DriverMaster_GetByKey]
    @CompanyId uniqueidentifier,
    @SchoolId  uniqueidentifier,
    @FirstName nvarchar(200),
    @LastName  nvarchar(200)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (1) *
    FROM dbo.DriverMaster WITH (NOLOCK)
    WHERE CompanyId = @CompanyId
      AND SchoolId  = @SchoolId
      AND FirstName = @FirstName
      AND LastName  = @LastName
      AND (IsDeleted = 0 OR IsDeleted IS NULL);
END
GO
/****** Object:  StoredProcedure [dbo].[DriverMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[DriverMaster_Update]
    @Id                      uniqueidentifier,
    @FirstName               nvarchar(200),
    @LastName                nvarchar(200),
    @DateOfBirth             datetime2(0) = NULL,
    @FathersName             nvarchar(200),
    @MothersName             nvarchar(200),
    @QualificationId         uniqueidentifier = NULL,
    @Address1                nvarchar(300),
    @Address2                nvarchar(300),
    @CityId                  uniqueidentifier = NULL,
    @StateId                 uniqueidentifier = NULL,
    @CountryId               uniqueidentifier = NULL,
    @ZipCode                 nvarchar(20),
    @MobileNumber            nvarchar(20),
    @PhoneNumber             nvarchar(20),
    @DriverImage             nvarchar(500),
    @LicenceNumber           nvarchar(100),
    @LicenceIssueDate        datetime2(0) = NULL,
    @LicenceValidUptoDate    datetime2(0) = NULL,
    @LicenceDescription      nvarchar(max),
    @LicenceImage            nvarchar(500),
    @LicenceType             nvarchar(100),
    @IsActive                bit,
    @IsDeleted               bit,
    @ModifiedBy              uniqueidentifier,
    @ModifiedDate            datetime2(0),
    @Status                  nvarchar(50),
    @StatusMessage           nvarchar(200)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.DriverMaster
    SET FirstName            = @FirstName,
        LastName             = @LastName,
        DateOfBirth          = @DateOfBirth,
        FathersName          = @FathersName,
        MothersName          = @MothersName,
        QualificationId      = @QualificationId,
        Address1             = @Address1,
        Address2             = @Address2,
        CityId               = @CityId,
        StateId              = @StateId,
        CountryId            = @CountryId,
        ZipCode              = @ZipCode,
        MobileNumber         = @MobileNumber,
        PhoneNumber          = @PhoneNumber,
        DriverImage          = @DriverImage,
        LicenceNumber        = @LicenceNumber,
        LicenceIssueDate     = @LicenceIssueDate,
        LicenceValidUptoDate = @LicenceValidUptoDate,
        LicenceDescription   = @LicenceDescription,
        LicenceImage         = @LicenceImage,
        LicenceType          = @LicenceType,
        IsActive             = @IsActive,
        IsDeleted            = @IsDeleted,
        ModifiedBy           = @ModifiedBy,
        ModifiedDate         = @ModifiedDate,
        Status               = @Status,
        StatusMessage        = @StatusMessage
    WHERE Id = @Id;

    RETURN CASE WHEN @@ROWCOUNT = 1 THEN 1 ELSE 0 END;
END
GO
/****** Object:  StoredProcedure [dbo].[DriverQualificationDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[DriverQualificationDetails_Create]
    @DriverId UNIQUEIDENTIFIER,
    @QualificationId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER,
    @Status NVARCHAR(50),
    @StatusMessage NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();
    INSERT INTO dbo.DriverQualificationDetails
    (
        Id, DriverId, QualificationId,
        CompanyId, SchoolId, IsActive, IsDeleted,
        CreatedBy, CreatedDate, Status, StatusMessage
    )
    VALUES
    (
        @NewId, @DriverId, @QualificationId,
        @CompanyId, @SchoolId, ISNULL(@IsActive, 0), 0,
        @CreatedBy, SYSUTCDATETIME(), ISNULL(@Status, ''), ISNULL(@StatusMessage, '')
    );
    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[DriverQualificationDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[DriverQualificationDetails_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.DriverQualificationDetails
    SET IsDeleted = 1,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[DriverQualificationDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[DriverQualificationDetails_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,
           DriverId,
           QualificationId,
           SchoolId,
           CompanyId,
           IsActive,
           IsDeleted,
           CreatedBy,
           CreatedDate,
           ModifiedBy,
           ModifiedDate,
           Status,
           StatusMessage
    FROM dbo.DriverQualificationDetails WITH (NOLOCK)
    WHERE ISNULL(IsDeleted, 0) = 0
    ORDER BY CreatedDate DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[DriverQualificationDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[DriverQualificationDetails_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,
           DriverId,
           QualificationId,
           SchoolId,
           CompanyId,
           IsActive,
           IsDeleted,
           CreatedBy,
           CreatedDate,
           ModifiedBy,
           ModifiedDate,
           Status,
           StatusMessage
    FROM dbo.DriverQualificationDetails WITH (NOLOCK)
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[DriverQualificationDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[DriverQualificationDetails_Update]
    @Id UNIQUEIDENTIFIER,
    @DriverId UNIQUEIDENTIFIER,
    @QualificationId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.DriverQualificationDetails
    SET DriverId = @DriverId,
        QualificationId = @QualificationId,
        IsActive = ISNULL(@IsActive, 0),
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Emp_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Emp_Create]  
    @FirstName NVARCHAR(50),  
    @LastName NVARCHAR(50),  
    @DOB DATETIME,  
    @DOJ DATETIME,  
    @ProbationStartDate DATETIME = NULL,  
    @ProbationPeriod INT = NULL,  
    @ConfirmationDate DATETIME = NULL,  
    @PANNumber NVARCHAR(50) = NULL,  
    @ESICNumber NVARCHAR(50) = NULL,  
    @PFNumeber NVARCHAR(50) = NULL,  
    @CurrentAddress1 NVARCHAR(255) = NULL,  
    @CurrentAddress2 NVARCHAR(255) = NULL,  
    @CurrentCityId UNIQUEIDENTIFIER = NULL,  
    @CurrentStateId UNIQUEIDENTIFIER = NULL,  
    @CurrentCountryId UNIQUEIDENTIFIER = NULL,  
    @CurrentZipCode NVARCHAR(50) = NULL,  
    @PermanentAddress1 NVARCHAR(255) = NULL,  
    @PermanentAddress2 NVARCHAR(255) = NULL,  
    @PermanentCityId UNIQUEIDENTIFIER = NULL,  
    @PermanentStateId UNIQUEIDENTIFIER = NULL,  
    @PermanentCountryId UNIQUEIDENTIFIER = NULL,  
    @PermanentZipCode NVARCHAR(50) = NULL,  
    @PhoneNumber NVARCHAR(50) = NULL,  
    @MobileNumber NVARCHAR(50) = NULL,  
    @EmailId NVARCHAR(150) = NULL,  
    @DepartmentId UNIQUEIDENTIFIER = NULL,  
    @DesignationId UNIQUEIDENTIFIER = NULL,  
    @PaymentModeId UNIQUEIDENTIFIER = NULL,  
    @EmployeeTypeId UNIQUEIDENTIFIER = NULL,  
    @CategoryId UNIQUEIDENTIFIER = NULL,  
    @BankAccountNumber NVARCHAR(100) = NULL,  
    @BankName NVARCHAR(100) = NULL,  
    @GenderId UNIQUEIDENTIFIER = NULL,  
    @BloodGroupId UNIQUEIDENTIFIER = NULL,  
    @GradeId UNIQUEIDENTIFIER = NULL,  
    @Image NVARCHAR(255) = NULL,  
    @EmployeeOldId UNIQUEIDENTIFIER = NULL,  
    @FathersName NVARCHAR(100) = NULL,  
    @MothersName NVARCHAR(100) = NULL,  
    @Description NVARCHAR(255) = NULL,  
    @LicenceNumber NVARCHAR(50) = NULL,  
    @LicenceIssueDate DATETIME = NULL,  
    @LicenceValidUpto DATETIME = NULL,  
    @LicenceDescription NVARCHAR(255) = NULL,  
    @LicenceImage NVARCHAR(255) = NULL,  
    @LicenceType NVARCHAR(50) = NULL,  
    @Salutation NVARCHAR(20) = NULL,  
    @DateOfLeaving DATETIME = NULL,  
    @MaritalStatus NVARCHAR(50) = NULL,  
    @YearsOfExperience NVARCHAR(50) = NULL,  
    @PrevioudSchoolCompany NVARCHAR(150) = NULL,  
    @AadhaarNumber NVARCHAR(20) = NULL,  
    @MathUpToClass INT = NULL,  
    @EnglishUptoClass INT = NULL,  
    @SSTUptoClass INT = NULL,  
    @CompanyId UNIQUEIDENTIFIER,  
    @SchoolId UNIQUEIDENTIFIER,  
    @IsActive BIT,  
    @CreatedBy UNIQUEIDENTIFIER,  
    @Status NVARCHAR(10) = NULL,  
    @StatusMessage NVARCHAR(255) = NULL  
AS  
BEGIN  
    SET NOCOUNT ON;  
  
    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();  
  
    INSERT INTO [dbo].[EmpMaster]  
    (  
        Id,  
        FirstName,  
        LastName,  
        DOB,  
        DOJ,  
        ProbationStartDate,  
        ProbationPeriod,  
        ConfirmationDate,  
        PANNumber,  
        ESICNumber,  
        PFNumeber,  
        CurrentAddress1,  
        CurrentAddress2,  
        CurrentCityId,  
        CurrentStateId,  
        CurrentCountryId,  
        CurrentZipCode,  
        PermanentAddress1,  
        PermanentAddress2,  
        PermanentCityId,  
        PermanentStateId,  
        PermanentCountryId,  
        PermanentZipCode,  
        PhoneNumber,  
        MobileNumber,  
        EmailId,  
        DepartmentId,  
        DesignationId,  
        PaymentModeId,  
        EmployeeTypeId,  
        CategoryId,  
        BankAccountNumber,  
        BankName,  
        GenderId,  
        BloodGroupId,  
        GradeId,  
        Image,  
        EmployeeOldId,  
        FathersName,  
        MothersName,  
        Description,  
        LicenceNumber,  
        LicenceIssueDate,  
        LicenceValidUpto,  
        LicenceDescription,  
        LicenceImage,  
        LicenceType,  
        Salutation,  
        DateOfLeaving,  
        MaritalStatus,  
        YearsOfExperience,  
        PrevioudSchoolCompany,  
        AadhaarNumber,  
        MathUpToClass,  
        EnglishUptoClass,  
        SSTUptoClass,  
        CompanyId,  
        SchoolId,  
        IsActive,  
        IsDeleted,  
        CreatedBy,  
        CreatedDate,  
        [Status],  
        StatusMessage  
    )  
    VALUES  
    (  
        @NewId,  
        @FirstName,  
        @LastName,  
        @DOB,  
        @DOJ,  
        @ProbationStartDate,  
        @ProbationPeriod,  
        @ConfirmationDate,  
        @PANNumber,  
        @ESICNumber,  
        @PFNumeber,  
        @CurrentAddress1,  
        @CurrentAddress2,  
        @CurrentCityId,  
        @CurrentStateId,  
        @CurrentCountryId,  
        @CurrentZipCode,  
        @PermanentAddress1,  
        @PermanentAddress2,  
        @PermanentCityId,  
        @PermanentStateId,  
        @PermanentCountryId,  
        @PermanentZipCode,  
        @PhoneNumber,  
        @MobileNumber,  
        @EmailId,  
        @DepartmentId,  
        @DesignationId,  
        @PaymentModeId,  
        @EmployeeTypeId,  
        @CategoryId,  
        @BankAccountNumber,  
        @BankName,  
        @GenderId,  
        @BloodGroupId,  
        @GradeId,  
        @Image,  
        @EmployeeOldId,  
        @FathersName,  
        @MothersName,  
        @Description,  
        @LicenceNumber,  
        @LicenceIssueDate,  
        @LicenceValidUpto,  
        @LicenceDescription,  
        @LicenceImage,  
        @LicenceType,  
        @Salutation,  
        @DateOfLeaving,  
        @MaritalStatus,  
        @YearsOfExperience,  
        @PrevioudSchoolCompany,  
        @AadhaarNumber,  
        @MathUpToClass,  
        @EnglishUptoClass,  
        @SSTUptoClass,  
        @CompanyId,  
        @SchoolId,  
        @IsActive,  
        0,  
        @CreatedBy,  
        SYSUTCDATETIME(),  
        ISNULL(@Status, N''),  
        ISNULL(@StatusMessage, N'')  
    );  
  
    SELECT Id = @NewId;  

    declare @MaritalStatusId as uniqueidentifier
    select @MaritalStatusId = Id from MaritalStatus where Value = @MaritalStatus

    if @EmployeeTypeId ='20F6CFED-447D-4C76-9B14-7ED3BF71F2C5' or @EmployeeTypeId = '7C3BE219-516F-456A-BBDB-93AA669A62D7'
    begin
        INSERT INTO [dbo].[TeacherMaster]
           ([Id]
           ,[FirstName]
           ,[LastName]
           ,[DOB]
           ,[DOJ]
           ,[DateOfLeaving]
           ,[Address]
           ,[CityId]
           ,[StateId]
           ,[CountryId]
           ,[ZipCode]
           ,[Gender]
           ,[MaritalStatusId]
           ,[Image]
           ,[Phone]
           ,[MobilePhone]
           ,[YearsOfExperience]
           ,[PreviousSchool]
           ,[Salutation]
           ,[Email]
           ,[CompanyId]
           ,[SchoolId]
           ,[IsActive]
           ,[IsDeleted]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[Status]
           ,[StatusMessage])
     VALUES
           ( @NewId,  
        @FirstName,  
        @LastName,  
        @DOB,  
        @DOJ,  
        null,
        @CurrentAddress1 + ' ' + @CurrentAddress2,  
        @CurrentCityId,  
        @CurrentStateId,  
        @CurrentCountryId,  
        @CurrentZipCode,  
        @GenderId,
        @MaritalStatusId,
        @Image,
        @PhoneNumber,
        @MobileNumber,
        @YearsOfExperience,
        null,
        @Salutation,
        @EmailId,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        getdate(),
        @CreatedBy,
        getdate(),
        @Status,
        @StatusMessage)

    end

    -- Cleaner
    if @EmployeeTypeId = '10C7623D-EBD8-4142-85EE-56B0F453B4FC'
    begin
        INSERT INTO [dbo].[CleanerMaster]
           ([Id]
           ,[Name]
           ,[Image]
           ,[FatherName]
           ,[Description]
           ,[IsActive]
           ,[IsDeleted]
           ,[CompanyId]
           ,[SchoolId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[Status]
           ,[StatusMessage])
     VALUES
           (@NewId,
           @FirstName + ' ' + @LastName,
           @Image,
           @FathersName,
           @Description,
           1,
           0,
           @CompanyId,
           @SchoolId,
           @CreatedBy,
           getdate(),
           @CreatedBy,
           getdate(),
           @Status,
           @StatusMessage)
    end

     -- Driver
    if @EmployeeTypeId = 'D61B4985-A882-4DE8-A3AD-D8C1E4FE2460'
    begin
       INSERT INTO [dbo].[DriverMaster]
           ([Id]
           ,[FirstName]
           ,[LastName]
           ,[DateOfBirth]
           ,[FathersName]
           ,[MothersName]
           ,[QualificationId]
           ,[Address1]
           ,[Address2]
           ,[CityId]
           ,[StateId]
           ,[CountryId]
           ,[ZipCode]
           ,[MobileNumber]
           ,[PhoneNumber]
           ,[DriverImage]
           ,[LicenceNumber]
           ,[LicenceIssueDate]
           ,[LicenceValidUptoDate]
           ,[LicenceDescription]
           ,[LicenceImage]
           ,[LicenceType]
           ,[CompanyId]
           ,[SchoolId]
           ,[IsActive]
           ,[IsDeleted]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[Status]
           ,[StatusMessage])
     VALUES
           (@NewId,
           @FirstName,
           @LastName,
           @DOB,
           @FathersName,
           @MothersName,
           null,    --@QualificationId,
           @CurrentAddress1,
           @CurrentAddress2,  
           @CurrentCityId,
           @CurrentStateId,
           @CurrentCountryId,
           @CurrentZipCode,  
           @MobileNumber,
           @PhoneNumber,
           @Image,
           @LicenceNumber,
           @LicenceIssueDate,
           @DOB+20,
           @LicenceDescription,
           @LicenceImage,
           @LicenceType,
           @CompanyId,
           @SchoolId,
           @IsActive,
           0,
           @CreatedBy,
           getdate(),
           @CreatedBy,
           getdate(),
           'INC',
           'Driver Added Successfully')
    end

    declare @UserName as varchar(100)
    declare @Password as varchar(30)
    declare @ctr as varchar(10)
    select @ctr = RIGHT('0000' + CAST(count(*) AS VARCHAR(4)), 4) from UserDetails --where SchoolId = @SchoolId

    select @UserName = concat(left(Id,4),@ctr)  from UserDetails --where SchoolId = @SchoolId

    select @Password = 'Simsaw@8546#'
    -- Add Entry to UserDetails table
    INSERT INTO [dbo].[UserDetails]
           ([Id]
           ,[UserName]
           ,[UserPassword]
           ,[FirstName]
           ,[LastName]
           ,[EmailAddress]
           ,[DesignationId]
           ,[UserRoleId]
           ,[IsSuperUser]
           ,[CompanyId]
           ,[SchoolId]
           ,[IsActive]
           ,[IsDeleted]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[Status]
           ,[StatusMessage])
     VALUES
           (@NewId,
           @UserName,
           @Password,
           @FirstName,
           @LastName,
           @EmailId,
           @DesignationId,
           @CategoryId,
           0,
           @CompanyId,
           @SchoolId,
           1,
           0,
           @CreatedBy,
           getdate(),
           @CreatedBy,
           getdate(),
           'INC',
           'User Added Successfully')
END  
GO
/****** Object:  StoredProcedure [dbo].[Emp_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Emp_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[EmpMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Emp_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Emp_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        FirstName,
        LastName,
        DOB,
        DOJ,
        ProbationStartDate,
        ProbationPeriod,
        ConfirmationDate,
        PANNumber,
        ESICNumber,
        PFNumeber,
        CurrentAddress1,
        CurrentAddress2,
        CurrentCityId,
        CurrentStateId,
        CurrentCountryId,
        CurrentZipCode,
        PermanentAddress1,
        PermanentAddress2,
        PermanentCityId,
        PermanentStateId,
        PermanentCountryId,
        PermanentZipCode,
        PhoneNumber,
        MobileNumber,
        EmailId,
        DepartmentId,
        DesignationId,
        PaymentModeId,
        EmployeeTypeId,
        CategoryId,
        BankAccountNumber,
        BankName,
        GenderId,
        BloodGroupId,
        GradeId,
        Image,
        EmployeeOldId,
        FathersName,
        MothersName,
        Description,
        LicenceNumber,
        LicenceIssueDate,
        LicenceValidUpto,
        LicenceDescription,
        LicenceImage,
        LicenceType,
        Salutation,
        DateOfLeaving,
        MaritalStatus,
        YearsOfExperience,
        PrevioudSchoolCompany,
        AadhaarNumber,
        MathUpToClass,
        EnglishUptoClass,
        SSTUptoClass,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[EmpMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Emp_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Emp_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        FirstName,
        LastName,
        DOB,
        DOJ,
        ProbationStartDate,
        ProbationPeriod,
        ConfirmationDate,
        PANNumber,
        ESICNumber,
        PFNumeber,
        CurrentAddress1,
        CurrentAddress2,
        CurrentCityId,
        CurrentStateId,
        CurrentCountryId,
        CurrentZipCode,
        PermanentAddress1,
        PermanentAddress2,
        PermanentCityId,
        PermanentStateId,
        PermanentCountryId,
        PermanentZipCode,
        PhoneNumber,
        MobileNumber,
        EmailId,
        DepartmentId,
        DesignationId,
        PaymentModeId,
        EmployeeTypeId,
        CategoryId,
        BankAccountNumber,
        BankName,
        GenderId,
        BloodGroupId,
        GradeId,
        Image,
        EmployeeOldId,
        FathersName,
        MothersName,
        Description,
        LicenceNumber,
        LicenceIssueDate,
        LicenceValidUpto,
        LicenceDescription,
        LicenceImage,
        LicenceType,
        Salutation,
        DateOfLeaving,
        MaritalStatus,
        YearsOfExperience,
        PrevioudSchoolCompany,
        AadhaarNumber,
        MathUpToClass,
        EnglishUptoClass,
        SSTUptoClass,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[EmpMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[Emp_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Emp_Update]
    @Id UNIQUEIDENTIFIER,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @DOB DATETIME,
    @DOJ DATETIME,
    @ProbationStartDate DATETIME = NULL,
    @ProbationPeriod INT = NULL,
    @ConfirmationDate DATETIME = NULL,
    @PANNumber NVARCHAR(50) = NULL,
    @ESICNumber NVARCHAR(50) = NULL,
    @PFNumeber NVARCHAR(50) = NULL,
    @CurrentAddress1 NVARCHAR(255) = NULL,
    @CurrentAddress2 NVARCHAR(255) = NULL,
    @CurrentCityId UNIQUEIDENTIFIER = NULL,
    @CurrentStateId UNIQUEIDENTIFIER = NULL,
    @CurrentCountryId UNIQUEIDENTIFIER = NULL,
    @CurrentZipCode NVARCHAR(50) = NULL,
    @PermanentAddress1 NVARCHAR(255) = NULL,
    @PermanentAddress2 NVARCHAR(255) = NULL,
    @PermanentCityId UNIQUEIDENTIFIER = NULL,
    @PermanentStateId UNIQUEIDENTIFIER = NULL,
    @PermanentCountryId UNIQUEIDENTIFIER = NULL,
    @PermanentZipCode NVARCHAR(50) = NULL,
    @PhoneNumber NVARCHAR(50) = NULL,
    @MobileNumber NVARCHAR(50) = NULL,
    @EmailId NVARCHAR(150) = NULL,
    @DepartmentId UNIQUEIDENTIFIER = NULL,
    @DesignationId UNIQUEIDENTIFIER = NULL,
    @PaymentModeId UNIQUEIDENTIFIER = NULL,
    @EmployeeTypeId UNIQUEIDENTIFIER = NULL,
    @CategoryId UNIQUEIDENTIFIER = NULL,
    @BankAccountNumber NVARCHAR(100) = NULL,
    @BankName NVARCHAR(100) = NULL,
    @GenderId UNIQUEIDENTIFIER = NULL,
    @BloodGroupId UNIQUEIDENTIFIER = NULL,
    @GradeId UNIQUEIDENTIFIER = NULL,
    @Image NVARCHAR(255) = NULL,
    @EmployeeOldId UNIQUEIDENTIFIER = NULL,
    @FathersName NVARCHAR(100) = NULL,
    @MothersName NVARCHAR(100) = NULL,
    @Description NVARCHAR(255) = NULL,
    @LicenceNumber NVARCHAR(50) = NULL,
    @LicenceIssueDate DATETIME = NULL,
    @LicenceValidUpto DATETIME = NULL,
    @LicenceDescription NVARCHAR(255) = NULL,
    @LicenceImage NVARCHAR(255) = NULL,
    @LicenceType NVARCHAR(50) = NULL,
    @Salutation NVARCHAR(20) = NULL,
    @DateOfLeaving DATETIME = NULL,
    @MaritalStatus NVARCHAR(50) = NULL,
    @YearsOfExperience NVARCHAR(50) = NULL,
    @PrevioudSchoolCompany NVARCHAR(150) = NULL,
    @AadhaarNumber NVARCHAR(20) = NULL,
    @MathUpToClass INT = NULL,
    @EnglishUptoClass INT = NULL,
    @SSTUptoClass INT = NULL,
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER,
    @Status NVARCHAR(10) = NULL,
    @StatusMessage NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[EmpMaster]
    SET 
        FirstName = @FirstName,
        LastName = @LastName,
        DOB = @DOB,
        DOJ = @DOJ,
        ProbationStartDate = @ProbationStartDate,
        ProbationPeriod = @ProbationPeriod,
        ConfirmationDate = @ConfirmationDate,
        PANNumber = @PANNumber,
        ESICNumber = @ESICNumber,
        PFNumeber = @PFNumeber,
        CurrentAddress1 = @CurrentAddress1,
        CurrentAddress2 = @CurrentAddress2,
        CurrentCityId = @CurrentCityId,
        CurrentStateId = @CurrentStateId,
        CurrentCountryId = @CurrentCountryId,
        CurrentZipCode = @CurrentZipCode,
        PermanentAddress1 = @PermanentAddress1,
        PermanentAddress2 = @PermanentAddress2,
        PermanentCityId = @PermanentCityId,
        PermanentStateId = @PermanentStateId,
        PermanentCountryId = @PermanentCountryId,
        PermanentZipCode = @PermanentZipCode,
        PhoneNumber = @PhoneNumber,
        MobileNumber = @MobileNumber,
        EmailId = @EmailId,
        DepartmentId = @DepartmentId,
        DesignationId = @DesignationId,
        PaymentModeId = @PaymentModeId,
        EmployeeTypeId = @EmployeeTypeId,
        CategoryId = @CategoryId,
        BankAccountNumber = @BankAccountNumber,
        BankName = @BankName,
        GenderId = @GenderId,
        BloodGroupId = @BloodGroupId,
        GradeId = @GradeId,
        Image = @Image,
        EmployeeOldId = @EmployeeOldId,
        FathersName = @FathersName,
        MothersName = @MothersName,
        Description = @Description,
        LicenceNumber = @LicenceNumber,
        LicenceIssueDate = @LicenceIssueDate,
        LicenceValidUpto = @LicenceValidUpto,
        LicenceDescription = @LicenceDescription,
        LicenceImage = @LicenceImage,
        LicenceType = @LicenceType,
        Salutation = @Salutation,
        DateOfLeaving = @DateOfLeaving,
        MaritalStatus = @MaritalStatus,
        YearsOfExperience = @YearsOfExperience,
        PrevioudSchoolCompany = @PrevioudSchoolCompany,
        AadhaarNumber = @AadhaarNumber,
        MathUpToClass = @MathUpToClass,
        EnglishUptoClass = @EnglishUptoClass,
        SSTUptoClass = @SSTUptoClass,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME(),
        [Status] = ISNULL(@Status, [Status]),
        StatusMessage = ISNULL(@StatusMessage, StatusMessage)
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[EmployeeCategory_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EmployeeCategory_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    /*
        TODO: Replace with your real table/column names:
        - EmployeeCategoryTable  -> your employee category master table
        - EmployeeCategoryId     -> PK column
        - EmployeeCategoryName   -> name/description column
        - IsActive               -> optional active flag
    */

    SELECT
        ec.Id   AS Id,
        ec.CategoryName AS [Name]
    FROM
        dbo.EmpCategoryMaster ec
     WHERE ec.IsActive = 1   -- uncomment if you have an active flag
    ORDER BY
        ec.CategoryName;
END;
GO
/****** Object:  StoredProcedure [dbo].[EmployeeType_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[EmployeeType_GetAll]
    @SchoolId uniqueidentifier = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,       -- or alias EmployeeTypeId AS Id
        TypeName as Name      -- or alias EmployeeTypeName/TypeName AS Name
    FROM dbo.EmpTypeMaster WITH (NOLOCK)
    WHERE (IsActive = 1 OR IsActive IS NULL)
      AND (IsDeleted = 0 OR IsDeleted IS NULL)
      AND (@SchoolId IS NULL OR SchoolId = @SchoolId)
    ORDER BY Name;
END
GO
/****** Object:  StoredProcedure [dbo].[EmpTypeMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EmpTypeMaster_Create]
    @TypeName VARCHAR(50),
    @Description VARCHAR(150),
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[EmpTypeMaster]
    (
        Id,
        TypeName,
        Description,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @TypeName,
        @Description,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        GETUTCDATE(),
        'INC',
        'In Process....'
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[EmpTypeMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EmpTypeMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[EmpTypeMaster]
    SET IsDeleted = 1,
        IsActive = 0
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[EmpTypeMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EmpTypeMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        TypeName,
        Description,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[EmpTypeMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[EmpTypeMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EmpTypeMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        TypeName,
        Description,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[EmpTypeMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[EmpTypeMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EmpTypeMaster_Update]
    @Id UNIQUEIDENTIFIER,
    @TypeName VARCHAR(50),
    @Description VARCHAR(150),
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[EmpTypeMaster]
    SET 
        TypeName = @TypeName,
        Description = @Description,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = GETUTCDATE()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[FeesCategoryMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[FeesCategoryMaster_Create]
    @FeesCatgoryName NVARCHAR(100),
    @Description     NVARCHAR(500) = NULL,
    @IsActive        BIT,
    @CreatedBy       UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO dbo.FeesCategoryMaster
    (
        Id,
        FeesCatgoryName,
        Description,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        Status,
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @FeesCatgoryName,
        ISNULL(@Description, ''),
        @IsActive,
        0,
        @CreatedBy,
        GETUTCDATE(),
        'INC',
        'In Process....'
    );

    -- Service expects a table with column 'Id'
    SELECT @NewId AS Id;
END
GO
/****** Object:  StoredProcedure [dbo].[FeesCategoryMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[FeesCategoryMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.FeesCategoryMaster
    SET
        IsDeleted   = 1,
        ModifiedDate = GETUTCDATE()
    WHERE Id = @Id
      AND IsDeleted = 0;

    IF @@ROWCOUNT = 1
        RETURN 1;
    ELSE
        RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[FeesCategoryMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[FeesCategoryMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        FeesCatgoryName,
        Description,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM dbo.FeesCategoryMaster
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[FeesCategoryMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[FeesCategoryMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        FeesCatgoryName,
        Description,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM dbo.FeesCategoryMaster
    WHERE Id = @Id
      AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[FeesCategoryMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[FeesCategoryMaster_Update]
    @Id              UNIQUEIDENTIFIER,
    @FeesCatgoryName NVARCHAR(100),
    @Description     NVARCHAR(500) = NULL,
    @IsActive        BIT,
    @ModifiedBy      UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.FeesCategoryMaster
    SET
        FeesCatgoryName = @FeesCatgoryName,
        Description     = ISNULL(@Description, ''),
        IsActive        = @IsActive,
        ModifiedBy      = @ModifiedBy,
        ModifiedDate    = GETUTCDATE()
    WHERE Id = @Id
      AND IsDeleted = 0;

    IF @@ROWCOUNT = 1
        RETURN 1;
    ELSE
        RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Gender_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Gender_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id,
        Gender 
    FROM 
        GenderMaster
    WHERE 
        IsActive = 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllUserDetails]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[GetAllUserDetails]
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Get user's authentication data
      SELECT   distinct UserDetails.Id, UserDetails.UserName, UserDetails.UserPassword, UserDetails.FirstName, UserDetails.LastName, UserDetails.EmailAddress, UserDetails.DesignationId, 
            DesigMaster.Name as DesignationName, 
                         UserDetails.UserRoleId, RoleMaster.Name AS RoleName, UserDetails.IsSuperUser, UserDetails.CompanyId, CompanyMaster.CompanyName, UserDetails.SchoolId, 
                         SchoolMaster.Name AS SchoolName, 
                         UserDetails.IsActive, UserDetails.IsDeleted, UserDetails.Status
FROM            UserDetails  LEFT JOIN
                         CityMaster ON UserDetails.Id = CityMaster.CreatedBy AND UserDetails.Id = CityMaster.ModifiedBy  LEFT JOIN
                         CompanyMaster ON UserDetails.CompanyId = CompanyMaster.Id LEFT JOIN
                         DeptMaster ON UserDetails.Id = DeptMaster.CreatedBy AND UserDetails.Id = DeptMaster.ModifiedBy AND CompanyMaster.Id = DeptMaster.CompanyId  LEFT JOIN
                         DesigMaster ON UserDetails.DesignationId = DesigMaster.Id AND UserDetails.Id = DesigMaster.CreatedBy AND UserDetails.Id = DesigMaster.ModifiedBy AND 
                         CompanyMaster.Id = DesigMaster.CompanyId  LEFT JOIN
                         RoleMaster ON UserDetails.UserRoleId = RoleMaster.Id AND UserDetails.Id = RoleMaster.CreatedBy AND UserDetails.Id = RoleMaster.ModifiedBy AND 
                         CompanyMaster.Id = RoleMaster.CompanyId  LEFT JOIN
                         SchoolMaster ON UserDetails.SchoolId = SchoolMaster.Id 
END
GO
/****** Object:  StoredProcedure [dbo].[GetSundaysbyyearandmonth]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--GetSundaysbyyearandmonth 2013,10
CREATE PROCEDURE [dbo].[GetSundaysbyyearandmonth] --2010,8
--2010 is year and 8 is month
@year varchar(10),
@month varchar(10)
as
begin
declare @date varchar(20)
--@first day is used to find the first day name in a month
declare @firstday varchar(50)
declare @start int
declare @dtLastDate datetime
Declare @totaldays int
---Calculate the first date of processing month
		Set @date =ltrim(str(@month))+'/'+'1/'++ltrim(str(@year))
	--Calculate last date of processing month
		Set @dtLastDate =ltrim(str(@month))+'/'+'1/'++ltrim(str(@year))

		Set @dtLastDate= DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@dtLastDate)+1,0))
--Select dateadd(mm,datediff(mm,-1,@year),-1)
set @totaldays=day(@dtLastDate)
set @firstday = datename(dw,@date)


--print @firstday
--SELECT datepart(dd,(getdate()));

Create table #TempSundays
(
	DayID int
)

set @start=
case @firstday when 'Monday' then 7
when 'Tuesday' then 6
when 'Wednesday'then 5
when 'Thursday'then 4
when 'Friday' then 3
when 'Saturday'then 2
else 1
end
while(@start<=@totaldays)
begin
Insert Into #TempSundays
(
	DayID
)
Select @start
set @start=@start+7
end
Select * from #TempSundays
Drop table #TempSundays
end



GO
/****** Object:  StoredProcedure [dbo].[GetUserFullName]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create  PROCEDURE [dbo].[GetUserFullName]
    @userName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Get user's authentication data
    SELECT isnull(FirstName,'') + ' ' + isnull(LastName,'')        
    FROM UserDetails 
    WHERE UserName = @userName 
        AND IsActive = 1
        AND IsDeleted = 0
    
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserNameById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create  PROCEDURE [dbo].[GetUserNameById]
    @Id as Uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;
    SELECT   distinct UserDetails.Id, UserDetails.UserName, UserDetails.UserPassword, UserDetails.FirstName, UserDetails.LastName, UserDetails.EmailAddress, UserDetails.DesignationId, 
            DesigMaster.Name as DesignationName, 
                         UserDetails.UserRoleId, RoleMaster.Name AS RoleName, UserDetails.IsSuperUser, UserDetails.CompanyId, CompanyMaster.CompanyName, UserDetails.SchoolId, 
                         SchoolMaster.Name AS SchoolName, 
                         UserDetails.IsActive, UserDetails.IsDeleted, UserDetails.Status
FROM            UserDetails  LEFT JOIN
                         CityMaster ON UserDetails.Id = CityMaster.CreatedBy AND UserDetails.Id = CityMaster.ModifiedBy  LEFT JOIN
                         CompanyMaster ON UserDetails.CompanyId = CompanyMaster.Id LEFT JOIN
                         DeptMaster ON UserDetails.Id = DeptMaster.CreatedBy AND UserDetails.Id = DeptMaster.ModifiedBy AND CompanyMaster.Id = DeptMaster.CompanyId  LEFT JOIN
                         DesigMaster ON UserDetails.DesignationId = DesigMaster.Id AND UserDetails.Id = DesigMaster.CreatedBy AND UserDetails.Id = DesigMaster.ModifiedBy AND 
                         CompanyMaster.Id = DesigMaster.CompanyId  LEFT JOIN
                         RoleMaster ON UserDetails.UserRoleId = RoleMaster.Id AND UserDetails.Id = RoleMaster.CreatedBy AND UserDetails.Id = RoleMaster.ModifiedBy AND 
                         CompanyMaster.Id = RoleMaster.CompanyId  LEFT JOIN
                         SchoolMaster ON UserDetails.SchoolId = SchoolMaster.Id 
    where UserDetails.Id = @Id
    
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserPrivileges]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[GetUserPrivileges]
    @userName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Get user's authentication data
   select Privileges.Id, PrivilegeName from RolePrivileges join RoleMaster on RolePrivileges.RoleId = RoleMaster.Id
   join Privileges on RolePrivileges.PrivilegeId = Privileges.Id
   join UserDetails on RoleMaster.Id = UserDetails.UserRoleId
   where UserDetails.UserName = @userName
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserRoleName]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[GetUserRoleName]
    @userName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Get user's authentication data
   select Name 
   from RoleMaster join UserDetails on UserDetails.UserRoleId = RoleMaster.Id 
   where UserName = @userName
END
GO
/****** Object:  StoredProcedure [dbo].[Grade_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Grade_GetAll]
    @SchoolId uniqueidentifier = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,      -- or alias GradeId AS Id
        GradeName as Name     -- or alias GradeName AS Name
    FROM dbo.GradeMaster WITH (NOLOCK)
    WHERE (IsActive = 1 OR IsActive IS NULL)
      AND (IsDeleted = 0 OR IsDeleted IS NULL)
      AND (@SchoolId IS NULL OR SchoolId = @SchoolId)
    ORDER BY Name;
END
GO
/****** Object:  StoredProcedure [dbo].[HolidayMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[HolidayMaster_Create]
    @Name NVARCHAR(150),
    @Description NVARCHAR(250),
    @TypeId UNIQUEIDENTIFIER,
    @FromDate DATETIME,
    @ToDate DATETIME,
    @Year UNIQUEIDENTIFIER,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsStaffApplicable BIT,
    @SessionId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[HolidayMaster]
    (
        Id,
        [Name],
        [Description],
        TypeId,
        FromDate,
        ToDate,
        [Year],
        CompanyId,
        SchoolId,
        IsStaffApplicable,
        SessionId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @Name,
        @Description,
        @TypeId,
        @FromDate,
        @ToDate,
        @Year,
        @CompanyId,
        @SchoolId,
        @IsStaffApplicable,
        @SessionId,
        @IsActive,
        0,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[HolidayMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[HolidayMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[HolidayMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[HolidayMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[HolidayMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        [Name],
        [Description],
        TypeId,
        FromDate,
        ToDate,
        [Year],
        CompanyId,
        SchoolId,
        IsStaffApplicable,
        SessionId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[HolidayMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[HolidayMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[HolidayMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        [Name],
        [Description],
        TypeId,
        FromDate,
        ToDate,
        [Year],
        CompanyId,
        SchoolId,
        IsStaffApplicable,
        SessionId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[HolidayMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[HolidayMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[HolidayMaster_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(150),
    @Description NVARCHAR(250),
    @TypeId UNIQUEIDENTIFIER,
    @FromDate DATETIME,
    @ToDate DATETIME,
    @Year UNIQUEIDENTIFIER,
    @IsStaffApplicable BIT,
    @SessionId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[HolidayMaster]
    SET 
        [Name] = @Name,
        [Description] = @Description,
        TypeId = @TypeId,
        FromDate = @FromDate,
        ToDate = @ToDate,
        [Year] = @Year,
        IsStaffApplicable = @IsStaffApplicable,
        SessionId = @SessionId,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[HolidayTypeMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[HolidayTypeMaster_Create]
    @HolidayTypeName NVARCHAR(200),
    @HolidayTypeDescription NVARCHAR(500),
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[HolidayTypeMaster]
    (
        Id,
        HolidayTypeName,
        HolidayTypeDescription,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @HolidayTypeName,
        @HolidayTypeDescription,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[HolidayTypeMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[HolidayTypeMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[HolidayTypeMaster]
    SET IsDeleted = 1,
        IsActive = 0
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[HolidayTypeMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[HolidayTypeMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        HolidayTypeName,
        HolidayTypeDescription,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[HolidayTypeMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[HolidayTypeMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[HolidayTypeMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        HolidayTypeName,
        HolidayTypeDescription,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[HolidayTypeMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[HolidayTypeMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[HolidayTypeMaster_Update]
    @Id UNIQUEIDENTIFIER,
    @HolidayTypeName NVARCHAR(200),
    @HolidayTypeDescription NVARCHAR(500),
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[HolidayTypeMaster]
    SET 
        HolidayTypeName = @HolidayTypeName,
        HolidayTypeDescription = @HolidayTypeDescription,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[IsUserExist]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create  PROCEDURE [dbo].[IsUserExist]
    @userName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Get user's authentication data
   select count(*) 
   from UserDetails 
   where UserName = @userName
END
GO
/****** Object:  StoredProcedure [dbo].[MaritalStatus_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[MaritalStatus_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,      -- or alias MaritalStatusId AS Id
        Value as  Name     -- or alias MaritalStatusName/StatusName AS Name
    FROM dbo.MaritalStatus WITH (NOLOCK)
    WHERE (IsActive = 1 OR IsActive IS NULL)
      AND (IsDeleted = 0 OR IsDeleted IS NULL)
    ORDER BY Name;
END
GO
/****** Object:  StoredProcedure [dbo].[Parent_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Parent_Create]
    @StudentGUID uniqueidentifier,
    @ParentFirstName varchar(50),
    @ParentLastName varchar(50) = '',
    @ParentDOB datetime = NULL,
    @QualificationId uniqueidentifier,
    @Occupation varchar(100) = '',
    @AnnualIncome decimal(18,2) = NULL,
    @DesignationId uniqueidentifier,
    @Phone varchar(50) = '',
    @Mobile varchar(50) = '',
    @Email varchar(100) = '',
    @Address1 varchar(250) = '',
    @Address2 varchar(250) = '',
    @CityId uniqueidentifier,
    @StateId uniqueidentifier,
    @CountryId uniqueidentifier,
    @ZipCode varchar(50) = '',
    @OfficeAddress1 varchar(250) = '',
    @OfficeAddress2 varchar(250) = '',
    @OfficeCityId uniqueidentifier,
    @OfficeStateId uniqueidentifier,
    @OfficeCountryId uniqueidentifier,
    @OfficeZipCode varchar(50) = '',
    @OfficePhone varchar(50) = '',
    @Image varchar(255) = '',
    @RelationTypeId uniqueidentifier,
    @SchoolId uniqueidentifier,
    @CompanyId uniqueidentifier,
    @IsActive bit = 1,
    @IsDeleted bit = 0,
    @CreatedBy uniqueidentifier,
    @CreatedDate datetime = NULL,
    @Status varchar(10) = 'INC',
    @StatusMessage nvarchar(255) = N'In Process....'
AS
BEGIN
    SET NOCOUNT ON;
    IF @CreatedDate IS NULL SET @CreatedDate = GETUTCDATE();

    DECLARE @NewId uniqueidentifier = NEWID();
    INSERT INTO dbo.ParentMaster
    (
        Id, StudentGUID, ParentFirstName, ParentLastName, ParentDOB,
        QualificationId, Occupation, AnnualIncome, DesignationId,
        Phone, Mobile, Email,
        Address1, Address2, CityId, StateId, CountryId, ZipCode,
        OfficeAddress1, OfficeAddress2, OfficeCityId, OfficeStateId, OfficeCountryId, OfficeZipCode, OfficePhone,
        Image, RelationTypeId, SchoolId, CompanyId, IsActive, IsDeleted, CreatedBy, CreatedDate, Status, StatusMessage
    )
    VALUES
    (
        @NewId, @StudentGUID, @ParentFirstName, @ParentLastName, @ParentDOB,
        @QualificationId, @Occupation, @AnnualIncome, @DesignationId,
        @Phone, @Mobile, @Email,
        @Address1, @Address2, @CityId, @StateId, @CountryId, @ZipCode,
        @OfficeAddress1, @OfficeAddress2, @OfficeCityId, @OfficeStateId, @OfficeCountryId, @OfficeZipCode, @OfficePhone,
        @Image, @RelationTypeId, @SchoolId, @CompanyId, @IsActive, @IsDeleted, @CreatedBy, @CreatedDate, @Status, @StatusMessage
    );

    SELECT @NewId AS Id;
END
GO
/****** Object:  StoredProcedure [dbo].[Parent_GetByStudentId]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Parent_GetByStudentId]
    @StudentGUID UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        Id,
        StudentGUID,
        ParentFirstName,
        ParentLastName,
        ParentDOB,
        QualificationId,
        Occupation,
        AnnualIncome,
        DesignationId,
        Phone,
        Mobile,
        Email,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        RelationTypeId,
        SchoolId,
        CompanyId,
        IsActive,
        IsDeleted
    FROM 
        dbo.ParentMaster
    WHERE 
        StudentGUID = @StudentGUID
        AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[PaymentMode_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PaymentMode_GetAll]
    @SchoolId UNIQUEIDENTIFIER = NULL    -- optional, because your C# sometimes does not pass it
AS
BEGIN
    SET NOCOUNT ON;

    /*
        TODO: Replace:
        - PaymentModeTable       with your real payment mode table name
        - PaymentModeId          with the PK column
        - PaymentModeName        with the name/description column
        - SchoolId               with the FK to school (if you have one)
    */

    SELECT
        pm.Id   AS Id,
        pm.Name AS [Name]
    FROM
        dbo.PaymentModeMaster pm
    WHERE
        (@SchoolId IS NULL OR pm.SchoolId = @SchoolId)
    ORDER BY
        pm.Name;
END;
GO
/****** Object:  StoredProcedure [dbo].[ProfessionMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Create profession
-- Used by: ProfessionMasterService.Create(ProfessionMaster profession)
-- Expects: a resultset with first row containing column [Id] = new Id
-- =============================================
CREATE   PROCEDURE [dbo].[ProfessionMaster_Create]
    @Name       NVARCHAR(100),
    @CompanyId  UNIQUEIDENTIFIER,
    @SchoolId   UNIQUEIDENTIFIER,
    @IsActive   BIT,
    @CreatedBy  UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO ProfessionMaster
    (
        Id,
        Name,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @Name,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,                           -- IsDeleted
        @CreatedBy,
        GETUTCDATE(),                -- CreatedDate
        NULL,                        -- ModifiedBy
        NULL,                        -- ModifiedDate
        'INC',                       -- Status (default from entity)
        'In Process....'             -- StatusMessage (default from entity)
    );

    -- Return new Id as first row/column, as expected by the service
    SELECT Id = @NewId;
END;
GO
/****** Object:  StoredProcedure [dbo].[ProfessionMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Delete profession (soft delete)
-- Used by: ProfessionMasterService.Delete(Guid id)
-- Expects: RETURN_VALUE = 1 on success, 0 on failure
-- =============================================
CREATE   PROCEDURE [dbo].[ProfessionMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @RowCount INT;

    UPDATE ProfessionMaster
    SET
        IsDeleted     = 1,
        ModifiedDate  = GETUTCDATE(),
        Status        = 'DEL',
        StatusMessage = 'Deleted'
    WHERE Id = @Id
      AND ISNULL(IsDeleted, 0) = 0;

    SET @RowCount = @@ROWCOUNT;

    IF @RowCount = 1
        RETURN 1;
    ELSE
        RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[ProfessionMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Get all professions (non-deleted)
-- Used by: ProfessionMasterService.GetAll()
-- =============================================
CREATE   PROCEDURE [dbo].[ProfessionMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM ProfessionMaster
    WHERE ISNULL(IsDeleted, 0) = 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[ProfessionMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Get profession by Id
-- Used by: ProfessionMasterService.GetById(Guid id)
-- =============================================
CREATE   PROCEDURE [dbo].[ProfessionMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM ProfessionMaster
    WHERE Id = @Id
      AND ISNULL(IsDeleted, 0) = 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[ProfessionMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Update profession
-- Used by: ProfessionMasterService.Update(ProfessionMaster profession)
-- Expects: RETURN_VALUE = 1 on success, 0 on failure
-- =============================================
CREATE   PROCEDURE [dbo].[ProfessionMaster_Update]
    @Id          UNIQUEIDENTIFIER,
    @Name        NVARCHAR(100),
    @SchoolId    UNIQUEIDENTIFIER,
    @IsActive    BIT,
    @ModifiedBy  UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @RowCount INT;

    UPDATE ProfessionMaster
    SET
        Name        = @Name,
        SchoolId    = @SchoolId,
        IsActive    = @IsActive,
        ModifiedBy  = @ModifiedBy,
        ModifiedDate= GETUTCDATE()
    WHERE Id = @Id
      AND ISNULL(IsDeleted, 0) = 0;

    SET @RowCount = @@ROWCOUNT;

    IF @RowCount = 1
        RETURN 1;   -- success
    ELSE
        RETURN 0;   -- not found / failed
END;
GO
/****** Object:  StoredProcedure [dbo].[Qualification_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Qualification_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT q.[Id], q.[QualificationName]
    FROM dbo.[QualificationMaster] q
    ORDER BY q.[QualificationName];
END
GO
/****** Object:  StoredProcedure [dbo].[QualificationMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[QualificationMaster_Create]
    @QualificationName       NVARCHAR(200),
    @IsTeachingQualification BIT,
    @IsActive                BIT,
    @CreatedBy               UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO dbo.QualificationMaster
    (
        Id,
        QualificationName,
        IsTeachingQualification,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @QualificationName,
        @IsTeachingQualification,
        @IsActive,
        0,                   -- IsDeleted
        @CreatedBy,
        SYSUTCDATETIME(),    -- CreatedDate
        NULL,                -- ModifiedBy
        NULL,                -- ModifiedDate
        NULL,                -- Status
        NULL                 -- StatusMessage
    );

    -- Return the new Id in a result set (as you do in Company_Create)
    SELECT @NewId AS Id;
    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[QualificationMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[QualificationMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1
        FROM dbo.QualificationMaster WITH (NOLOCK)
        WHERE Id = @Id AND IsDeleted = 0
    )
    BEGIN
        RETURN 0;
    END

    UPDATE dbo.QualificationMaster
    SET
        IsDeleted    = 1,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[QualificationMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[QualificationMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        QualificationName,
        IsTeachingQualification,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM dbo.QualificationMaster WITH (NOLOCK)
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[QualificationMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[QualificationMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        QualificationName,
        IsTeachingQualification,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM dbo.QualificationMaster WITH (NOLOCK)
    WHERE Id = @Id
      AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[QualificationMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[QualificationMaster_Update]
    @Id                      UNIQUEIDENTIFIER,
    @QualificationName       NVARCHAR(200),
    @IsTeachingQualification BIT,
    @IsActive                BIT,
    @ModifiedBy              UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1
        FROM dbo.QualificationMaster WITH (NOLOCK)
        WHERE Id = @Id AND IsDeleted = 0
    )
    BEGIN
        RETURN 0;
    END

    UPDATE dbo.QualificationMaster
    SET
        QualificationName       = @QualificationName,
        IsTeachingQualification = @IsTeachingQualification,
        IsActive                = @IsActive,
        ModifiedBy              = @ModifiedBy,
        ModifiedDate            = SYSUTCDATETIME()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[RelationType_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RelationType_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        r.[Id],
        r.[Name]
    FROM dbo.[RelationTypeMaster] r
    ORDER BY r.[Name];
END
GO
/****** Object:  StoredProcedure [dbo].[Religion_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Religion_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, ReligionName as Name
    FROM dbo.ReligionMaster
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Report_ExportEmployeeLeaves]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Report_ExportEmployeeLeaves]
    @SearchTerm NVARCHAR(100) = NULL,
    @Department NVARCHAR(100) = NULL,
    @LeaveType NVARCHAR(100) = NULL,
    @Status NVARCHAR(50) = NULL,
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        e.EmployeeId,
        e.FullName AS EmployeeName,
        e.Department,
        lt.Name AS LeaveType,
        la.FromDate,
        la.ToDate,
        la.Status,
        la.Reason,
        la.ApprovedDate,
        u.FullName AS ApprovedBy
    FROM 
        LeaveApplications la
        INNER JOIN Employees e ON la.EmployeeId = e.Id
        INNER JOIN LeaveTypes lt ON la.LeaveTypeId = lt.Id
        LEFT JOIN AspNetUsers u ON la.ApprovedBy = u.Id
    WHERE 
        (@SearchTerm IS NULL OR 
         e.FullName LIKE '%' + @SearchTerm + '%' OR
         e.Department LIKE '%' + @SearchTerm + '%' OR
         lt.Name LIKE '%' + @SearchTerm + '%' OR
         la.Status LIKE '%' + @SearchTerm + '%' OR
         la.Reason LIKE '%' + @SearchTerm + '%')
        AND (@Department IS NULL OR e.Department = @Department)
        AND (@LeaveType IS NULL OR lt.Name = @LeaveType)
        AND (@Status IS NULL OR la.Status = @Status)
        AND (@StartDate IS NULL OR la.FromDate >= @StartDate)
        AND (@EndDate IS NULL OR la.ToDate <= @EndDate)
    ORDER BY
        la.FromDate DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[Report_GetEmployeeLeaves]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Report_GetEmployeeLeaves
CREATE   PROCEDURE [dbo].[Report_GetEmployeeLeaves]
    @PageNumber INT = 1,
    @PageSize INT = 10,
    @SearchTerm NVARCHAR(100) = NULL,
    @SortColumn NVARCHAR(50) = 'FromDate',
    @SortDirection NVARCHAR(4) = 'DESC',
    @Department NVARCHAR(100) = NULL,
    @LeaveType NVARCHAR(100) = NULL,
    @Status NVARCHAR(50) = NULL,
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;
    
    -- Main query with pagination
    SELECT 
        la.Id,
        la.EmployeeId,
        e.FullName AS EmployeeName,
        e.Department,
        lt.Name AS LeaveType,
        la.FromDate,
        la.ToDate,
        DATEDIFF(day, la.FromDate, la.ToDate) + 1 AS Days,
        la.Status,
        la.Reason,
        la.ApprovedDate,
        u.FullName AS ApprovedBy
    FROM 
        LeaveApplications la
        INNER JOIN Employees e ON la.EmployeeId = e.Id
        INNER JOIN LeaveTypes lt ON la.LeaveTypeId = lt.Id
        LEFT JOIN AspNetUsers u ON la.ApprovedBy = u.Id
    WHERE 
        (@SearchTerm IS NULL OR 
         e.FullName LIKE '%' + @SearchTerm + '%' OR
         e.Department LIKE '%' + @SearchTerm + '%' OR
         lt.Name LIKE '%' + @SearchTerm + '%' OR
         la.Status LIKE '%' + @SearchTerm + '%' OR
         la.Reason LIKE '%' + @SearchTerm + '%')
        AND (@Department IS NULL OR e.Department = @Department)
        AND (@LeaveType IS NULL OR lt.Name = @LeaveType)
        AND (@Status IS NULL OR la.Status = @Status)
        AND (@StartDate IS NULL OR la.FromDate >= @StartDate)
        AND (@EndDate IS NULL OR la.ToDate <= @EndDate)
    ORDER BY
        CASE WHEN @SortDirection = 'ASC' AND @SortColumn = 'EmployeeName' THEN e.FullName END ASC,
        CASE WHEN @SortDirection = 'DESC' AND @SortColumn = 'EmployeeName' THEN e.FullName END DESC,
        CASE WHEN @SortDirection = 'ASC' AND @SortColumn = 'Department' THEN e.Department END ASC,
        CASE WHEN @SortDirection = 'DESC' AND @SortColumn = 'Department' THEN e.Department END DESC,
        -- Add more sort columns as needed
        CASE WHEN @SortDirection = 'ASC' AND @SortColumn = 'FromDate' THEN la.FromDate END ASC,
        CASE WHEN @SortDirection = 'DESC' AND @SortColumn = 'FromDate' THEN la.FromDate END DESC
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;
    
    -- Get total count
    SELECT COUNT(*)
    FROM 
        LeaveApplications la
        INNER JOIN Employees e ON la.EmployeeId = e.Id
        INNER JOIN LeaveTypes lt ON la.LeaveTypeId = lt.Id
    WHERE 
        (@SearchTerm IS NULL OR 
         e.FullName LIKE '%' + @SearchTerm + '%' OR
         e.Department LIKE '%' + @SearchTerm + '%' OR
         lt.Name LIKE '%' + @SearchTerm + '%' OR
         la.Status LIKE '%' + @SearchTerm + '%' OR
         la.Reason LIKE '%' + @SearchTerm + '%')
        AND (@Department IS NULL OR e.Department = @Department)
        AND (@LeaveType IS NULL OR lt.Name = @LeaveType)
        AND (@Status IS NULL OR la.Status = @Status)
        AND (@StartDate IS NULL OR la.FromDate >= @StartDate)
        AND (@EndDate IS NULL OR la.ToDate <= @EndDate);
END
GO
/****** Object:  StoredProcedure [dbo].[Report_GetFeeCollection]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Report_GetFeeCollection]
    @PageNumber INT = 1,
    @PageSize INT = 10,
    @SearchTerm NVARCHAR(100) = NULL,
    @SortColumn NVARCHAR(50) = 'PaymentDate',
    @SortDirection NVARCHAR(4) = 'DESC',
    @ClassId INT = NULL,
    @FeeTypeId INT = NULL,
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
    @Status NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;
    
    -- Main query with pagination
    SELECT 
        fp.Id,
        fp.ReceiptNo,
        fp.PaymentDate,
        s.Id AS StudentId,
        s.FirstName + ' ' + s.LastName AS StudentName,
        c.Name AS ClassName,
        ft.Name AS FeeType,
        fp.Amount,
        fp.Discount,
        fp.Fine,
        fp.PaidAmount,
        fp.PaymentMode,
        fp.Status,
        u.FullName AS CollectedBy
    FROM 
        FeePayments fp
        INNER JOIN Students s ON fp.StudentId = s.Id
        INNER JOIN Classes c ON s.ClassId = c.Id
        INNER JOIN FeeTypes ft ON fp.FeeTypeId = ft.Id
        INNER JOIN AspNetUsers u ON fp.CollectedBy = u.Id
    WHERE 
        (@SearchTerm IS NULL OR 
         fp.ReceiptNo LIKE '%' + @SearchTerm + '%' OR
         s.FirstName + ' ' + s.LastName LIKE '%' + @SearchTerm + '%' OR
         c.Name LIKE '%' + @SearchTerm + '%' OR
         ft.Name LIKE '%' + @SearchTerm + '%')
        AND (@ClassId IS NULL OR s.ClassId = @ClassId)
        AND (@FeeTypeId IS NULL OR fp.FeeTypeId = @FeeTypeId)
        AND (@StartDate IS NULL OR fp.PaymentDate >= @StartDate)
        AND (@EndDate IS NULL OR fp.PaymentDate <= @EndDate)
        AND (@Status IS NULL OR fp.Status = @Status)
    ORDER BY
        CASE WHEN @SortDirection = 'ASC' AND @SortColumn = 'PaymentDate' THEN fp.PaymentDate END ASC,
        CASE WHEN @SortDirection = 'DESC' AND @SortColumn = 'PaymentDate' THEN fp.PaymentDate END DESC,
        CASE WHEN @SortDirection = 'ASC' AND @SortColumn = 'StudentName' THEN s.FirstName + ' ' + s.LastName END ASC,
        CASE WHEN @SortDirection = 'DESC' AND @SortColumn = 'StudentName' THEN s.FirstName + ' ' + s.LastName END DESC
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;
    
    -- Summary data
    SELECT 
        SUM(fp.Amount) AS TotalFees,
        SUM(fp.Discount) AS TotalDiscount,
        SUM(fp.Fine) AS TotalFine,
        SUM(fp.PaidAmount) AS TotalCollection
    FROM 
        FeePayments fp
        INNER JOIN Students s ON fp.StudentId = s.Id
    WHERE 
        (@SearchTerm IS NULL OR 
         fp.ReceiptNo LIKE '%' + @SearchTerm + '%' OR
         s.FirstName + ' ' + s.LastName LIKE '%' + @SearchTerm + '%')
        AND (@ClassId IS NULL OR s.ClassId = @ClassId)
        AND (@FeeTypeId IS NULL OR fp.FeeTypeId = @FeeTypeId)
        AND (@StartDate IS NULL OR fp.PaymentDate >= @StartDate)
        AND (@EndDate IS NULL OR fp.PaymentDate <= @EndDate)
        AND (@Status IS NULL OR fp.Status = @Status);
    
    -- Total count
    SELECT COUNT(*)
    FROM 
        FeePayments fp
        INNER JOIN Students s ON fp.StudentId = s.Id
    WHERE 
        (@SearchTerm IS NULL OR 
         fp.ReceiptNo LIKE '%' + @SearchTerm + '%' OR
         s.FirstName + ' ' + s.LastName LIKE '%' + @SearchTerm + '%')
        AND (@ClassId IS NULL OR s.ClassId = @ClassId)
        AND (@FeeTypeId IS NULL OR fp.FeeTypeId = @FeeTypeId)
        AND (@StartDate IS NULL OR fp.PaymentDate >= @StartDate)
        AND (@EndDate IS NULL OR fp.PaymentDate <= @EndDate)
        AND (@Status IS NULL OR fp.Status = @Status);
END
GO
/****** Object:  StoredProcedure [dbo].[Report_GetInventoryItems]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Report_GetInventoryItems]
    @PageNumber INT = 1,
    @PageSize INT = 10,
    @SearchTerm NVARCHAR(100) = NULL,
    @SortColumn NVARCHAR(50) = 'ItemName',
    @SortDirection NVARCHAR(4) = 'ASC',
    @CategoryId INT = NULL,
    @SupplierId INT = NULL,
    @Status NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;
    
    -- Main query with pagination
    SELECT 
        i.Id,
        i.ItemCode,
        i.ItemName,
        i.CategoryId,
        c.Name AS CategoryName,
        i.SupplierId,
        s.Name AS SupplierName,
        i.Unit,
        i.PurchasePrice,
        i.SellingPrice,
        i.QuantityInStock,
        i.MinStockLevel,
        i.Description,
        CASE 
            WHEN i.QuantityInStock <= 0 THEN 'Out of Stock'
            WHEN i.QuantityInStock <= i.MinStockLevel THEN 'Low Stock'
            ELSE 'In Stock'
        END AS StockStatus
    FROM 
        InventoryItems i
        LEFT JOIN Categories c ON i.CategoryId = c.Id
        LEFT JOIN Suppliers s ON i.SupplierId = s.Id
    WHERE 
        (@SearchTerm IS NULL OR 
         i.ItemCode LIKE '%' + @SearchTerm + '%' OR
         i.ItemName LIKE '%' + @SearchTerm + '%' OR
         i.Description LIKE '%' + @SearchTerm + '%')
        AND (@CategoryId IS NULL OR i.CategoryId = @CategoryId)
        AND (@SupplierId IS NULL OR i.SupplierId = @SupplierId)
        AND (
            @Status IS NULL OR 
            (@Status = 'in_stock' AND i.QuantityInStock > 0) OR
            (@Status = 'low_stock' AND i.QuantityInStock > 0 AND i.QuantityInStock <= i.MinStockLevel) OR
            (@Status = 'out_of_stock' AND i.QuantityInStock <= 0)
        )
    ORDER BY
        CASE WHEN @SortDirection = 'ASC' AND @SortColumn = 'ItemName' THEN i.ItemName END ASC,
        CASE WHEN @SortDirection = 'DESC' AND @SortColumn = 'ItemName' THEN i.ItemName END DESC,
        CASE WHEN @SortDirection = 'ASC' AND @SortColumn = 'ItemCode' THEN i.ItemCode END ASC,
        CASE WHEN @SortDirection = 'DESC' AND @SortColumn = 'ItemCode' THEN i.ItemCode END DESC
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;
    
    -- Total count
    SELECT COUNT(*)
    FROM 
        InventoryItems i
    WHERE 
        (@SearchTerm IS NULL OR 
         i.ItemCode LIKE '%' + @SearchTerm + '%' OR
         i.ItemName LIKE '%' + @SearchTerm + '%' OR
         i.Description LIKE '%' + @SearchTerm + '%')
        AND (@CategoryId IS NULL OR i.CategoryId = @CategoryId)
        AND (@SupplierId IS NULL OR i.SupplierId = @SupplierId)
        AND (
            @Status IS NULL OR 
            (@Status = 'in_stock' AND i.QuantityInStock > 0) OR
            (@Status = 'low_stock' AND i.QuantityInStock > 0 AND i.QuantityInStock <= i.MinStockLevel) OR
            (@Status = 'out_of_stock' AND i.QuantityInStock <= 0)
        );
END
GO
/****** Object:  StoredProcedure [dbo].[Report_GetItemStockMovement]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Report_GetItemStockMovement]
    @ItemId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        sm.TransactionDate,
        sm.TransactionType,
        sm.ReferenceNo,
        sm.QuantityIn,
        sm.QuantityOut,
        sm.Balance,
        sm.Notes
    FROM 
        StockMovements sm
    WHERE 
        sm.ItemId = @ItemId
    ORDER BY 
        sm.TransactionDate DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[Report_GetStudents]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Report_GetStudents]
    @PageNumber INT = 1,
    @PageSize INT = 10,
    @SearchTerm NVARCHAR(100) = NULL,
    @SortColumn NVARCHAR(50) = 'AdmissionNo',
    @SortDirection NVARCHAR(4) = 'ASC',
    @ClassId INT = NULL,
    @SectionId INT = NULL,
    @Status NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;
    
    -- Main query with pagination
    SELECT 
        s.Id,
        s.AdmissionNo,
        s.FirstName + ' ' + s.LastName AS FullName,
        c.Name AS ClassName,
        sec.Name AS SectionName,
        s.Gender,
        s.DateOfBirth,
        s.ContactNumber,
        s.Status,
        s.Address,
        s.FatherName,
        s.MotherName
    FROM 
        Students s
        INNER JOIN Classes c ON s.ClassId = c.Id
        INNER JOIN Sections sec ON s.SectionId = sec.Id
    WHERE 
        (@SearchTerm IS NULL OR 
         s.AdmissionNo LIKE '%' + @SearchTerm + '%' OR
         s.FirstName + ' ' + s.LastName LIKE '%' + @SearchTerm + '%' OR
         s.FatherName LIKE '%' + @SearchTerm + '%' OR
         s.MotherName LIKE '%' + @SearchTerm + '%' OR
         s.ContactNumber LIKE '%' + @SearchTerm + '%')
        AND (@ClassId IS NULL OR s.ClassId = @ClassId)
        AND (@SectionId IS NULL OR s.SectionId = @SectionId)
        AND (@Status IS NULL OR s.Status = @Status)
    ORDER BY
        CASE WHEN @SortDirection = 'ASC' AND @SortColumn = 'AdmissionNo' THEN s.AdmissionNo END ASC,
        CASE WHEN @SortDirection = 'DESC' AND @SortColumn = 'AdmissionNo' THEN s.AdmissionNo END DESC,
        CASE WHEN @SortDirection = 'ASC' AND @SortColumn = 'FullName' THEN s.FirstName + ' ' + s.LastName END ASC,
        CASE WHEN @SortDirection = 'DESC' AND @SortColumn = 'FullName' THEN s.FirstName + ' ' + s.LastName END DESC,
        -- Add more sort columns as needed
        s.AdmissionNo ASC
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;
    
    -- Get total count
    SELECT COUNT(*)
    FROM 
        Students s
    WHERE 
        (@SearchTerm IS NULL OR 
         s.AdmissionNo LIKE '%' + @SearchTerm + '%' OR
         s.FirstName + ' ' + s.LastName LIKE '%' + @SearchTerm + '%' OR
         s.FatherName LIKE '%' + @SearchTerm + '%' OR
         s.MotherName LIKE '%' + @SearchTerm + '%' OR
         s.ContactNumber LIKE '%' + @SearchTerm + '%')
        AND (@ClassId IS NULL OR s.ClassId = @ClassId)
        AND (@SectionId IS NULL OR s.SectionId = @SectionId)
        AND (@Status IS NULL OR s.Status = @Status);
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RoleMaster_Create]
    @Name NVARCHAR(100),
    @Description NVARCHAR(500) = NULL,
    @IsActive BIT = 1,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER,
    @CreatedDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Id UNIQUEIDENTIFIER = NEWID();
    DECLARE @IsDeleted BIT = 0;
    
    -- Check if role with same name already exists
    IF EXISTS (SELECT 1 FROM RoleMaster WHERE Name = @Name AND IsDeleted = 0)
    BEGIN
        -- Return empty result set to indicate failure
        SELECT CAST(NULL AS UNIQUEIDENTIFIER) AS Id;
        RETURN -1; -- Or appropriate error code
    END
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Insert the new role
        INSERT INTO RoleMaster (
            Id,
            Name,
            Description,
            IsActive,
            IsDeleted,
            CompanyId,
            SchoolId,
            CreatedBy,
            CreatedDate
        ) VALUES (
            @Id,
            @Name,
            @Description,
            @IsActive,
            @IsDeleted,
            @CompanyId,
            @SchoolId,
            @CreatedBy,
            @CreatedDate
        );
        
        -- Return the new role ID
        SELECT @Id AS Id;
        
        COMMIT TRANSACTION;
        RETURN 1; -- Success
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        -- Re-throw the error
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RoleMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Check if role exists and is not deleted
    IF NOT EXISTS (SELECT 1 FROM RoleMaster WHERE Id = @Id AND IsDeleted = 0)
    BEGIN
        RETURN 0; -- Not found or already deleted
    END
    
    BEGIN TRY
        -- Soft delete
        UPDATE 
            RoleMaster
        SET 
            IsDeleted = 1,
            ModifiedDate = GETUTCDATE()
        WHERE 
            Id = @Id;
            
        RETURN 1; -- Success
    END TRY
    BEGIN CATCH
        RETURN 0; -- Error
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RoleMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        Description,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[RoleMaster]
    WHERE IsDeleted = 0
    ORDER BY Name;
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RoleMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id,
        Name,
        Description,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate
    FROM 
        RoleMaster
    WHERE 
        Id = @Id
        AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RoleMaster_Update]
    @Id UNIQUEIDENTIFIER,
    @RoleName NVARCHAR(100),
    @Description NVARCHAR(500) = NULL,
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Check if role exists
    IF NOT EXISTS (SELECT 1 FROM RoleMaster WHERE Id = @Id AND IsDeleted = 0)
    BEGIN
        RETURN 0; -- Not found
    END
    
    -- Check if another role with the same name exists
    IF EXISTS (SELECT 1 FROM RoleMaster WHERE Id <> @Id AND Name = @RoleName AND IsDeleted = 0)
    BEGIN
        RETURN 0; -- Duplicate name
    END
    
    BEGIN TRY
        UPDATE 
            RoleMaster
        SET 
            Name = @RoleName,
            Description = @Description,
            IsActive = @IsActive,
            ModifiedBy = @ModifiedBy,
            ModifiedDate = GETUTCDATE()
        WHERE 
            Id = @Id;
            
        RETURN 1; -- Success
    END TRY
    BEGIN CATCH
        RETURN 0; -- Error
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[School_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[School_Create]
    @Name NVARCHAR(200),
    @Description NVARCHAR(MAX) = NULL,
    @Email NVARCHAR(256) = NULL,
    @Address1 NVARCHAR(500) = NULL,
    @Address2 NVARCHAR(500) = NULL,
    @CityId UNIQUEIDENTIFIER = NULL,
    @StateId UNIQUEIDENTIFIER = NULL,
    @CountryId UNIQUEIDENTIFIER = NULL,
    @ZipCode NVARCHAR(50) = NULL,
    @EstablishmentYear NVARCHAR(50) = NULL,
    @JudistrictionCityId UNIQUEIDENTIFIER = NULL,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO dbo.SchoolMaster
    (
        Id,
        Name,
        Description,
        Email,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        EstablishmentYear,
        JudistrictionCityId,
        IsActive,
        IsDeleted,
        CompanyId,
        CreatedBy,
        CreatedDate,
        Status,
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @Name,
        ISNULL(@Description, ''),
        ISNULL(@Email, ''),
        ISNULL(@Address1, ''),
        ISNULL(@Address2, ''),
        @CityId,
        @StateId,
        @CountryId,
        ISNULL(@ZipCode, ''),
        ISNULL(@EstablishmentYear, ''),
        @JudistrictionCityId,
        @IsActive,
        0,                                  -- IsDeleted
        @CompanyId,
        @CreatedBy,
        SYSUTCDATETIME(),
        '',                                 -- Status
        ''                                  -- StatusMessage
    );

    -- Return a resultset with the Id (as expected by SchoolService)
    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[School_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[School_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.SchoolMaster
    SET
        IsDeleted = 1,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id
      AND IsDeleted = 0;

    IF @@ROWCOUNT = 1
        RETURN 1;
    RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[School_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[School_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        Description,
        Email,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        EstablishmentYear,
        JudistrictionCityId,
        IsActive,
        IsDeleted,
        CompanyId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM dbo.SchoolMaster WITH (NOLOCK)
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[School_GetByCompany]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[School_GetByCompany]
    @CompanyId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        CompanyId,
        Name,           -- or SchoolName as Name
        Description,
        Email,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        Phone,
        EstablishmentYear,
        Mobile,
        JudistrictionCountryId,
        JudistrictionStateId,
        JudistrictionCityId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM SchoolMaster
    WHERE CompanyId = @CompanyId AND IsDeleted = 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[School_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[School_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        Description,
        Email,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        Phone,
        EstablishmentYear,
        Mobile,
        JudistrictionCountryId,
        JudistrictionStateId,
        JudistrictionCityId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[SchoolMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[School_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[School_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @Description NVARCHAR(MAX) = NULL,
    @Email NVARCHAR(256) = NULL,
    @Address1 NVARCHAR(500) = NULL,
    @Address2 NVARCHAR(500) = NULL,
    @CityId UNIQUEIDENTIFIER = NULL,
    @StateId UNIQUEIDENTIFIER = NULL,
    @CountryId UNIQUEIDENTIFIER = NULL,
    @ZipCode NVARCHAR(50) = NULL,
    @EstablishmentYear NVARCHAR(50) = NULL,
    @JudistrictionCityId UNIQUEIDENTIFIER = NULL,
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.SchoolMaster
    SET
        Name = @Name,
        Description = ISNULL(@Description, ''),
        Email = ISNULL(@Email, ''),
        Address1 = ISNULL(@Address1, ''),
        Address2 = ISNULL(@Address2, ''),
        CityId = @CityId,
        StateId = @StateId,
        CountryId = @CountryId,
        ZipCode = ISNULL(@ZipCode, ''),
        EstablishmentYear = ISNULL(@EstablishmentYear, ''),
        JudistrictionCityId = @JudistrictionCityId,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id
      AND IsDeleted = 0;

    IF @@ROWCOUNT = 1
        RETURN 1;
    RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[SchoolContact_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SchoolContact_Create]
    @SchoolId       UNIQUEIDENTIFIER,
    @FirstName      NVARCHAR(200),
    @LastName       NVARCHAR(200) = NULL,
    @Email          NVARCHAR(256) = NULL,
    @Phone          NVARCHAR(50) = NULL,
    @MobilePhone    NVARCHAR(50) = NULL,
    @AddressLine1   NVARCHAR(300) = NULL,
    @AddressLine2   NVARCHAR(300) = NULL,
    @CityId         UNIQUEIDENTIFIER,
    @StateId        UNIQUEIDENTIFIER,
    @CountryId      UNIQUEIDENTIFIER,
    @IsActive       BIT,
    @CreatedBy      UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO dbo.SchoolContactMaster
    (
        Id,
        SchoolId,
        FirstName,
        LastName,
        Email,
        Phone,
        MobilePhone,
        AddressLine1,
        AddressLine2,
        CityId,
        StateId,
        CountryId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        Status,
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @SchoolId,
        ISNULL(@FirstName, ''),
        ISNULL(@LastName, ''),
        ISNULL(@Email, ''),
        ISNULL(@Phone, ''),
        ISNULL(@MobilePhone, ''),
        ISNULL(@AddressLine1, ''),
        ISNULL(@AddressLine2, ''),
        @CityId,
        @StateId,
        @CountryId,
        @IsActive,
        0,
        @CreatedBy,
        GETUTCDATE(),
        '',
        ''
    );

    SELECT @NewId AS Id;
END
GO
/****** Object:  StoredProcedure [dbo].[SchoolContact_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SchoolContact_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.SchoolContactMaster WITH (NOLOCK) WHERE Id = @Id AND IsDeleted = 0)
        RETURN 0;

    UPDATE dbo.SchoolContactMaster
    SET IsDeleted = 1,
        ModifiedDate = GETUTCDATE()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 0 RETURN 0;
    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[SchoolContact_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SchoolContact_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        sc.Id,
        sc.SchoolId,
        sc.FirstName,
        sc.LastName,
        sc.Email,
        sc.Phone,
        sc.MobilePhone,
        sc.AddressLine1,
        sc.AddressLine2,
        sc.CityId,
        c.CityName AS CityName,
        sc.StateId,
        s.StateName AS StateName,
        sc.CountryId,
        co.CountryName AS CountryName,
        sc.IsActive,
        sc.IsDeleted,
        sc.CreatedBy,
        LTRIM(RTRIM(CONCAT(u1.FirstName, ' ', u1.LastName))) AS CreatedByName,
        sc.CreatedDate,
        sc.ModifiedBy,
        LTRIM(RTRIM(CONCAT(u2.FirstName, ' ', u2.LastName))) AS ModifiedByName,
        sc.ModifiedDate,
        sc.Status,
        sc.StatusMessage
    FROM dbo.SchoolContactMaster AS sc WITH (NOLOCK)
    LEFT JOIN dbo.CityMaster AS c WITH (NOLOCK) ON c.Id = sc.CityId
    LEFT JOIN dbo.StateMaster AS s WITH (NOLOCK) ON s.Id = sc.StateId
    LEFT JOIN dbo.CountryMaster AS co WITH (NOLOCK) ON co.Id = sc.CountryId
    LEFT JOIN dbo.UserDetails AS u1 WITH (NOLOCK) ON u1.Id = sc.CreatedBy
    LEFT JOIN dbo.UserDetails AS u2 WITH (NOLOCK) ON u2.Id = sc.ModifiedBy
    WHERE sc.IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[SchoolContact_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SchoolContact_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        SchoolId,
        FirstName,
        LastName,
        Email,
        Phone,
        MobilePhone,
        AddressLine1,
        AddressLine2,
        CityId,
        StateId,
        CountryId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM dbo.SchoolContactMaster WITH (NOLOCK)
    WHERE Id = @Id AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[SchoolContact_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SchoolContact_Update]
    @Id             UNIQUEIDENTIFIER,
    @SchoolId       UNIQUEIDENTIFIER,
    @FirstName      NVARCHAR(200),
    @LastName       NVARCHAR(200) = NULL,
    @Email          NVARCHAR(256) = NULL,
    @Phone          NVARCHAR(50) = NULL,
    @MobilePhone    NVARCHAR(50) = NULL,
    @AddressLine1   NVARCHAR(300) = NULL,
    @AddressLine2   NVARCHAR(300) = NULL,
    @CityId         UNIQUEIDENTIFIER,
    @StateId        UNIQUEIDENTIFIER,
    @CountryId      UNIQUEIDENTIFIER,
    @IsActive       BIT,
    @ModifiedBy     UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.SchoolContactMaster WITH (NOLOCK) WHERE Id = @Id AND IsDeleted = 0)
        RETURN 0;

    UPDATE dbo.SchoolContactMaster
    SET 
        SchoolId = @SchoolId,
        FirstName = ISNULL(@FirstName, ''),
        LastName = ISNULL(@LastName, ''),
        Email = ISNULL(@Email, ''),
        Phone = ISNULL(@Phone, ''),
        MobilePhone = ISNULL(@MobilePhone, ''),
        AddressLine1 = ISNULL(@AddressLine1, ''),
        AddressLine2 = ISNULL(@AddressLine2, ''),
        CityId = @CityId,
        StateId = @StateId,
        CountryId = @CountryId,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = GETUTCDATE()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 0 RETURN 0;
    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Section_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Section_Create]
    @Name NVARCHAR(200),
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[SectionMaster]
    (
        Id,
        Name,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @Name,
        @IsActive,
        0,
        @CompanyId,
        @SchoolId,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[Section_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Section_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SectionMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Section_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Section_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[SectionMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Section_GetByClassId]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Section_GetByClassId]
    @ClassId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        S.Id,
        S.Name,
        S.IsActive,
        S.IsDeleted,
        S.CompanyId,
        S.SchoolId,
        S.CreatedBy,
        S.CreatedDate,
        S.ModifiedBy,
        S.ModifiedDate,
        S.[Status],
        S.StatusMessage
    FROM [dbo].[SectionMaster] AS S
    INNER JOIN [dbo].[ClassSectionDetail] AS CSD
        ON CSD.SectionMasterId = S.Id
    WHERE CSD.ClassMasterId = @ClassId
        AND S.IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Section_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Section_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[SectionMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[Section_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Section_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SectionMaster]
    SET 
        Name = @Name,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[SessionMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SessionMaster_Create]
    @Value       NVARCHAR(100),
    @Description NVARCHAR(250),
    @CompanyId   UNIQUEIDENTIFIER,
    @SchoolId    UNIQUEIDENTIFIER,
    @IsActive    BIT,
    @CreatedBy   UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[SessionMaster]
    (
        Id,
        [Value],
        [Description],
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @Value,
        @Description,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[SessionMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SessionMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SessionMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[SessionMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SessionMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        [Value],
        [Description],
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[SessionMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[SessionMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SessionMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        [Value],
        [Description],
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[SessionMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[SessionMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SessionMaster_Update]
    @Id         UNIQUEIDENTIFIER,
    @Value      NVARCHAR(100),
    @Description NVARCHAR(250),
    @IsActive   BIT,
    @SchoolId   UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SessionMaster]
    SET
        [Value]       = @Value,
        [Description] = @Description,
        IsActive      = @IsActive,
        SchoolId      = @SchoolId,
        ModifiedBy    = @ModifiedBy,
        ModifiedDate  = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeaching_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_NonTeaching_Delete]
    @Id UNIQUEIDENTIFIER,
    @DeletedBy UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE [dbo].[NonTeachingMaster]
    SET 
        IsDeleted = 1,
        DeletedBy = @DeletedBy,
        DeletedOn = GETUTCDATE()
    WHERE 
        Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeaching_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_NonTeaching_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id, FirstName, MiddleName, LastName, DOB, DOJ, DateOfLeaving,
        Address, CityId, StateId, CountryId, ZipCode, Gender, MaritalStatusId,
        Image, Phone, MobilePhone, Email, EmployeeCode, Designation, Department,
        Qualification, Salary, BankAccountNumber, BankName, IFSCCode, PAN,
        AadharNumber, EmergencyContactName, EmergencyContactNumber, EmergencyContactRelation,
        CompanyId, SchoolId, IsActive, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn,
        DeletedBy, DeletedOn, IsDeleted
    FROM 
        [dbo].[NonTeachingMaster]
    WHERE 
        IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeaching_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_NonTeaching_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id, FirstName, MiddleName, LastName, DOB, DOJ, DateOfLeaving,
        Address, CityId, StateId, CountryId, ZipCode, Gender, MaritalStatusId,
        Image, Phone, MobilePhone, Email, EmployeeCode, Designation, Department,
        Qualification, Salary, BankAccountNumber, BankName, IFSCCode, PAN,
        AadharNumber, EmergencyContactName, EmergencyContactNumber, EmergencyContactRelation,
        CompanyId, SchoolId, IsActive, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn,
        DeletedBy, DeletedOn, IsDeleted
    FROM 
        [dbo].[NonTeachingMaster]
    WHERE 
        Id = @Id
        AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeaching_GetBySchoolId]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_NonTeaching_GetBySchoolId]
    @SchoolId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id, FirstName, MiddleName, LastName, DOB, DOJ, DateOfLeaving,
        Address, CityId, StateId, CountryId, ZipCode, Gender, MaritalStatusId,
        Image, Phone, MobilePhone, Email, EmployeeCode, Designation, Department,
        Qualification, Salary, BankAccountNumber, BankName, IFSCCode, PAN,
        AadharNumber, EmergencyContactName, EmergencyContactNumber, EmergencyContactRelation,
        CompanyId, SchoolId, IsActive, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn,
        DeletedBy, DeletedOn, IsDeleted
    FROM 
        [dbo].[NonTeachingMaster]
    WHERE 
        SchoolId = @SchoolId
        AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeaching_Insert]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_NonTeaching_Insert]
    @Id UNIQUEIDENTIFIER,
    @FirstName NVARCHAR(100),
    @MiddleName NVARCHAR(100) = NULL,
    @LastName NVARCHAR(100),
    @DOB DATE,
    @DOJ DATE,
    @DateOfLeaving DATE = NULL,
    @Address NVARCHAR(500) = NULL,
    @CityId UNIQUEIDENTIFIER = NULL,
    @StateId UNIQUEIDENTIFIER = NULL,
    @CountryId UNIQUEIDENTIFIER = NULL,
    @ZipCode NVARCHAR(20) = NULL,
    @Gender NVARCHAR(10) = NULL,
    @MaritalStatusId UNIQUEIDENTIFIER = NULL,
    @Image VARBINARY(MAX) = NULL,
    @Phone NVARCHAR(20) = NULL,
    @MobilePhone NVARCHAR(20) = NULL,
    @Email NVARCHAR(100) = NULL,
    @EmployeeCode NVARCHAR(50) = NULL,
    @Designation NVARCHAR(100) = NULL,
    @Department NVARCHAR(100) = NULL,
    @Qualification NVARCHAR(200) = NULL,
    @Salary DECIMAL(18, 2) = NULL,
    @BankAccountNumber NVARCHAR(50) = NULL,
    @BankName NVARCHAR(100) = NULL,
    @IFSCCode NVARCHAR(20) = NULL,
    @PAN NVARCHAR(20) = NULL,
    @AadharNumber NVARCHAR(20) = NULL,
    @EmergencyContactName NVARCHAR(200) = NULL,
    @EmergencyContactNumber NVARCHAR(20) = NULL,
    @EmergencyContactRelation NVARCHAR(100) = NULL,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT = 1,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[NonTeachingMaster]
    (
        Id, FirstName, MiddleName, LastName, DOB, DOJ, DateOfLeaving,
        Address, CityId, StateId, CountryId, ZipCode, Gender, MaritalStatusId,
        Image, Phone, MobilePhone, Email, EmployeeCode, Designation, Department,
        Qualification, Salary, BankAccountNumber, BankName, IFSCCode, PAN,
        AadharNumber, EmergencyContactName, EmergencyContactNumber, EmergencyContactRelation,
        CompanyId, SchoolId, IsActive, CreatedBy, CreatedOn
    )
    VALUES
    (
        @Id, @FirstName, @MiddleName, @LastName, @DOB, @DOJ, @DateOfLeaving,
        @Address, @CityId, @StateId, @CountryId, @ZipCode, @Gender, @MaritalStatusId,
        @Image, @Phone, @MobilePhone, @Email, @EmployeeCode, @Designation, @Department,
        @Qualification, @Salary, @BankAccountNumber, @BankName, @IFSCCode, @PAN,
        @AadharNumber, @EmergencyContactName, @EmergencyContactNumber, @EmergencyContactRelation,
        @CompanyId, @SchoolId, @IsActive, @CreatedBy, GETUTCDATE()
    );
    
    RETURN SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeaching_ToggleStatus]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_NonTeaching_ToggleStatus]
    @Id UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE [dbo].[NonTeachingMaster]
    SET 
        IsActive = ~IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedOn = GETUTCDATE()
    WHERE 
        Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeaching_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_NonTeaching_Update]
    @Id UNIQUEIDENTIFIER,
    @FirstName NVARCHAR(100),
    @MiddleName NVARCHAR(100) = NULL,
    @LastName NVARCHAR(100),
    @DOB DATE,
    @DOJ DATE,
    @DateOfLeaving DATE = NULL,
    @Address NVARCHAR(500) = NULL,
    @CityId UNIQUEIDENTIFIER = NULL,
    @StateId UNIQUEIDENTIFIER = NULL,
    @CountryId UNIQUEIDENTIFIER = NULL,
    @ZipCode NVARCHAR(20) = NULL,
    @Gender NVARCHAR(10) = NULL,
    @MaritalStatusId UNIQUEIDENTIFIER = NULL,
    @Image VARBINARY(MAX) = NULL,
    @Phone NVARCHAR(20) = NULL,
    @MobilePhone NVARCHAR(20) = NULL,
    @Email NVARCHAR(100) = NULL,
    @EmployeeCode NVARCHAR(50) = NULL,
    @Designation NVARCHAR(100) = NULL,
    @Department NVARCHAR(100) = NULL,
    @Qualification NVARCHAR(200) = NULL,
    @Salary DECIMAL(18, 2) = NULL,
    @BankAccountNumber NVARCHAR(50) = NULL,
    @BankName NVARCHAR(100) = NULL,
    @IFSCCode NVARCHAR(20) = NULL,
    @PAN NVARCHAR(20) = NULL,
    @AadharNumber NVARCHAR(20) = NULL,
    @EmergencyContactName NVARCHAR(200) = NULL,
    @EmergencyContactNumber NVARCHAR(20) = NULL,
    @EmergencyContactRelation NVARCHAR(100) = NULL,
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE [dbo].[NonTeachingMaster]
    SET 
        FirstName = @FirstName,
        MiddleName = @MiddleName,
        LastName = @LastName,
        DOB = @DOB,
        DOJ = @DOJ,
        DateOfLeaving = @DateOfLeaving,
        Address = @Address,
        CityId = @CityId,
        StateId = @StateId,
        CountryId = @CountryId,
        ZipCode = @ZipCode,
        Gender = @Gender,
        MaritalStatusId = @MaritalStatusId,
        Image = @Image,
        Phone = @Phone,
        MobilePhone = @MobilePhone,
        Email = @Email,
        EmployeeCode = @EmployeeCode,
        Designation = @Designation,
        Department = @Department,
        Qualification = @Qualification,
        Salary = @Salary,
        BankAccountNumber = @BankAccountNumber,
        BankName = @BankName,
        IFSCCode = @IFSCCode,
        PAN = @PAN,
        AadharNumber = @AadharNumber,
        EmergencyContactName = @EmergencyContactName,
        EmergencyContactNumber = @EmergencyContactNumber,
        EmergencyContactRelation = @EmergencyContactRelation,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedOn = GETUTCDATE()
    WHERE 
        Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeachingDocument_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 5. Delete a document
CREATE   PROCEDURE [dbo].[sp_NonTeachingDocument_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM NonTeachingDocumentDetails
    WHERE Id = @Id;
    
    RETURN @@ROWCOUNT;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeachingDocument_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 2. Get a single document by ID
CREATE   PROCEDURE [dbo].[sp_NonTeachingDocument_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id,
        NonTeachingId,
        DocumentTypeId,
        DocumentType,
        DocumentNumber,
        DocumentPath,
        IssueDate,
        ExpiryDate,
        Remarks,
        IsActive,
        IsVerified,
        VerifiedBy,
        VerifiedOn,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate
    FROM 
        NonTeachingDocumentDetails
    WHERE 
        Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeachingDocument_GetByNonTeachingId]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- 1. Get all documents for a non-teaching staff member
CREATE   PROCEDURE [dbo].[sp_NonTeachingDocument_GetByNonTeachingId]
    @NonTeachingId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id,
        NonTeachingId,
        DocumentTypeId,
        DocumentType,
        DocumentNumber,
        DocumentPath,
        IssueDate,
        ExpiryDate,
        Remarks,
        IsActive,
        IsVerified,
        VerifiedBy,
        VerifiedOn,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate
    FROM 
        NonTeachingDocumentDetails
    WHERE 
        NonTeachingId = @NonTeachingId
    ORDER BY 
        CreatedDate DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeachingDocument_Insert]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 3. Insert a new document
CREATE   PROCEDURE [dbo].[sp_NonTeachingDocument_Insert]
    @Id UNIQUEIDENTIFIER,
    @NonTeachingId UNIQUEIDENTIFIER,
    @DocumentTypeId INT,
    @DocumentType NVARCHAR(100) = NULL,
    @DocumentNumber NVARCHAR(255) = NULL,
    @DocumentPath NVARCHAR(500) = NULL,
    @IssueDate DATETIME = NULL,
    @ExpiryDate DATETIME = NULL,
    @Remarks NVARCHAR(500) = NULL,
    @IsActive BIT = 1,
    @IsVerified BIT = 0,
    @VerifiedBy NVARCHAR(450) = NULL,
    @VerifiedOn DATETIME = NULL,
    @CreatedBy NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO NonTeachingDocumentDetails (
        Id,
        NonTeachingId,
        DocumentTypeId,
        DocumentType,
        DocumentNumber,
        DocumentPath,
        IssueDate,
        ExpiryDate,
        Remarks,
        IsActive,
        IsVerified,
        VerifiedBy,
        VerifiedOn,
        CreatedBy,
        CreatedDate
    ) VALUES (
        @Id,
        @NonTeachingId,
        @DocumentTypeId,
        @DocumentType,
        @DocumentNumber,
        @DocumentPath,
        @IssueDate,
        @ExpiryDate,
        @Remarks,
        @IsActive,
        @IsVerified,
        @VerifiedBy,
        @VerifiedOn,
        @CreatedBy,
        GETUTCDATE()
    );
    
    RETURN SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeachingDocument_ToggleVerification]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 6. Toggle document verification status
CREATE   PROCEDURE [dbo].[sp_NonTeachingDocument_ToggleVerification]
    @Id UNIQUEIDENTIFIER,
    @IsVerified BIT,
    @VerifiedBy NVARCHAR(450) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE NonTeachingDocumentDetails
    SET 
        IsVerified = @IsVerified,
        VerifiedBy = CASE WHEN @IsVerified = 1 THEN @VerifiedBy ELSE NULL END,
        VerifiedOn = CASE WHEN @IsVerified = 1 THEN GETUTCDATE() ELSE NULL END,
        ModifiedBy = @VerifiedBy,
        ModifiedDate = GETUTCDATE()
    WHERE 
        Id = @Id;
        
    RETURN @@ROWCOUNT;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_NonTeachingDocument_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 4. Update an existing document
CREATE   PROCEDURE [dbo].[sp_NonTeachingDocument_Update]
    @Id UNIQUEIDENTIFIER,
    @DocumentTypeId INT,
    @DocumentType NVARCHAR(100) = NULL,
    @DocumentNumber NVARCHAR(255) = NULL,
    @DocumentPath NVARCHAR(500) = NULL,
    @IssueDate DATETIME = NULL,
    @ExpiryDate DATETIME = NULL,
    @Remarks NVARCHAR(500) = NULL,
    @IsActive BIT = 1,
    @IsVerified BIT = 0,
    @VerifiedBy NVARCHAR(450) = NULL,
    @VerifiedOn DATETIME = NULL,
    @ModifiedBy NVARCHAR(450) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE NonTeachingDocumentDetails
    SET 
        DocumentTypeId = @DocumentTypeId,
        DocumentType = @DocumentType,
        DocumentNumber = @DocumentNumber,
        DocumentPath = @DocumentPath,
        IssueDate = @IssueDate,
        ExpiryDate = @ExpiryDate,
        Remarks = @Remarks,
        IsActive = @IsActive,
        IsVerified = @IsVerified,
        VerifiedBy = @VerifiedBy,
        VerifiedOn = @VerifiedOn,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = GETUTCDATE()
    WHERE 
        Id = @Id;
        
    RETURN @@ROWCOUNT;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Privilege_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_Privilege_Create]
    @Id UNIQUEIDENTIFIER,
    @PrivilegeName NVARCHAR(100),
    @IsActive BIT = 1,
    @CreatedBy UNIQUEIDENTIFIER,
    @PrivilegeParentId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        SET @Id = ISNULL(@Id, NEWID());
        
        IF NOT EXISTS (SELECT 1 FROM Privileges WHERE PrivilegeName = @PrivilegeName AND IsDeleted = 0)
        BEGIN
            INSERT INTO Privileges (
                Id,
                PrivilegeName,
                IsActive,
                CreatedBy,
                CreatedDate,
                Status,
                StatusMessage,
                PrivilegeParentId
            ) VALUES (
                @Id,
                @PrivilegeName,
                @IsActive,
                @CreatedBy,
                GETUTCDATE(),
                'COM',
                'Privilege created successfully',
                @PrivilegeParentId
            );
            
            SELECT @Id AS Id;
        END
        ELSE
        BEGIN
            SELECT CAST('00000000-0000-0000-0000-000000000000' AS UNIQUEIDENTIFIER) AS Id;
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
        RETURN -1;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Privilege_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_Privilege_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Privileges WHERE Id = @Id AND IsDeleted = 0)
        BEGIN
            RETURN 0; -- Not found
        END
        
        -- Check if this privilege is being referenced by any role
        IF EXISTS (SELECT 1 FROM RolePrivileges WHERE PrivilegeId = @Id)
        BEGIN
            RETURN -1; -- In use by roles
        END
        
        -- Soft delete
        UPDATE Privileges
        SET 
            IsDeleted = 1,
            ModifiedDate = GETUTCDATE(),
            Status = 'DEL',
            StatusMessage = 'Privilege deleted'
        WHERE 
            Id = @Id;
            
        RETURN 1; -- Success
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
        RETURN -2; -- Error
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Privilege_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_Privilege_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id,
        PrivilegeName,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage,
        PrivilegeParentId
    FROM 
        Privileges
    WHERE 
        IsDeleted = 0
    ORDER BY 
        PrivilegeName;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Privilege_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_Privilege_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id,
        PrivilegeName,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage,
        PrivilegeParentId
    FROM 
        Privileges
    WHERE 
        Id = @Id 
        AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Privilege_GetByRoleId]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_Privilege_GetByRoleId]
    @RoleId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.Id,
        p.PrivilegeName,
        p.IsActive,
        p.PrivilegeParentId,
        rp.RoleId,
        rp.IsActive AS IsAssigned
    FROM 
        Privileges p
    LEFT JOIN 
        RolePrivileges rp ON p.Id = rp.PrivilegeId AND rp.RoleId = @RoleId
    WHERE 
        p.IsDeleted = 0
    ORDER BY 
        p.PrivilegeName;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Privilege_IsInUse]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_Privilege_IsInUse]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @IsInUse BIT = 0;
    
    IF EXISTS (SELECT 1 FROM RolePrivileges WHERE PrivilegeId = @Id)
    BEGIN
        SET @IsInUse = 1;
    END
    
    SELECT @IsInUse AS IsInUse;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Privilege_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_Privilege_Update]
    @Id UNIQUEIDENTIFIER,
    @PrivilegeName NVARCHAR(100),
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER,
    @PrivilegeParentId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Privileges WHERE Id = @Id AND IsDeleted = 0)
        BEGIN
            RETURN 0; -- Not found
        END
        
        IF EXISTS (SELECT 1 FROM Privileges WHERE Id != @Id AND PrivilegeName = @PrivilegeName AND IsDeleted = 0)
        BEGIN
            RETURN -1; -- Duplicate name
        END
        
        UPDATE Privileges
        SET 
            PrivilegeName = @PrivilegeName,
            IsActive = @IsActive,
            ModifiedBy = @ModifiedBy,
            ModifiedDate = GETUTCDATE(),
            Status = 'UPD',
            StatusMessage = 'Privilege updated successfully',
            PrivilegeParentId = @PrivilegeParentId
        WHERE 
            Id = @Id;
            
        RETURN 1; -- Success
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
        RETURN -2; -- Error
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RolePrivilege_GetByRoleId]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_RolePrivilege_GetByRoleId]
    @RoleId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        rp.Id,
        rp.RoleId,
        rp.PrivilegeId,
        p.PrivilegeName,
        rp.IsActive
    FROM 
        RolePrivileges rp
    INNER JOIN 
        Privileges p ON rp.PrivilegeId = p.Id
    WHERE 
        rp.RoleId = @RoleId
        AND rp.IsDeleted = 0
        AND p.IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RolePrivilege_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_RolePrivilege_Update]
    @RoleId UNIQUEIDENTIFIER,
    @PrivilegeIds dbo.UniqueIdentifierList READONLY,
    @ModifiedBy UNIQUEIDENTIFIER,
    @ModifiedDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Mark existing role privileges as deleted
        UPDATE RolePrivileges
        SET 
            IsDeleted = 1,
            ModifiedBy = @ModifiedBy,
            ModifiedDate = @ModifiedDate
        WHERE 
            RoleId = @RoleId
            AND IsDeleted = 0;

        -- Insert new role privileges
        INSERT INTO RolePrivileges (
            Id,
            RoleId,
            PrivilegeId,
            IsActive,
            IsDeleted,
            CreatedBy,
            CreatedDate
        )
        SELECT 
            NEWID(),
            @RoleId,
            p.Id,
            1, -- IsActive
            0, -- IsDeleted
            @ModifiedBy, -- CreatedBy
            @ModifiedDate -- CreatedDate
        FROM 
            @PrivilegeIds p
        INNER JOIN 
            Privileges priv ON p.Id = priv.Id
        WHERE 
            priv.IsDeleted = 0
            AND priv.IsActive = 1;

        COMMIT TRANSACTION;
        RETURN 1; -- Success
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        THROW;
    END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SetCurrentAcademicYear]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create a stored procedure to set only one academic year as current
CREATE   PROCEDURE [dbo].[sp_SetCurrentAcademicYear]
    @AcademicYearId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION;
    
    -- First, set all years to not current
    UPDATE [dbo].[AcademicYear]
    SET [IsCurrent] = 0,
        [ModifiedDate] = GETDATE(),
        [ModifiedBy] = @AcademicYearId; -- Using @AcademicYearId as ModifiedBy for simplicity
    
    -- Then set the specified year as current
    UPDATE [dbo].[AcademicYear]
    SET [IsCurrent] = 1,
        [ModifiedDate] = GETDATE(),
        [ModifiedBy] = @AcademicYearId -- Using @AcademicYearId as ModifiedBy for simplicity
    WHERE [Id] = @AcademicYearId;
    
    COMMIT TRANSACTION;
END;
GO
/****** Object:  StoredProcedure [dbo].[State_GetByCountry]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[State_GetByCountry]
    @CountryId uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        s.[Id],
        s.[StateName]
    FROM dbo.[StateMaster] s
    WHERE s.[CountryId] = @CountryId
    ORDER BY s.[StateName];
END
GO
/****** Object:  StoredProcedure [dbo].[Student_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Student_Create]
    @Id UNIQUEIDENTIFIER = NULL,
    @RollNumber UNIQUEIDENTIFIER = NULL,

    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100) = NULL,

    @Address NVARCHAR(500) = NULL,
    @CityId UNIQUEIDENTIFIER = NULL,
    @StateId UNIQUEIDENTIFIER = NULL,
    @CountryId UNIQUEIDENTIFIER = NULL,
    @ZipCode NVARCHAR(20) = NULL,
    @ContactNumber NVARCHAR(20) = NULL,
    @EmergencyContactNumber NVARCHAR(20) = NULL,

    @DOB DATE = NULL,
    @DOJ DATE = NULL,
    @RegistrationNumber NVARCHAR(50) = NULL,

    @ClassId UNIQUEIDENTIFIER = NULL,
    @SectionId UNIQUEIDENTIFIER = NULL,

    @AvailTransport BIT = NULL,
    @Image NVARCHAR(300) = NULL,

    @Email NVARCHAR(100) = NULL,
    @Phone NVARCHAR(20) = NULL,              -- ADDED to fix error
    @CategoryId UNIQUEIDENTIFIER = NULL,

    @SiblingsIfAny BIT = NULL,
    @SiblingClassId UNIQUEIDENTIFIER = NULL,
    @Gender UNIQUEIDENTIFIER = NULL,

    @DisabilityAny NVARCHAR(500) = NULL,
    @MedicalAlleryAny NVARCHAR(500) = NULL,

    @BirthCityId UNIQUEIDENTIFIER = NULL,
    @BirthStateId UNIQUEIDENTIFIER = NULL,
    @BirthCountryId UNIQUEIDENTIFIER = NULL,

    @PreviousSchoolAttended NVARCHAR(200) = NULL,
    @PreviousSchoolClassId UNIQUEIDENTIFIER = NULL,
    @PreviousSchoolPercentage DECIMAL(5,2) = NULL,
    @PreviousSchoolRank NVARCHAR(50) = NULL,
    @PreviousSchoolBoardId UNIQUEIDENTIFIER = NULL,
    @PreviousSchoolFromDate DATE = NULL,
    @PreviousSchoolToDate DATE = NULL,
    @WithdrawnDate DATE = NULL,
    @WithdrawnReason NVARCHAR(500) = NULL,

    @BloodGroupId UNIQUEIDENTIFIER = NULL,
    @Nationality UNIQUEIDENTIFIER = NULL,
    @Hobbies NVARCHAR(500) = NULL,
    @ReligionId UNIQUEIDENTIFIER = NULL,

    @RouteId UNIQUEIDENTIFIER = NULL,
    @RouteStopDetailsId UNIQUEIDENTIFIER = NULL,
    @ClassTeacherId UNIQUEIDENTIFIER = NULL,
    @RoutePickAndDrop BIT = NULL,

    @FeesDiscountCategoryMasterId UNIQUEIDENTIFIER = NULL,
    @TutionFees DECIMAL(18,2) = NULL,
    @AnnualFees DECIMAL(18,2) = NULL,
    @TransportFees DECIMAL(18,2) = NULL,
    @UseTransportFees BIT = NULL,

    @SessionId UNIQUEIDENTIFIER = NULL,

    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,

    @IsActive BIT = 1,
    @IsDeleted BIT = 0,

    @CreatedBy UNIQUEIDENTIFIER,
    @CreatedDate DATETIME2 = NULL,

    @Status NVARCHAR(50) = NULL,
    @StatusMessage NVARCHAR(200) = NULL,

    @HouseAllotted UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = ISNULL(@Id, NEWID());
    DECLARE @CurrentDate DATETIME2 = ISNULL(@CreatedDate, SYSUTCDATETIME());
    DECLARE @FinalRoll UNIQUEIDENTIFIER = ISNULL(@RollNumber, NEWID());

    INSERT INTO dbo.StudentMaster
    (
        Id, RollNumber, FirstName, LastName, Address, CityId, StateId, CountryId, ZipCode,
        ContactNumber, EmergencyContactNumber, DOB, DOJ, RegistrationNumber, ClassId, SectionId,
        AvailTransport, Image, Email, CategoryId, SiblingsIfAny, SiblingClassId, Gender,
        DisabilityAny, MedicalAlleryAny, BirthCityId, BirthStateId, BirthCountryId,
        PreviousSchoolAttended, PreviousSchoolClassId, PreviousSchoolPercentage, PreviousSchoolRank,
        PreviousSchoolBoardId, PreviousSchoolFromDate, PreviousSchoolToDate, WithdrawnDate, WithdrawnReason,
        BloodGroupId, Nationality, Hobbies, ReligionId, Phone, RouteId, RouteStopDetailsId, ClassTeacherId,
        RoutePickAndDrop, FeesDiscountCategoryMasterId, TutionFees, AnnualFees, TransportFees, UseTransportFees,
        SessionId, CompanyId, SchoolId, IsActive, IsDeleted, CreatedBy, CreatedDate, Status, StatusMessage, HouseAllotted
    )
    VALUES
    (
        @NewId, @FinalRoll, @FirstName, ISNULL(@LastName, N''), ISNULL(@Address, N''), @CityId, @StateId, @CountryId,
        ISNULL(@ZipCode, N''), ISNULL(@ContactNumber, N''), ISNULL(@EmergencyContactNumber, N''), @DOB, @DOJ,
        ISNULL(@RegistrationNumber, N''), @ClassId, @SectionId, ISNULL(@AvailTransport, 0), ISNULL(@Image, N''),
        ISNULL(@Email, N''), @CategoryId, ISNULL(@SiblingsIfAny, 0), @SiblingClassId, @Gender, ISNULL(@DisabilityAny, N''),
        ISNULL(@MedicalAlleryAny, N''), @BirthCityId, @BirthStateId, @BirthCountryId, ISNULL(@PreviousSchoolAttended, N''),
        @PreviousSchoolClassId, @PreviousSchoolPercentage, ISNULL(@PreviousSchoolRank, N''), @PreviousSchoolBoardId,
        @PreviousSchoolFromDate, @PreviousSchoolToDate, @WithdrawnDate, ISNULL(@WithdrawnReason, N''), @BloodGroupId,
        @Nationality, ISNULL(@Hobbies, N''), @ReligionId, ISNULL(@Phone, N''), @RouteId, @RouteStopDetailsId,
        @ClassTeacherId, ISNULL(@RoutePickAndDrop, 0), @FeesDiscountCategoryMasterId, @TutionFees, @AnnualFees,
        @TransportFees, ISNULL(@UseTransportFees, 0), @SessionId, @CompanyId, @SchoolId, ISNULL(@IsActive, 1),
        ISNULL(@IsDeleted, 0), @CreatedBy, @CurrentDate, ISNULL(@Status, N'Active'),
        ISNULL(@StatusMessage, N'Student created successfully'), @HouseAllotted
    );

    SELECT @NewId AS Id;
END
GO
/****** Object:  StoredProcedure [dbo].[Student_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Student_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Result INT = 0;
    
    IF EXISTS (SELECT 1 FROM StudentMaster WHERE Id = @Id AND IsDeleted = 0)
    BEGIN
        -- Soft delete the student
        UPDATE StudentMaster
        SET 
            IsDeleted = 1,
            Status = 'Inactive',
            StatusMessage = 'Student marked as deleted'
        WHERE 
            Id = @Id;
            
        SET @Result = 1;
    END
    
    RETURN @Result;
END
GO
/****** Object:  StoredProcedure [dbo].[Student_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Student_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        RollNumber,
        FirstName,
        LastName,
        Email,
        Phone,
        DOB,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM 
        StudentMaster
    WHERE 
        IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Student_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Student_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT        StudentMaster.Id, StudentMaster.RollNumber, StudentMaster.FirstName, StudentMaster.LastName, StudentMaster.Address, StudentMaster.CityId, StudentMaster.StateId, 
                         StudentMaster.CountryId, StudentMaster.ZipCode, StudentMaster.ContactNumber, StudentMaster.EmergencyContactNumber, StudentMaster.DOB, StudentMaster.DOJ, 
                         StudentMaster.RegistrationNumber, StudentMaster.ClassId, StudentMaster.SectionId, StudentMaster.AvailTransport, StudentMaster.Image, StudentMaster.Email, StudentMaster.CategoryId, 
                         StudentMaster.SiblingsIfAny, StudentMaster.SiblingClassId, StudentMaster.Gender, StudentMaster.DisabilityAny, StudentMaster.MedicalAlleryAny, StudentMaster.BirthCityId, 
                         StudentMaster.BirthStateId, StudentMaster.BirthCountryId, StudentMaster.PreviousSchoolAttended, StudentMaster.PreviousSchoolClassId, StudentMaster.PreviousSchoolPercentage, 
                         StudentMaster.PreviousSchoolRank, StudentMaster.PreviousSchoolBoardId, StudentMaster.PreviousSchoolFromDate, StudentMaster.PreviousSchoolToDate, StudentMaster.WithdrawnDate, 
                         StudentMaster.WithdrawnReason, StudentMaster.BloodGroupId, StudentMaster.Nationality, StudentMaster.Hobbies, StudentMaster.ReligionId, StudentMaster.Phone, StudentMaster.RouteId, 
                         StudentMaster.RouteStopDetailsId, StudentMaster.ClassTeacherId, StudentMaster.RoutePickAndDrop, StudentMaster.FeesDiscountCategoryMasterId, StudentMaster.TutionFees, 
                         StudentMaster.AnnualFees, StudentMaster.TransportFees, StudentMaster.UseTransportFees, StudentMaster.SessionId, StudentMaster.CompanyId, StudentMaster.SchoolId, StudentMaster.IsActive, 
                         StudentMaster.IsDeleted, StudentMaster.CreatedBy, StudentMaster.CreatedDate, StudentMaster.ModifiedBy, StudentMaster.ModifiedDate, StudentMaster.Status, StudentMaster.StatusMessage, 
                         StudentMaster.HouseAllotted, CompanyMaster.CompanyName, SchoolMaster.Name AS SchoolName, CountryMaster.CountryName, StateMaster.StateName, CityMaster.CityName, 
                         ClassMaster.Name as ClassName, SectionMaster.Name AS SectionName
FROM            StudentMaster LEFT JOIN
                         ClassMaster ON StudentMaster.ClassId = ClassMaster.Id LEFT JOIN
                         SectionMaster ON StudentMaster.SectionId = SectionMaster.Id LEFT JOIN
                         CompanyMaster ON SectionMaster.CompanyId = CompanyMaster.Id AND ClassMaster.CompanyId = CompanyMaster.Id AND StudentMaster.CompanyId = CompanyMaster.Id LEFT JOIN
                         SchoolMaster ON SectionMaster.SchoolId = SchoolMaster.Id  LEFT JOIN
                         CountryMaster ON StudentMaster.CountryId = CountryMaster.Id LEFT JOIN
                         StateMaster ON StudentMaster.StateId = StateMaster.Id LEFT JOIN
                         CityMaster ON StudentMaster.CityId = CityMaster.Id
    WHERE 
        StudentMaster.Id = @Id
        AND StudentMaster.IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Student_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Student_Update]
    @Id UNIQUEIDENTIFIER,

    @RollNumber UNIQUEIDENTIFIER = NULL,

    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100) = NULL,

    @Address NVARCHAR(500) = NULL,
    @CityId UNIQUEIDENTIFIER = NULL,
    @StateId UNIQUEIDENTIFIER = NULL,
    @CountryId UNIQUEIDENTIFIER = NULL,
    @ZipCode NVARCHAR(20) = NULL,
    @ContactNumber NVARCHAR(20) = NULL,
    @EmergencyContactNumber NVARCHAR(20) = NULL,

    @DOB DATE = NULL,
    @DOJ DATE = NULL,
    @RegistrationNumber NVARCHAR(50) = NULL,

    @ClassId UNIQUEIDENTIFIER = NULL,
    @SectionId UNIQUEIDENTIFIER = NULL,

    @AvailTransport BIT = NULL,
    @Image NVARCHAR(300) = NULL,

    @Email NVARCHAR(100) = NULL,
    @Phone NVARCHAR(20) = NULL,
    @CategoryId UNIQUEIDENTIFIER = NULL,

    @SiblingsIfAny BIT = NULL,
    @SiblingClassId UNIQUEIDENTIFIER = NULL,
    @Gender UNIQUEIDENTIFIER = NULL,

    @DisabilityAny NVARCHAR(500) = NULL,
    @MedicalAlleryAny NVARCHAR(500) = NULL,

    @BirthCityId UNIQUEIDENTIFIER = NULL,
    @BirthStateId UNIQUEIDENTIFIER = NULL,
    @BirthCountryId UNIQUEIDENTIFIER = NULL,

    @PreviousSchoolAttended NVARCHAR(200) = NULL,
    @PreviousSchoolClassId UNIQUEIDENTIFIER = NULL,
    @PreviousSchoolPercentage DECIMAL(5,2) = NULL,
    @PreviousSchoolRank NVARCHAR(50) = NULL,
    @PreviousSchoolBoardId UNIQUEIDENTIFIER = NULL,
    @PreviousSchoolFromDate DATE = NULL,
    @PreviousSchoolToDate DATE = NULL,
    @WithdrawnDate DATE = NULL,
    @WithdrawnReason NVARCHAR(500) = NULL,

    @BloodGroupId UNIQUEIDENTIFIER = NULL,
    @Nationality UNIQUEIDENTIFIER = NULL,
    @Hobbies NVARCHAR(500) = NULL,
    @ReligionId UNIQUEIDENTIFIER = NULL,

    @RouteId UNIQUEIDENTIFIER = NULL,
    @RouteStopDetailsId UNIQUEIDENTIFIER = NULL,
    @ClassTeacherId UNIQUEIDENTIFIER = NULL,
    @RoutePickAndDrop BIT = NULL,

    @FeesDiscountCategoryMasterId UNIQUEIDENTIFIER = NULL,
    @TutionFees DECIMAL(18,2) = NULL,
    @AnnualFees DECIMAL(18,2) = NULL,
    @TransportFees DECIMAL(18,2) = NULL,
    @UseTransportFees BIT = NULL,

    @SessionId UNIQUEIDENTIFIER = NULL,

    @CompanyId UNIQUEIDENTIFIER = NULL, -- usually not changed on update; include only if needed
    @SchoolId UNIQUEIDENTIFIER = NULL,

    @IsActive BIT,
    @IsDeleted BIT = 0,

    @ModifiedBy UNIQUEIDENTIFIER,
    @Status NVARCHAR(50) = NULL,
    @StatusMessage NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CurrentDate DATETIME2 = SYSUTCDATETIME();
    DECLARE @Result INT = 0;

    IF EXISTS (SELECT 1 FROM dbo.StudentMaster WITH (UPDLOCK, HOLDLOCK) WHERE Id = @Id AND IsDeleted = 0)
    BEGIN
        UPDATE dbo.StudentMaster
        SET
            RollNumber                = ISNULL(@RollNumber, RollNumber),
            FirstName                 = @FirstName,
            LastName                  = ISNULL(@LastName, N''),
            Address                   = ISNULL(@Address, N''),
            CityId                    = @CityId,
            StateId                   = @StateId,
            CountryId                 = @CountryId,
            ZipCode                   = ISNULL(@ZipCode, N''),
            ContactNumber             = ISNULL(@ContactNumber, N''),
            EmergencyContactNumber    = ISNULL(@EmergencyContactNumber, N''),
            DOB                       = @DOB,
            DOJ                       = @DOJ,
            RegistrationNumber        = ISNULL(@RegistrationNumber, N''),
            ClassId                   = @ClassId,
            SectionId                 = @SectionId,
            AvailTransport            = ISNULL(@AvailTransport, 0),
            Image                     = ISNULL(@Image, N''),
            Email                     = ISNULL(@Email, N''),
            Phone                     = ISNULL(@Phone, N''),
            CategoryId                = @CategoryId,
            SiblingsIfAny             = ISNULL(@SiblingsIfAny, 0),
            SiblingClassId            = @SiblingClassId,
            Gender                    = @Gender,
            DisabilityAny             = ISNULL(@DisabilityAny, N''),
            MedicalAlleryAny          = ISNULL(@MedicalAlleryAny, N''),
            BirthCityId               = @BirthCityId,
            BirthStateId              = @BirthStateId,
            BirthCountryId            = @BirthCountryId,
            PreviousSchoolAttended    = ISNULL(@PreviousSchoolAttended, N''),
            PreviousSchoolClassId     = @PreviousSchoolClassId,
            PreviousSchoolPercentage  = @PreviousSchoolPercentage,
            PreviousSchoolRank        = ISNULL(@PreviousSchoolRank, N''),
            PreviousSchoolBoardId     = @PreviousSchoolBoardId,
            PreviousSchoolFromDate    = @PreviousSchoolFromDate,
            PreviousSchoolToDate      = @PreviousSchoolToDate,
            WithdrawnDate             = @WithdrawnDate,
            WithdrawnReason           = ISNULL(@WithdrawnReason, N''),

            BloodGroupId              = @BloodGroupId,
            Nationality               = @Nationality,
            Hobbies                   = ISNULL(@Hobbies, N''),
            ReligionId                = @ReligionId,

            RouteId                   = @RouteId,
            RouteStopDetailsId        = @RouteStopDetailsId,
            ClassTeacherId            = @ClassTeacherId,
            RoutePickAndDrop          = ISNULL(@RoutePickAndDrop, 0),

            FeesDiscountCategoryMasterId = @FeesDiscountCategoryMasterId,
            TutionFees                = @TutionFees,
            AnnualFees                = @AnnualFees,
            TransportFees             = @TransportFees,
            UseTransportFees          = ISNULL(@UseTransportFees, 0),

            SessionId                 = @SessionId,
            CompanyId                 = ISNULL(@CompanyId, CompanyId), -- or keep existing if null
            SchoolId                  = ISNULL(@SchoolId, SchoolId),

            IsActive                  = @IsActive,
            IsDeleted                 = ISNULL(@IsDeleted, IsDeleted),

            ModifiedBy                = @ModifiedBy,
            ModifiedDate              = @CurrentDate,
            Status                    = ISNULL(@Status, N'Updated'),
            StatusMessage             = ISNULL(@StatusMessage, N'Student updated successfully')
        WHERE Id = @Id;

        SET @Result = 1;
    END

    RETURN @Result;
END
GO
/****** Object:  StoredProcedure [dbo].[StudentMaster_GetBySchool]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[StudentMaster_GetBySchool]
    @SchoolId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        sm.Id,
        sm.FirstName,
        sm.LastName,
        sm.ClassId,
        cm.Name AS ClassName,
        sm.SectionId,
        sm.Gender,
        sm.ContactNumber,
        sm.Address,
        sm.CityId,
        sm.StateId,
        sm.CountryId,
        sm.Email,
        sm.IsActive,
        sm.CreatedBy,
        sm.CreatedDate,
        sm.ModifiedBy,
        sm.ModifiedDate,
        sm.Status,
        sm.StatusMessage
    FROM 
        StudentMaster sm
    LEFT JOIN 
        ClassMaster cm ON sm.ClassId = cm.Id
    LEFT JOIN 
        SectionMaster sms ON sm.SectionId = sms.Id
    WHERE 
        sm.SchoolId = @SchoolId
        AND sm.IsActive = 1
        AND (sm.IsDeleted = 0 OR sm.IsDeleted IS NULL)
    ORDER BY 
        sm.FirstName, sm.LastName;
END
GO
/****** Object:  StoredProcedure [dbo].[Subject_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Subject_Create]
    @SubjectName NVARCHAR(100),
    @ClassId UNIQUEIDENTIFIER,
    @IsScholastic BIT,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[SubjectMaster]
    (
        Id,
        SubjectName,
        ClassId,
        IsScholastic,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @SubjectName,
        @ClassId,
        @IsScholastic,
        @IsActive,
        0,
        @CompanyId,
        @SchoolId,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'INC',
        N'Subject Added Successfully'
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[Subject_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Subject_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SubjectMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Subject_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Subject_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        SubjectName,
        ClassId,
        IsScholastic,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[SubjectMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Subject_GetByClassId]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Subject_GetByClassId]
    @ClassId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT s.*
    FROM Subjects s
    INNER JOIN ClassSubjects cs ON s.Id = cs.SubjectId
    WHERE cs.ClassId = @ClassId
    AND s.IsActive = 1
    ORDER BY s.SubjectName
END
GO
/****** Object:  StoredProcedure [dbo].[Subject_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Subject_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        SubjectName,
        ClassId,
        IsScholastic,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[SubjectMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[Subject_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Subject_Update]
    @Id UNIQUEIDENTIFIER,
    @SubjectName NVARCHAR(100),
    @ClassId UNIQUEIDENTIFIER,
    @IsScholastic BIT,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SubjectMaster]
    SET 
        SubjectName = @SubjectName,
        ClassId = @ClassId,
        IsScholastic = @IsScholastic,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
		Status = 'INC',
        StatusMessage = 'Subject Updated Successfully',
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[SubjectCategory_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SubjectCategory_Create]
    @Name NVARCHAR(200),
    @Description NVARCHAR(500),
    @ParentId UNIQUEIDENTIFIER,
    @SubjectId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER,
    @SessionId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    -- Fallback to latest active session if @SessionId isn't provided
    IF (@SessionId IS NULL)
    BEGIN
        SELECT TOP 1 @SessionId = Id
        FROM [dbo].[SessionMaster]
        WHERE IsActive = 1
        ORDER BY CreatedDate DESC;

        IF (@SessionId IS NULL)
        BEGIN
            SELECT TOP 1 @SessionId = Id
            FROM [dbo].[SessionMaster]
            ORDER BY CreatedDate DESC;
        END
    END

    INSERT INTO [dbo].[SubjectCategoryDetails]
    (
        Id,
        Name,
        Description,
        ParentId,
        SubjectId,
        SessionId,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @Name,
        @Description,
        @ParentId,
        @SubjectId,
        @SessionId,
        @IsActive,
        0,
        @CompanyId,
        @SchoolId,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[SubjectCategory_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SubjectCategory_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SubjectCategoryDetails]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[SubjectCategory_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SubjectCategory_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Description,
        ParentId,
        SubjectId,
        SessionId,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[SubjectCategoryDetails]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[SubjectCategory_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SubjectCategory_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Description,
        ParentId,
        SubjectId,
        SessionId,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[SubjectCategoryDetails]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[SubjectCategory_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SubjectCategory_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @Description NVARCHAR(500),
    @ParentId UNIQUEIDENTIFIER,
    @SubjectId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER,
    @SessionId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- If session not provided, keep existing stored value
    IF (@SessionId IS NULL)
    BEGIN
        SELECT @SessionId = SessionId FROM [dbo].[SubjectCategoryDetails] WHERE Id = @Id;
    END

    UPDATE [dbo].[SubjectCategoryDetails]
    SET 
        Name = @Name,
        Description = @Description,
        ParentId = @ParentId,
        SubjectId = @SubjectId,
        SessionId = @SessionId,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Supplier_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_Create]
    @Name NVARCHAR(200),
    @Description NVARCHAR(500) = NULL,
    @Address1 NVARCHAR(255) = NULL,
    @Address2 NVARCHAR(255) = NULL,
    @CityId UNIQUEIDENTIFIER,
    @StateId UNIQUEIDENTIFIER,
    @CountryId UNIQUEIDENTIFIER,
    @ZipCode NVARCHAR(50) = NULL,
    @PhonbeNumber NVARCHAR(50) = NULL,
    @MobileNumber NVARCHAR(50) = NULL,
    @EmailId NVARCHAR(150) = NULL,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[SupplierMaster]
    (
        Id,
        Name,
        Description,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        PhonbeNumber,
        MobileNumber,
        EmailId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @Name,
        @Description,
        @Address1,
        @Address2,
        @CityId,
        @StateId,
        @CountryId,
        @ZipCode,
        @PhonbeNumber,
        @MobileNumber,
        @EmailId,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[Supplier_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SupplierMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Supplier_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Description,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        PhonbeNumber,
        MobileNumber,
        EmailId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[SupplierMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Supplier_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Description,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        PhonbeNumber,
        MobileNumber,
        EmailId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[SupplierMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[Supplier_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @Description NVARCHAR(500) = NULL,
    @Address1 NVARCHAR(255) = NULL,
    @Address2 NVARCHAR(255) = NULL,
    @CityId UNIQUEIDENTIFIER,
    @StateId UNIQUEIDENTIFIER,
    @CountryId UNIQUEIDENTIFIER,
    @ZipCode NVARCHAR(50) = NULL,
    @PhonbeNumber NVARCHAR(50) = NULL,
    @MobileNumber NVARCHAR(50) = NULL,
    @EmailId NVARCHAR(150) = NULL,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SupplierMaster]
    SET 
        Name = @Name,
        Description = @Description,
        Address1 = @Address1,
        Address2 = @Address2,
        CityId = @CityId,
        StateId = @StateId,
        CountryId = @CountryId,
        ZipCode = @ZipCode,
        PhonbeNumber = @PhonbeNumber,
        MobileNumber = @MobileNumber,
        EmailId = @EmailId,
        CompanyId = @CompanyId,
        SchoolId = @SchoolId,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[SystemParameters_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SystemParameters_Create]
    @ParameterName      VARCHAR(50),
    @ParameterValue     VARCHAR(255) = NULL,
    @Description        VARCHAR(1000) = NULL,
    @CompanyId          UNIQUEIDENTIFIER,
    @SchoolId           UNIQUEIDENTIFIER,
    @IsActive           BIT,
    @CreatedBy          UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[SystemParameters]
    (
        [Id],
        [ParameterName],
        [ParameterValue],
        [Description],
        [CompanyId],
        [SchoolId],
        [IsActive],
        [IsDeleted],
        [CreatedBy],
        [CreatedDate]
    )
    VALUES
    (
        @NewId,
        @ParameterName,
        @ParameterValue,
        @Description,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        GETUTCDATE()
    );

    SELECT @NewId AS [Id];
END
GO
/****** Object:  StoredProcedure [dbo].[SystemParameters_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SystemParameters_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SystemParameters]
    SET [IsDeleted] = 1,
        [ModifiedDate] = GETUTCDATE()
    WHERE [Id] = @Id AND ISNULL([IsDeleted], 0) = 0;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[SystemParameters_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SystemParameters_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [ParameterName],
        [ParameterValue],
        [Description],
        [CompanyId],
        [SchoolId],
        [IsActive],
        [IsDeleted],
        [CreatedBy],
        [CreatedDate],
        [ModifiedBy],
        [ModifiedDate],
        [Status],
        [StatusMessage]
    FROM [dbo].[SystemParameters]
    WHERE ISNULL([IsDeleted], 0) = 0
    ORDER BY [ParameterName];
END
GO
/****** Object:  StoredProcedure [dbo].[SystemParameters_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SystemParameters_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [ParameterName],
        [ParameterValue],
        [Description],
        [CompanyId],
        [SchoolId],
        [IsActive],
        [IsDeleted],
        [CreatedBy],
        [CreatedDate],
        [ModifiedBy],
        [ModifiedDate],
        [Status],
        [StatusMessage]
    FROM [dbo].[SystemParameters]
    WHERE [Id] = @Id AND ISNULL([IsDeleted], 0) = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[SystemParameters_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SystemParameters_Update]
    @Id                 UNIQUEIDENTIFIER,
    @ParameterName      VARCHAR(50),
    @ParameterValue     VARCHAR(255) = NULL,
    @Description        VARCHAR(1000) = NULL,
    @CompanyId          UNIQUEIDENTIFIER,
    @SchoolId           UNIQUEIDENTIFIER,
    @IsActive           BIT,
    @ModifiedBy         UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SystemParameters]
    SET 
        [ParameterName] = @ParameterName,
        [ParameterValue] = @ParameterValue,
        [Description]    = @Description,
        [CompanyId]      = @CompanyId,
        [SchoolId]       = @SchoolId,
        [IsActive]       = @IsActive,
        [ModifiedBy]     = @ModifiedBy,
        [ModifiedDate]   = GETUTCDATE()
    WHERE [Id] = @Id AND ISNULL([IsDeleted], 0) = 0;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Teacher_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Teacher_Create]
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @DOB DATETIME,
    @DOJ DATETIME NULL,
    @DateOfLeaving DATETIME NULL,
    @Address NVARCHAR(250),
    @CityId UNIQUEIDENTIFIER NULL,
    @StateId UNIQUEIDENTIFIER NULL,
    @CountryId UNIQUEIDENTIFIER NULL,
    @ZipCode NVARCHAR(20),
    @Gender UNIQUEIDENTIFIER NULL,
    @MaritalStatusId UNIQUEIDENTIFIER NULL,
    @Image NVARCHAR(500),
    @Email NVARCHAR(150),
    @Phone NVARCHAR(50),
    @MobilePhone NVARCHAR(50),
    @YearsOfExperience NVARCHAR(50),
    @PreviousSchool NVARCHAR(150),
    @Salutation NVARCHAR(50),
    @IsActive BIT,
    @IsDeleted BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER,
    @Status NVARCHAR(50),
    @StatusMessage NVARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[TeacherMaster]
    (
        Id,
        FirstName,
        LastName,
        DOB,
        DOJ,
        DateOfLeaving,
        Address,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        Gender,
        MaritalStatusId,
        Image,
        Email,
        Phone,
        MobilePhone,
        YearsOfExperience,
        PreviousSchool,
        Salutation,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @FirstName,
        @LastName,
        @DOB,
        @DOJ,
        @DateOfLeaving,
        @Address,
        @CityId,
        @StateId,
        @CountryId,
        @ZipCode,
        @Gender,
        @MaritalStatusId,
        @Image,
        @Email,
        @Phone,
        @MobilePhone,
        @YearsOfExperience,
        @PreviousSchool,
        @Salutation,
        @IsActive,
        @IsDeleted,
        @CompanyId,
        @SchoolId,
        @CreatedBy,
        SYSUTCDATETIME(),
        @Status,
        @StatusMessage
    );

    -- Sync EmpMaster on create
    IF NOT EXISTS (SELECT 1 FROM [dbo].[EmpMaster] WHERE Id = @NewId)
    BEGIN
        INSERT INTO [dbo].[EmpMaster]
        (
            Id,
            FirstName,
            LastName,
            EmailId,
            PhoneNumber,
            DOB,
            CompanyId,
            SchoolId,
            IsActive,
            IsDeleted,
            CreatedBy,
            CreatedDate
        )
        VALUES
        (
            @NewId,
            @FirstName,
            @LastName,
            @Email,
            @Phone,
            @DOB,
            @CompanyId,
            @SchoolId,
            @IsActive,
            @IsDeleted,
            @CreatedBy,
            SYSUTCDATETIME()
        );
    END
    ELSE
    BEGIN
        UPDATE [dbo].[EmpMaster]
        SET FirstName = @FirstName,
            LastName = @LastName,
            EmailId = @Email,
            PhoneNumber = @Phone,
            DOB = @DOB,
            CompanyId = @CompanyId,
            SchoolId = @SchoolId,
            IsActive = @IsActive,
            IsDeleted = @IsDeleted,
            ModifiedBy = @CreatedBy,
            ModifiedDate = SYSUTCDATETIME()
        WHERE Id = @NewId;
    END

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[Teacher_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Teacher_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TeacherMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    -- Soft delete in EmpMaster as well
    IF EXISTS (SELECT 1 FROM [dbo].[EmpMaster] WHERE Id = @Id)
    BEGIN
        UPDATE [dbo].[EmpMaster]
        SET IsDeleted = 1,
            ModifiedDate = SYSUTCDATETIME()
        WHERE Id = @Id;
    END

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Teacher_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Teacher_GetAll]
    @SchoolId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM TeacherMaster
    WHERE SchoolId = @SchoolId
    AND IsActive = 1
    ORDER BY FirstName, LastName
END
GO
/****** Object:  StoredProcedure [dbo].[Teacher_GetAll_SchoolId]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CReate PROCEDURE [dbo].[Teacher_GetAll_SchoolId]
    @SchoolId uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        FirstName,
        LastName,
        DOB,
        DOJ,
        DateOfLeaving,
        Address,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        Gender,
        MaritalStatusId,
        Image,
        Phone,
        MobilePhone,
        YearsOfExperience,
        PreviousSchool,
        Salutation,
        Email,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[TeacherMaster]
    WHERE SchoolId = @SchoolId and IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Teacher_GetAllActive]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Teacher_GetAllActive]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        t.*,
        c.Name AS CityName,
        s.Name AS StateName,
        co.Name AS CountryName,
        g.Name AS GenderName,
        ms.Name AS MaritalStatus
    FROM 
        TeacherMaster t
    LEFT JOIN 
        CityMaster c ON t.CityId = c.Id
    LEFT JOIN 
        StateMaster s ON t.StateId = s.Id
    LEFT JOIN 
        CountryMaster co ON t.CountryId = co.Id
    LEFT JOIN 
        GenderMaster g ON t.Gender = g.Id
    LEFT JOIN 
        MaritalStatusMaster ms ON t.MaritalStatusId = ms.Id
    WHERE 
        t.IsActive = 1 
        AND t.IsDeleted = 0
    ORDER BY 
        t.FirstName, t.LastName;
END
GO
/****** Object:  StoredProcedure [dbo].[Teacher_GetAllActive_BySchool]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Teacher_GetAllActive_BySchool]
    @SchoolId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        t.*,
        c.Name AS CityName,
        s.Name AS StateName,
        co.Name AS CountryName,
        g.Name AS GenderName,
        ms.Name AS MaritalStatus
    FROM 
        TeacherMaster t
    LEFT JOIN 
        CityMaster c ON t.CityId = c.Id
    LEFT JOIN 
        StateMaster s ON t.StateId = s.Id
    LEFT JOIN 
        CountryMaster co ON t.CountryId = co.Id
    LEFT JOIN 
        GenderMaster g ON t.Gender = g.Id
    LEFT JOIN 
        MaritalStatusMaster ms ON t.MaritalStatusId = ms.Id
    WHERE 
        t.IsActive = 1 
        AND t.IsDeleted = 0
        AND t.SchoolId = @SchoolId
    ORDER BY 
        t.FirstName, t.LastName;
END
GO
/****** Object:  StoredProcedure [dbo].[Teacher_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Teacher_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        FirstName,
        LastName,
        DOB,
        DOJ,
        DateOfLeaving,
        Address,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        Gender,
        MaritalStatusId,
        Image,
        Phone,
        MobilePhone,
        YearsOfExperience,
        PreviousSchool,
        Salutation,
        Email,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[TeacherMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[Teacher_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Teacher_Update]
    @Id UNIQUEIDENTIFIER,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @DOB DATETIME,
    @DOJ DATETIME NULL,
    @DateOfLeaving DATETIME NULL,
    @Address NVARCHAR(250),
    @CityId UNIQUEIDENTIFIER NULL,
    @StateId UNIQUEIDENTIFIER NULL,
    @CountryId UNIQUEIDENTIFIER NULL,
    @ZipCode NVARCHAR(20),
    @Gender UNIQUEIDENTIFIER NULL,
    @MaritalStatusId UNIQUEIDENTIFIER NULL,
    @Image NVARCHAR(500),
    @Email NVARCHAR(150),
    @Phone NVARCHAR(50),
    @MobilePhone NVARCHAR(50),
    @YearsOfExperience NVARCHAR(50),
    @PreviousSchool NVARCHAR(150),
    @Salutation NVARCHAR(50),
    @IsActive BIT,
    @IsDeleted BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER,
    @Status NVARCHAR(50),
    @StatusMessage NVARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TeacherMaster]
    SET 
        FirstName = @FirstName,
        LastName = @LastName,
        DOB = @DOB,
        DOJ = @DOJ,
        DateOfLeaving = @DateOfLeaving,
        Address = @Address,
        CityId = @CityId,
        StateId = @StateId,
        CountryId = @CountryId,
        ZipCode = @ZipCode,
        Gender = @Gender,
        MaritalStatusId = @MaritalStatusId,
        Image = @Image,
        Email = @Email,
        Phone = @Phone,
        MobilePhone = @MobilePhone,
        YearsOfExperience = @YearsOfExperience,
        PreviousSchool = @PreviousSchool,
        Salutation = @Salutation,
        IsActive = @IsActive,
        IsDeleted = @IsDeleted,
        SchoolId = @SchoolId,
        [Status] = @Status,
        StatusMessage = @StatusMessage,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    -- Sync EmpMaster on update (upsert)
    IF EXISTS (SELECT 1 FROM [dbo].[EmpMaster] WHERE Id = @Id)
    BEGIN
        UPDATE [dbo].[EmpMaster]
        SET FirstName = @FirstName,
            LastName = @LastName,
            EmailId = @Email,
            PhoneNumber = @Phone,
            DOB = @DOB,
            CompanyId = CompanyId, -- keep existing unless you want to change via teacher
            SchoolId = @SchoolId,
            IsActive = @IsActive,
            IsDeleted = @IsDeleted,
            ModifiedBy = @ModifiedBy,
            ModifiedDate = SYSUTCDATETIME()
        WHERE Id = @Id;
    END
    ELSE
    BEGIN
        INSERT INTO [dbo].[EmpMaster]
        (
            Id,
            FirstName,
            LastName,
            EmailId,
            PhoneNumber,
            DOB,
            CompanyId,
            SchoolId,
            IsActive,
            IsDeleted,
            CreatedBy,
            CreatedDate
        )
        VALUES
        (
            @Id,
            @FirstName,
            @LastName,
            @Email,
            @Phone,
            @DOB,
            (SELECT CompanyId FROM [dbo].[TeacherMaster] WHERE Id = @Id),
            @SchoolId,
            @IsActive,
            @IsDeleted,
            @ModifiedBy,
            SYSUTCDATETIME()
        );
    END

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[TeacherClassDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherClassDetails_Create]
    @TeacherId UNIQUEIDENTIFIER,
    @ClassId UNIQUEIDENTIFIER,
    @SectionId UNIQUEIDENTIFIER,
    @SubjectId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[TeacherClassDetails]
    (
        Id,
        TeacherId,
        ClassId,
        SectionId,
        SubjectId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @TeacherId,
        @ClassId,
        @SectionId,
        @SubjectId,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[TeacherClassDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherClassDetails_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TeacherClassDetails]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[TeacherClassDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherClassDetails_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        TeacherId,
        ClassId,
        SectionId,
        SubjectId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[TeacherClassDetails]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[TeacherClassDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherClassDetails_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        TeacherId,
        ClassId,
        SectionId,
        SubjectId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[TeacherClassDetails]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[TeacherClassDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherClassDetails_Update]
    @Id UNIQUEIDENTIFIER,
    @TeacherId UNIQUEIDENTIFIER,
    @ClassId UNIQUEIDENTIFIER,
    @SectionId UNIQUEIDENTIFIER,
    @SubjectId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TeacherClassDetails]
    SET 
        TeacherId = @TeacherId,
        ClassId = @ClassId,
        SectionId = @SectionId,
        SubjectId = @SubjectId,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[TeacherDocumentDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherDocumentDetails_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        TeacherId,
        Name,
        Description,
        FileName,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM dbo.TeacherDocumentDetails WITH (NOLOCK)
    WHERE (IsDeleted = 0 OR IsDeleted IS NULL)
  AND (IsActive = 1 OR IsActive IS NULL);
END;
GO
/****** Object:  StoredProcedure [dbo].[TeacherDocumentDetails_GetByTeacher]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherDocumentDetails_GetByTeacher]
    @TeacherId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        TeacherId,
        Name,
        Description,
        FileName,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM dbo.TeacherDocumentDetails WITH (NOLOCK)
    WHERE TeacherId = @TeacherId
      AND (IsDeleted = 0 OR IsDeleted IS NULL);
END;
GO
/****** Object:  StoredProcedure [dbo].[TeacherQualificationDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherQualificationDetails_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        TeacherId,
        QualificationId,
        SchoolId,
        CompanyId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM dbo.TeacherQualificationDetails WITH (NOLOCK)
    WHERE (IsDeleted = 0 OR IsDeleted IS NULL);
END;
GO
/****** Object:  StoredProcedure [dbo].[TeacherQualificationDetails_GetByTeacher]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherQualificationDetails_GetByTeacher]
    @TeacherId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        TeacherId,
        QualificationId,
        SchoolId,
        CompanyId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM dbo.TeacherQualificationDetails WITH (NOLOCK)
    WHERE TeacherId = @TeacherId
      AND (IsDeleted = 0 OR IsDeleted IS NULL);
END;
GO
/****** Object:  StoredProcedure [dbo].[TeacherSectionDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherSectionDetails_Create]
    @TeacherId      UNIQUEIDENTIFIER,
    @ClassId        UNIQUEIDENTIFIER,
    @SectionId      UNIQUEIDENTIFIER,
    @SubjectId      UNIQUEIDENTIFIER,
    @IsClassTeacher BIT,
    @IsActive       BIT,
    @CompanyId      UNIQUEIDENTIFIER,
    @SchoolId       UNIQUEIDENTIFIER,
    @CreatedBy      UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[TeacherSectionDetails]
    (
        Id,
        TeacherId,
        ClassId,
        SectionId,
        SubjectId,
        IsClassTeacher,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @TeacherId,
        @ClassId,
        @SectionId,
        @SubjectId,
        @IsClassTeacher,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[TeacherSectionDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherSectionDetails_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TeacherSectionDetails]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[TeacherSectionDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherSectionDetails_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        TeacherId,
        ClassId,
        SectionId,
        SubjectId,
        IsClassTeacher,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[TeacherSectionDetails]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[TeacherSectionDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherSectionDetails_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        TeacherId,
        ClassId,
        SectionId,
        SubjectId,
        IsClassTeacher,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[TeacherSectionDetails]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[TeacherSectionDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherSectionDetails_Update]
    @Id             UNIQUEIDENTIFIER,
    @TeacherId      UNIQUEIDENTIFIER,
    @ClassId        UNIQUEIDENTIFIER,
    @SectionId      UNIQUEIDENTIFIER,
    @SubjectId      UNIQUEIDENTIFIER,
    @IsClassTeacher BIT,
    @IsActive       BIT,
    @SchoolId       UNIQUEIDENTIFIER,
    @ModifiedBy     UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TeacherSectionDetails]
    SET 
        TeacherId      = @TeacherId,
        ClassId        = @ClassId,
        SectionId      = @SectionId,
        SubjectId      = @SubjectId,
        IsClassTeacher = @IsClassTeacher,
        IsActive       = @IsActive,
        SchoolId       = @SchoolId,
        ModifiedBy     = @ModifiedBy,
        ModifiedDate   = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[TeacherSubjectDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherSubjectDetails_Create]
    @TeacherId UNIQUEIDENTIFIER,
    @SubjectId UNIQUEIDENTIFIER,
    @ClassId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[TeacherSubjectDetails]
    (
        Id,
        TeacherId,
        SubjectId,
        ClassId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @TeacherId,
        @SubjectId,
        @ClassId,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[TeacherSubjectDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherSubjectDetails_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TeacherSubjectDetails]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[TeacherSubjectDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherSubjectDetails_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        TeacherId,
        SubjectId,
        ClassId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[TeacherSubjectDetails]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[TeacherSubjectDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherSubjectDetails_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        TeacherId,
        SubjectId,
        ClassId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[TeacherSubjectDetails]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[TeacherSubjectDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherSubjectDetails_Update]
    @Id UNIQUEIDENTIFIER,
    @TeacherId UNIQUEIDENTIFIER,
    @SubjectId UNIQUEIDENTIFIER,
    @ClassId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TeacherSubjectDetails]
    SET 
        TeacherId = @TeacherId,
        SubjectId = @SubjectId,
        ClassId = @ClassId,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create a new time table period
CREATE   PROCEDURE [dbo].[TimeTablePeriod_Create]
    @Id UNIQUEIDENTIFIER,
    @ClassId UNIQUEIDENTIFIER,
    @SectionId UNIQUEIDENTIFIER,
    @SubjectId UNIQUEIDENTIFIER,
    @PeriodId UNIQUEIDENTIFIER,
    @DayOfWeek INT,
    @SessionId UNIQUEIDENTIFIER,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO TimeTableClassPeriodDetails (
        Id,
        ClassId,
        SectionId,
        SubjectId,
        PeriodId,
        DayOfWeek,
        SessionId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        Status,
        StatusMessage
    ) VALUES (
        @Id,
        @ClassId,
        @SectionId,
        @SubjectId,
        @PeriodId,
        @DayOfWeek,
        @SessionId,
        @CompanyId,
        @SchoolId,
        1, -- IsActive
        0, -- IsDeleted
        @CreatedBy,
        GETDATE(),
        'Active', -- Default status
        NULL -- No status message by default
    );
    
    SELECT @Id AS Id;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Delete a time table period (soft delete)
CREATE   PROCEDURE [dbo].[TimeTablePeriod_Delete]
    @Id UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE TimeTableClassPeriodDetails
    SET 
        IsDeleted = 1,
        IsActive = 0,
        Status = 'Deleted',
        ModifiedBy = @ModifiedBy,
        ModifiedDate = GETDATE()
    WHERE Id = @Id;
    
    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_DeleteByClassSectionAndAcademicYear]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[TimeTablePeriod_DeleteByClassSectionAndAcademicYear]
    @ClassId UNIQUEIDENTIFIER,
    @SectionId UNIQUEIDENTIFIER,
    @AcademicYearId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE TimeTableClassPeriodDetails
    SET 
        IsDeleted = 1,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = GETUTCDATE()
    WHERE 
        ClassId = @ClassId
        AND SectionId = @SectionId
       --AND AcademicYearId = @AcademicYearId
        AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Get all time table periods
CREATE   PROCEDURE [dbo].[TimeTablePeriod_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM TimeTableClassPeriodDetails WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_GetByClassSectionAndAcademicYear]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- TimeTablePeriod_GetByClassSectionAndAcademicYear
-- =============================================
CREATE   PROCEDURE [dbo].[TimeTablePeriod_GetByClassSectionAndAcademicYear]
    @ClassId UNIQUEIDENTIFIER,
    @SectionId UNIQUEIDENTIFIER,
    @AcademicYearId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ttp.*,
        s.SubjectName,
        u.FirstName + ' ' + u.LastName AS TeacherName
    FROM 
        [dbo].[TimeTableClassPeriodDetails] ttp
    LEFT JOIN 
        [dbo].[SubjectMaster] s ON ttp.SubjectId = s.Id
    LEFT JOIN 
        [dbo].[UserMaster] u ON ttp.TeacherId = u.Id
    WHERE 
        ttp.ClassId = @ClassId
        AND ttp.SectionId = @SectionId
        AND ttp.SessionId = @AcademicYearId
        AND ttp.IsDeleted = 0
    ORDER BY 
        ttp.DayOfWeek, ttp.CreatedDate;
END;
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Get time table period by ID
CREATE   PROCEDURE [dbo].[TimeTablePeriod_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM TimeTableClassPeriodDetails WHERE Id = @Id AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_GetBySetupId]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[TimeTablePeriod_GetBySetupId]
    @SetupId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        ttp.*
    FROM 
        TimeTableClassPeriodDetails ttp
    WHERE 
        ttp.Id = @SetupId
        AND ttp.IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_GetBySubjectId]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- TimeTablePeriod_GetBySubjectId
-- =============================================
CREATE   PROCEDURE [dbo].[TimeTablePeriod_GetBySubjectId]
    @SubjectId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ttp.*,
        s.SubjectName,
        u.FirstName + ' ' + u.LastName AS TeacherName,
        c.ClassName,
        sec.SectionName,
        tm.PeriodNumber,
        tm.StartTime,
        tm.EndTime
    FROM 
        [dbo].[TimeTableClassPeriodDetails] ttp
    INNER JOIN 
        [dbo].[SubjectMaster] s ON ttp.SubjectId = s.Id
    LEFT JOIN 
        [dbo].[UserMaster] u ON ttp.TeacherId = u.Id
    INNER JOIN
        [dbo].[ClassMaster] c ON ttp.ClassId = c.Id
    INNER JOIN
        [dbo].[SectionMaster] sec ON ttp.SectionId = sec.Id
    INNER JOIN
        [dbo].[TimeTablePeriodMaster] tm ON ttp.PeriodId = tm.Id
    WHERE 
        ttp.SubjectId = @SubjectId
        AND ttp.IsDeleted = 0
    ORDER BY 
        ttp.DayOfWeek, tm.StartTime;
END;
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_GetByTeacherId]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- TimeTablePeriod_GetByTeacherId
-- =============================================
CREATE   PROCEDURE [dbo].[TimeTablePeriod_GetByTeacherId]
    @TeacherId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ttp.*,
        s.SubjectName,
        u.FirstName + ' ' + u.LastName AS TeacherName,
        c.ClassName,
        sec.SectionName,
        tm.PeriodNumber,
        tm.StartTime,
        tm.EndTime
    FROM 
        [dbo].[TimeTableClassPeriodDetails] ttp
    INNER JOIN 
        [dbo].[SubjectMaster] s ON ttp.SubjectId = s.Id
    INNER JOIN 
        [dbo].[UserMaster] u ON ttp.TeacherId = u.Id
    INNER JOIN
        [dbo].[ClassMaster] c ON ttp.ClassId = c.Id
    INNER JOIN
        [dbo].[SectionMaster] sec ON ttp.SectionId = sec.Id
    INNER JOIN
        [dbo].[TimeTablePeriodMaster] tm ON ttp.PeriodId = tm.Id
    WHERE 
        ttp.TeacherId = @TeacherId
        AND ttp.IsDeleted = 0
    ORDER BY 
        ttp.DayOfWeek, tm.StartTime;
END;
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_Insert]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      <Author,,Name>
-- Create date: <Create Date,,>
-- Description: Stored procedures for TimeTablePeriod operations
-- =============================================

-- =============================================
-- TimeTablePeriod_Insert
-- =============================================
CREATE   PROCEDURE [dbo].[TimeTablePeriod_Insert]
    @Id UNIQUEIDENTIFIER,
    @ClassId UNIQUEIDENTIFIER,
    @SectionId UNIQUEIDENTIFIER,
    @SubjectId UNIQUEIDENTIFIER,
    @TeacherId UNIQUEIDENTIFIER = NULL,
    @PeriodId UNIQUEIDENTIFIER,
    @DayOfWeek INT,
    @SessionId UNIQUEIDENTIFIER,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT = 1,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[TimeTableClassPeriodDetails]
    (
        [Id],
        [ClassId],
        [SectionId],
        [SubjectId],
        [PeriodId],
        [DayOfWeek],
        [SessionId],
        [CompanyId],
        [SchoolId],
        [IsActive],
        [IsDeleted],
        [CreatedBy],
        [CreatedDate],
        [Status],
        [StatusMessage]
    )
    VALUES
    (
        @Id,
        @ClassId,
        @SectionId,
        @SubjectId,
        @PeriodId,
        @DayOfWeek,
        @SessionId,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0, -- IsDeleted
        @CreatedBy,
        GETUTCDATE(),
        'ACT', -- Status
        'Active' -- StatusMessage
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_IsClassroomAvailable]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- TimeTablePeriod_IsClassroomAvailable
-- =============================================
CREATE   PROCEDURE [dbo].[TimeTablePeriod_IsClassroomAvailable]
    @ClassroomId UNIQUEIDENTIFIER,
    @DayOfWeek INT,
    @StartTime TIME,
    @EndTime TIME,
    @ExcludePeriodId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IsAvailable BIT = 1;

    IF EXISTS (
        SELECT 1
        FROM [dbo].[TimeTableClassPeriodDetails] ttp
        INNER JOIN [dbo].[TimeTablePeriodMaster] tm ON ttp.PeriodId = tm.Id
        WHERE ttp.ClassId = @ClassroomId
          AND ttp.DayOfWeek = @DayOfWeek
          AND ttp.Id <> ISNULL(@ExcludePeriodId, '00000000-0000-0000-0000-000000000000')
          AND ttp.IsDeleted = 0
          AND (
              (@StartTime >= tm.StartTime AND @StartTime < tm.EndTime) OR
              (@EndTime > tm.StartTime AND @EndTime <= tm.EndTime) OR
              (@StartTime <= tm.StartTime AND @EndTime >= tm.EndTime)
          )
    )
    BEGIN
        SET @IsAvailable = 0;
    END

    SELECT @IsAvailable AS IsAvailable;
END;
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_IsTeacherAvailable]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- TimeTablePeriod_IsTeacherAvailable
-- =============================================
CREATE   PROCEDURE [dbo].[TimeTablePeriod_IsTeacherAvailable]
    @TeacherId UNIQUEIDENTIFIER,
    @DayOfWeek INT,
    @StartTime TIME,
    @EndTime TIME,
    @ExcludePeriodId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IsAvailable BIT = 1;

    IF EXISTS (
        SELECT 1
        FROM [dbo].[TimeTableClassPeriodDetails] ttp
        INNER JOIN [dbo].[TimeTablePeriodMaster] tm ON ttp.PeriodId = tm.Id
        WHERE ttp.DayOfWeek = @DayOfWeek
          AND ttp.Id <> ISNULL(@ExcludePeriodId, '00000000-0000-0000-0000-000000000000')
          AND ttp.IsDeleted = 0
          AND (
              (@StartTime >= tm.StartTime AND @StartTime < tm.EndTime) OR
              (@EndTime > tm.StartTime AND @EndTime <= tm.EndTime) OR
              (@StartTime <= tm.StartTime AND @EndTime >= tm.EndTime)
          )
    )
    BEGIN
        SET @IsAvailable = 0;
    END

    SELECT @IsAvailable AS IsAvailable;
END;
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_Save]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[TimeTablePeriod_Save]
    @Id UNIQUEIDENTIFIER,
    @ClassId UNIQUEIDENTIFIER,
    @SectionId UNIQUEIDENTIFIER,
    @SubjectId UNIQUEIDENTIFIER,
    @PeriodId UNIQUEIDENTIFIER,
    @DayOfWeek INT,
    @SessionId UNIQUEIDENTIFIER,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT = 1,
    @IsDeleted BIT = 0,
    @CreatedBy UNIQUEIDENTIFIER,
    @CreatedDate DATETIME,
    @Status NVARCHAR(50) = 'ACT',
    @StatusMessage NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM TimeTableClassPeriodDetails WHERE Id = @Id)
    BEGIN
        -- Insert new record
        INSERT INTO TimeTableClassPeriodDetails (
            Id,
            ClassId,
            SectionId,
            SubjectId,
            PeriodId,
            DayOfWeek,
            SessionId,
            CompanyId,
            SchoolId,
            IsActive,
            IsDeleted,
            CreatedBy,
            CreatedDate,
            Status,
            StatusMessage
        ) VALUES (
            @Id,
            @ClassId,
            @SectionId,
            @SubjectId,
            @PeriodId,
            @DayOfWeek,
            @SessionId,
            @CompanyId,
            @SchoolId,
            @IsActive,
            @IsDeleted,
            @CreatedBy,
            @CreatedDate,
            @Status,
            @StatusMessage
        );
    END
    ELSE
    BEGIN
        -- Update existing record
        UPDATE TimeTableClassPeriodDetails
        SET 
            ClassId = @ClassId,
            SectionId = @SectionId,
            SubjectId = @SubjectId,
            PeriodId = @PeriodId,
            DayOfWeek = @DayOfWeek,
            SessionId = @SessionId,
            CompanyId = @CompanyId,
            SchoolId = @SchoolId,
            IsActive = @IsActive,
            IsDeleted = @IsDeleted,
            ModifiedBy = @CreatedBy,
            ModifiedDate = GETUTCDATE(),
            Status = @Status,
            StatusMessage = @StatusMessage
        WHERE 
            Id = @Id;
    END
    
    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriod_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Update an existing time table period
CREATE   PROCEDURE [dbo].[TimeTablePeriod_Update]
    @Id UNIQUEIDENTIFIER,
    @ClassId UNIQUEIDENTIFIER,
    @SectionId UNIQUEIDENTIFIER,
    @SubjectId UNIQUEIDENTIFIER,
    @PeriodId UNIQUEIDENTIFIER,
    @DayOfWeek INT,
    @SessionId UNIQUEIDENTIFIER,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @Status NVARCHAR(50) = NULL,
    @StatusMessage NVARCHAR(MAX) = NULL,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE TimeTableClassPeriodDetails
    SET 
        ClassId = @ClassId,
        SectionId = @SectionId,
        SubjectId = @SubjectId,
        PeriodId = @PeriodId,
        DayOfWeek = @DayOfWeek,
        SessionId = @SessionId,
        CompanyId = @CompanyId,
        SchoolId = @SchoolId,
        IsActive = @IsActive,
        Status = @Status,
        StatusMessage = @StatusMessage,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = GETDATE()
    WHERE Id = @Id;
    
    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriodMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TimeTablePeriodMaster_Create]
    @Description   NVARCHAR(200),
    @PeriodNumber  NVARCHAR(50),
    @StartTime     TIME,
    @EndTime       TIME,
    @SessionId     UNIQUEIDENTIFIER,
    @CompanyId     UNIQUEIDENTIFIER,
    @SchoolId      UNIQUEIDENTIFIER,
    @IsActive      BIT,
    @CreatedBy     UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[TimeTablePeriodMaster]
    (
        Id,
        [Description],
        PeriodNumber,
        StartTime,
        EndTime,
        SessionId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @Description,
        @PeriodNumber,
        @StartTime,
        @EndTime,
        @SessionId,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriodMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TimeTablePeriodMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TimeTablePeriodMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriodMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TimeTablePeriodMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        [Description],
        PeriodNumber,
        StartTime,
        EndTime,
        SessionId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[TimeTablePeriodMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriodMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TimeTablePeriodMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        [Description],
        PeriodNumber,
        StartTime,
        EndTime,
        SessionId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[TimeTablePeriodMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriodMaster_GetBySetupId]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[TimeTablePeriodMaster_GetBySetupId]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        ttp.Id,
        ttp.Description,
        ttp.StartTime,
        ttp.EndTime,
        ttp.PeriodNumber,
        ttp.IsActive,
        ttp.IsDeleted,
        ttp.CreatedBy,
        ttp.CreatedDate,
        ttp.ModifiedBy,
        ttp.ModifiedDate,
        ttp.Status,
        ttp.StatusMessage
    FROM 
        TimeTablePeriodMaster ttp
    WHERE 
        ttp.IsDeleted = 0
    ORDER BY 
        ttp.StartTime;
END


select * from TimeTablePeriodMaster
GO
/****** Object:  StoredProcedure [dbo].[TimeTablePeriodMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TimeTablePeriodMaster_Update]
    @Id           UNIQUEIDENTIFIER,
    @Description  NVARCHAR(200),
    @PeriodNumber NVARCHAR(50),
    @StartTime    TIME,
    @EndTime      TIME,
    @SessionId    UNIQUEIDENTIFIER,
    @IsActive     BIT,
    @SchoolId     UNIQUEIDENTIFIER,
    @ModifiedBy   UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TimeTablePeriodMaster]
    SET 
        [Description] = @Description,
        PeriodNumber  = @PeriodNumber,
        StartTime     = @StartTime,
        EndTime       = @EndTime,
        SessionId     = @SessionId,
        IsActive      = @IsActive,
        SchoolId      = @SchoolId,
        ModifiedBy    = @ModifiedBy,
        ModifiedDate  = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTableSetupDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TimeTableSetupDetails_Create]
    @SchoolStartTime         TIME,
    @SchoolEndTime           TIME,
    @PeriodStartTime         TIME,
    @TotalPeriods            INT,
    @PeriodDuration          INT,
    @RecessDuration          INT,
    @RecessAfterPeriod       INT,
    @FruitRecessDuration     INT      = NULL,
    @FruitRecessAfterPeriod  INT      = NULL,
    @SessionId               UNIQUEIDENTIFIER,
    @CompanyId               UNIQUEIDENTIFIER,
    @SchoolId                UNIQUEIDENTIFIER,
    @IsActive                BIT,
    @CreatedBy               UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[TimeTableSetupDetails]
    (
        Id,
        SchoolStartTime,
        SchoolEndTime,
        PeriodStartTime,
        TotalPeriods,
        PeriodDuration,
        RecessDuration,
        RecessAfterPeriod,
        FruitRecessDuration,
        FruitRecessAfterPeriod,
        SessionId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @SchoolStartTime,
        @SchoolEndTime,
        @PeriodStartTime,
        @TotalPeriods,
        @PeriodDuration,
        @RecessDuration,
        @RecessAfterPeriod,
        @FruitRecessDuration,
        @FruitRecessAfterPeriod,
        @SessionId,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTableSetupDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TimeTableSetupDetails_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TimeTableSetupDetails]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTableSetupDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TimeTableSetupDetails_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        SchoolStartTime,
        SchoolEndTime,
        PeriodStartTime,
        TotalPeriods,
        PeriodDuration,
        RecessDuration,
        RecessAfterPeriod,
        FruitRecessDuration,
        FruitRecessAfterPeriod,
        SessionId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[TimeTableSetupDetails]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTableSetupDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TimeTableSetupDetails_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        SchoolStartTime,
        SchoolEndTime,
        PeriodStartTime,
        TotalPeriods,
        PeriodDuration,
        RecessDuration,
        RecessAfterPeriod,
        FruitRecessDuration,
        FruitRecessAfterPeriod,
        SessionId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[TimeTableSetupDetails]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[TimeTableSetupDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TimeTableSetupDetails_Update]
    @Id                      UNIQUEIDENTIFIER,
    @SchoolStartTime         TIME,
    @SchoolEndTime           TIME,
    @PeriodStartTime         TIME,
    @TotalPeriods            INT,
    @PeriodDuration          INT,
    @RecessDuration          INT,
    @RecessAfterPeriod       INT,
    @FruitRecessDuration     INT      = NULL,
    @FruitRecessAfterPeriod  INT      = NULL,
    @SessionId               UNIQUEIDENTIFIER,
    @IsActive                BIT,
    @SchoolId                UNIQUEIDENTIFIER,
    @ModifiedBy              UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TimeTableSetupDetails]
    SET
        SchoolStartTime        = @SchoolStartTime,
        SchoolEndTime          = @SchoolEndTime,
        PeriodStartTime        = @PeriodStartTime,
        TotalPeriods           = @TotalPeriods,
        PeriodDuration         = @PeriodDuration,
        RecessDuration         = @RecessDuration,
        RecessAfterPeriod      = @RecessAfterPeriod,
        FruitRecessDuration    = @FruitRecessDuration,
        FruitRecessAfterPeriod = @FruitRecessAfterPeriod,
        SessionId              = @SessionId,
        IsActive               = @IsActive,
        SchoolId               = @SchoolId,
        ModifiedBy             = @ModifiedBy,
        ModifiedDate           = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserDetails]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create  PROCEDURE [dbo].[UpdateUserDetails]
    @Id as Uniqueidentifier,
    @UserName as varchar(250),
    @UserPassword as varchar(250),
    @FirstName as varchar(250),
    @LastName as varchar(250),
    @Email as varchar(250),
    @RoleId as uniqueidentifier,
    @DesignationId as uniqueidentifier,
    @CompanyId as uniqueidentifier,
    @SchoolId as uniqueidentifier,
    @IsSuperUser as bit,
    @IsActive as bit,
    @IsDeleted as bit,
    @CreatedBy as uniqueidentifier,
    @CreatedOn as datetime,
    @ModifiedBy as uniqueidentifier,
    @ModifiedOn as datetime
AS
BEGIN
    SET NOCOUNT ON;
    update UserDetails 
        set UserName = @UserName,
        UserPassword = @UserPassword,
        FirstName = @FirstName,
        LastName = @LastName,
        EmailAddress = @Email,
        DesignationId = @DesignationId,
        UserRoleId = @RoleId,
        IsSuperUser = @IsSuperUser,
        CompanyId = @CompanyId,
        SchoolId = @SchoolId,
        IsActive = @IsActive,
        IsDeleted = @IsDeleted,
        CreatedBy = @CreatedBy,
        CreatedDate = @CreatedOn,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = @ModifiedOn,
        Status = 'INC',
        StatusMessage = 'User Updated Successfully'
    where Id = @Id
    
END
GO
/****** Object:  StoredProcedure [dbo].[UserDetails_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserDetails_Create]
    @UserName       NVARCHAR(256),
    @UserPassword   NVARCHAR(256),
    @FirstName      NVARCHAR(200),
    @LastName       NVARCHAR(200) = NULL,
    @EmailAddress   NVARCHAR(256) = NULL,
    @DesignationId  UNIQUEIDENTIFIER,
    @UserRoleId     UNIQUEIDENTIFIER = NULL,
    @IsSuperUser    BIT = 0,
    @CompanyId      UNIQUEIDENTIFIER = NULL,
    @SchoolId       UNIQUEIDENTIFIER = NULL,
    @IsActive       BIT,
    @CreatedBy      UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO dbo.UserDetails
    (
        Id,
        UserName,
        UserPassword,
        FirstName,
        LastName,
        EmailAddress,
        DesignationId,
        UserRoleId,
        IsSuperUser,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        Status,
        StatusMessage
    )
    VALUES
    (
        @NewId,
        ISNULL(@UserName, ''),
        ISNULL(@UserPassword, ''),
        ISNULL(@FirstName, ''),
        ISNULL(@LastName, ''),
        ISNULL(@EmailAddress, ''),
        @DesignationId,
        @UserRoleId,
        ISNULL(@IsSuperUser, 0),
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        GETUTCDATE(),
        '',
        ''
    );

    SELECT @NewId AS Id;
END
GO
/****** Object:  StoredProcedure [dbo].[UserDetails_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserDetails_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.UserDetails WITH (NOLOCK) WHERE Id = @Id AND IsDeleted = 0)
        RETURN 0;

    UPDATE dbo.UserDetails
    SET IsDeleted = 1,
        ModifiedDate = GETUTCDATE()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 0 RETURN 0;
    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[UserDetails_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserDetails_GetAll]    
AS    
BEGIN    
    SET NOCOUNT ON;    
    
    SELECT     
        u.Id,    
  u.FirstName,  
  u.LastName,  
        u.UserName,    
        LTRIM(RTRIM(CONCAT(u.FirstName, ' ', u.LastName))) AS FullName,    
        u.EmailAddress,    
        r.Name as RoleName,    
        d.Name as DesignationName,    
        c.CompanyName,    
        s.Name as SchoolName,    
        u.IsActive,    
        u.IsDeleted,    
        u.CreatedBy,    
        u.CreatedDate,    
        u.ModifiedBy,    
        u.ModifiedDate,    
        u.Status,    
        u.StatusMessage,
		ISNULL(u.IsSuperUser, 0) AS IsSuperUser
    FROM dbo.UserDetails AS u WITH (NOLOCK)    
    LEFT JOIN dbo.RoleMaster AS r WITH (NOLOCK) ON r.Id = u.UserRoleId    
    LEFT JOIN dbo.DesigMaster AS d WITH (NOLOCK) ON d.Id = u.DesignationId    
    LEFT JOIN dbo.CompanyMaster AS c WITH (NOLOCK) ON c.Id = u.CompanyId    
    LEFT JOIN dbo.SchoolMaster AS s WITH (NOLOCK) ON s.Id = u.SchoolId    
    WHERE u.IsDeleted = 0;    
END   
GO
/****** Object:  StoredProcedure [dbo].[UserDetails_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserDetails_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        UserName,
        UserPassword,
        FirstName,
        LastName,
        EmailAddress,
        DesignationId,
        UserRoleId,
        IsSuperUser,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM dbo.UserDetails WITH (NOLOCK)
    WHERE Id = @Id AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[UserDetails_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserDetails_Update]
    @Id             UNIQUEIDENTIFIER,
    @UserName       NVARCHAR(256),
    @UserPassword   NVARCHAR(256),
    @FirstName      NVARCHAR(200),
    @LastName       NVARCHAR(200) = NULL,
    @EmailAddress   NVARCHAR(256) = NULL,
    @DesignationId  UNIQUEIDENTIFIER,
    @UserRoleId     UNIQUEIDENTIFIER = NULL,
    @IsSuperUser    BIT = 0,
    @CompanyId      UNIQUEIDENTIFIER = NULL,
    @SchoolId       UNIQUEIDENTIFIER = NULL,
    @IsActive       BIT,
    @ModifiedBy     UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.UserDetails WITH (NOLOCK) WHERE Id = @Id AND IsDeleted = 0)
        RETURN 0;

    UPDATE dbo.UserDetails
    SET 
        UserName = ISNULL(@UserName, ''),
        UserPassword = ISNULL(@UserPassword, ''),
        FirstName = ISNULL(@FirstName, ''),
        LastName = ISNULL(@LastName, ''),
        EmailAddress = ISNULL(@EmailAddress, ''),
        DesignationId = @DesignationId,
        UserRoleId = @UserRoleId,
        IsSuperUser = ISNULL(@IsSuperUser, 0),
        CompanyId = @CompanyId,
        SchoolId = @SchoolId,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = GETUTCDATE()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 0 RETURN 0;
    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_select_SMSTaskSchedule]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




  
  
  
--usp_select_SMSTaskSchedule 5  
CREATE Procedure [dbo].[usp_select_SMSTaskSchedule]    
(    
 @day As Integer  )    
As    
Begin    
Declare @query varchar(3999)    
  
Set @query='Select SMSTask.STS_ID, 
		   SMSTask.STS_NAME,   
		   SMSTaskSchedule.STS_SCHEDULE_STARTTIME     
   From SMSTaskSchedule Join SMSTask    
   on SMSTaskSchedule.STS_SCHEDULE_TASK_ID=SMSTask.STS_ID'    
If @day=1    
Begin    
  Set @query=@query + ' Where STS_SCHEDULE_Sunday=1'    
End    
Else If @day=2    
Begin    
 Set @query=@query+' Where STS_SCHEDULE_Monday=1'    
End    
Else If @day=3    
Begin    
 Set @query=@query+' Where STS_SCHEDULE_Tuesday=1'    
End    
Else If @day=4    
Begin    
 Set @query=@query+' Where STS_SCHEDULE_Wednesday=1'    
End    
Else If @day=5    
Begin    
 Set @query=@query+' Where STS_SCHEDULE_THRUSDAY=1'    
End    
Else If @day=6    
Begin    
 Set @query=@query+' Where STS_SCHEDULE_Friday=1'    
End    
Else If @day=7    
Begin    
 Set @query=@query+' Where STS_SCHEDULE_Saturday=1'    
End    
 Set @query=@query + ' And STS_STATUS_ID = 1 Order by SMSTaskSchedule.STS_SCHEDULE_STARTTIME'    
--Exec(@query)    
  
  
CREATE TABLE #tempSMSTaskScheduleMain(        
STS_TaskId int,        
STS_TaskName varchar(100),        
 STS_StartTime datetime,        
)    
  
  
   
Insert Into #tempSMSTaskScheduleMain         
Exec(@query)        
     
Update #tempSMSTaskScheduleMain         
Set STS_StartTime = Convert(datetime, Convert(varchar(4),Datepart(year,getdate()))+'-'+Convert(varchar(3),Datepart(month,getdate()))+'-'+Convert(varchar(3),Datepart(day,getdate()))+' '+ RIGHT(STS_StartTime,7))     
From #tempSMSTaskScheduleMain     
  
Select STS_TaskID as STS_TASK_ID,   
STS_TaskName,   
STS_StartTime,
STS_NOTIFICATION_SEND_EMAIL,
STS_NOTIFICATION_SEND_SMS,
STS_NOTIFICATION_RECEIEVER_ID ,
NOTIFICATION_RECEIVER_NAME as STS_NOTIFICATION_RECEIVER,
STS_REPEAT_SCHEDULE,
STS_STATUS_ID ,
STS_LAST_RUN_DATE,
STS_LAST_RUN_STATUS_ID,

 STSSM_NAME  as STS_STATUS
from #tempSMSTaskScheduleMain 
Join SMSTask 
On  STS_TaskID = STS_ID
Join NotificationReceiverMaster 
On STS_NOTIFICATION_RECEIEVER_ID  = NOTIFICATION_RECEIVER_ID 
Join SMSTaskStatusMaster 
on STS_STATUS_ID = STSSM_ID 
Where (STS_LAST_RUN_STATUS_ID is null or  STS_LAST_RUN_STATUS_ID <> 4 )
And (STS_LAST_RUN_DATE is null 
or  
Convert(datetime, Convert(varchar(4),Datepart(year,STS_LAST_RUN_DATE))+'-'+Convert(varchar(3),Datepart(month,STS_LAST_RUN_DATE))+'-'+Convert(varchar(3),Datepart(day,STS_LAST_RUN_DATE)))
<>
Convert(datetime, Convert(varchar(4),Datepart(year,STS_StartTime))+'-'+Convert(varchar(3),Datepart(month,STS_StartTime))+'-'+Convert(varchar(3),Datepart(day,STS_StartTime)))

)
--Where ATS_StartTime>=GETDATE()   
Order By STS_StartTime  

  
Delete From  #tempSMSTaskScheduleMain  
  
End    
 

GO
/****** Object:  StoredProcedure [dbo].[VehicleTypeMaster_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[VehicleTypeMaster_Create]
    @VehicleType NVARCHAR(100),
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[VehicleTypeMaster]
    (
        Id,
        VehicleType,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @VehicleType,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[VehicleTypeMaster_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[VehicleTypeMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[VehicleTypeMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[VehicleTypeMaster_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[VehicleTypeMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleType,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[VehicleTypeMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[VehicleTypeMaster_GetByCompany]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[VehicleTypeMaster_GetByCompany]
    @CompanyId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleType,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[VehicleTypeMaster]
    WHERE CompanyId = @CompanyId AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[VehicleTypeMaster_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[VehicleTypeMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleType,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[VehicleTypeMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[VehicleTypeMaster_GetBySchool]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[VehicleTypeMaster_GetBySchool]
    @SchoolId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleType,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[VehicleTypeMaster]
    WHERE SchoolId = @SchoolId AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[VehicleTypeMaster_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[VehicleTypeMaster_Update]
    @Id UNIQUEIDENTIFIER,
    @VehicleType NVARCHAR(100),
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[VehicleTypeMaster]
    SET 
        VehicleType = @VehicleType,
        CompanyId = @CompanyId,
        SchoolId = @SchoolId,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Vendor_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Vendor_Create]
    @VendorName NVARCHAR(200),
    @Description NVARCHAR(500) = NULL,
    @Address1 NVARCHAR(255) = NULL,
    @Address2 NVARCHAR(255) = NULL,
    @CityId UNIQUEIDENTIFIER,
    @StateId UNIQUEIDENTIFIER,
    @CountryId UNIQUEIDENTIFIER,
    @ZipCode NVARCHAR(50) = NULL,
    @ContactNumber NVARCHAR(50) = NULL,
    @MobileNumber NVARCHAR(50) = NULL,
    @EmailId NVARCHAR(150) = NULL,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[VendorMaster]
    (
        Id,
        VendorName,
        Description,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        ContactNumber,
        MobileNumber,
        EmailId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @VendorName,
        @Description,
        @Address1,
        @Address2,
        @CityId,
        @StateId,
        @CountryId,
        @ZipCode,
        @ContactNumber,
        @MobileNumber,
        @EmailId,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[Vendor_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Vendor_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[VendorMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Vendor_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Vendor_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VendorName,
        Description,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        ContactNumber,
        MobileNumber,
        EmailId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[VendorMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Vendor_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Vendor_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VendorName,
        Description,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        ContactNumber,
        MobileNumber,
        EmailId,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[VendorMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[Vendor_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Vendor_Update]
    @Id UNIQUEIDENTIFIER,
    @VendorName NVARCHAR(200),
    @Description NVARCHAR(500) = NULL,
    @Address1 NVARCHAR(255) = NULL,
    @Address2 NVARCHAR(255) = NULL,
    @CityId UNIQUEIDENTIFIER,
    @StateId UNIQUEIDENTIFIER,
    @CountryId UNIQUEIDENTIFIER,
    @ZipCode NVARCHAR(50) = NULL,
    @ContactNumber NVARCHAR(50) = NULL,
    @MobileNumber NVARCHAR(50) = NULL,
    @EmailId NVARCHAR(150) = NULL,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[VendorMaster]
    SET 
        VendorName = @VendorName,
        Description = @Description,
        Address1 = @Address1,
        Address2 = @Address2,
        CityId = @CityId,
        StateId = @StateId,
        CountryId = @CountryId,
        ZipCode = @ZipCode,
        ContactNumber = @ContactNumber,
        MobileNumber = @MobileNumber,
        EmailId = @EmailId,
        CompanyId = @CompanyId,
        SchoolId = @SchoolId,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Visitor_Create]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Visitor_Create]
    @VehicleNumber NVARCHAR(50),
    @VehicleName   NVARCHAR(100),
    @DateOfEntry   DATETIME,
    @ArrivalTime   TIME,
    @ExitTime      TIME,
    @Purpose       NVARCHAR(255),
    @ContactPerson NVARCHAR(100),
    @Address1      NVARCHAR(255),
    @Address2      NVARCHAR(255),
    @CityId        UNIQUEIDENTIFIER,
    @StateId       UNIQUEIDENTIFIER,
    @CountryId     UNIQUEIDENTIFIER,
    @ZipCode       NVARCHAR(20),
    @CompanyId     UNIQUEIDENTIFIER = NULL,
    @SchoolId      UNIQUEIDENTIFIER = NULL,
    @IsActive      BIT,
    @CreatedBy     UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[VisitorMaster]
    (
        Id,
        VehicleNumber,
        VehicleName,
        DateOfEntry,
        ArrivalTime,
        ExitTime,
        Purpose,
        ContactPerson,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @VehicleNumber,
        @VehicleName,
        @DateOfEntry,
        @ArrivalTime,
        @ExitTime,
        @Purpose,
        @ContactPerson,
        @Address1,
        @Address2,
        @CityId,
        @StateId,
        @CountryId,
        @ZipCode,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,                      -- IsDeleted
        @CreatedBy,
        SYSUTCDATETIME(),
        N'INC',                 -- default as in entity
        N'In Process....'       -- default as in entity
    );

    -- VisitorService.Create expects a DataTable with column "Id"
    SELECT Id = @NewId;
END
GO
/****** Object:  StoredProcedure [dbo].[Visitor_Delete]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Visitor_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[VisitorMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Visitor_GetAll]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Visitor_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleNumber,
        VehicleName,
        DateOfEntry,
        ArrivalTime,
        ExitTime,
        Purpose,
        ContactPerson,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[VisitorMaster]
    WHERE IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Visitor_GetById]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Visitor_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleNumber,
        VehicleName,
        DateOfEntry,
        ArrivalTime,
        ExitTime,
        Purpose,
        ContactPerson,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[VisitorMaster]
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[Visitor_Update]    Script Date: 08-01-2026 18:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Visitor_Update]
    @Id            UNIQUEIDENTIFIER,
    @VehicleNumber NVARCHAR(50),
    @VehicleName   NVARCHAR(100),
    @DateOfEntry   DATETIME,
    @ArrivalTime   TIME,
    @ExitTime      TIME,
    @Purpose       NVARCHAR(255),
    @ContactPerson NVARCHAR(100),
    @Address1      NVARCHAR(255),
    @Address2      NVARCHAR(255),
    @CityId        UNIQUEIDENTIFIER,
    @StateId       UNIQUEIDENTIFIER,
    @CountryId     UNIQUEIDENTIFIER,
    @ZipCode       NVARCHAR(20),
    @CompanyId     UNIQUEIDENTIFIER = NULL,
    @SchoolId      UNIQUEIDENTIFIER = NULL,
    @IsActive      BIT,
    @ModifiedBy    UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[VisitorMaster]
    SET
        VehicleNumber = @VehicleNumber,
        VehicleName   = @VehicleName,
        DateOfEntry   = @DateOfEntry,
        ArrivalTime   = @ArrivalTime,
        ExitTime      = @ExitTime,
        Purpose       = @Purpose,
        ContactPerson = @ContactPerson,
        Address1      = @Address1,
        Address2      = @Address2,
        CityId        = @CityId,
        StateId       = @StateId,
        CountryId     = @CountryId,
        ZipCode       = @ZipCode,
        CompanyId     = @CompanyId,
        SchoolId      = @SchoolId,
        IsActive      = @IsActive,
        ModifiedBy    = @ModifiedBy,
        ModifiedDate  = SYSUTCDATETIME()
    WHERE Id = @Id
      AND IsDeleted = 0;

    RETURN 1;
END
GO
