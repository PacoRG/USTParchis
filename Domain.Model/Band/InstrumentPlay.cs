using Domain.Model.Base;
using Domain.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Band
{
    public class InstrumentPlay : BaseEntity
    {
        [Required]
        public InstrumentType Instrument { get; set; }

        [Required]
        public VoiceType Voice { get; set; }

        [Required]
        public bool IsPrincipal { get; set; }
    }
}
