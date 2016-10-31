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

namespace FindersKeepers.Templates.Application.ElitePacks
{
    /// <summary>
    /// Interaction logic for SetAffixes.xaml
    /// </summary>
    public partial class SetAffixes
    {
  
    
        public SetAffixes(object app) : base(app)
        {
            InitializeComponent();
          
        }

        public void PaintPreview()
        {
            /*
            Preview.Children.Clear();

            if (Affix == null)
                return;

            Thickness BorderThickness = new Thickness(Affix.Styles.BorderSize);
            FontFamily Font = Extensions.GetFont("DINPro Regular");


            Border Border = new System.Windows.Controls.Border
            {
                BorderThickness = BorderThickness,
                BorderBrush = Extensions.HexToBrush("#"+ Affix.Styles.BorderColor),
              
                Background = new SolidColorBrush
                {
                    Color = Extensions.HexToColor("#" + Affix.Styles.Background).Color,
                    Opacity = 0.9
                },
                Padding = new Thickness(5, 0, 5, 0)
            };

            Label AffixText = new Label
            {
                FontFamily = Font,
                Content = Affix.Name,
                Foreground =  Extensions.HexToBrush("#"+Affix.Styles.Foreground),
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            Border.Child = AffixText;
            Preview.Children.Add(Border);

            BorderSize.Text = Affix.Styles.BorderSize.ToString();
            BorderColor.Text = Affix.Styles.BorderColor.ToUpper();
            BorderColorBG.Background = Extensions.HexToBrush("#" + Affix.Styles.BorderColor);
            Background.Text = Affix.Styles.Background.ToUpper();
            BackgroundColorBG.Background = Extensions.HexToBrush("#" + Affix.Styles.Background);
            Foreground.Text = Affix.Styles.Foreground.ToUpper();
            FontColorBG.Background = Extensions.HexToBrush("#" + Affix.Styles.Foreground);
            Enabled.Source = (Affix.Enabled) ? Extensions.FKImage("checked") : Extensions.FKImage("_checked");
            */
        }

        public void Set()
        {
            /*App._root.PageContainer.Width = 457;
            App._root.PageContainer.Margin = new Thickness(0);

            Affix = App._root.SelectedItem.CastHelper<FKAffixes.FKAffix>();
            PaintPreview();*/
        }

        private void OpenPicker(object sender, MouseButtonEventArgs e)
        {

        }

        private void PreviewCheck(object sender, TextCompositionEventArgs e)
        {
            int hexNumber;
            e.Handled = !int.TryParse(e.Text, out hexNumber);
        }

        private void PreviewCheckHex(object sender, TextCompositionEventArgs e)
        {
            int hexNumber;
            e.Handled = !int.TryParse(e.Text, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.CurrentCulture, out hexNumber);
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            /*if (Affix == null || sender.CastVisual<TextBox>().Text.Length == 0)
                return;

            try
            {
                Affix.Styles.GetType().GetProperty(sender.CastVisual<TextBox>().Name).SetValue(Affix.Styles, int.Parse(sender.CastVisual<TextBox>().Text)) ;
                PaintPreview();
                Update();
            }

            catch (Exception ex)
            {
                sender.CastVisual<TextBox>().Text = Affix.Styles.GetType().GetProperty(sender.CastVisual<TextBox>().Name).GetValue(Affix.Styles, null).ToString();
                PaintPreview();
                Update();
            }

            sender.CastVisual<TextBox>().CaretIndex = sender.CastVisual<TextBox>().Text.Length;*/
        }

        private void TextChangedHex(object sender, TextChangedEventArgs e)
        {
           /* if (Affix == null || sender.CastVisual<TextBox>().Text.Length < 5)
                return;

            try
            {
                int number;

                if (!int.TryParse(sender.CastVisual<TextBox>().Text, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out number))
                    sender.CastVisual<TextBox>().Text = Affix.Styles.GetType().GetProperty(sender.CastVisual<TextBox>().Name).GetValue(Affix.Styles, null).ToString();

                Affix.Styles.GetType().GetProperty(sender.CastVisual<TextBox>().Name).SetValue(Affix.Styles, sender.CastVisual<TextBox>().Text.ToUpper());
                PaintPreview();
                Update();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Value needs to be a hex-string");
                PaintPreview();
                Update();
            }

            sender.CastVisual<TextBox>().CaretIndex = sender.CastVisual<TextBox>().Text.Length;*/
        }

        private void Enable(object sender, MouseButtonEventArgs e)
        {
           /* bool Value = Affix.Enabled.FlipBool();
            Affix.Enabled = Value;
            sender.CastVisual<Image>().Source = (Value) ? Extensions.FKImage("checked") : Extensions.FKImage("_checked");*/
        }

        private void Update()
        {
           /* Affix.Styles.BackgroundBrush = Extensions.HexToBrush("#" + Affix.Styles.Background);
            Affix.Styles.ForegroundBrush = Extensions.HexToBrush("#" + Affix.Styles.Foreground);
            Affix.Styles.BorderBrush = Extensions.HexToBrush("#" + Affix.Styles.BorderColor);*/
        }
    }
}
