using System.Threading;

namespace SchoolPortal.DBAccess
{
    // You need this delegate to handle any errors thrown during the async junk.
    public class AsyncHelper
    {
        public object? State { get; set; }
        public AsyncErrorDelegate? ErrorDelegate { get; set; }
        public AsyncDelegate? CallbackDelegate { get; set; }
        public SynchronizationContext? SynchronizationContext { get; set; }
        public AsyncCommand? Command { get; set; }
    }
}
