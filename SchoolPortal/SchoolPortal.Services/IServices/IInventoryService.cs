using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IInventoryService
    {
        List<InventoryMaster> GetAll();
        InventoryMaster? GetById(Guid id);
        Guid Create(InventoryMaster entity);
        bool Update(InventoryMaster entity);
        bool Delete(Guid id);
    }
}