using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamurai
{
    public class Battle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsBrutal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        private ICollection<SamuraiBattle> SamuraiBattles { get; set; }

    }
}
