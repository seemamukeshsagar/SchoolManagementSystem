// SchoolPortal.Services.IServices/INonTeachingDocumentDetailsService.cs
using SchoolPortal.Entities.Models;
using System;
using System.Collections.Generic;

namespace SchoolPortal.Services.IServices
{
    public interface INonTeachingDocumentDetailsService
    {
        IEnumerable<NonTeachingDocumentDetails> GetByNonTeachingId(Guid nonTeachingId);
        NonTeachingDocumentDetails GetDocumentById(Guid id);
        bool Add(NonTeachingDocumentDetails entity);
        bool Update(NonTeachingDocumentDetails entity);
        bool Delete(Guid id);
    }
}

