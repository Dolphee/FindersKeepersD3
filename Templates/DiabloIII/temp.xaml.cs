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
using Enigma.D3.Helpers;
using Enigma.D3.UI.Controls;
using Enigma.D3;
using FindersKeepers.Helpers;
using FindersKeepers.Controller.GameManagerData;
using FindersKeepers.Controller;

namespace FindersKeepers.Templates
{
    /// <summary>
    /// Interaction logic for temp.xaml
    /// </summary>
    public partial class temp : UserControl
    {
        public temp(string param)
        {
            InitializeComponent();

            var x = UXHelper.GetControl<UXItemsControl>(param);
            var Data = GetItems();

            Canvas.SetLeft(this, x.x468_UIRect.Left + 270);
            Canvas.SetTop(this, x.x468_UIRect.Top);


            int ActorSnoId = Enigma.D3.Engine.Current.Read<int>(x.Address + 0x0E50);
            int Value = Data.ContainsKey(ActorSnoId) ? Attributes.JewelRank.GetValue(Data[ActorSnoId]) : 0;
            aa.Text = Value.ToString();
            var xe = new System.Text.RegularExpressions.Regex(@"\d+");
           var tes= xe.Match(param);

            aas.Text = tes.NextMatch().Value.ToString();
            //aas.Text = ._tilerow"+Row+"._item"+i+".Item"
           // ObjectDumper.Dump(x, param);
        }


        public Dictionary<int, ActorCommonData> GetItems()
        {
            Dictionary<int, ActorCommonData> Data = new Dictionary<int, ActorCommonData>();

            foreach (var x in GameManager.Instance.GManager.GList.MainAccount.DiabloIII.ObjectManager.Storage.ActorCommonDataManager.Dereference().x00_ActorCommonData)
                if (!Data.ContainsKey(x.x000_Id))
                    Data.Add(x.x000_Id, x);

            return Data;
        }

    }
}
