using Microsoft.AspNetCore.Mvc;

namespace Recordings.APIs;

[ApiController()]
public class VideosController : VideosControllerBase
{
    public VideosController(IVideosService service)
        : base(service) { }
}
