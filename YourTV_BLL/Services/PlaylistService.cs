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

namespace YourTV_BLL.Services
{
    public class PlaylistService: IPlaylistService
    {
        IUnitOfWork Database { get; set; }

        public PlaylistService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<PlaylistDTO> GetAllByUser(string userId)
        {
            IEnumerable<PlaylistDTO> playlistsDto;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Playlist, PlaylistDTO>();
            });
            var mapper = config.CreateMapper();
            var userPlaylists = Database.Playlists.GetAll().Where(p => p.ApplicationUserId == userId).ToList();
            playlistsDto = mapper.Map<IEnumerable<Playlist>, IEnumerable<PlaylistDTO>>(userPlaylists);
            return playlistsDto;
        }

        public async Task AddAsync(PlaylistDTO playlistDto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PlaylistDTO, Playlist>();
            });
            var mapper = config.CreateMapper();
            Playlist playlist = mapper.Map<PlaylistDTO, Playlist>(playlistDto);
            Database.Playlists.Create(playlist);
            await Database.SaveAsync();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}

