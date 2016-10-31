using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Enigma.D3.Helpers;
using System.Globalization;
using Enigma.Memory;
using FindersKeepers.DebugWorker;

namespace FindersKeepers
{

    public static class Effects
    {
        public static Extensions.EffectEntity Animate(this Extensions.EffectEntity Element, DependencyProperty Property, int Duration, Extensions.FadeDirection Direction)
        {
            SolidColorBrush bElement = Element.Get<SolidColorBrush>("Background");
            ColorAnimation Animation;

            if (Element.Data != null)
                Animation = ReturnAnimation(Element, Duration, Direction);
            else
                Animation = new ColorAnimation { From = Element.Has<string>("From").ToColor(false), To = Element.Has<string>("To").ToColor(false), Duration = new Duration(TimeSpan.FromMilliseconds(Duration)) };

            Element.If<ColorAnimation>(Animation);

            bElement.BeginAnimation(Property, Animation);
            return Element;
        }

        public static Extensions.EffectEntity AnimateBorder(this Extensions.EffectEntity Element, int Duration, Extensions.FadeDirection Direction)
        {
                if (Element.Element.GetType() != typeof(Border))
                    return Element;

                Border Border = (Border)Element.Element;
                ColorAnimation Animation = new ColorAnimation
                {
                    To = (Direction == Extensions.FadeDirection.FadeIn) ? Element.Data.Effects.Hover.ToColor(false) : "1e3c53".ToColor(false),
                    From = (Direction == Extensions.FadeDirection.FadeIn) ? "1e3c53".ToColor(false) : Element.Data.Effects.Hover.ToColor(false),
                    Duration = new Duration(TimeSpan.FromMilliseconds(Duration)),
                    EasingFunction = Element.Has<EasingFunctionBase>("Easing")
                };

                Storyboard sb = new Storyboard();
                sb.Children.Add(Animation);
                Storyboard.SetTarget(Animation, Border);
                Storyboard.SetTargetProperty(Animation, new PropertyPath("(Border.BorderBrush).(SolidColorBrush.Color)"));
                sb.Begin();

            return Element;
        }

        public static Extensions.EffectEntity AnimateBorder(this FrameworkElement Border, Extensions.EffectEntity Element, int Duration, Extensions.FadeDirection Direction)
        {
            if (Border.GetType() != typeof(Border))
                return Element;

            Border B = (Border)Border;
            ColorAnimation Animation = new ColorAnimation
            {
                To = (Direction == Extensions.FadeDirection.FadeIn) ? Element.Data.Effects.Hover.ToColor(false) : "475966".ToColor(false),
                From = (Direction == Extensions.FadeDirection.FadeIn) ? "475966".ToColor(false) : Element.Data.Effects.Hover.ToColor(false),
                Duration = new Duration(TimeSpan.FromMilliseconds(Duration)),
                EasingFunction = Element.Has<EasingFunctionBase>("Easing")
            };
            Storyboard sb = new Storyboard();
            sb.Children.Add(Animation);
            Storyboard.SetTarget(Animation, Border);
            Storyboard.SetTargetProperty(Animation, new PropertyPath("(Border.BorderBrush).(SolidColorBrush.Color)"));
            sb.Begin();

            return Element;
        }

        public static Extensions.EffectEntity Set<T>(this Extensions.EffectEntity Element, string Property, T Value)
        {
            Element.Element.GetType().GetProperty(Property).SetValue(Element.Element, Value);
            return Element;
        }

        public static Extensions.EffectEntity Opacity(this Extensions.EffectEntity Element, double Value)
        {
            Brush Background = Element.Get<Brush>("Background");
            Background.GetType().GetProperty("Opacity").SetValue(Background, Value);

            return Element;
        }

