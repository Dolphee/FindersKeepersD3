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
using System.Reflection;
using FindersKeepers.Templates.Filters;
using FindersKeepers.Controller;

namespace FindersKeepers
{
    public partial class BaseDesign : UserControl
    {
        public List<FKMenuHelper.MenuStruct> Items = new List<FKMenuHelper.MenuStruct>();
        public List<FKMenuHelper.MenuStruct> SubItems = new List<FKMenuHelper.MenuStruct>();
        public List<FKMenuHelper.MenuStruct> SubMenu = new List<FKMenuHelper.MenuStruct>();
        public List<FKMenuHelper.MenuStruct> SubMenuSelector = new List<FKMenuHelper.MenuStruct>();

        public object Page = null;
        public object SelectedItem = null;

        public BaseDesign(bool HideTop = false)
        {
           InitializeComponent();

           if (!HideTop)
               return;

           RemoveHeader();

        }

        public void Set<T>()
        {
            try
            {
                typeof(T).Create(this);
            }

            catch(Exception e)
             {
                MessageBox.Show(e.ToString());
            }
        }

        public void SetNavigation(List<string> Items, Image Image = null)
        {
            NavigationFields.Children.Clear();

            if (Image == null)
            {

            }


            foreach (string i in Items)
            {

            }
        }

        public void SetParamMenu(UIElement e)
        {
            IconMenuRight.Children.Add(e);
        }


