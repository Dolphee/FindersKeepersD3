using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller;
using System.Windows.Controls;
using FK.UI;

namespace FindersKeepers
{

    public interface IGameStatus
    {
        void OnExit();
        void OnJoin();
        void OnDestroy();
        void OnCreation();
    }

    public interface IFKControl
    {
        void IUpdate();
    }


    public interface IGameChanged
    {
        void IGameChanged();
    }

    public interface IWPF
    {
        UserControl Control
        {
            get; set;
        }

        void Empty();

    }

    public interface IFKWPF
    {
        IFKControl Control
        {
            get; set;
        }
    }
    interface IRefresh
    {
        void Set();
    }
    
    interface SetDefault
    {
        object _DEFAULT();
    }

    interface SetDefault<T>
    {
        T _DEFAULT();
    }

    interface HasName
    {
        string Name
        {
            get; set;
        }
    }

    interface ICache
    {
        void OnStartup();
    }

    public class TuplePair<T1, T2>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public TuplePair() { }
        public TuplePair(T1 First, T2 Second)
        {
            Item1 = First;
            Item2 = Second;
        }
    }

    public class OverlayItems : IDisposable
    {
        public SimpleItem Item {get;set; }
        public Type ItemType { get; set; }
        public ushort Transition { get; set; }
        public FKAccounts.Multibox Account;
        public UniqueItem _Legendary { get; set; }
        public UniqueItem Legendary { get { return _Legendary; } set { } }
        public System.Windows.Media.Imaging.BitmapImage PathType { get; set; }
        public bool Close { get; set; }
        private System.Threading.Timer Timer;
        public int Position {get;set; }

        public OverlayItems()
        {
            Timer = new System.Threading.Timer((state) =>
            {
                Timer.Change(System.Threading.Timeout.Infinite, 5000);
                Close = true;
            });

            Timer.Change(5000, 5000);
        }

        public void Dispose()
        {
            if(Timer != null)
                Timer.Dispose();
        }

        public enum Type 
        {
            Item,
            Other
        }

        public class SimpleItem
        {
            public FindersKeepers.Controller.Enums.ItemQuality ItemQuality {get;set; }
            public Helpers.SNO.ItemData SNOItem { get; set; }
            public bool AncientItem { get; set; }
            public bool ShowAncient { get; set; }

            public SimpleItem(Helpers.Item I)
            {
                ItemQuality = I.ItemQuality;
                SNOItem = I.SNOItem;
                AncientItem = I.AncientItem;
                ShowAncient = I.ShowAncient;
            }
        }

    }

    public static class FKMethods
    {
        public static System.Windows.Media.FontFamily _HelveticaFont;
        public static System.Windows.Media.FontFamily _DinProFont;

        public static System.Windows.Media.FontFamily HelveticaFont
        {
            get
            {
                if (_HelveticaFont == null)
                    _HelveticaFont = Extensions.GetFont("Helvetica Rounded LT Black");

                return _HelveticaFont;
            }
            set
            {
                _HelveticaFont = Extensions.GetFont("Helvetica Rounded LT Black");
            }
        }

        public static System.Windows.Media.FontFamily DinProFont
        {
            get
            {
                if (_DinProFont == null)
                    _HelveticaFont = Extensions.GetFont("DINPro.otf#DINPro Regular");

                return _DinProFont;
            }
            set
            {
                _DinProFont = Extensions.GetFont("DINPro.otf#DINPro Regular");
            }
        }

        public struct TagHelper
          {
              public object Handler {get;set; }
              public Type Link { get; set; }
              public bool Template { get; set; }
              public bool HideTopMenu { get; set; }
              public FKMenuHelper.MenuStruct.MouseEnter.Colors Transition { get; set; }
          }            
    }
}
