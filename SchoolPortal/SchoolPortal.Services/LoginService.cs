using SchoolPortal.DBAccess;
using SchoolPortal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SchoolPortal.Services.ServiceViewModels;
using SchoolPortal.Services.IServices;

namespace SchoolPortal.Services
{
    public class LoginService : ILoginService
    {
        public UserDetailsOutput? AuthenticateUser(string userName, string password)
        {
            Proc p = new Proc("AuthenticateUser");
            p.Timeout = 10;
            p["@userName"] = userName;
            p["@password"] = password;
            p["@IsAuthenticated"] = false;
            p["@UserId"] = Guid.Empty;

            var result = (UserDetailsOutput?)p.LoanReader((reader, args) =>
            {
                if (!reader.Read())
                {
                    return null!;
                }

                var userDetails = new UserDetailsOutput();

                string GetStringSafe(params string[] names)
                {
                    foreach (var n in names)
                    {
                        int ord;
                        try { ord = reader.GetOrdinal(n); }
                        catch (IndexOutOfRangeException) { continue; }
                        if (!reader.IsDBNull(ord)) return reader.GetValue(ord)?.ToString() ?? string.Empty;
                    }
                    return string.Empty;
                }

                var idStr = GetStringSafe("UserId", "Id");
                if (Guid.TryParse(idStr, out var parsedId))
                {
                    userDetails.Id = parsedId;
                }

                userDetails.UserName = GetStringSafe("UserName", "Username", "Login");

                var full = GetStringSafe("FullName");
                if (string.IsNullOrWhiteSpace(full))
                {
                    var fn = GetStringSafe("FirstName", "First_Name", "First");
                    var ln = GetStringSafe("LastName", "Last_Name", "Last");
                    full = string.Join(" ", new[] { fn, ln }.Where(s => !string.IsNullOrWhiteSpace(s)));
                }
                userDetails.FullName = full;

                userDetails.EmailAddress = GetStringSafe("Email", "EmailAddress", "EmailId", "EmailID");

                userDetails.DesignationName = GetStringSafe("DesignationName", "Designation", "DesigName");

                userDetails.RoleName = GetStringSafe("RoleName");

                var activeStr = GetStringSafe("IsActive", "Active");
                if (bool.TryParse(activeStr, out var activeBool))
                {
                    userDetails.IsActive = activeBool;
                }
                else if (int.TryParse(activeStr, out var activeInt))
                {
                    userDetails.IsActive = activeInt != 0;
                }

                var companyIdStr = GetStringSafe("CompanyId");
                if (Guid.TryParse(companyIdStr, out var parsedCompanyId))
                {
                    userDetails.CompanyId = parsedCompanyId;
                }

                var schoolIdStr = GetStringSafe("SchoolId");
                if (Guid.TryParse(schoolIdStr, out var parsedSchoolId))
                {
                    userDetails.SchoolId = parsedSchoolId;
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        string GetPrivilegeName()
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var name = reader.GetName(i);
                                if (name.Equals("PrivilegeName", StringComparison.OrdinalIgnoreCase) ||
                                    name.Equals("Privilege", StringComparison.OrdinalIgnoreCase) ||
                                    name.Equals("Name", StringComparison.OrdinalIgnoreCase))
                                {
                                    return reader.IsDBNull(i) ? string.Empty : reader.GetValue(i)?.ToString() ?? string.Empty;
                                }
                            }
                            return string.Empty;
                        }

                        var privilegeName = GetPrivilegeName();
                        if (!string.IsNullOrEmpty(privilegeName))
                        {
                            userDetails.Privileges.Add(privilegeName);
                        }
                    }
                }

