using System;

namespace Application.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public DateTime? EndedAt { get; set; }

        public int State { get; set; }
    }
}
