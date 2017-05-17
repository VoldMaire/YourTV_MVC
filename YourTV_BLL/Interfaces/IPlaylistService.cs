using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourTV_BLL.DTO;
using YourTV_BLL.Infrastructure;

namespace YourTV_BLL.Interfaces
{
    public interface IPlaylistService: IDisposable
    {
        IEnumerable<PlaylistDTO> GetAllByUser(string userId);
        PlaylistDTO GetLastByName(string name);
        PlaylistDTO GetById(int id);
        Task<OperationDetails> CreateAsync(PlaylistDTO playlistDto);
        Task<OperationDetails> DeletePlaylist(int playlstId);
    }
}