        public void SetHeader()
        {
            try
            {
                TopMenu.Visibility = Visibility.Visible;
                TopMenuBorder.Visibility = Visibility.Visible;
                PageContainer.Height = 340;
                DrawMenu();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void RemoveHeader(bool Clean = true)
        {
            if(Clean)
                CleanHeader();

            TopMenu.Visibility = Visibility.Collapsed;
            TopMenuBorder.Visibility = Visibility.Collapsed;
            PageContainer.Height = 400;
        }

        public void CleanHeader()
        {
            this.Items = new List<FKMenuHelper.MenuStruct>();
            IconMenu.Children.Clear();
        }

        public void PainDropDown<T>(List<T> List, FKMenuHelper.MenuStruct Item, bool Alone = false)
        {
            Border Border = new Border
            {
                Width = 141,
                Height = 32,
                Background = Extensions.HexToBrush(Item.MouseEvent.Color.Reset.Color.ToString(), false),
                BorderBrush = Extensions.HexToBrush("#e9e9e9", false),
                BorderThickness = new Thickness(0, 1, 0, 1),
                CornerRadius = new CornerRadius(0),
                // Tag = new FKMethods.TagHelper { Transition = Item.MouseEvent.Color },
                ToolTip = new Label { Content = Item.Title },
                Name = "DropDown"
            };

            Border.Background = Extensions.HexToBrush(Item.MouseEvent.Color.Active.Color.ToString(), false);
            Border.BorderThickness = (Alone) ? new Thickness(1) : new Thickness(1, 1, 0, 1);
            Border.CornerRadius = (Alone) ? new CornerRadius(4) : new CornerRadius(4,0, 0, 4);
            ComboBox Box = new ComboBox { Width = 140, Height = 28, SelectedIndex = 0 };

            foreach (T Ix in List)
            {
                if (Ix.Equals(List.First()))
                {
                    SelectedItem = Ix;
                    xSubMenu.Content = ((HasName)Ix).Name;
                }

                ComboBoxItem Itemx = new ComboBoxItem { Content = ((HasName)Ix).Name, Tag = Ix };
                Box.Items.Add(Itemx);
            }

            Box.SelectionChanged += (s, e) =>
            {
                SelectedItem = Box.Items[Box.SelectedIndex].CastVisual<ComboBoxItem>().TagHelper<T>();
                xSubMenu.Content = Box.Items[Box.SelectedIndex].CastVisual<ComboBoxItem>().TagHelper<HasName>().Name;
                LoadPage(this.Page, null);
            };
            
            Border.Child = Box;
            IconMenu.Children.Add(Border); 
        }

        public void LoadPage(object Page, object sender, object Item = null, bool NewPage = false)
        {
            if (Page == null)
                return;

            PageContainer.Children.Clear();
            PageContainer.Children.Add((UIElement)Page);
            this.Page = Page;

            if (Item != null)
            {
                if (Item is FKMenuHelper.MenuStruct)
                {
                    bool Match = false;

                    foreach (Border x in IconMenu.Children.OfType<Border>())
                        if (x.Name == "DropDown")
                            Match = true;

                    if(!Match)
                        xSubMenu.Content = ((FKMenuHelper.MenuStruct)Item).Title;

                    FKMenuHelper.MenuStruct xItem = (FKMenuHelper.MenuStruct)Item;

                    if (PageContainer.Width != 400 && !xItem.OwnResize)
                    {
                        PageContainer.Width = 400;
                        PageContainer.Margin = new Thickness(40, 20, 0, 0);
                    }
                }
            }

            if (NewPage)
            {
                if (PageContainer.Width != 400)
                {
                    PageContainer.Width = 400;
                    PageContainer.Margin = new Thickness(40, 20, 0, 0);
                }
            }
             
            if (Page is IRefresh)
                ((IRefresh)Page).Set();

            if(sender != null)
                ResetTopMenu(sender);
        }

        public void ResetTopMenu(object sender)
        {
            FKMethods.TagHelper s = (FKMethods.TagHelper)((Border)sender).Tag;

            foreach (UIElement x in IconMenu.Children.OfType<UIElement>())
                if (x is Border)
                    if(((Border)x).Name != "DropDown")
                        ((Border)x).Background = Extensions.HexToBrush(s.Transition.Reset.Color.ToString(),false);

            ((Border)sender).Background = Extensions.HexToBrush(s.Transition.Active.Color.ToString(), false);
        }

        public void DrawSubMenu()
        {
            SubItemsControls.Children.Clear();

            if (SubMenu.Count != 0 && SubMenuContainer.Children.Count < 1)
                DrawSubMenuContainer();

            if (SubMenu.Count == 0)
                SubMenuContainer.Visibility = Visibility.Collapsed;

            foreach (FKMenuHelper.MenuStruct Item in SubItems)
            {
                Border Border = new Border
                {
                    Width = 145,
                    Height = 25,
                    Background = (Item.Equals(SubItems.First())) ? Extensions.HexToColor("#ffffff") : Extensions.HexToColor("Transparent"),
                    BorderBrush = (Item.Equals(SubItems.First())) ? Extensions.HexToColor("#eeeeee") : Extensions.HexToColor("Transparent"),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(4),
                    Tag = Item.Invoke,
                    ToolTip = new Label { Content = Item.Title },
                    Margin = new Thickness(0, 0, 0, 5),
                    Cursor = Cursors.Hand
                };

                TextBlock Text = new TextBlock
                {
                    Width = 120,
                    Height = 20,
                    Text = Item.Title,
                    FontFamily = new FontFamily("Kartika"),
                    FontSize = 10,
                    Foreground = (Item.Equals(SubItems.First())) ? Extensions.HexToColor("#666666") : Extensions.HexToColor("#ababab"),
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0,5,0,0)
                };

                Border.Child = Text;
                Border.MouseDown += ((s, e) => { 
                    if (Item.InvokeMethod == null)
                    {
                        MessageBox.Show("Not available in beta"); return;
                    }
                    Item.InvokeMethod.Invoke(); SubMenuReset(Border);
                    xSelected.Content = Item.Title;
                });

                SubItemsControls.Children.Add(Border);
            }
        }

        public void SubMenuReset(Border b)
        {
            foreach (Border border in SubItemsControls.Children.OfType<Border>())
            {
                border.Background = Extensions.HexToColor("Transparent");
                border.BorderBrush = Extensions.HexToColor("Transparent");
                TextBlock Child = (TextBlock)border.Child;
                Child.Foreground = Extensions.HexToColor("#ababab");
            }

            b.BorderBrush = Extensions.HexToColor("#eeeeee");
            b.Background = Extensions.HexToColor("#ffffff");
            TextBlock TChild = (TextBlock)b.Child;
            TChild.Foreground = Extensions.HexToColor("#666666");
        }

        public void DrawSubMenuContainerSelector()
        {

            ComboBox x = new ComboBox { Width = 100 };
            x.Items.Add("Item");
            x.Items.Add("Gem");
            x.Items.Add("Materials");
            x.SelectedIndex = 0;

            Border b = new Border
            {
                Width = 75,
                Height = 40,
                Background = Extensions.HexToBrush("#566c5a"),
                BorderThickness = new Thickness(1),
                Margin = new Thickness(5, -1, 0, 0),
            };

            TextBlock Text = new TextBlock
            {
                Width = 50,
                Height = 25,
                Foreground = Brushes.White,
                Text = "CREATE",
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 3, 0, 0),
                FontFamily = Extensions.GetFont("DinPro"),
                FontWeight = FontWeights.Bold
            };

            Text.ToolTip = new Label { Content = "Create new filter" };

            SubMenuContainerSelector.Children.Add(x);
            b.Child = Text;
            SubMenuContainerSelector.Children.Add(b);

            b.MouseDown += ((s,e) =>
            {

            });

            /*
            SubMenuContainerSelector.Parent.CastVisual<Border>().BorderThickness = new Thickness(0,1,0,1);
            SubMenuContainerSelector.Parent.CastVisual<Border>().BorderBrush = Extensions.HexToBrush("#e9e9e9", false);

            foreach (FKMenuHelper.MenuStruct Item in SubMenuSelector)
            {
                Border Border = new Border
                {
                    Width = 35,
                    Height = 35,
                    Background = Extensions.HexToBrush(Item.MouseEvent.Color.Reset.Color.ToString(), false),
                    BorderBrush = Extensions.HexToBrush("#e9e9e9", false),
                    BorderThickness = new Thickness(0),
                    CornerRadius = new CornerRadius(0),
                    Tag = new FKMethods.TagHelper { Transition = Item.MouseEvent.Color },
                    ToolTip = new Label { Content = Item.Title},
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center
                };

                Border.Child = Item.Image;
                Image BorderImage = Border.Child as Image;

                Border.MouseEnter += ((s, e) => { BorderImage.Source = Item.MouseEvent.Enter; Border.AnimateBackground(Border.Tag); });
                Border.MouseLeave += ((s, e) => { BorderImage.Source = Item.MouseEvent.Leave; Border.AnimateBackground(Border.Tag, true); });
                Border.MouseDown += ((s, e) => { if (Item.InvokeMethod == null) { return; } Item.InvokeMethod.Invoke(); });

                SubMenuContainerSelector.Children.Add(Border);
            }*/
        }


