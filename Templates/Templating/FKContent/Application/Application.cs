using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Templates.Filters;
using FindersKeepers.Controller;
using FindersKeepers.Templates.Application;
using FindersKeepers.Templates.Application.NPC;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;


namespace FindersKeepers.Templates.Application
{
    public class Application
    {
        public BaseDesign _root;
        public object Selected;
        public Dictionary<int, List<FKMenuHelper.MenuStruct>> Menu = new Dictionary<int,List<FKMenuHelper.MenuStruct>>();
        public FKMenuHelper.MenuStruct.MouseEnter.Colors Transition;

        public Application(BaseDesign root)
        {   
            _root = root;
            
            SetMenu();
            SetFilters();

            _root.xMenu.Content = "Application";
            _root.xSelected.Content = "Ingame Overlay";
            _root.xSubMenu.Content = "Minimap Settings";
            _root.Items = Menu[1];

            _root.DrawMenu();
            _root.DrawSubMenu();
            //root.LoadPage(new Overlay.OverlaySettings(this), null);
        }

        public void SetFilters()
        {
            _root.SubItems.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Ingame Overlay",
                InvokeMethod = (() =>
                {
                    _root.CleanHeader();
                    //_root.LoadPage(new Overlay.OverlaySettings(this), null, null, true);
                    _root.Items = Menu[1];
                    _root.SetHeader();
                }),
            });

