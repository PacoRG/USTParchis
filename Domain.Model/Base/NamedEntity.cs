using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Base
{
    public class NamedEntity : BaseEntity
    {
        [MaxLength(255)]
        [Required(ErrorMessage ="nameRequired")]
        public string Name { get; set; }
    }
}
