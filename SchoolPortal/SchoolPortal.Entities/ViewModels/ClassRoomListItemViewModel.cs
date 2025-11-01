using System;

namespace SchoolPortal.Entities.ViewModels
{
    public class ClassRoomListItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string SchoolName { get; set; } = string.Empty;
    }
}
