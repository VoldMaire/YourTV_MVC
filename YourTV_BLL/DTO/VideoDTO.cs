using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTV_BLL.DTO
{
    public class VideoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Views { get; set; }
        public int Duration { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string ApplicationUserId { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }
        public IEnumerable<UserDTO> UsersLiked { get; set; }
    }
}
