using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FindersKeepers.Templates.Filters;
using FindersKeepers.Controller;
using System.Windows.Media.Imaging;

namespace FindersKeepers.Templates.Filters
{
    public class Filters
    {
       // public static FKMethods.PageHelper PageHelper = new FKMethods.PageHelper();
       // public FKFilters CurrentFilter;
       // public Dictionary<FKMethods.ListType, UIElement> Controls;
        public BaseDesign _root;
        public FilterItems Item = Config.Get<FKFilters>().Filters.First();
        
        public Filters(BaseDesign root)
        {   
            _root = root;
            _root.xMenu.Content = "Filters";
            _root.xSelected.Content = Config.Get<FKFilters>().Filters.FirstOrDefault().Name;
            _root.xSubMenu.Content = "General Settings";
            SetMenu();
            SetFilters();
            SetSubMenu();
            SetSubMenuSelector();

            _root.DrawMenu();
            _root.DrawSubMenu();
            _root.DrawSubMenuContainerSelector();
           // root.LoadPage(new General(this), null);

            // root.LoadPage(new NoFilter(this), null);

            //root.LoadPage(new Templates.Filters.MultiboxFilter(this), null);

            /*Border Border = new Border
            {
                Width = 141,
                Height = 32,
                Background = Extensions.HexToBrush("#ffffff", false),
                BorderBrush = Extensions.HexToBrush("#e9e9e9", false),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(2),
                // Tag = new FKMethods.TagHelper { Transition = Item.MouseEvent.Color },
                Name = "DropDown",
                Margin = new Thickness(0,10,0,0)
            };

            ComboBox Box = new ComboBox { Width = 140, Height = 28, SelectedIndex = 0 };
            ComboBoxItem Itemx = new ComboBoxItem { Content = "Item Filter" };
            ComboBoxItem Itemx2 = new ComboBoxItem { Content = "Crafting Mats" };
            ComboBoxItem Itemx3 = new ComboBoxItem { Content = "Gems Filter" };
            Box.Items.Add(Itemx);
            Box.Items.Add(Itemx2);
            Box.Items.Add(Itemx3);

            Border.Child = Box;
            _root.SetParamMenu(Border);*/
        }

        public void SetFilters()
        {
            _root.SubItems.Clear();

            foreach (FilterItems item in Config.Get<FKFilters>().Filters)
            {
                _root.SubItems.Add(new FKMenuHelper.MenuStruct
                {
                    Title = item.Name,
                    InvokeMethod = (() =>  _Set(item)),
                    Invoke = item
                });
            }
        }

        private void _Set(FilterItems item)
        {
            try{
                Item = Config.Get<FKFilters>().Filters.Single(x => x.ID == item.ID && x.Name == item.Name) ;
               _root.LoadPage(_root.Page, null);
               _root.xSelected.Content = Item.Name;
            }

            catch{
                MessageBox.Show("Couldn't not change active filter, please check your config file");
            }
        }

        public void DeleteFilter()
        {
           int ID = Item.ID;

           Config.Get<FKFilters>().Filters.Remove(Item);
           Item = Config.Get<FKFilters>().Filters.Find(x => x.ID == ID - 1);
           SetFilters();
           _root.DrawSubMenu();
           //_root.LoadPage(new General(this), null);
        }

        public void CreateFilter()
        {
            //_root.SubMenuContainerSelectorContainer.Visibility = Visibility.Visible;


            int ID = Config.Get<FKFilters>().Filters.Count+1;
            try
            {
                Config.Get<FKFilters>().Filters.Add(new FilterItems
                {
                    AttributesFilter = new HashSet<int>(Enum.GetValues(typeof(Controller.Pickit.Attributes.ItemType)).Cast<int>().ToArray()),
                    Enabled = true,
                    ID = ID,
                    Name = "My Filter " + ID,
                    Quality = Controller.Enums.ItemQuality.Legendary,
                    SoundId = 0,
                    OverlayIngame = true
                });

                Item = Config.Get<FKFilters>().Filters.Last();
                SetFilters();
                _root.DrawSubMenu();
                //_root.LoadPage(new General(this), null);
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void SetSubMenuSelector()
        {
            FKMenuHelper.MenuStruct.MouseEnter.Colors Transition = new FKMenuHelper.MenuStruct.MouseEnter.Colors
            {
                Active = Extensions.HexToColor("#fffff0"),
                Hover = Extensions.HexToColor("#e9e9e9"),
                Reset = Extensions.HexToColor("#ffffff"),
                FadeIn = 400,
                FadeOut = 100
            };

            _root.SubMenuSelector.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Create a new filter for Items",
                Image = new Image
                {
                    Width = 20,
                    Height = 20,
                    Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/ItemFilter.png"))
                },
                Invoke = null,
                MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                {
                    Enter = "pack://application:,,,./Images/FK/ItemFilter.png".ToImage(),
                    Leave = "pack://application:,,,./Images/FK/ItemFilter.png".ToImage(),
                    Color = Transition
                }
            });

            _root.SubMenuSelector.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Create a new filter for Gems",
                Image = new Image
                {
                    Width = 16,
                    Height = 16,
                    Source = Extensions.ItemImage("Gems/emerald_19")
                },
                Invoke = null,
                MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                {
                    Enter = Extensions.ItemImage("Gems/emerald_19"),
                    Leave = Extensions.ItemImage("Gems/emerald_19"),
                    Color = Transition
                }
            });

