using SchoolPortal.DBAccess;
using Schoolortal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolPortal.Services.ServiceViewModels;

namespace SchoolPortal.Services
{
    public class LoginService : ILoginService
    {
        public UserDetailsOutput? AuthenticateUser(string userName, string password)
        {
            Proc p = new Proc("AuthenticateUser");
            p["@userName"] = userName;
            p["@password"] = password;
            p["@IsAuthenticated"] = false; // Initialize OUTPUT parameter
            p["@UserId"] = Guid.Empty; // Initialize OUTPUT parameter
            
            // Create DataTables to hold the results
            DataTable userDetailsTable = new DataTable();
            DataTable privilegesTable = new DataTable();
            
            DataSet resultSet = new DataSet();
            resultSet.Tables.Add(userDetailsTable);
            resultSet.Tables.Add(privilegesTable);
            
            p.Exec(resultSet);
            
            // Check the @IsAuthenticated OUTPUT parameter
            var authResult = p.Parameters["@IsAuthenticated"].Value;
            bool isAuthenticated = authResult != null && authResult != DBNull.Value && Convert.ToBoolean(authResult);
            
            if (!isAuthenticated)
            {
                return null;
            }
            
            // Build UserDetails object from results
            UserDetailsOutput userDetails = new UserDetailsOutput();
            
            // Local helpers to safely read varying column names
            string GetString(DataRow r, params string[] names)
            {
                foreach (var n in names)
                {
                    if (r.Table.Columns.Contains(n))
                    {
                        var v = r[n];
                        if (v != null && v != DBNull.Value) return v.ToString() ?? string.Empty;
                    }
                }
                return string.Empty;
            }
            
            // Get user details from first result set (guard for missing tables/columns)
            if (resultSet.Tables.Count > 0 && resultSet.Tables[2].Rows.Count > 0)
            {
                var userRow = resultSet.Tables[2].Rows[0];
                
                // Id: try UserId, then Id
                var idStr = GetString(userRow, "UserId", "Id");
                if (Guid.TryParse(idStr, out var parsedId))
                {
                    userDetails.Id = parsedId;
                }
                
                // Username variants
                userDetails.UserName = GetString(userRow, "UserName", "Username", "Login");
                
                // FullName or compose from FirstName/LastName
                var full = GetString(userRow, "FullName");
                if (string.IsNullOrWhiteSpace(full))
                {
                    var fn = GetString(userRow, "FirstName", "First_Name", "First");
                    var ln = GetString(userRow, "LastName", "Last_Name", "Last");
                    full = string.Join(" ", new[] { fn, ln }.Where(s => !string.IsNullOrWhiteSpace(s)));
                }
                userDetails.FullName = full;
                
                // Email variants
                userDetails.EmailAddress = GetString(userRow, "Email", "EmailAddress", "EmailId", "EmailID");
                
                // IsActive variants (bool or 0/1)
                var activeStr = GetString(userRow, "IsActive", "Active");
                if (bool.TryParse(activeStr, out var activeBool))
                {
                    userDetails.IsActive = activeBool;
                }
                else if (int.TryParse(activeStr, out var activeInt))
                {
                    userDetails.IsActive = activeInt != 0;
                }
            }
            
            // Get privileges from second result set
            if (resultSet.Tables.Count > 1 && resultSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in resultSet.Tables[0].Rows)
                {
                    var privilegeName = GetString(row, "PrivilegeName", "Privilege", "Name");
                    if (!string.IsNullOrEmpty(privilegeName))
                    {
                        userDetails.Privileges.Add(privilegeName);
                    }
                }
            }
            
            return userDetails;
        }

        public string ChangePassword(string userName, string oldPassword, string newPassword)
        {
            Proc p = new Proc("ChangePassword");
            p["@userName"] = userName;
            p["@oldPassword"] = oldPassword;
            p["@newPassword"] = newPassword;
            p.Exec();
            var ret = p.Parameters["@RETURN_VALUE"].Value;
            int code = ret == null || ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return code == 1 ? "Password changed successfully" : "Failed to change password";
        }
    }
}  
