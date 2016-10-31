using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FindersKeepers.Templates.Filters;
using FindersKeepers.Controller;
using System.Windows.Media.Imaging;

namespace FindersKeepers.Templates.Support
{
    public class Support
    {
        public BaseDesign _root;

        public Support(BaseDesign root)
        {
            _root = root;
            SetFilters();

            _root.xMenu.Content = "Support";
            _root.xSelected.Content = "General Settings";
            _root.xSubMenu.Content = "";
            _root.DrawMenu();
            _root.DrawSubMenu();
            root.LoadPage(new GeneralSettings(this), null);
        }
        public void SetFilters()
        {
            _root.SubItems.Add(new FKMenuHelper.MenuStruct
            {
                Title ="General Settings",
                InvokeMethod = (() => _root.LoadPage(new GeneralSettings(this), null, null, true)),
                Invoke = null
            });

            _root.SubItems.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Multibox Macros",
                InvokeMethod = (() => _root.LoadPage(new MultiMacro(this), null, null, true)),
                Invoke = null
            });

            _root.SubItems.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Multibox Accounts",
                InvokeMethod = (() => {
                    _root.LoadPage(new Accounts(this), null, null, true);
                }),
                Invoke = null
            });

            _root.SubItems.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Item Tracker",
                Invoke = null
            });

            _root.SubItems.Add(new FKMenuHelper.MenuStruct
            {
                Title = "Experience Tracker",
                Invoke = null
            });

            _root.SubItems.Add(new FKMenuHelper.MenuStruct
            {
                Title = "WebTracker",
                Invoke = null
            });


        }
    }
}
