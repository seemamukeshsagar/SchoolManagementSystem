using SchoolPortal.Entities.Models;

public static class TimeTableExtensions
{
	public static TimeTablePeriodMaster ToTimeTablePeriodMaster(this TimeTableClassPeriodDetails details, TimeTablePeriodMaster? master = null)
	{
		if (master == null)
		{
			master = new TimeTablePeriodMaster();
		}

		master.Id = details.Id;
		master.CompanyId = details.CompanyId;
		master.SchoolId = details.SchoolId;
		master.IsActive = details.IsActive;
		master.IsDeleted = details.IsDeleted;
		master.CreatedBy = details.CreatedBy;
		master.CreatedDate = details.CreatedDate;
		master.ModifiedBy = details.ModifiedBy;
		master.ModifiedDate = details.ModifiedDate;
		master.Status = details.Status;
		master.StatusMessage = details.StatusMessage;
		master.IsBreak = details.IsBreak;
		master.BreakName = details.BreakName;

		return master;
	}

	public static TimeTableClassPeriodDetails ToTimeTableClassPeriodDetails(this TimeTablePeriodMaster master, TimeTableClassPeriodDetails? details = null)
	{
		if (details == null)
		{
			details = new TimeTableClassPeriodDetails();
		}

		details.Id = master.Id;
		details.CompanyId = master.CompanyId;
		details.SchoolId = master.SchoolId;
		details.IsActive = master.IsActive;
		details.IsDeleted = master.IsDeleted;
		details.CreatedBy = master.CreatedBy;
		details.CreatedDate = master.CreatedDate;
		details.ModifiedBy = master.ModifiedBy;
		details.ModifiedDate = master.ModifiedDate;
		details.Status = master.Status;
		details.StatusMessage = master.StatusMessage;
		details.IsBreak = master.IsBreak;
		details.BreakName = master.BreakName;

		return details;
	}
}