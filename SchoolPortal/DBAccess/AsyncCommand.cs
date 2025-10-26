using System.Data.SqlClient;

namespace SchoolPortal.DBAccess
{
	public class AsyncCommand
	{
		private readonly SqlCommand _command;

		public AsyncCommand(SqlCommand command)
		{
			_command = command;
		}

		public void Cancel()
		{
			_command.Cancel();
		}
	}
}