using Domain.Model.Base;
using Domain.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Sheet : BaseEntity
    {
        [Required]
        public InstrumentType Instrument { get; set; }

        [Required]
        public VoiceType Voice { get; set; }

        [Required]
        [MaxLength(1000)]
        public string FileName { get; set; }

        [Required]
        public bool IsAnnotated { get; set; }
    }
}
