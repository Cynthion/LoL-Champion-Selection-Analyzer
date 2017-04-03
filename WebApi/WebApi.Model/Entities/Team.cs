using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model.Entities
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TeamId { get; set; }

        public bool Winner { get; set; }

        public bool FirstBlood { get; set; }

        public bool FirstTower { get; set; }

        public bool FirstInhibitor { get; set; }

        public bool FirstDragon { get; set; }

        public bool FirstRiftHerald { get; set; }

        public bool FirstBaron { get; set; }

        public int TowerKills { get; set; }

        public int InhibitorKills { get; set; }

        public int DragonKills { get; set; }

        public int RiftHeraldKills { get; set; }

        public int BaronKills { get; set; }

        public ICollection<Participant> Participants { get; set; }
    }
}
