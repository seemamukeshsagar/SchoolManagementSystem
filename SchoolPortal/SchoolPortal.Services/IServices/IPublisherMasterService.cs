using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IPublisherMasterService
    {
        List<PublisherMaster> GetAll();
        PublisherMaster? GetById(Guid id);
        Task<PublisherMaster?> GetByIdAsync(Guid id);
        Guid Create(PublisherMaster publisher);
        bool Update(PublisherMaster publisher);
        Task<bool> UpdateAsync(PublisherMaster publisher);
        bool Delete(Guid id);
    }
}