        public void DrawSubMenuContainer()
        {
            foreach (FKMenuHelper.MenuStruct Item in SubMenu)
            {
                Border Border = new Border
                {
                    Width = 35,
                    Height = 32,
                    Background = Extensions.HexToBrush(Item.MouseEvent.Color.Reset.Color.ToString(), false),
                    BorderBrush = Extensions.HexToBrush("#e9e9e9", false),
                    BorderThickness = new Thickness(0, 1, 0, 1),
                    CornerRadius = new CornerRadius(0),
                    Tag = new FKMethods.TagHelper { Transition = Item.MouseEvent.Color },
                    ToolTip = new Label { Content = Item.Title }
                };

                if (Item.Equals(SubMenu.First()))
                {
                    Border.Background = Extensions.HexToBrush(Item.MouseEvent.Color.Active.Color.ToString(), false);
                    Border.BorderThickness = new Thickness(1, 1, 0, 1);
                    Border.CornerRadius = new CornerRadius(4, 0, 0, 4);
                }

                else if ((SubMenu[0].Items != null && Item.Equals(SubMenu[1])))
                {
                    Border.Background = Extensions.HexToBrush(Item.MouseEvent.Color.Active.Color.ToString(), false);
                }

                else if (Item.Equals(SubMenu.Last()))
                {
                    Border.BorderThickness = new Thickness(0, 1, 1, 1);
                    Border.CornerRadius = new CornerRadius(0, 4, 4, 0);
                }

                Border.Child = Item.Image;
                Image BorderImage = Border.Child as Image;



                Border.MouseEnter += ((s, e) => { BorderImage.Source = Item.MouseEvent.Enter; Border.AnimateBackground(Border.Tag); });
                Border.MouseLeave += ((s, e) => { BorderImage.Source = Item.MouseEvent.Leave; Border.AnimateBackground(Border.Tag, true); });
                Border.MouseDown += ((s, e) => { if(Item.InvokeMethod == null) { return;} Item.InvokeMethod.Invoke(); });
                SubMenuContainer.Children.Add(Border);
            }
        }

