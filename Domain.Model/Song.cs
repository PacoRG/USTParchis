using Domain.Model.Base;
using Domain.Model.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Song : BaseEntity
    {
        public Song()
        {
            Videos = new List<Link>();
            Sheets = new List<Sheet>();
        }

        [Required]
        [MaxLength]
        public string Name { get; set; }

        [Required]
        public SongType Type { get; set; }

        [Required]
        public bool Active { get; set; }

        public Author Author { get; set; }

        public ICollection<Link> Videos { get; set; }

        public ICollection<Sheet> Sheets { get; set; }
    }
}