                return userDetails;
            }, primeReader: false);

            var authObj = p.Parameters["@IsAuthenticated"].Value;
            bool isAuthenticated = authObj != null && authObj != DBNull.Value && Convert.ToBoolean(authObj);
            if (!isAuthenticated)
            {
                return null;
            }

            return result;
        }

        public Task<UserDetailsOutput?> AuthenticateUserAsync(string userName, string password)
        {
            var tcs = new TaskCompletionSource<UserDetailsOutput?>(TaskCreationOptions.RunContinuationsAsynchronously);

            Proc p = new Proc("AuthenticateUser");
            p.Timeout = 10;
            p["@userName"] = userName;
            p["@password"] = password;
            p["@IsAuthenticated"] = false;
            p["@UserId"] = Guid.Empty;

            AsyncDelegate onCompleted = (cmd, reader, state) =>
            {
                try
                {
                    if (!reader.Read())
                    {
                        tcs.TrySetResult(null);
                        return;
                    }

                    var userDetails = new UserDetailsOutput();

                    string GetStringSafe(params string[] names)
                    {
                        foreach (var n in names)
                        {
                            int ord;
                            try { ord = reader.GetOrdinal(n); }
                            catch (IndexOutOfRangeException) { continue; }
                            if (!reader.IsDBNull(ord)) return reader.GetValue(ord)?.ToString() ?? string.Empty;
                        }
                        return string.Empty;
                    }

                    var idStr = GetStringSafe("UserId", "Id");
                    if (Guid.TryParse(idStr, out var parsedId))
                    {
                        userDetails.Id = parsedId;
                    }

                    userDetails.UserName = GetStringSafe("UserName", "Username", "Login");

                    var full = GetStringSafe("FullName");
                    if (string.IsNullOrWhiteSpace(full))
                    {
                        var fn = GetStringSafe("FirstName", "First_Name", "First");
                        var ln = GetStringSafe("LastName", "Last_Name", "Last");
                        full = string.Join(" ", new[] { fn, ln }.Where(s => !string.IsNullOrWhiteSpace(s)));
                    }
                    userDetails.FullName = full;

                    userDetails.EmailAddress = GetStringSafe("Email", "EmailAddress", "EmailId", "EmailID");
                    userDetails.DesignationName = GetStringSafe("DesignationName", "Designation", "DesigName");
                    userDetails.RoleName = GetStringSafe("RoleName");

                    var activeStr = GetStringSafe("IsActive", "Active");
                    if (bool.TryParse(activeStr, out var activeBool))
                    {
                        userDetails.IsActive = activeBool;
                    }
                    else if (int.TryParse(activeStr, out var activeInt))
                    {
                        userDetails.IsActive = activeInt != 0;
                    }

                    var companyIdStr = GetStringSafe("CompanyId");
                    if (Guid.TryParse(companyIdStr, out var parsedCompanyId))
                    {
                        userDetails.CompanyId = parsedCompanyId;
                    }

                    var schoolIdStr = GetStringSafe("SchoolId");
                    if (Guid.TryParse(schoolIdStr, out var parsedSchoolId))
                    {
                        userDetails.SchoolId = parsedSchoolId;
                    }

                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            string privilegeName = string.Empty;
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var name = reader.GetName(i);
                                if (name.Equals("PrivilegeName", StringComparison.OrdinalIgnoreCase) ||
                                    name.Equals("Privilege", StringComparison.OrdinalIgnoreCase) ||
                                    name.Equals("Name", StringComparison.OrdinalIgnoreCase))
                                {
                                    privilegeName = reader.IsDBNull(i) ? string.Empty : reader.GetValue(i)?.ToString() ?? string.Empty;
                                    break;
                                }
                            }
                            if (!string.IsNullOrEmpty(privilegeName))
                            {
                                userDetails.Privileges.Add(privilegeName);
                            }
                        }
                    }

                    tcs.TrySetResult(userDetails);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            };

            AsyncErrorDelegate onError = (cmd, ex) =>
            {
                tcs.TrySetException(ex);
            };

            p.ExecAsync(asyncState: null, callbackDelegate: onCompleted, errorDel: onError, synchronizationContext: new SynchronizationContext());

            return tcs.Task;
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
