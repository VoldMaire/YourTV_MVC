using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourTV_BLL.DTO;

namespace YourTV_WEB.Models
{
    public class PlaylistsViewModel
    {
        public IEnumerable<PlaylistDTO> Playlists { get; set; }      
        public PlaylistConcreteViewModel PlaylistConcrete { get; set; }
    }
}