            _root.SubMenuSelector.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Create a new filter for Crafting Materials",
                Image = new Image
                {
                    Width = 16,
                    Height = 16,
                    Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/FilterCrafting.png"))
                },
                Invoke = null,
                MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                {
                    Enter = "pack://application:,,,../Images/FK/FilterCrafting.png".ToImage(),
                    Leave = "pack://application:,,,./Images/FK/FilterCrafting.png".ToImage(),
                    Color = Transition,
                }
            });
        }


        public void SetSubMenu()
        {
            FKMenuHelper.MenuStruct.MouseEnter.Colors Transition = new FKMenuHelper.MenuStruct.MouseEnter.Colors
            {
                Active = Extensions.HexToColor("#ffffff"),
                Hover = Extensions.HexToColor("#fbfbfb"),
                Reset = Extensions.HexToColor("#f0f0f0"),
                FadeIn = 400,
                FadeOut = 100
            };

            _root.SubMenu.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Here you can add / delete or enable your item filters",
                Image = new Image
                {
                    Width = 16,
                    Height = 16,
                    Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/Icons/pin.png"))
                },
                Invoke = null,
                MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                {
                    Enter = "pack://application:,,,./Images/FK/Icons/pin.png".ToImage(),
                    Leave = "pack://application:,,,./Images/FK/Icons/pin.png".ToImage(),
                    Color = Transition
                }
            });

            _root.SubMenu.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Add new Item filter",
                Image = new Image
                {
                    Width = 16,
                    Height = 16,
                    Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/Icons/plus.png"))
                },
                Invoke = null,
                InvokeMethod = (() => CreateFilter()),
                MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                {
                    Enter = "pack://application:,,,./Images/FK/Icons/plus.png".ToImage(),
                    Leave = "pack://application:,,,./Images/FK/Icons/plus.png".ToImage(),
                    Color = Transition
                }
            });

            _root.SubMenu.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Delete selected filter",
                Image = new Image
                {
                    Width = 16,
                    Height = 16,
                    Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/Icons/remove.png"))
                },
                Invoke = null,
                InvokeMethod = (() => DeleteFilter()),
                MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                {
                    Enter = "pack://application:,,,./Images/FK/Icons/remove.png".ToImage(),
                    Leave = "pack://application:,,,./Images/FK/Icons/remove.png".ToImage(),
                    Color = Transition
                }
            });

        }

        public void SetMenu()
        {
            FKMenuHelper.MenuStruct.MouseEnter.Colors Transition = new FKMenuHelper.MenuStruct.MouseEnter.Colors
            {
                Active = Extensions.HexToColor("#ffffff"),
                Hover = Extensions.HexToColor("#fbfbfb"),
                Reset = Extensions.HexToColor("#f0f0f0"),
                FadeIn = 400,
                FadeOut = 100
            };

            _root.Items.Add(new FKMenuHelper.MenuStruct
            {
                Title = "General Settings",
                Image = new Image
                {
                    Width = 16,
                    Height = 16,
                    Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/Icons/SettingActive.png"))
                },
               // Invoke = new General(this),
                MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                {
                    Enter = "pack://application:,,,./Images/FK/Icons/SettingActive.png".ToImage(),
                    Leave = "pack://application:,,,./Images/FK/Icons/SettingActive.png".ToImage(),
                    Color = Transition
                }
            });

            _root.Items.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Overlay Settings",
                Image = new Image
                {
                    Width = 16,
                    Height = 16,
                    Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/Icons/intersect.png"))
                },
                //Invoke = new Overlay(this),
                MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                {
                    Enter = "pack://application:,,,./Images/FK/Icons/intersect.png".ToImage(),
                    Leave = "pack://application:,,,./Images/FK/Icons/intersect.png".ToImage(),
                    Color = Transition
                }
            });

            _root.Items.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Sound & Multibox options",
                Image = new Image
                {
                    Width = 16,
                    Height = 16,
                    Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/Icons/Multibox.png"))
                },
                Invoke = new MultiboxFilter(this),
                MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                {
                    Enter = "pack://application:,,,./Images/FK/Icons/MultiboxActive.png".ToImage(),
                    Leave = "pack://application:,,,./Images/FK/Icons/Multibox.png".ToImage(),
                    Color = Transition
                }
            });

            _root.Items.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Filters",
                Image = new Image
                {
                    Width = 16,
                    Height = 16,
                    Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/Icons/Filters.png"))
                },
                Invoke = new ItemFilter(this),
                MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                {
                    Enter = "pack://application:,,,./Images/FK/Icons/FiltersActive.png".ToImage(),
                    Leave = "pack://application:,,,./Images/FK/Icons/Filters.png".ToImage(),
                    Color = Transition
                }
            });

            _root.Items.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Pickit",
                Image = new Image
                {
                    Width = 16,
                    Height = 16,
                    Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/Icons/Pickit.png"))
                },
                Invoke = null,
                MouseEvent = new FKMenuHelper.MenuStruct.MouseEnter
                {
                    Enter = "pack://application:,,,./Images/FK/Icons/PickitActive.png".ToImage(),
                    Leave = "pack://application:,,,./Images/FK/Icons/Pickit.png".ToImage(),
                    Color = Transition
                }
            });
        }
        
        public void SetState(object sender)
        {
            FilterItems Item = (FilterItems)((Image)sender).Tag;
            FilterItems Real = null;

            if(Config.Get<FKFilters>().Filters.Exists(x => x.ID == Item.ID))
                Real = Config.Get<FKFilters>().Filters.Find(x => x.ID == Item.ID);

            bool NewState = Item.Enabled ? false : true;
            Item.Enabled = NewState;
            Real.Enabled = NewState;

            ((Image)sender).Source = Extensions.FKImage( NewState ? "checked" : "_checked");
        }
    }
}
