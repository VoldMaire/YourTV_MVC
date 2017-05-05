using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourTV_DAL.Entities
{
    public class Playlist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Description { get; set; }
        public ICollection<Video> Videos { get; set; }
        public Playlist()
        {
            Videos = new List<Video>();
        }
    }
}
