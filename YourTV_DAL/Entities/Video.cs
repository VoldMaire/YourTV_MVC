using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourTV_DAL.Entities
{
    public class Video
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Views { get; set; }

        public string Path { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public Video()
        {
            Categories = new List<Category>();
        }
    }
}
