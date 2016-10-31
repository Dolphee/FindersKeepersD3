using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PropertyChanged;

namespace FindersKeepers.Templates.Templating.FKTemplates
{
    [ImplementPropertyChanged]
    public class FilterController : LayoutHelper, IDesign
    {
        public IDesignHelper IDesignHelper {get; set; }
        public FilterItems Filter { get; set; }
        public IDesignUpdate CurrentTarget { get; set; }
        public bool IsTemplate {get;set; }

        public FilterController()
        {
            IDesignHelper = new IDesignHelper();
            IDesignHelper.Settings.UseSideControl = true;
            IDesignHelper.Settings.UseMainMenu = true;
            
            PopulateMenu();
            PopulateFilters();
            IDesignHelper.Menu.FirstOrDefault().Target();
        }
        
        public void PopulateMenu()
        {
            IDesignHelper.Menu.Add(new IDesignHelper.IMenu { Name = "General Settings", isFirst = true, isActive = true,
                Image = "pack://application:,,,./Images/FK/Icons/SettingActive.png".ToImage(),
                ImageHover = "pack://application:,,,./Images/FK/Icons/SettingActive.png".ToImage(),
                Target = (() => GetPage<Filters.General>())
            });

            IDesignHelper.Menu.Add(new IDesignHelper.IMenu
            {
                Name = "Overlay Settings",
                Image = "pack://application:,,,./Images/FK/Icons/intersect.png".ToImage(),
                ImageHover = "pack://application:,,,./Images/FK/Icons/intersect.png".ToImage(),
                Target = (() => GetPage<Filters.Overlay>())
            });

            IDesignHelper.Menu.Add(new IDesignHelper.IMenu
            {
                Name = "Sound & Multibox options",
                Image = "pack://application:,,,./Images/FK/Icons/Multibox.png".ToImage(),
                ImageHover = "pack://application:,,,./Images/FK/Icons/MultiboxActive.png".ToImage(),
                Target = (() => GetPage<Filters.MultiboxFilter>())
            });

            IDesignHelper.Menu.Add(new IDesignHelper.IMenu
            {
                Name = "Item Filters",
                Image = "pack://application:,,,./Images/FK/Icons/Filters.png".ToImage(),
                ImageHover = "pack://application:,,,./Images/FK/Icons/FiltersActive.png".ToImage(),
                Target = (() => GetPage<Filters.ItemFilter>())
            });

            IDesignHelper.Menu.Add(new IDesignHelper.IMenu
            {
                Name = "Pickit",
                isLast = true,
                Image = "pack://application:,,,./Images/FK/Icons/Pickit.png".ToImage(),
                ImageHover = "pack://application:,,,./Images/FK/Icons/PickitActive.png".ToImage()
            });
        }

        public void PopulateFilters()
        {
            Filter = Config.Get<FKFilters>().Filters.FirstOrDefault();

            foreach (FilterItems f in Config.Get<FKFilters>().Filters)
            {
                IDesignHelper.ISideMenu Data = new IDesignHelper.ISideMenu
                {
                    Data = f,
                    isActive = f.Equals(Config.Get<FKFilters>().Filters.FirstOrDefault()),
                    EntryId = f.ID
                };

                Data.Target = (() => SetTarget(Data));
                IDesignHelper.SideMenu.Add(Data);
            }
        }

        public void SetTarget(object Item)
        {
            Filter = (FilterItems)((IDesignHelper.ISideMenu)Item).Data;

            if (CurrentTarget is IDesignUpdate)
                CurrentTarget.CollectionChanged();
        }

        public void Remove(FilterItems Item) {
            System.Windows.MessageBox.Show(Item.Name);
        }
    }
}
