using Domain.Model.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Author :BaseEntity
    {
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
    }
}
