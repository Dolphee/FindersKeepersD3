using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FindersKeepers.Controller;

namespace FindersKeepers.Assets
{
    public interface ISimpleAnimate
    {
        MenuContainer.Effect Effects { get; set; }
    }

    public static class MenuHelper
    {
        public static ObservableCollection<MenuContainer> Create()
        {
            ObservableCollection<MenuContainer> List = new ObservableCollection<MenuContainer>();
            // List.Add(new MenuContainer("Macro", "ff99d3", new MenuContainer.ImageInformation { IconName = "./Images/FK/macro_hover.png" }));

            List.Add(
                new MenuContainer("Overview", 
                    null, 
                    new MenuContainer.Effect {
                        Default = "2b4c65",
                        Hover = "3e759c"
                    }, 
                    new MenuContainer.ImageInformation {
                        IconName = "./Images/FK/FHome_hover.png"
                    })
            );

            List.Add(
                new MenuContainer("Filters", 
                    typeof(FindersKeepers.Templates.Templating.FKTemplates.FilterController), 
                    new MenuContainer.Effect {
                        Default = "2b4c65",
                        Hover = "ff7272"
                    }, 
                    new MenuContainer.ImageInformation {
                        IconName = "./Images/FK/Filters_hover.png"
                    })
            );

            List.Add(
                new MenuContainer("Overlay Settings", 
                    typeof(FindersKeepers.Templates.Application.ApplicationController), 
                    new MenuContainer.Effect {
                        Default = "2b4c65",
                        Hover = "ffa842"
                    },
                    new MenuContainer.ImageInformation {
                        IconName = "./Images/FK/app_hover.png"
                    })
            );

            List.Add(
                new MenuContainer("Statistics", 
                    null, 
                    new MenuContainer.Effect {
                        Default = "2b4c65",
                        Hover = "ff99d3"
                    }, 
                    new MenuContainer.ImageInformation {
                        IconName = "./Images/FK/stats_hover.png"
                    })
            );

            List.Add(
                new MenuContainer("Settings", 
                typeof(FindersKeepers.Templates.Support.SupportController), 
                new MenuContainer.Effect {
                    Default = "2b4c65",
                    Hover = "d56fff"
                }, 
                new MenuContainer.ImageInformation {
                    IconName = "./Images/FK/support.png"
                })
            );

            List.Add(
                new MenuContainer("FK WebTracker", 
                null,
                new MenuContainer.Effect {
                    Default = "2b4c65",
                    Hover = "5883ff"
                }, 
                new MenuContainer.ImageInformation {
                    IconName = "./Images/FK/cloud.png"
                })
            );

            List.Add(
                new MenuContainer("Game Manager", 
                null, 
                new MenuContainer.Effect {
                    Default = "2b4c65",
                    Hover = "ffe84e"
                }, 
                new MenuContainer.ImageInformation {
                    IconName = "/Images/FK/Icons/gamemanager.png"
                })
            );

            List.Add(
                new MenuContainer("Start FindersKeepers", 
                typeof(Templates.RunController), 
                new MenuContainer.Effect {
                    Default = "2b4c65",
                    Hover = "50a864"
                }, 
                new MenuContainer.ImageInformation {
                    IconName = "./Images/FK/Icons/Start.png"
                }, 
                "GameInit")
            );

            List.Add(
                new MenuContainer("Stop FindersKeepers", 
                typeof(FindersKeepers.Templates.GameInit), 
                new MenuContainer.Effect {
                    Default = "2b4c65",
                    Hover = "ff4c4c"
                }, 
                new MenuContainer.ImageInformation {
                    IconName = "./Images/FK/Icons/Pause.png"
                }, 
                "", 
                false)
            );

            return List;
        }
    }

    public class MenuContainer : NotifyingObject, ISimpleAnimate
    {
        public bool _IsActive { get; set; }
        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;

                    if (value)
                        return;

                    Effects.Color = "fff";
                    BorderColor = "475966";
                    Refresh("IsActive");
                }
            }
        }

        public string _Border { get; set; }
        public string BorderColor { get { return _Border; } set { if (_Border != value) { _Border = value; Refresh("Border"); } } }
        public Type Target { get; set; }
        public UIElement _Template { get; set; }
        public UIElement Template
        {
            get
            {
                if (_Template == null)
                    _Template = (ExtendName != null) ? (UIElement)Activator.CreateInstance(Target, ExtendName) : (UIElement)Activator.CreateInstance(Target);

                return _Template;
            }
        }
        public bool IsVisible { get; set; }
        public string MName { get; set; }
        public ImageInformation Icon { get; set; }
        public Effect Effects { get; set; }
        public object ExtendName { get; set; }

        public class Effect : NotifyingObject
        {
            public string Hover { get; set; }
            public string Default { get; set; }

            public string _Color { get; set; }
            public string Color
            {
                get
                {
                    return _Color;
                }

                set
                {
                    if (value != _Color)
                        Refresh("Color");
                }
            }
        }

        public MenuContainer(string name, Type T, Effect e, ImageInformation i, string Extend = "", bool Visible = true)
        {
            MName = name;
            Target = T;
            Icon = i;
            IsVisible = Visible;
            Effects = e;
            Effects._Color = Effects.Default;
            BorderColor = "475966";

            if (Extend != "")
                ExtendName = Extend;

        }

        public class ImageInformation
        {
            public string IconName { get; set; }
            public Size IconSize { get; set; }
            public Thickness IconMargin { get; set; }
        }
    }
}
