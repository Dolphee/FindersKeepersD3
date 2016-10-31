using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Media;
using PropertyChanged;

namespace FindersKeepers
{
    [Serializable, ImplementPropertyChanged]
    public class FKAccounts : SetDefault, ICache
    {
        [XmlArray("Accounts")]
        [XmlArrayItem("Account")]
        public List<Multibox> Accounts
        {
            get; set;
        }

        public void OnStartup()
        {
            foreach (var i in Accounts)
                i.Foreground = Extensions.HexToBrush("#" + i.TextColor);
        }

        public Multibox GetAccount(int Id)
        {
            if (Accounts.Exists(x => x.MultiboxID == Id))
                return Accounts.Find(x => x.MultiboxID == Id);

            return null;
        }
        public Multibox GetAccount(string ProcessName)
        {
            if (Accounts.Exists(x => x.BattleTag == ProcessName))
                return Accounts.Find(x => x.BattleTag == ProcessName);

            return null;
        }

        public Multibox GetIfNotPresent()
        {
            Multibox Account = Accounts.FirstOrDefault();
            Account.MainAccount = true;

            return Account;
        }


        public object _DEFAULT()
        {
            return new FKAccounts
            {
                Accounts = new List<Multibox>()
                {
                    new Multibox {
                        Nickname = "Account 1",
                        Enabled = true,
                        TextColor = "949494",
                        ItemTracking = true,
                        ExperienceTracker = true,
                        BattleTag = "Not Set",
                        MainAccount = true,
                        MultiboxID = 0,
                   },

                    new Multibox {
                        Nickname = "Account 2",
                        Enabled = true,
                        TextColor = "949494",
                        ItemTracking = true,
                        ExperienceTracker = true,
                        BattleTag = "Not Set",
                        MainAccount = false,
                        MultiboxID = 1,
                   },

                    new Multibox {
                        Nickname = "Account 3",
                        Enabled = true,
                        TextColor = "949494",
                        ItemTracking = true,
                        ExperienceTracker = true,
                        BattleTag = "Not Set",
                        MainAccount = false,
                        MultiboxID = 2,
                   },

                    new Multibox {
                        Nickname = "Account 4",
                        Enabled = true,
                        TextColor = "949494",
                        ItemTracking = true,
                        ExperienceTracker = true,
                        BattleTag = "Not Set",
                        MainAccount = false,
                        MultiboxID = 3,
                   },
                }
            };
        }

        [Serializable, ImplementPropertyChanged]
        public class Multibox
        {
            [XmlIgnore]
            public Brush Foreground;

            [XmlElement]
            public int MultiboxID { get; set; }
            [XmlAttribute]
            public string Nickname { get; set; }
            [XmlElement]
            public string BattleTag { get; set; }
            [XmlElement]
            public bool Enabled { get; set; }
            [XmlElement]
            public bool MainAccount { get; set; }
            [XmlElement]
            public bool ItemTracking { get; set; }
            [XmlElement]
            public bool ExperienceTracker { get; set; }
            [XmlElement]
            public string TextColor { get; set; }
        }
    }
}
