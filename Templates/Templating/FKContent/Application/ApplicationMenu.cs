using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using FindersKeepers.Templates.Templating.FKTemplates;
using FindersKeepers.Templates.Application;

namespace FindersKeepers.Templates.Templating
{
    [ImplementPropertyChanged]
    public class ApplicationMenu
    {
        public static Dictionary<ApplicationType, List<IDesignHelper.IMenu>> Entry { get;set; }
        public static Dictionary<ApplicationType, IEnumerable<object>> DropdownEntries = new Dictionary<ApplicationType, IEnumerable<object>>();
        public static ApplicationController Target { get;set; }

        public static bool AlreadyExists(ApplicationType type)
        {
            if (Entry == null)
                Entry = new Dictionary<ApplicationType, List<IDesignHelper.IMenu>>();

            return Entry.ContainsKey(type);
        }

        public static List<IDesignHelper.IMenu> TryGet(ApplicationType type) 
        {
            if (!AlreadyExists(type))
                Populate();

            return GetLink(type);
        }

        public static List<IDesignHelper.IMenu> GetLink(ApplicationType type)
        {
            Extensions.TryInvoke(() => {
                    Target.DataHandler = Entry[type].Single(x => x.isActive).Data;
                    Entry[type].Single(x => x.isActive).Target.Invoke();
                }
            );

            return Entry[type]; 
        }

        public static void LoadPage<T>(ApplicationType Collection = ApplicationType.None, bool IsTemplate = false) where T : System.Windows.UIElement
        {
            /* Will not be used when dropdown decide the data */
            if(Collection != ApplicationType.None)
                Extensions.TryInvoke(() => Target.DataHandler = Entry[Collection].Single(x => x.isActive).Data);

            Target.IsTemplate = IsTemplate;
            Target.GetPage<T>();
        }

        public static void Populate()
        {
            Overlay();
            MinimapNPC();
            ElitePacks();
        }

        public static void Overlay()
        {
            if (AlreadyExists(ApplicationType.Overlay))
                return;

            List<IDesignHelper.IMenu> Entries = new List<IDesignHelper.IMenu>();

            Entries.Add(new IDesignHelper.IMenu
            {
                Name = "Minimap Settings",
                Image = "pack://application:,,,./Images/FK/Icons/intersect.png".ToImage(),
                ImageHover = "pack://application:,,,./Images/FK/Icons/intersect.png".ToImage(),
                isFirst = true,
                isActive = true,
                Data = Config.Get<FKConfig>().General.MiniMapSettings,
                Target = (() => LoadPage<Application.Overlay.OverlaySettings>(ApplicationType.Overlay))
            });

            Entries.Add(new IDesignHelper.IMenu
            {
                Name = "Skillbar Settings",
                Image = "pack://application:,,,./Images/FK/Icons/flags.png".ToImage(),
                ImageHover = "pack://application:,,,./Images/FK/Icons/flags.png".ToImage(),
                Data = Config.Get<FKConfig>().General.Skills,
                Target = (() => LoadPage<Application.Overlay.Skillbar>(ApplicationType.Overlay))
            });

            Entries.Add(new IDesignHelper.IMenu
            {
                Name = "Experiencebar Settings",
                Image = "pack://application:,,,./Images/FK/Icons/pins.png".ToImage(),
                ImageHover = "pack://application:,,,./Images/FK/Icons/pins.png".ToImage(),
                Data = Config.Get<FKConfig>().General.Experience,
                Target = (() => LoadPage<Application.Overlay.ExperienceBar>(ApplicationType.Overlay))
            });

            Entries.Add(new IDesignHelper.IMenu
            {
                Name = "Misc Settings",
                Image = "pack://application:,,,./Images/FK/Icons/droplets.png".ToImage(),
                ImageHover = "pack://application:,,,./Images/FK/Icons/droplets.png".ToImage(),
                isLast = true,
                Data = Config.Get<FKConfig>().General.Misc,
                Target = (() => LoadPage<Application.Overlay.MiscBar>(ApplicationType.Overlay))
            });

            Entry.Add(ApplicationType.Overlay, Entries);
        }

        //public 

        public static void MinimapNPC()
        {
            if (AlreadyExists(ApplicationType.MinimapNPC))
                return;

            List<IDesignHelper.IMenu> Entries = new List<IDesignHelper.IMenu>();

            DropdownEntries.Add(ApplicationType.MinimapNPC, 
                Config.Get<FKMinimap>().DefaultMapItem.CustomActors.Select(x => x.Value)
                .Concat(Config.Get<FKMinimap>().DefaultMapItem.DefaultActors.Select(x => x.Value).OrderBy(x => x.Name))
            );

            Entries.Add(new IDesignHelper.IMenu
            {
                Name = "Settings",
                Image = "pack://application:,,,./Images/FK/Icons/SettingActive.png".ToImage(),
                ImageHover = "pack://application:,,,./Images/FK/Icons/SettingActive.png".ToImage(),
                isActive = true,
                Data = { },
                Target = (() => { LoadPage<Application.NPC.Settings>();  })
            });

            Entries.Add(new IDesignHelper.IMenu
            {
                Name = "Minimap Options",
                Image = "pack://application:,,,./Images/FK/Icons/minimap.png".ToImage(),
                ImageHover = "pack://application:,,,./Images/FK/Icons/minimap.png".ToImage(),
                isActive = false,
                isLast = true,
                Data = { },
                Target = (() => LoadPage<Application.NPC.MinimapItem>(ApplicationType.None,true))
            });

            Entry.Add(ApplicationType.MinimapNPC, Entries);
        }

        public static void ElitePacks()
        {
            if (AlreadyExists(ApplicationType.ElitePacks))
                return;

            List<IDesignHelper.IMenu> Entries = new List<IDesignHelper.IMenu>();

            DropdownEntries.Add(ApplicationType.ElitePacks, Config.Get<FKAffixes>().Affixes.OrderBy(x => x.Name));

            Entries.Add(new IDesignHelper.IMenu
            {
                Name = "Settings",
                Image = "pack://application:,,,./Images/FK/Icons/SettingActive.png".ToImage(),
                ImageHover = "pack://application:,,,./Images/FK/Icons/SettingActive.png".ToImage(),
                isActive = true,
                Data = { },
                Target = (() => { LoadPage<Application.ElitePacks.GeneralSettings>(); })
            });

            Entries.Add(new IDesignHelper.IMenu
            {
                Name = "Minimap Options",
                Image = "pack://application:,,,./Images/FK/Icons/minimap.png".ToImage(),
                ImageHover = "pack://application:,,,./Images/FK/Icons/minimap.png".ToImage(),
                isActive = false,
                isLast = true,
                Data = { },
                Target = (() => LoadPage<Application.ElitePacks.SetAffixes>(ApplicationType.None, true))
            });

            Entry.Add(ApplicationType.ElitePacks, Entries);
        }


        public enum ApplicationType : int
        {
            None = -1,
            Overlay,
            MinimapNPC,
            SoundManager,
            ElitePacks,
            IngameOverlay,
            OverlayItems,
            Styles,
            Misc
        }


    }
}
