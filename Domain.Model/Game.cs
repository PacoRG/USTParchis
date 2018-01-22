using Domain.Model.Base;
using Domain.Model.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Game : NamedEntity
    {
        public Game()
        {

        }

        [MaxLength(255)]
        [Required]

        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }

        public DateTime? EndedAt { get; set; }

        [Required]
        public GameState State { get; set; }
    }
}
