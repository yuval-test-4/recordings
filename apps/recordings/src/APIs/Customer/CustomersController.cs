using Microsoft.AspNetCore.Mvc;

namespace Recordings.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
