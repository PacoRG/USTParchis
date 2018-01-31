using Domain.Model.Base;
using Domain.Model.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Band
{
    public class BandComponent : BaseEntity
    {
        public BandComponent()
        {
            this.Plays = new List<InstrumentPlay>();
        }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(80)]
        public string LastName { get; set; }

        [MaxLength(20)]
        public string ContactPhone { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public RoleType Role { get; set; }

        public List<InstrumentPlay> Plays { get; set; }
    }
}
