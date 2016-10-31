using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FindersKeepers.Controller.Enums;

namespace FindersKeepers
{
    public class FKStats : SetDefault
    {
        [XmlElement]
        public Items FKStat {
            get; set; }

        [Serializable]
        public class Items
        {
            [XmlArray("Accounts")]
            [XmlArrayItem("Account", typeof(FKItem))]
            public List<FKItem> Accounts { get; set; }

            [XmlArray("RiftManager")]
            [XmlArrayItem("Rift", typeof(Rift.RiftStats))]
            public List<Rift.RiftStats> RiftManager;

            public class FKItem
            {
                [XmlElement]
                public int ID { get; set; }

                [XmlElement]
                public ItemBase Items { get; set; }
                public class ItemBase
                {
                    [XmlAttribute]
                    public long White;
                    [XmlAttribute]
                    public long Magic;
                    [XmlAttribute]
                    public long Rare;
                    [XmlAttribute]
                    public long Legendary;
                    [XmlAttribute]
                    public long Set;
                }
            }
       }

        public object _DEFAULT()
        {
            return new FKStats
            {
                FKStat = new Items { Accounts = new List<FKStats.Items.FKItem>(), RiftManager = new List<Rift.RiftStats>() }
            };
        }
    }

}
