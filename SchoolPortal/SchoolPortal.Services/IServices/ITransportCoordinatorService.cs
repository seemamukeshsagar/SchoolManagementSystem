using System;
using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortal.Services.IServices
{
    public interface ITransportCoordinatorService
    {
        TransportCoordinator? GetByKey(Guid companyId, Guid schoolId, string firstName, string lastName);
        List<TransportCoordinator> GetAll();
        TransportCoordinator? GetById(Guid id);        
    }
}
