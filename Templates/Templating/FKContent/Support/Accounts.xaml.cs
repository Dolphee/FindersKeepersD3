using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FindersKeepers.Controller;
using FindersKeepers.Templates.Templating.FKTemplates;
using PropertyChanged;

namespace FindersKeepers.Templates.Support
{
    /// <summary>
    /// Interaction logic for Accounts.xaml
    /// </summary>
    /// 
    [ImplementPropertyChanged]
    public partial class Accounts
    {
        public FKAccounts.Multibox Account {get;set; }

        public Accounts(object supp) : base(supp)
        {
            InitializeComponent();

            TryGetDataObject<SupportController>().IDesignHelper.Settings.UseMainMenu = true;
            Account = Config.Get<FKAccounts>().Accounts.FirstOrDefault();
            Set();
        }

        public void Set()
        {
            TryGetDataObject<SupportController>().IDesignHelper.Menu.Clear();

            foreach (var x in Config.Get<FKAccounts>().Accounts)
            {
                TryGetDataObject<SupportController>().IDesignHelper.Menu.Add(
                    new IDesignHelper.IMenu
                    {
                        Name = "Account : " + x.Nickname,
                        isActive = x.Equals(Config.Get<FKAccounts>().Accounts.FirstOrDefault()),
                        isFirst = x.Equals(Config.Get<FKAccounts>().Accounts.FirstOrDefault()),
                        isLast = x.Equals(Config.Get<FKAccounts>().Accounts.LastOrDefault()),
                        Image = "pack://application:,,,./Images/FK/Icons/Account.png".ToImage(),
                        ImageHover = "pack://application:,,,./Images/FK/Icons/Account.png".ToImage(),
                        Target = (() => { Account = x; })           
                    });
            }
        }

        private void MakeMainAccount(object sender, MouseButtonEventArgs e)
        {
            Config.Get<FKAccounts>().Accounts.ForEach(x => x.MainAccount = false);
            Account.MainAccount = Account.MainAccount.FlipBool();
        }
    }
}
