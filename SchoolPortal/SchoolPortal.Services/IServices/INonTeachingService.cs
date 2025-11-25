// SchoolPortal.Services.IServices/INonTeachingService.cs
using SchoolPortal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal.Services.IServices
{
    /// <summary>
    /// Service interface for managing non-teaching staff members
    /// </summary>
    public interface INonTeachingService
    {
        /// <summary>
        /// Gets all non-teaching staff members asynchronously
        /// </summary>
        Task<IEnumerable<NonTeachingMaster>> GetAllAsync();
        
        /// <summary>
        /// Gets all non-teaching staff members for a specific school asynchronously
        /// </summary>
        /// <param name="schoolId">The ID of the school</param>
        Task<IEnumerable<NonTeachingMaster>> GetBySchoolIdAsync(Guid schoolId);
        
        /// <summary>
        /// Gets a non-teaching staff member by ID asynchronously
        /// </summary>
        /// <param name="id">The ID of the non-teaching staff member</param>
        Task<NonTeachingMaster> GetByIdAsync(Guid id);
        
        /// <summary>
        /// Adds a new non-teaching staff member asynchronously
        /// </summary>
        /// <param name="entity">The non-teaching staff member to add</param>
        /// <returns>The number of rows affected</returns>
        Task<int> AddAsync(NonTeachingMaster entity);
        
        /// <summary>
        /// Updates an existing non-teaching staff member asynchronously
        /// </summary>
        /// <param name="entity">The non-teaching staff member to update</param>
        /// <returns>True if the update was successful, false otherwise</returns>
        Task<bool> UpdateAsync(NonTeachingMaster entity);
        
        /// <summary>
        /// Deletes a non-teaching staff member asynchronously
        /// </summary>
        /// <param name="id">The ID of the non-teaching staff member to delete</param>
        /// <param name="deletedBy">The ID of the user performing the deletion</param>
        /// <returns>True if the deletion was successful, false otherwise</returns>
        Task<bool> DeleteAsync(Guid id, Guid? deletedBy);
        
        /// <summary>
        /// Toggles the active status of a non-teaching staff member asynchronously
        /// </summary>
        /// <param name="id">The ID of the non-teaching staff member</param>
        /// <param name="modifiedBy">The ID of the user performing the update</param>
        /// <returns>True if the status was toggled successfully, false otherwise</returns>
        Task<bool> ToggleStatusAsync(Guid id, Guid? modifiedBy);        
    }
}

