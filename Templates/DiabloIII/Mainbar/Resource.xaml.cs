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
using FindersKeepers.Controller.GameManagerData;
using Enigma.D3.Helpers;

namespace FindersKeepers.Templates.Mainbar
{
    /// <summary>
    /// Interaction logic for Resource.xaml
    /// </summary>
    public partial class Resource : UserControl, IGameStatus, IFKControl
    {
        public bool DynamicSize {get; set; }
        public bool DynamicSizeChanged { get; set; }
        public double _last;
        public double _lastRegen;
        public int ResourceType = -1;

        public double _disc;
        public double _discRegen;

        public Resource()
        {
            InitializeComponent();

           // if(GameManagerAccountHelper.Current.Gamestate.InGame)
              //  OnJoin();
        }

        public void IUpdate()
        {
            if (ResourceType == 5)
            {
                UpdateDemonHunter();
                return;
            }

            double GetCurrent = Attributes.ResourceCur.GetValue(GameManagerAccountHelper.Current.Player, ResourceType);
            double Regen = Attributes.ResourceRegenPerSecond.GetValue(GameManagerAccountHelper.Current.Player, ResourceType);

            if (GetCurrent != _last)
            {
                string Formatted = Math.Round(GetCurrent, 0).ToString();
                Extensions.Execute.UIThread(() => ResourceCurrent.Text = Formatted);
                _last = GetCurrent;
            }

            if (Regen != _lastRegen)
            {
                string Formatted = Math.Round(Regen, 1).ToString();
                Extensions.Execute.UIThread(() => ResourceRegen.Text = Formatted);
                _lastRegen = Regen;
            }
        }

        public void UpdateDemonHunter()
        {
            double Hatred = Attributes.ResourceCur.GetValue(GameManagerAccountHelper.Current.Player, ResourceType);
            double Regen = Attributes.ResourceRegenPerSecond.GetValue(GameManagerAccountHelper.Current.Player, ResourceType);

            double Disc = Attributes.ResourceCur.GetValue(GameManagerAccountHelper.Current.Player, 6);
            double DiscRegen = Attributes.ResourceRegenPerSecond.GetValue(GameManagerAccountHelper.Current.Player, 6);

            if (Hatred != _last)
            {
                string Formatted = Math.Round(Hatred, 0).ToString();
                Extensions.Execute.UIThread(() =>  HatredCurrent.Text = Formatted);
                _last = Hatred;
            }

            if (Regen != _lastRegen)
            {
                string Formatted = Math.Round(Regen, 1).ToString();
                Extensions.Execute.UIThread(() => ResouceRegen.Text = Formatted);
                _lastRegen = Regen;
            }

            if (Disc != _disc)
            {
                string Formatted = Math.Round(Disc, 0).ToString();
                Extensions.Execute.UIThread(() => DiscCurrent.Text = Formatted);
                _disc = Disc;
            }

            if (DiscRegen != _discRegen)
            {
                string Formatted = Math.Round(DiscRegen, 1).ToString();
                Extensions.Execute.UIThread(() => DiscRegens.Text = Formatted);
                _discRegen = DiscRegen;
            }

        }

        public void OnExit()
        {
            Extensions.Execute.UIThread(() =>
            {
                  Container.Visibility = Visibility.Hidden;
                  DHContainer.Visibility = Visibility.Hidden;
            });
        }

        public void OnJoin()
        {

            if (GameManagerAccountHelper.Current.HeroHelper.HeroClass == Enigma.D3.Enums.HeroClass.Barbarian)
                ResourceType = (int)Enigma.D3.Enums.ResourceType.Fury;
            else if (GameManagerAccountHelper.Current.HeroHelper.HeroClass == Enigma.D3.Enums.HeroClass.Monk)
                ResourceType = (int)Enigma.D3.Enums.ResourceType.Spirit;
            else if (GameManagerAccountHelper.Current.HeroHelper.HeroClass == Enigma.D3.Enums.HeroClass.Crusader)
                ResourceType = (int)Enigma.D3.Enums.ResourceType.Faith;
            else if (GameManagerAccountHelper.Current.HeroHelper.HeroClass == Enigma.D3.Enums.HeroClass.Witchdoctor)
                ResourceType = (int)Enigma.D3.Enums.ResourceType.Mana;
            else if (GameManagerAccountHelper.Current.HeroHelper.HeroClass == Enigma.D3.Enums.HeroClass.Wizard)
                ResourceType = (int)Enigma.D3.Enums.ResourceType.Arcanum;
            else if (GameManagerAccountHelper.Current.HeroHelper.HeroClass == Enigma.D3.Enums.HeroClass.DemonHunter)
                ResourceType = (int)Enigma.D3.Enums.ResourceType.Hatred;

            IUpdate();

            Extensions.Execute.UIThread(() =>
            {
                if (ResourceType == 5)
                {
                    if (DHContainer.Visibility == Visibility.Hidden)
                        DHContainer.Visibility = Visibility.Visible;
                }
                else
                {
                    if (Container.Visibility == Visibility.Hidden)
                        Container.Visibility = Visibility.Visible;
                }
            });
        }

        public void OnCreation() {
            OnJoin();
        }
        public void OnDestroy(){ }


    }
}
