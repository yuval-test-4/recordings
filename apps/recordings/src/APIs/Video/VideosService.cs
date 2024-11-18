using Recordings.Infrastructure;

namespace Recordings.APIs;

public class VideosService : VideosServiceBase
{
    public VideosService(RecordingsDbContext context)
        : base(context) { }
}
