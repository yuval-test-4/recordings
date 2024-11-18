using Recordings.Infrastructure;

namespace Recordings.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(RecordingsDbContext context)
        : base(context) { }
}
