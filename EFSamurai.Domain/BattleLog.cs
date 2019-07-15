using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamurai
{
    public class BattleLog
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Mirrored with FK
        public int BattleId { get; set; }
        public Battle Battle { get; set; }

        // Mirrored
        // public int BattleEventId { get; set; }
        public ICollection<BattleEvent> BattleEvents { get; set; }
    }
}
