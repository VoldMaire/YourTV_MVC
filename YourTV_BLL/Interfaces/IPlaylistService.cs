using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourTV_BLL.DTO;

namespace YourTV_BLL.Interfaces
{
    public interface IPlaylistService: IDisposable
    {
        IEnumerable<PlaylistDTO> GetAllByUser(string userId);
        Task AddAsync(PlaylistDTO playlistDto);
    }
}