        public static Extensions.EffectEntity AnimateOpacity(this Extensions.EffectEntity Element, double FromValue, double ToValue, int Duration)
        {
            DoubleAnimation Animation = new DoubleAnimation(FromValue, ToValue, new Duration(TimeSpan.FromMilliseconds(Duration)), FillBehavior.Stop); 
            Element.If<DoubleAnimation>(Animation);

            Brush bElement = Element.Get<SolidColorBrush>("Background");

            bElement.BeginAnimation(SolidColorBrush.OpacityProperty, Animation);
            return Element;
        }

        public static T Get<T>(this Extensions.EffectEntity Element, string Property) where T : class
        {
            return (T)Element.Element.GetType().GetProperty(Property).GetValue(Element.Element);
        }

        public static void If<T>(this Extensions.EffectEntity Element, T Animation) where T : Timeline
        {
            if (Element.Func != null)
                Animation.Completed += ((s, e) => { Element.Func.Invoke(); Element.Func = null; });
        }

        public static Extensions.EffectEntity Event(this Extensions.EffectEntity Element, Action Invoke)
        {
            Element.Func = Invoke;
            return Element;
        }

        public static T Has<T>(this Extensions.EffectEntity Element, string property) where T : class
        {
            if (Element.Properties.ContainsKey(property))
               return (T)Element.Properties[property];

            return null;
        }

        public static ColorAnimation ReturnAnimation(Extensions.EffectEntity Element, int Duration, Extensions.FadeDirection Direction)
        {
            return new ColorAnimation
            {
                To = (Direction == Extensions.FadeDirection.FadeIn) ? Element.Data.Effects.Hover.ToColor(false) : Element.Data.Effects.Default.ToColor(false),
                From = (Direction == Extensions.FadeDirection.FadeIn) ? Element.Data.Effects.Default.ToColor(false) : Element.Data.Effects.Hover.ToColor(false),
                Duration = new Duration(TimeSpan.FromMilliseconds(Duration)), EasingFunction = Element.Has<EasingFunctionBase>("Easing")
            };
        }

        public static Extensions.EffectEntity Register(this Extensions.EffectEntity Element, string Property, object Value)
        {
            Element.Properties.Add(Property, Value);
            return Element;
        }

    }

    public static class Extensions
    {
        public static EffectEntity Create(this FrameworkElement e)
        {
             return new EffectEntity(e);
        }

        public class EffectEntity
        {
            public FrameworkElement Element { get; set; }
            public Assets.ISimpleAnimate Data { get { return Element.Tag as Assets.ISimpleAnimate; } }
            public Dictionary<string, object> Properties { get;set; }
            public Action Func { get; set; }
            public EffectEntity(FrameworkElement e) { Element = e; Properties = new Dictionary<string, object>(); }
        }

        public static class Execute
        {
            public static List<Action> UIThreadInvoke { get; set; }

            public static void UIThread(Action action, bool Invoke = true)
            {
                try
                {
                    var dispatcher = Application.Current.Dispatcher;
                    if (dispatcher.CheckAccess())
                    {
                        action.Invoke();
                    }
                    else
                    {
                        if(!Invoke)
                            dispatcher.BeginInvoke(action);
                        else
                            dispatcher.Invoke(action);
                    }
                }

                catch (Exception e)
                {
                    DebugWriter.Write(e);
                }
            }

            public static void InvokeUI(Action action)
            {
                UIThreadInvoke.Add(action);
            }

            public static void Render()
            {
                try
                {
                    UIThread(() =>
                    {
                        foreach (var x in Execute.UIThreadInvoke.ToList())
                            x.Invoke();

                        Execute.UIThreadInvoke.Clear();
                    }, false);
                }

                catch (Exception e)
                {
                    DebugWriter.Write(e);
                }
            }

        }

        public static class Performance
        {
            public static List<long> Numbers = new List<long>();
            public static System.IO.StreamWriter fs;

          /*  public static void WriteToFile(string name, Action Action , out System.IO.StreamWriter Writer) 
            {
               Writer = new System.IO.StreamWriter("aa.txt",true);
               Action.Invoke();
            }*/