        public void DrawMenu()
        {
            try
            {
                foreach (FKMenuHelper.MenuStruct Item in Items)
                {
                    Border Border = new Border
                    {
                        Width = 35,
                        Height = 32,
                        Background = Extensions.HexToBrush(Item.MouseEvent.Color.Reset.Color.ToString(), false),
                        BorderBrush = Extensions.HexToBrush("#e9e9e9", false),
                        BorderThickness = new Thickness(0, 1, 0, 1),
                        CornerRadius = new CornerRadius(0),
                        Tag = new FKMethods.TagHelper { Transition = Item.MouseEvent.Color },
                        ToolTip = new Label { Content = Item.Title }
                    };

                    bool Match = false;

                    foreach (Border x in IconMenu.Children.OfType<Border>())
                        if (x.Name == "DropDown")
                            Match = true;

                    if (Match)
                    {
                        if (Item.Equals(Items.First()))
                        {
                            Border.Background = Extensions.HexToBrush(Item.MouseEvent.Color.Active.Color.ToString(), false);
                            Border.BorderThickness = new Thickness(0, 1, 0, 1);
                            Border.CornerRadius = new CornerRadius(0, 0, 0, 0);
                        }

                        else if (Item.Equals(Items.Last()))
                        {
                            Border.BorderThickness = new Thickness(0, 1, 1, 1);
                            Border.CornerRadius = new CornerRadius(0, 4, 4, 0);
                        }
                    }

                    else
                    {
                        if (Item.Equals(Items.First()))
                        {
                            Border.Background = Extensions.HexToBrush(Item.MouseEvent.Color.Active.Color.ToString(), false);
                            Border.BorderThickness = new Thickness(1, 1, 0, 1);
                            Border.CornerRadius = new CornerRadius(4, 0, 0, 4);
                        }

                        else if ((Items[0].Items != null && Item.Equals(Items[1])))
                        {
                            Border.Background = Extensions.HexToBrush(Item.MouseEvent.Color.Active.Color.ToString(), false);
                        }

                        else if (Item.Equals(Items.Last()))
                        {
                            Border.BorderThickness = new Thickness(0, 1, 1, 1);
                            Border.CornerRadius = new CornerRadius(0, 4, 4, 0);
                        }
                    }

                    Border.Child = new Image() { Source = Item.Image.Source, Width = Item.Image.Width, Height = Item.Image.Height };
                    Image BorderImage = Border.Child as Image;

                    Border.MouseEnter += ((s, e) => { BorderImage.Source = Item.MouseEvent.Enter; Border.AnimateBackground(Border.Tag); });
                    Border.MouseLeave += ((s, e) => { BorderImage.Source = Item.MouseEvent.Leave; Border.AnimateBackground(Border.Tag, true); });
                    Border.MouseDown += ((s, e) => { LoadPage(Item.Invoke, Border, Item); });
                    IconMenu.Children.Add(Border);
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void Move(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MainWindow.Window.DragMove();
            }
            catch
            {
            }
        }


        public void BackgroundEventWhite(object sender, EventArgs e, bool Out = false )
        {
            if(Out)
                ((Border)sender).Background = Extensions.HexToBrush("#ffffff", false);
            else
                ((Border)sender).Background = Extensions.HexToBrush("#f0f0f0", false);
        }

        private void CloseFK(object sender, MouseButtonEventArgs e)
        {
        }

        private void MiniMize(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderBrush = Extensions.HexToBrush("#eeeeee", false);
              ((Border)sender).BorderThickness = new Thickness(1,0,0,1);
        }

        private void MiniMizeOut(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderBrush = Extensions.HexToBrush("transparent", false);
            ((Border)sender).BorderThickness = new Thickness(0, 0, 0, 0);
        }

    }
}
