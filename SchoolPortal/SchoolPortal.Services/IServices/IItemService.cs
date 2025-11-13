using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IItemService
    {
        List<ItemMaster> GetAll();
        ItemMaster? GetById(Guid id);
        Guid Create(ItemMaster item);
        bool Update(ItemMaster item);
        bool Delete(Guid id);
    }
}