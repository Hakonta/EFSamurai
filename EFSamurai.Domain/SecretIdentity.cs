using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFSamurai
{
    public class SecretIdentity
    {
        public int Id { get; set; }
        public string RealName { get; set; }
        [ForeignKey("Samurai")]
        public int SamuraiID { get; set; }
        public Samurai Samurai { get; set; }

    }
}
