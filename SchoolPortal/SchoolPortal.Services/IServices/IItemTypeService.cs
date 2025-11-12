using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface IItemTypeService
    {
        List<ItemTypeMaster> GetAll();
        ItemTypeMaster? GetById(Guid id);
        Guid Create(ItemTypeMaster itemType);
        bool Update(ItemTypeMaster itemType);
        bool Delete(Guid id);
        string ItemTypeNameById(Guid id);
    }
}
