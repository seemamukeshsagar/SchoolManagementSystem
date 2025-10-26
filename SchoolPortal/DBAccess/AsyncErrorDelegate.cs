using System;

namespace SchoolPortal.DBAccess
{
	public delegate void AsyncErrorDelegate(AsyncCommand command, Exception exception);
}