using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using FindersKeepers.Templates.Templating.FKTemplates;
using FindersKeepers.Templates.Templating;

namespace FindersKeepers.Templates.Application
{
    [ImplementPropertyChanged]
    public class ApplicationController : LayoutHelper, IDesign
    {
        public IDesignHelper IDesignHelper { get; set; }
        public object DataHandler { get; set; }
        public IDesignUpdate CurrentTarget { get; set; }
        public bool IsTemplate { get;set; }

        public ApplicationController()
        {
            IDesignHelper = new IDesignHelper();
            IDesignHelper.Settings.UseSideControl = true;
            IDesignHelper.Settings.UseMainMenu = true;
        
            ApplicationMenu.Target = this;

            PopulateRightMenu();
            IDesignHelper.SideMenu[0].Target();
        }

        public void GenerateLink(ApplicationMenu.ApplicationType Type)
        {
            IDesignHelper.Menu.Clear();

            if (ApplicationMenu.DropdownEntries.ContainsKey(Type))
                IDesignHelper.RegisterIMenuContainer(ApplicationMenu.DropdownEntries[Type]);
            else
                IDesignHelper.UnregisterIMenuContainer();

            ApplicationMenu.TryGet(Type).ForEach((x) => {
                IDesignHelper.Menu.Add(x);
            });
        }

        public void PopulateRightMenu()
        {
            IDesignHelper.SideMenu.Add(new IDesignHelper.ISideMenu
            {
                Data = new { Name = "Ingame Overlay"},
                isActive = true,
                EntryId = 0,
                Target = (() =>  GenerateLink(ApplicationMenu.ApplicationType.Overlay))
            });

            IDesignHelper.SideMenu.Add(new IDesignHelper.ISideMenu
            {
                Data = new { Name = "Minimap Overlay & NPC" },
                isActive = false,
                EntryId = 1,
                HasDropdown = true,
                Target = (() => GenerateLink(ApplicationMenu.ApplicationType.MinimapNPC) )
            });

            IDesignHelper.SideMenu.Add(new IDesignHelper.ISideMenu
            {
                Data = new { Name = "Sound Manager" },
                isActive = false,
                EntryId = 2,
            });

            IDesignHelper.SideMenu.Add(new IDesignHelper.ISideMenu
            {
                Data = new { Name = "Elite Packs" },
                isActive = false,
                EntryId = 3,
                HasDropdown = true,
                Target = (() => GenerateLink(ApplicationMenu.ApplicationType.ElitePacks))
            });

            IDesignHelper.SideMenu.Add(new IDesignHelper.ISideMenu
            {
                Data = new { Name = "Ingame Overlay" },
                isActive = false,
                EntryId = 4,
            });

            IDesignHelper.SideMenu.Add(new IDesignHelper.ISideMenu
            {
                Data = new { Name = "Overlay Items" },
                isActive = false,
                EntryId = 5,
            });

            IDesignHelper.SideMenu.Add(new IDesignHelper.ISideMenu
            {
                Data = new { Name = "Styles" },
                isActive = false,
                EntryId = 6,
            });

            IDesignHelper.SideMenu.Add(new IDesignHelper.ISideMenu
            {
                Data = new { Name = "Miscellanous" },
                isActive = false,
                EntryId = 7,
            });
        }

        public void SetTarget(object Item)
        {
            if (CurrentTarget is IDesignUpdate)
                CurrentTarget.CollectionChanged();
        }
    }
}
