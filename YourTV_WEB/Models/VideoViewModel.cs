using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourTV_BLL.DTO;
using System.ComponentModel.DataAnnotations;

namespace YourTV_WEB.Models
{
    public class VideoViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Views { get; set; }
        public int Duration { get; set; }
        public string Path { get; set; }
        public string ApplicationUserId { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }
        public IEnumerable<UserDTO> UserLiked { get; set; }
    }
}