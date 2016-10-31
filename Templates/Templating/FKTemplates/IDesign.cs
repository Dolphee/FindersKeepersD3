using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindersKeepers.Controller;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;
using PropertyChanged;

namespace FindersKeepers.Templates.Templating.FKTemplates
{
    public interface IDesign
    {
        IDesignHelper IDesignHelper {get; set; }
        IDesignUpdate CurrentTarget { get; set; }
        bool IsTemplate { get;set; }
        void SetTarget(object Target);
    }

    public interface IMainUpdate
    {
        void UpdateMain(UIElement Control);
    }

    public interface IDesignUpdate
    {
        void CollectionChanged();
    }

    [ImplementPropertyChanged]
    public class IDesignHelper
    {
        public ISettings Settings {get; set; }
        public ObservableCollection<ISideMenu> SideMenu { get; set; }
        public ObservableCollection<IMenu> Menu { get; set; }
        public IMenuContainer DropDown { get; set; }
        public bool HasDropDown { get { return DropDown != null; } }

        [ImplementPropertyChanged]
        public class ISettings
        {
            public bool SideControlIcons { get;set; }
            public bool UseSideControl { get; set; }
            public bool UseMainMenu { get; set; }
            public bool UseDropDown {get;set; }
            public bool UseFillMain {get;set; }
        }

        [ImplementPropertyChanged]
        public class ISideMenu
        {
            public int EntryId {get;set; }
            public object Data {get;set; }
            public bool isActive { get; set; }
            public bool isLast { get; set; }
            public Action Target { get; set; }
            public object MenuData { get;set; }
            public bool HasDropdown {get;set; }
        }

        [ImplementPropertyChanged]
        public class IMenu
        {
            public bool isActive { get;set; }
            public bool isLast { get; set; }
            public bool isFirst { get; set; }
            public object Template {get;set; }
            public string Name { get; set; }
            public Action Target { get; set; }
            public ImageSource Image {get; set; }
            public ImageSource ImageHover { get; set; }
            public object Data {get; set; }
        }

        [ImplementPropertyChanged]
        public class IMenuContainer
        {
            public ObservableCollection<object> DataSet {get;set; }
            public Action Target { get; set; }

            public object Selected { get {
                    return DataSet.FirstOrDefault();
                } set { } }

            public object Data {get;set; }

            [ImplementPropertyChanged]
            public class IMenuContainerHelper
            {
                public object DataSet {get;set; }
                public string Name { get; set; }
                public bool isActive {get;set; }
            }
        }

        public void RegisterIMenuContainer(IEnumerable<object> List)
        {
            DropDown = new IMenuContainer();
            DropDown.DataSet = new ObservableCollection<object>();

            DropDown.Data = List.FirstOrDefault();

            foreach (object obj in List)
                DropDown.DataSet.Add(obj);
        }

        public void UnregisterIMenuContainer()
        {
            DropDown = null;
        }

        public IDesignHelper()
        {
            Settings = new ISettings();
            SideMenu = new ObservableCollection<ISideMenu>();
            Menu = new ObservableCollection<IMenu>();
        }
    }
}
