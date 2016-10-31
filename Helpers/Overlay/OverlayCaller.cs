using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller;
using FindersKeepers.Helpers;
using System.Windows.Controls;
using System.Windows;
using FindersKeepers.Templates;
using FindersKeepers.Templates.MinimapNotifyObjects;

namespace FindersKeepers.Helpers.Overlay
{
    public class OverlayCaller
    {
        public static bool Value;

        private static bool D3Overlay()
        {
            return GameManager.Instance.GManager.GRef.Attacher != null;
        }

        private static void DetermineAction<T>(bool SkipMain = false) where T : UserControl
        {
            if (!D3Overlay())
                return;

            if (Extensions.CreatePage(typeof(T)) is IUpdate && !SkipMain) //
            {
                if (Value)
                    GameManager.Instance.GManager.GRef.Attacher.MainBar.CastVisual<MainBar>().Add<T>(typeof(T).Name, true);
                else
                    GameManager.Instance.GManager.GRef.Attacher.MainBar.CastVisual<MainBar>().Remove(typeof(T).Name);
            }

            else
            {
                if (Value)
                    GameManager.Instance.GManager.GRef.Attacher.Add<T>(typeof(T).Name);
                else
                    GameManager.Instance.GManager.GRef.Attacher.Remove(typeof(T).Name);
            }
        }

        private static void DetermineActionWPF<T>(string Name) where T : IWPF
        {
            if (!D3Overlay())
                return;

            if (Value)
                GameManager.Instance.GManager.GRef.Attacher.AddWPF<T>(Name);
            else
                GameManager.Instance.GManager.GRef.Attacher.Remove(Name);
        }


        public static T SearchControl<T>() where T : UserControl
        {
            if (!D3Overlay())
                return null;

            return (T)GameManager.Instance.GManager.GRef.Attacher.Search<T>(typeof(T).Name);
        }

        public static void ToggleDamangeNumbers()
        {
            if (GameManager.Instance.GManager.GRef.Actors != null)
                if (GameManager.Instance.GManager.GRef.Actors.DamangeNumbers != null)
                    GameManager.Instance.GManager.GRef.Actors.DamangeNumbers.Enabled = Value;
        }

        public static void ToggleExperienceNumbers()
        {
            MainBar MainBar = SearchControl<MainBar>();

            if (MainBar == null)
                return;

            if(Value)
                MainBar.Add<Templates.ExperienceItems>("ShowNumbers");
            else
                MainBar.Remove("ShowNumbers");
        }

        public static void ExperienceOverlay()
        {
            MainBar MainBar = SearchControl<MainBar>();

            if (MainBar == null)
                return;

            //if (Value)
               // MainBar.AddWPF<Templates.Mainbar.ExperienceHelperObserver>("ExperienceHelper");
            //else
               // MainBar.Remove("ExperienceHelper");

        }

        public static void ToggleMinimap()
        {
            //DetermineActionWPF<MinimapNotify>("Minimap");
        }

        public static void ToggleLargeMap()
        {
            //DetermineActionWPF<LargeMapNotify>("LargeMap");
        }

        public static void ToggleResource()
        {
            DetermineAction<Templates.Mainbar.Resource>();
        }

        public static void ToggleLife()
        {
            DetermineAction<Templates.Mainbar.Health>();
        }

        public static void RevealMinimap()
        {
            //Minimap Minimap = SearchControl<Minimap>();

            //if (Minimap == null)
                //return;

            //Minimap.MinimapItems.RevealMinimap.Enabled = Value;
        }

        public static void RevealLargeMap()
        {
            LargeMap Minimap = SearchControl<LargeMap>();

            if (Minimap == null)
                return;

           // Minimap.MapItems.RevealLargemap.Enabled = Value;
        }

        public static void QuestMarker()
        {
            //Minimap Minimap = SearchControl<Minimap>();
           // LargeMap LargeMap = SearchControl<LargeMap>();

            //if (Minimap == null && LargeMap == null)
              //  return;

           // Minimap.MinimapItems.QuestMarker = Value;
           // LargeMap.MapItems.QuestMarker = Value;
        }

        public static void Inventory()
        {
            DetermineAction<Templates.Inventory.Inventory>(true);
        }

        public static void ToggleSkillCooldown()
        {
            MainBar MainBar = SearchControl<MainBar>();

            if (MainBar == null)
                return;

            if (Value)
                MainBar.Add<Skillbar>("Skill");
            else
                MainBar.Remove("Skill");
        }

        public static void ToggleBuffs()
        {
            MainBar MainBar = SearchControl<MainBar>();

            if (MainBar == null)
                return;

            if (Value)
            {
                MainBar.Add<BuffsControl>("Buffs");
            }

            else
            {
               // Buffs.Reset();
                MainBar.Remove("Buffs");
            }
        }

        public static void NotEnabled()
        {
            MessageBox.Show("This feature has not been implemented yet");
        }

    }
}
