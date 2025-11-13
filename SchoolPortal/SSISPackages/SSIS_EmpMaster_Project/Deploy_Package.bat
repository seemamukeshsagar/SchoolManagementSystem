@echo off
echo SSIS Package Deployment Script
echo ==============================
echo This script deploys the EmpMaster_Import.dtsx package to SQL Server

echo.
echo Prerequisites:
echo 1. SQL Server Integration Services must be installed
echo 2. You must have permissions to deploy SSIS packages
echo 3. Update the variables below before running

echo.
echo Edit the following variables as needed:
echo - Server Name
echo - Package Path
echo - Package Name

echo.
echo Sample deployment command (uncomment to use):
echo # dtutil /FILE "EmpMaster_Import.dtsx" /COPY SQL;"\Packages\EmpMaster_Import" /QUIET

echo.
echo For more information on dtutil, visit:
echo https://learn.microsoft.com/en-us/sql/integration-services/dtutil-utility?view=sql-server-ver17

pause