            _root.SubItems.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Minimap Overlay & NPC",
                InvokeMethod = (() =>
                {
                    _root.CleanHeader();
                    _root.PainDropDown<MapItem>(new List<MapItem>(
                         Config.Get<FKMinimap>().DefaultMapItem.CustomActors.Select(x => x.Value)
                        .Concat(Config.Get<FKMinimap>().DefaultMapItem.DefaultActors.Select(x => x.Value).OrderBy(x => x.Name))),
                        new FKMenuHelper.MenuStruct { Title = "Select Actor type", MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter { Color = Transition } });
                    _root.Items = Menu[0];
                    _root.SetHeader();
                    _root.LoadPage(new Settings(this), null, null, true);
                }),
            });

            _root.SubItems.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Elite Packs",
                OwnResize = true,
                InvokeMethod = (() =>
                {
                    _root.CleanHeader();
                    _root.PainDropDown<FKAffixes.FKAffix>(Config.Get<FKAffixes>().Affixes.OrderBy(x => x.Name).ToList(),
                         new FKMenuHelper.MenuStruct{ Title = "Select Affix", MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter{ Color = Transition}});
                    _root.Items = Menu[2];
                    _root.SetHeader();
                    _root.LoadPage(new ElitePacks.GeneralSettings(this), null, null, true);
                }),
            });

            _root.SubItems.Add(new FKMenuHelper.MenuStruct { Title = "Overlay Items" });
            _root.SubItems.Add(new FKMenuHelper.MenuStruct { Title = "Styles" });
            _root.SubItems.Add(new FKMenuHelper.MenuStruct { Title = "Miscellanous" });
        }

        public void SetMenu()
        {
            Transition = new FKMenuHelper.MenuStruct.MouseEnter.Colors
            {
                Active = Extensions.HexToColor("#ffffff"),
                Hover = Extensions.HexToColor("#fbfbfb"),
                Reset = Extensions.HexToColor("#f0f0f0"),
                FadeIn = 400,
                FadeOut = 100
            };

            Menu.Add(0, new List<FKMenuHelper.MenuStruct>(){
             
                new FKMenuHelper.MenuStruct
                {
                    Title = "Settings",
                    Image = new Image
                    {
                        Width = 16,
                        Height = 16,
                        Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/Icons/SettingActive.png"))
                    },
                    Invoke = new Settings(this),

                    MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                    {
                        Enter = "pack://application:,,,./Images/FK/Icons/SettingActive.png".ToImage(),
                        Leave = "pack://application:,,,./Images/FK/Icons/SettingActive.png".ToImage(),
                        Color = Transition
                    }
                },

                new FKMenuHelper.MenuStruct
                {
                    Title = "Minimap Options",
                    Image = new Image
                    {
                        Width = 16,
                        Height = 16,
                        Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/Icons/minimap.png"))
                    },
                    Invoke = new MinimapItem(this),
                    OwnResize = true,
                    MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                    {
                        Enter = "pack://application:,,,./Images/FK/Icons/minimap.png".ToImage(),
                        Leave = "pack://application:,,,./Images/FK/Icons/minimap.png".ToImage(),
                        Color = Transition
                    }
                }
            });

            Menu.Add(1, new List<FKMenuHelper.MenuStruct>(){
                new FKMenuHelper.MenuStruct
                {
                    Title = "Minimap Settings",
                    Image = new Image
                    {
                        Width = 16,
                        Height = 16,
                        Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/Icons/intersect.png"))
                    },
                  //  Invoke = new Overlay.OverlaySettings(this),
                    MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                    {
                        Enter = "pack://application:,,,./Images/FK/Icons/intersect.png".ToImage(),
                        Leave = "pack://application:,,,./Images/FK/Icons/intersect.png".ToImage(),
                        Color = Transition
                    }
                },

                new FKMenuHelper.MenuStruct
                {
                    Title = "Skillbar Settings",
                    Image = new Image
                    {
                        Width = 16,
                        Height = 16,
                        Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/Icons/flags.png"))
                    },
                    Invoke = new Overlay.Skillbar(this),
                    MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                    {
                        Enter = "pack://application:,,,./Images/FK/Icons/flags.png".ToImage(),
                        Leave = "pack://application:,,,./Images/FK/Icons/flags.png".ToImage(),
                        Color = Transition
                    }
                },

                new FKMenuHelper.MenuStruct
                {
                    Title = "Experiencebar Settings",
                    Image = new Image
                    {
                        Width = 16,
                        Height = 16,
                        Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/Icons/pins.png"))
                    },
                    Invoke = new Overlay.ExperienceBar(this),
                    MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                    {
                        Enter = "pack://application:,,,./Images/FK/Icons/pins.png".ToImage(),
                        Leave = "pack://application:,,,./Images/FK/Icons/pins.png".ToImage(),
                        Color = Transition
                    }
                },

                new FKMenuHelper.MenuStruct
                {
                    Title = "Misc Settings",
                    Image = new Image
                    {
                        Width = 16,
                        Height = 16,
                        Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/Icons/droplets.png"))
                    },
                    Invoke = new Overlay.MiscBar(this),
                    MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                    {
                        Enter = "pack://application:,,,./Images/FK/Icons/droplets.png".ToImage(),
                        Leave = "pack://application:,,,./Images/FK/Icons/droplets.png".ToImage(),
                        Color = Transition
                    }
                }
            });


            Menu.Add(2, new List<FKMenuHelper.MenuStruct>(){

                new FKMenuHelper.MenuStruct
                {
                    Title = "General Setting (Applies to all)",
                    Image = new Image
                    {
                        Width = 16,
                        Height = 16,
                        Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/Icons/pins.png"))
                    },
                    Invoke = new ElitePacks.GeneralSettings(this),
                    MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                    {
                        Enter = "pack://application:,,,./Images/FK/Icons/pins.png".ToImage(),
                        Leave = "pack://application:,,,./Images/FK/Icons/pins.png".ToImage(),
                        Color = Transition
                    }
                },

                new FKMenuHelper.MenuStruct
                {
                    Title = "Affix Settings",
                    Image = new Image
                    {
                        Width = 16,
                        Height = 16,
                        Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/Icons/SettingActive.png"))
                    },
                    Invoke = new ElitePacks.SetAffixes(this),
                    OwnResize = true,
                    MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                    {
                        Enter = "pack://application:,,,./Images/FK/Icons/SettingActive.png".ToImage(),
                        Leave = "pack://application:,,,./Images/FK/Icons/SettingActive.png".ToImage(),
                        Color = Transition
                    }
                }
            });

        }
    }
}
