using System.Collections.Generic;

namespace YourTV_BLL.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<VideoDTO> Videos { get; set; }
    }
}
