using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;
using SchoolPortal.Services.Models;
using SchoolPortalApp.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IUserDetailsService
    {
        List<UserDetailsListViewModel> GetAll();
        Task<List<UserDetailsListViewModel>> GetAllAsync();
        UserDetails? GetById(Guid id);
        Task<UserDetailsViewModel?> GetUserDetailsByIdAsync(Guid id);
        UserDetails? GetByUsernameOrEmail(string usernameOrEmail);
        Task<UserDetailsViewModel?> GetByUsernameOrEmailAsync(string usernameOrEmail);
        UserDetails? GetByUsernameOrEmail(string username, string email);
        Task<UserDetailsViewModel?> GetByUsernameOrEmailAsync(string username, string email);
        Guid Create(UserDetails entity);
        Task<Guid> CreateAsync(UserDetails entity);
        bool Update(UserDetails entity);
        Task<bool> UpdateAsync(UserDetails entity);
        bool Delete(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}