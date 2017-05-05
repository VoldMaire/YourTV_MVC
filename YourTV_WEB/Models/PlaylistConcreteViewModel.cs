using System.ComponentModel.DataAnnotations;

namespace YourTV_WEB.Models
{
    public class PlaylistConcreteViewModel
    {
        [Required(ErrorMessage ="Cannot create playlist without name")]
        [MaxLength(15, ErrorMessage = "Max length of playlist's name is 15.")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}