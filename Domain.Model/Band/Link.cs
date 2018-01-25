using Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class Link : BaseEntity
    {
        [Required]
        [MaxLength()]

        public string URL { get; set; }
    }
}
