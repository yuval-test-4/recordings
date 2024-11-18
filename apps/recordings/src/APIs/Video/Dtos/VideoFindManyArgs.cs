using Microsoft.AspNetCore.Mvc;
using Recordings.APIs.Common;
using Recordings.Infrastructure.Models;

namespace Recordings.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class VideoFindManyArgs : FindManyInput<Video, VideoWhereInput> { }
