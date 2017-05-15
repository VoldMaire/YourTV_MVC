using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTV_BLL.DTO;
using YourTV_DAL.Interfaces;
using YourTV_DAL.Repositories;
using YourTV_BLL.Interfaces;
using YourTV_DAL.Entities;
using AutoMapper;
using YourTV_BLL.Infrastructure;


namespace YourTV_BLL.Services
{
    public class VideoService : IVideoService
    {
        IUnitOfWork Database { get; set; }

        public VideoService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<VideoDTO> GetVideoById()
        {
            throw new NotImplementedException();
        }

        public async Task<OperationDetails> AddVideo(VideoDTO videoDto)
        {
            if(videoDto == null)
                throw new ArgumentNullException("Can't update video with null value.");

            ApplicationUser user = await Database.UserManager.FindByNameAsync(videoDto.UserName);
            Video video = new Video();
            video.Name = videoDto.Name;
            video.ApplicationUserId = user.Id;
            video.Path = videoDto.Path;
            video.Description = videoDto.Description;
            video.Duration = videoDto.Duration;
            video.PlaylistId = videoDto.PlaylistId;
            Database.Videos.Create(video);
            await Database.SaveAsync();
            return new OperationDetails(true, "Adding was successful.", "");
        }

        public async Task<OperationDetails> UpdateVideo(VideoDTO videoDto)
        {
            if(videoDto == null)
                throw new ArgumentNullException("Can't update video with null value.");

            Video video = Database.Videos.GetAll().Where(v=>v.Path == videoDto.Path).First();
            if (video != null)
            {
                video.Name = videoDto.Name;
                video.PlaylistId = videoDto.PlaylistId;
                video.IsDeleted = videoDto.IsDeleted;
                video.Description = videoDto.Description;
                Database.Videos.Update(video);
                await Database.SaveAsync();
                return new OperationDetails(true, "Updated successfully.", "");
            }
            else return new OperationDetails(false, "Such video doesn't exist", "Path");
        }

        public async Task<OperationDetails> AddLike(int videoId, string userId)
        {
            ApplicationUser user = await Database.UserManager.FindByIdAsync(userId);
            Video video = await Database.Videos.GetAsync(videoId);
            if (user == null)
                return new OperationDetails(false, "Such user doesn't exist.", "ApplicationUserId");
            if (video == null)
                return new OperationDetails(false, "Video with this id doesn't exist.", "Id");

            video.UsersLiked.Add(user);
            Database.Videos.Update(video);
            await Database.SaveAsync();
            return new OperationDetails(true, "Liked was successfully", "");
        }

        public async Task<OperationDetails> AddComment(CommentDTO commentDto)
        {
            if (commentDto == null)
                throw new ArgumentNullException("Can't add comment with null value.");

            ApplicationUser user = await Database.UserManager.FindByIdAsync(commentDto.ApplicationUserId);
            Video video = await Database.Videos.GetAsync(commentDto.VideoId);
            if (video == null)
                return new OperationDetails(false, "This video doesn't exist.", "VideoId");
            if (user == null)
                return new OperationDetails(false, "This user doesn't exist.", "UserId");

            Comment comment = new Comment();
            comment.Text = commentDto.Text;
            comment.ApplicationUserId = commentDto.ApplicationUserId;
            Database.Comments.Create(comment);
            video.Comments.Add(comment);
            Database.Videos.Update(video);
            await Database.SaveAsync();
            return new OperationDetails(true, "Adding of comment was successfully.", "");
        }

        public VideoDTO GetVideo(int videoId)
        {
            Video video = Database.Videos.Get(videoId);
            if (video != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Video, VideoDTO>();
                });
                var mapper = config.CreateMapper();
                VideoDTO videoDto = new VideoDTO();
                videoDto = mapper.Map<Video, VideoDTO>(video);
                return videoDto;
            }
            else
                return null;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