            public static void Start(Action x)
            {
                System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch();
                w.Start();

                x.Invoke();
                w.Stop();
                Numbers.Add(w.ElapsedTicks);
            }

            public static void Save()
            {
                if (Numbers.Count < 1)
                    return;

                Numbers.Sort();

                using (fs = new System.IO.StreamWriter("./_PerformanceData.txt", true))
                {
                    fs.WriteLine("-------------------------------------");
                    fs.WriteLine("Average: "+ (Numbers.Sum() / Numbers.Count) );
                    fs.WriteLine("Highest: "+ Numbers.Last());
                    fs.WriteLine("Lowest: "+ Numbers.First());
                }
            }
        }

        public static Brush HexToBrush(string Hex, bool Freeze = true)
        {
            try
            {
                if (Hex == "Transparent")
                {
                    SolidColorBrush color = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    if(Freeze)
                        color.Freeze();

                    return color;
                }

                Brush colors = (Brush)new BrushConverter().ConvertFrom(Hex);
                if (Freeze)
                    colors.Freeze();
                return colors;
            }

            catch
            {
                return null;
            }
        }

        public static T CastVisual<T>(this object obj)
        {
            return (T)(obj);
        }

        public static Pen GetPen(Brush Brush, double border)
        {
            try
            {
                Pen pen = new Pen(Brush, border);
                pen.Freeze();
                return pen;
            }

            catch
            {
                return null;
            }
        }

        public static SolidColorBrush HexToColor(string Hex, bool Freeze = true)
        {
            try
            {
                SolidColorBrush Colors = default(SolidColorBrush);
                if (Hex == "Transparent")
                    Colors = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                else
                    Colors = ((SolidColorBrush)new BrushConverter().ConvertFrom(Hex));

                if (Freeze)
                    Colors.Freeze();

                return Colors;
            }

            catch
            {
                return null;
            }
        }

        public static NumberFormatInfo Info = CultureInfo.InvariantCulture.NumberFormat;
        public static string MakeString(this double Double)
        {
            return Double.ToString(Info);
        }

        public static BitmapImage FKImage(string ImageName)
        {   
            try
            {
                BitmapImage Image = new BitmapImage(new Uri("pack://application:,,,./Images/FK/"+ImageName+".png"));
                Image.Freeze();
                return Image;
            }

            catch
            {
                return null;
            }
        }

        public static BitmapSource RotateImage(string Image)
        {
            try
            {
                TransformedBitmap tBmp = new TransformedBitmap();
                tBmp.BeginInit();

                tBmp.Source = new BitmapImage(new Uri("pack://application:,,,./Images/FK/" + Image + ".png"));
                RotateTransform rt = new RotateTransform { Angle = 0 };
                tBmp.Transform = rt;
                tBmp.EndInit();

                //Create a new source after the transform
                BitmapSource s1 = tBmp;
                return s1;
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return null;
        }

        public static BitmapImage ItemImage(string ImageName)
        {
            try
            {
                BitmapImage Image = new BitmapImage(new Uri("pack://application:,,,/FK.UI;Component/Items/" + ImageName + ".png"));
                Image.Freeze();
                return Image;
            }

            catch
            {
                return null;
            }
        }

        public static FKMethods.TagHelper TagHelper(this object Obj)
        {
            return (FKMethods.TagHelper)((Border)Obj).Tag;
        }

        public static object TagHelper(this object Obj, Type Type)
        {
            return (object)Convert.ChangeType(((Control)Obj).Tag, Type);
        }

        public static T TagHelper<T>(this object Obj)
        {
            return (T)((Control)Obj).Tag;
        }

        public static T CastHelper<T>(this object Obj)
        {
            try
            {
                return (T)Obj;
            }

            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                return default(T);
            }
        }

