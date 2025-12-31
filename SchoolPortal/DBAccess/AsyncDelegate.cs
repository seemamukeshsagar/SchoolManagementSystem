using Microsoft.Data.SqlClient;

namespace SchoolPortal.DBAccess
{
	/// <summary>
	/// helper class holds state information
	/// </summary>

	// You need this delegate in order to fill the grid from
	// a thread other than the form's thread. See the HandleCallback
	// procedure for more information.
	public delegate void AsyncDelegate(AsyncCommand command, SqlDataReader reader, object asyncState);
}