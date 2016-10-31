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

namespace FindersKeepers.Templates.Application.NPC
{
    /// <summary>
    /// Interaction logic for MinimapItem.xaml
    /// </summary>
    public partial class MinimapItem
    {
        public MapItemShape ShapeControl { get { return Extensions.GetNestedReflection<MapItemShape>("IDesignHelper.DropDown.Data.Shape", DataObject); } }
        public IEnumerable<int> ItemShapes { get { return Enum.GetValues(typeof(ItemShape)).Cast<int>(); } }

        public MinimapItem(object App) : base(App)
        {
            InitializeComponent();
        }

        private void UpdateShape(object sender, TextChangedEventArgs e)
        {
            Preview.Children.Clear();

            if (ShapeControl == null)
                return;

            Preview.Children.Add(ShapeControl.ToShape());
        }

        private void Update(object sender, SelectionChangedEventArgs e)
        {
            UpdateShape(null, null);
        }

        /*
        public void Set()
        {
            App._root.PageContainer.Width = 457;
            App._root.PageContainer.Margin = new Thickness(0);

            if (App._root.SelectedItem.CastHelper<MapItem>().ItemElement == MapItemElement.AT_Custom)
                Shape = Config.Get<FKMinimap>().DefaultMapItem.CustomActors.Where(x => x.Key == (int)App._root.SelectedItem.CastHelper<MapItem>().Identifier).FirstOrDefault().Value.Shape;

            else
                Shape = Config.Get<FKMinimap>().DefaultMapItem.DefaultActors.Where(x => x.Key == (int)App._root.SelectedItem.CastHelper<MapItem>().ItemElement).FirstOrDefault().Value.Shape;
            Update();
        }

        public void Update()
        {
            try
            {
                Preview.Children.Clear();

                if (Shape.Shape == ItemShape.Ellipse)
                {
                    Ellipse Shap = new Ellipse();
                    Preview.Width = Shape.Width +5;
                    Preview.Height = Shape.Height +5;

                    Shap.Width = Shape.Width +5;
                    Shap.Height = Shape.Height +5;
                    Shap.Fill = Extensions.HexToBrush(Shape.Fill);
                    Shap.Opacity = Shape.Opacity;
                    Shap.StrokeThickness = Shape.StrokeThickness;
                    Shap.Stroke = Extensions.HexToBrush(Shape.Stroke);
                  
                    FillColor.Background = (Shape.Fill == "Transparent") ? Brushes.Black: Extensions.HexToBrush(Shape.Fill);
                    SetFill.Text = (Shape.Fill == "Transparent") ? "None" : Shape.Fill.Replace("#","").ToUpper();

                    StrokeColor.Background = (Shape.Stroke == "Transparent") ? Brushes.Black : Extensions.HexToBrush(Shape.Stroke);
                    SetStroke.Text = (Shape.Stroke == "Transparent") ? "None" : Shape.Stroke.Replace("#", "").ToUpper();
    
                    ShapeType.SelectedIndex = 0;
                    SetWidth.Text = Shape.Width.ToString();
                    SetHeight.Text = Shape.Height.ToString();
                    SetStrokeThickness.Text = Shape.StrokeThickness.ToString();
                    SetOpacity.Text = (Shape.Opacity * 100).ToString();
                    Preview.Children.Add(Shap);

                }

                else
                {
                    Rectangle Shap = new Rectangle();
                    Preview.Width = Shape.Width +5;
                    Preview.Height = Shape.Height +5;

                    ShapeType.SelectedIndex = 1;
                    Shap.Width = Shape.Width + 5;
                    Shap.Height = Shape.Height +5;
                    Shap.Fill = Extensions.HexToBrush(Shape.Fill);
                    Shap.Opacity = Shape.Opacity;
                    Shap.Stroke = Extensions.HexToBrush(Shape.Stroke);
                    Shap.StrokeThickness = Shape.StrokeThickness;

                    FillColor.Background = (Shape.Fill == "Transparent") ? Brushes.Black : Extensions.HexToBrush(Shape.Fill);
                    SetFill.Text = (Shape.Fill == "Transparent") ? "None" : Shape.Fill.Replace("#", "").ToUpper();

                    StrokeColor.Background = (Shape.Stroke == "Transparent") ? Brushes.Black : Extensions.HexToBrush(Shape.Stroke);
                    SetStroke.Text = (Shape.Stroke == "Transparent") ? "None" : Shape.Stroke.Replace("#", "").ToUpper();

                    SetWidth.Text = Shape.Width.ToString();
                    SetHeight.Text = Shape.Height.ToString();
                    SetStrokeThickness.Text = Shape.StrokeThickness.ToString();
                    SetOpacity.Text = (Shape.Opacity * 100).ToString();

                    Preview.Children.Add(Shap);
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ChangeShape(object sender, SelectionChangedEventArgs e)
        {
            if (Shape == null)
                return;

            try
            {
                Shape.Shape = (sender.CastVisual<ComboBox>().SelectedIndex == 0) ? ItemShape.Ellipse : ItemShape.Rectangle;
                Update();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool IsNumber(object sender, TextChangedEventArgs e)
        {
            double parsedValue;

            if (!double.TryParse(sender.CastVisual<TextBox>().Text, out parsedValue))
                return false;

            return true;
        }

        private void ChangeWidth(object sender, TextChangedEventArgs e)
        {
            if (Shape == null)
                return;

            if (!IsNumber(sender,e))
            {
                SetWidth.Text = Shape.Width.ToString();
                SetLast(sender);
                return;
            }

            Shape.Width = Int32.Parse(sender.CastVisual<TextBox>().Text);
            Update();
        }

        private void ChangeHeight(object sender, TextChangedEventArgs e)
        {
            if (Shape == null)
                return;

            if (!IsNumber(sender,e))
            {
                SetHeight.Text = Shape.Height.ToString();
                SetLast(sender);
                return;
            }

            Shape.Height = Int32.Parse(sender.CastVisual<TextBox>().Text);
            Update();
        }

        private void ChangeOpacity(object sender, TextChangedEventArgs e)
        {
            double value = 0.0;

            if (Shape == null)
                return;

            if (!IsNumber(sender,e))
            {
                SetOpacity.Text = (Shape.Opacity * 100).ToString();
                SetLast(sender);
                return;
            }

            value = ((double)((Int32.Parse(sender.CastVisual<TextBox>().Text))) / 100d);

            if (value > 1)
                value = 1;

            Shape.Opacity = value;
            Update();
        }

        private void ChangeStrokeThickness(object sender, TextChangedEventArgs e)
        {
            if (Shape == null)
                return;

            if (!IsNumber(sender, e))
            {
                SetStrokeThickness.Text = Shape.StrokeThickness.ToString();
                SetLast(sender);
                ChangeStroke();
                return;
            }

            Shape.StrokeThickness = Int32.Parse(sender.CastVisual<TextBox>().Text);
            ChangeStroke();
            Update();
        }

        private void SetLast(object sender)
        {
            int textLength = sender.CastVisual<TextBox>().Text.Length;
            sender.CastVisual<TextBox>().SelectionStart = textLength;
            sender.CastVisual<TextBox>().SelectionLength = 0;
        }

        private void ChangeFillColor(object sender, TextChangedEventArgs e)
        {
            if (Shape == null)
                return;

            if (sender.CastVisual<TextBox>().Text.ToLower() == "none")
            {
                Shape.Fill = "Transparent";
                ChangeStroke();
                Update();
            }

            if (sender.CastVisual<TextBox>().Text.Length != 6)
                return;

            System.Text.RegularExpressions.Regex Regex = new System.Text.RegularExpressions.Regex("^[0-9A-Fa-f]+$");

            if (Regex.IsMatch(sender.CastVisual<TextBox>().Text))
                Shape.Fill = "#"+sender.CastVisual<TextBox>().Text;

            ChangeStroke();
            Update();
        }

        private void ChangeStrokeColor(object sender, TextChangedEventArgs e)
        {
            if (Shape == null)
                return;

            if (sender.CastVisual<TextBox>().Text.ToLower() == "none")
            {
                Shape.Stroke = "Transparent";
                ChangeStroke();
                Update();
            }

            if (sender.CastVisual<TextBox>().Text.Length != 6)
                return;

            System.Text.RegularExpressions.Regex Regex = new System.Text.RegularExpressions.Regex("^[0-9A-Fa-f]+$");

            if (Regex.IsMatch(sender.CastVisual<TextBox>().Text))
                Shape.Stroke = "#" + sender.CastVisual<TextBox>().Text;

            ChangeStroke();
            Update();
        }

        private void ChangeStroke()
        {
            Shape.FillBrush = Extensions.HexToBrush(Shape.Fill);
            Shape.StrokeBrush = Extensions.HexToBrush(Shape.Stroke);
        }

        private void OpenPicker(object sender, MouseButtonEventArgs e)
        {
   
        }9*/
    }
}
