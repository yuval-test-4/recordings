using Recordings.APIs.Dtos;
using Recordings.Infrastructure.Models;

namespace Recordings.APIs.Extensions;

public static class VideosExtensions
{
    public static Video ToDto(this VideoDbModel model)
    {
        return new Video
        {
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Id = model.Id,
            Name = model.Name,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static VideoDbModel ToModel(
        this VideoUpdateInput updateDto,
        VideoWhereUniqueInput uniqueId
    )
    {
        var video = new VideoDbModel { Id = uniqueId.Id, Name = updateDto.Name };

        if (updateDto.CreatedAt != null)
        {
            video.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            video.CustomerId = updateDto.Customer;
        }
        if (updateDto.UpdatedAt != null)
        {
            video.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return video;
    }
}
