using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamurai
{
    public class SamuraiBattle
    {
        public int SamuraiId { get; set; }
        public Samurai Samurai { get; set; }

        public int BattleID { get; set; }
        public Battle battle { get; set; }
    }
}
