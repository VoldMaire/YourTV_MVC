
using System;
using System.Threading.Tasks;
using YourTV_BLL.DTO;
using YourTV_BLL.Infrastructure;

namespace YourTV_BLL.Interfaces
{
    public interface IVideoService: IDisposable
    {
        Task<OperationDetails> AddVideo(VideoDTO videoDto);
        Task<OperationDetails> UpdateVideo(VideoDTO videoDto);
        Task<OperationDetails> AddLike(int videoId, string userId);
        Task<OperationDetails> AddComment(CommentDTO commentDto);
        VideoDTO GetVideo(int videoId);
    }
}
