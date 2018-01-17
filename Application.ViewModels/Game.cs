using System;

namespace Application.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel()
        {

        }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public DateTime? EndedAt { get; set; }

        public int State { get; set; }
    }
}
