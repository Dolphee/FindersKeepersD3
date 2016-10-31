using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using FindersKeepers.Templates.Templating.FKTemplates;
using FindersKeepers.Templates.Templating;

namespace FindersKeepers.Templates.Support
{
    [ImplementPropertyChanged]
    public class SupportController : LayoutHelper, IDesign
    {
        public IDesignHelper IDesignHelper { get; set; }
        public object DataHandler { get; set; }
        public IDesignUpdate CurrentTarget { get; set; }
        public bool IsTemplate { get; set; }

        public SupportController()
        {
            IDesignHelper = new IDesignHelper();
            IDesignHelper.Settings.UseSideControl = true;
            IDesignHelper.Settings.UseMainMenu = false;

            PopulateRightMenu();
            IDesignHelper.SideMenu[0].Target();
        }

        public void PopulateRightMenu()
        {
            IDesignHelper.SideMenu.Add(new IDesignHelper.ISideMenu
            {
                Data = new { Name = "General Settings" },
                isActive = true,
                EntryId = 0,
                Target = (() => GetPage<GeneralSettings>())
            });

            IDesignHelper.SideMenu.Add(new IDesignHelper.ISideMenu
            {
                Data = new { Name = "Multibox Macros" },
                isActive = false,
                EntryId = 1,
                HasDropdown = true,
                Target = (() => GetPage<MultiMacro>())
            });

            IDesignHelper.SideMenu.Add(new IDesignHelper.ISideMenu
            {
                Data = new { Name = "Multibox Accounts" },
                isActive = false,
                EntryId = 2,
                Target = (() => GetPage<Accounts>())
            });

            IDesignHelper.SideMenu.Add(new IDesignHelper.ISideMenu
            {
                Data = new { Name = "Item Tracker" },
                isActive = false,
                EntryId = 3, 
                HasDropdown = true,
            });

            IDesignHelper.SideMenu.Add(new IDesignHelper.ISideMenu
            {
                Data = new { Name = "Experience Tracker" },
                isActive = false,
                EntryId = 4,
                HasDropdown = true,
            });

            IDesignHelper.SideMenu.Add(new IDesignHelper.ISideMenu
            {
                Data = new { Name = "WebTracker" },
                isActive = false,
                EntryId = 5,
                HasDropdown = true,
            });

        }
        public void SetTarget(object Item)
        {
            if (CurrentTarget is IDesignUpdate)
                CurrentTarget.CollectionChanged();
        }
    }
}