        public static void AnimateForeground(this UIElement Element, object Transition, bool Leaving = false, bool Skip = false)
        {
            try
            {
                SolidColorBrush ElementBackground = (SolidColorBrush)Element.GetType().GetProperty("Foreground").GetValue(Element);
                FKMethods.TagHelper TagHelper = (FKMethods.TagHelper)Transition;

                ColorAnimation Animation = new ColorAnimation();
                Animation.From = (Leaving) ? TagHelper.Transition.Hover.Color : TagHelper.Transition.Reset.Color;
                Animation.To = (Leaving) ? TagHelper.Transition.Reset.Color : TagHelper.Transition.Hover.Color;
                Animation.Duration = new Duration(TimeSpan.FromMilliseconds((Leaving) ? TagHelper.Transition.FadeOut : TagHelper.Transition.FadeIn));
                ElementBackground.BeginAnimation(SolidColorBrush.ColorProperty, Animation);
            }

            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public static void AnimateBackground(this Border Element, object Transition, bool Leaving = false, bool Skip = false)
        {
            try
            {
                if (Transition is FKMethods.TagHelper)
                {
                    SolidColorBrush ElementBackground = (SolidColorBrush)Element.GetType().GetProperty("Background").GetValue(Element);
                    FKMethods.TagHelper TagHelper = (FKMethods.TagHelper)Transition;

                    if (ElementBackground.ToString() == TagHelper.Transition.Active.ToString().ToString() && !Skip)
                        return;

                    if (Element.Child is Border)
                    {
                        SolidColorBrush ResetBorder = (TagHelper.Transition.ResetBorder == default(SolidColorBrush)) ? Extensions.HexToColor("#475966") : TagHelper.Transition.ResetBorder;

                        Border BorderChild = (Border)Element.Child;
                        ColorAnimation BorderAnimation = new ColorAnimation();
                        BorderAnimation.From = (Leaving) ? TagHelper.Transition.Hover.Color : ResetBorder.Color;
                        BorderAnimation.To = (Leaving) ? ResetBorder.Color : TagHelper.Transition.Hover.Color;
                        BorderAnimation.Duration = new Duration(TimeSpan.FromMilliseconds((Leaving) ? TagHelper.Transition.FadeOut : TagHelper.Transition.FadeIn));
                        Storyboard sb = new Storyboard();
                        sb.Children.Add(BorderAnimation);
                        Storyboard.SetTarget(BorderAnimation, BorderChild);
                        Storyboard.SetTargetProperty(BorderAnimation, new PropertyPath("(Border.BorderBrush).(SolidColorBrush.Color)"));
                        sb.Begin();
                    }

                    ElementBackground.Opacity = 0.6;

                    ColorAnimation Animation = new ColorAnimation();
                    Animation.From = (Leaving) ? TagHelper.Transition.Hover.Color : TagHelper.Transition.Reset.Color;
                    Animation.To = (Leaving) ? TagHelper.Transition.Reset.Color : TagHelper.Transition.Hover.Color;
                    Animation.Duration = new Duration(TimeSpan.FromMilliseconds((Leaving) ? TagHelper.Transition.FadeOut : TagHelper.Transition.FadeIn));
                    ElementBackground.BeginAnimation(SolidColorBrush.ColorProperty, Animation);
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static T FindChild<T>(this DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        public static bool FlipBool(this bool value)
        {
            return (value) ? false : true;
        }

        public static T GetVisual<T>(this Visual myVisual, string Property, string Name) where T : UIElement
        {
            T Found = null;
            try
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(myVisual); i++)
                {
                    Visual childVisual = (Visual)VisualTreeHelper.GetChild(myVisual, i);

                    if ((childVisual as T) == null)
                    {
                        Found = GetVisual<T>(childVisual, Property, Name);

                        if (Found != null)
                            break;
                    }

                    else if (childVisual.GetType() == typeof(T))
                    {
                        T Visual = childVisual.CastHelper<T>();
                        if (Visual.GetType().GetProperty(Property).GetValue(Visual) != null)
                        {
                            if (Visual.GetType().GetProperty(Property).GetValue(Visual).ToString() == Name)
                            {
                                Found = Visual;
                                break;
                            }
                        }
                    }
                }
            }

            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return Found;
        }


        public static ImageSource ToImage(this string ImageName)
        {
            try
            {
                return new Image().Source = new BitmapImage(new Uri(ImageName));
            }

            catch
            {
                return null;
            }
        }

        public static FontFamily GetFont(string Font)
        {
            return new FontFamily(new Uri("pack://application:,,,/Images/"), "./#"+Font);
        }

        public static double GetAttribute(this Enigma.D3.ActorCommonData Actor, Enigma.D3.Enums.AttributeId Attrib, int Modifier = -1)
        {
            try
            {
                return Actor.GetAttributeValue(Attrib, Modifier);
            }

            catch
            {
                return -1;
            }
        }

        public static T FindParent<T>(this DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }

        public static object Create(this Type Type, BaseDesign _root)
        {
            return Activator.CreateInstance(Type, new object[] { _root });
        }

        public static T Create<T>(object p)
        {
            return (T)Activator.CreateInstance(typeof(T), p);
        }

        public static object CreatePage(this Type Type)
        {
            return Activator.CreateInstance(Type, null);
        }

        public static object CreatePage(this Type Type, object Params)
        {
            return Activator.CreateInstance(Type, Params);
        }

        public static T CallUI<T>(this UIElement owner, string Name, string Attrib) where T : FrameworkElement
        {
            T _control = default(T);

            Extensions.Execute.UIThread(() =>
            {
                _control = owner.GetVisual<T>(Name, Attrib);
            });

            return (T)_control;
        }

        public static T SetUI<T>(this UIElement owner, string Name, object value) where T : UIElement
        {
            Extensions.Execute.UIThread(() =>
            {
                owner.GetType().GetProperty(Name).SetValue(owner, value);
            });

            return (T)owner;
        }

        public static string FormatNumber(this double Number, string suffix = "")
        {
            if (suffix == "/h" || suffix == "%h" || suffix == "#")
            {
                if (Number > 1000000000)
                    return Number.ToString("#,#0,,,.##B", System.Globalization.CultureInfo.InvariantCulture);
                else if (Number > 1000000)
                    return Number.ToString("#,#0,,M", System.Globalization.CultureInfo.InvariantCulture);
                else if (Number > 100000)
                    return Number.ToString("#,#0,K", System.Globalization.CultureInfo.InvariantCulture);
            }

            return Number.ToString();
        }

        public static string FormatNumber(this float Number, string suffix = "")
        {
            string Value = Number.ToString();

            if (Number > 1000000000)
                Value = Number.ToString("#,#0,,,.##B", System.Globalization.CultureInfo.InvariantCulture);
            else if (Number > 1000000)
                Value = Number.ToString("#,#0,,M", System.Globalization.CultureInfo.InvariantCulture);
            else if (Number > 100000)
                Value = Number.ToString("#,#0,K", System.Globalization.CultureInfo.InvariantCulture);

            return Value + suffix;
        }


        public static T ReadChain<T>(this MemoryReader reader, MemoryAddress address, params int[] offsets)
        {
            for (int i = 0; i < offsets.Length; i++)
            {
                address = reader.Read<MemoryAddress>(address);
                address += offsets[i];
            }
            return reader.Read<T>(address);
        }

        public static T AnimateFade<T>(this UIElement e, double fromValue, double toValue, Duration Duration, TimeSpan Wait = default(TimeSpan), Action Completed = null) where T : UIElement
        {
            if (e.Opacity != fromValue) // Animation in progress
                return default(T);

            DoubleAnimation Animation = new DoubleAnimation(fromValue, toValue, Duration);

            if (Wait != default(TimeSpan))
                Animation.BeginTime = Wait;
            if(Completed != null)
                Animation.Completed += ((s,ex) => { Completed.Invoke(); });

			e.BeginAnimation(FrameworkElement.OpacityProperty, Animation, HandoffBehavior.SnapshotAndReplace);
			return (T)e;
        }

        public static T AnimateSize<T>(this UIElement e, double fromValue, double toValue, Duration Duration, TimeSpan Wait = default(TimeSpan), Action Completed = null) where T : UIElement
        {
            DoubleAnimation Animation = new DoubleAnimation(fromValue, toValue, Duration){ AutoReverse = true, RepeatBehavior = RepeatBehavior.Forever };

            if (Wait != default(TimeSpan))
                Animation.BeginTime = Wait;
            if (Completed != null)
                Animation.Completed += ((s, ex) => { Completed.Invoke(); });

            e.BeginAnimation(FrameworkElement.WidthProperty, Animation, HandoffBehavior.SnapshotAndReplace);
            e.BeginAnimation(FrameworkElement.HeightProperty, Animation, HandoffBehavior.SnapshotAndReplace);

            return (T)e;
        }

        public static T AnimateDependency<T>(this UIElement e, DependencyProperty prop, double fromValue, double toValue, Duration Duration) where T : UIElement
        {
            DoubleAnimation Animation = new DoubleAnimation(fromValue, toValue, Duration) { AutoReverse = false, EasingFunction = new CircleEase() };

            e.BeginAnimation(prop, Animation, HandoffBehavior.SnapshotAndReplace);
            return (T)e;
        }

        public static T Animate<T>(this UIElement e, List<DependencyProperty> Animate, double fromValue, double toValue, Action Completed = null) where T : UIElement
        {
            DoubleAnimation Animation = new DoubleAnimation(fromValue, toValue, new Duration()) { AutoReverse = false, EasingFunction = new BackEase() };

            if (Completed != null)
                Animation.Completed += ((s, ex) => { Completed.Invoke(); });


            foreach(var Invoke in Animate)
                e.BeginAnimation(Invoke, Animation, HandoffBehavior.SnapshotAndReplace);

            return (T)e;
        }

        public static FrameworkElement AnimateDependencyBackground<T>(this FrameworkElement Element, DependencyProperty Property, int Duration, FadeDirection Direction) where T : Assets.ISimpleAnimate
        {
            T Data = (T)Element.Tag;
            SolidColorBrush bElement = (SolidColorBrush)Element.GetType().GetProperty("Background").GetValue(Element);
            bElement.Opacity = 0.6;

            ColorAnimation Animation = ReturnAnimation(Data, Duration, Direction);
            bElement.BeginAnimation(Property, Animation);

            return Element;
        }

        public static ColorAnimation ReturnAnimation(Assets.ISimpleAnimate Data, int Duration, FadeDirection Direction)
        {
            return new ColorAnimation
            {
                To = (Direction == FadeDirection.FadeIn) ? Data.Effects.Hover.ToColor() : Data.Effects.Default.ToColor(),
                From = (Direction == FadeDirection.FadeIn) ? Data.Effects.Default.ToColor() : Data.Effects.Hover.ToColor(),
                Duration = new Duration(TimeSpan.FromMilliseconds(Duration))
            };
        }

        public static void After(this ColorAnimation a, Action e)
        {
            a.Completed += ((s,es) => e.Invoke());
        }

        public static T GetNestedReflection<T>(string Value, object objectref)
        {
            return Extensions.TryInvoke<T>(() =>
            {
                string[] f = Value.Split('.');
                object info = objectref;

                foreach (string value in f)
                    info = info.GetType().GetProperty(value).GetValue(info);

                return (T)info;
             });
        }

        public static void SetNewValue<T>(string Value, object objectref, T NewValue)
        {
            string[] fields = Value.Split('.');
            string propertyField = fields.LastOrDefault();
            fields = fields.Take(fields.Count() - 1).ToArray();

            object obj = GetBaseCollection(fields, objectref);
            object oldValue = obj.GetType().GetProperty(propertyField).GetValue(obj);

            object newValue = NewValue;

            if (typeof(T) == typeof(bool))
                newValue = ((bool)oldValue).FlipBool();
            
            obj.GetType().GetProperty(propertyField).SetValue(obj, newValue);
        }

        public static object GetBaseCollection(string[] Value, object objectref)
        {
            object info = objectref;

            foreach (string value in Value)
                info = info.GetType().GetProperty(value).GetValue(info);

            return info;
        }

        public static void SetValue<T>(this System.Reflection.PropertyInfo objectref, T value)
        {
            objectref.SetValue(objectref, value);
        }

        public static Color ToColor(this string color, bool Freeze = true)
        {
            if (color == "Transparent")
                return Extensions.ToTransparent(Freeze).Color;

            return Extensions.HexToColor(color.IndexOf('#') == -1 ? "#"+color : color, Freeze).Color;
        }

        public static Brush ToBrush(this string color, bool Frozen = true)
        {
            return Extensions.HexToBrush((color.IndexOf('#') == -1 ? "#" + color : color), Frozen);
        }

        public static SolidColorBrush ToTransparent(bool freeze = true)
        {
            SolidColorBrush color = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

            if (freeze)
                color.Freeze();

            return color;
        }

        public enum FadeDirection
        {
            FadeOut,
            FadeIn
        }

        /// <summary>
        /// Values [ First Value in both Tuples is Height, Second in both is the opacity ] 
        /// <para>First Value in both Tuples = Height</para> 
        /// <para>Last Value in both Tuples = Opacity</para> 
        /// <para>Set Height to -1 = No height animation</para> 
        ///  </summary>
        public static T Fade<T>(this UIElement e, FadeDirection Direction, Tuple<double,double> fromValue, Tuple<double, double> toValue, Duration Duration, TimeSpan Wait = default(TimeSpan), Action Completed = null) where T : UIElement
        {
            DoubleAnimation AnimationHeight = new DoubleAnimation(fromValue.Item1, toValue.Item1, Duration) ;
            DoubleAnimation Animation = new DoubleAnimation(fromValue.Item2, toValue.Item2, Duration);

            if (Direction == FadeDirection.FadeIn && e.Visibility == Visibility.Collapsed)
                e.Visibility = Visibility.Visible;

           // if (Direction == FadeDirection.FadeIn && e.Visibility == Visibility.Collapsed)
             //   e.Visibility = Visibility.Visible;


            if (Wait != default(TimeSpan))
                Animation.BeginTime = Wait;
            if (Completed != null)
                Animation.Completed += ((s, ex) =>
                {
                    Completed.Invoke();
                });

            else
            {
                Animation.Completed += ((s, ex) =>
                {
                    if (Direction == FadeDirection.FadeOut && e.Visibility == Visibility.Visible)
                        e.Visibility = Visibility.Collapsed;

                    if (Direction == FadeDirection.FadeIn && e.Visibility == Visibility.Collapsed)
                        e.Visibility = Visibility.Visible;
                });
            }

            e.BeginAnimation(FrameworkElement.OpacityProperty, Animation, HandoffBehavior.SnapshotAndReplace);
            e.BeginAnimation(FrameworkElement.HeightProperty, AnimationHeight, HandoffBehavior.SnapshotAndReplace);

            return (T)e;
        }

        public static bool TryInvoke(this Action TryAction)
        {
            try
            {
                TryAction.Invoke();
                return true;
            }   

            catch(Exception e)
            {
                DebugWriter.Write(e);
            }

            return false;
        }

        public static T TryInvoke<T>(Func<T> Action, bool CreateLog = false) 
        {
            try
            {
                return Action.Invoke();
            }

            catch (Exception e)
            {
                if(CreateLog)
                    DebugWriter.Write(e);
            }

            return default(T);
        }

    }
}
