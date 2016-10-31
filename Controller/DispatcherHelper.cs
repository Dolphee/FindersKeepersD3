using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FindersKeepers
{
    public class DispatcherHelper
    {
        public class UIHelper
        {
            public object Control;
            public string Value;
            public Action Invoke = null;

            public void InvokeMethod()
            {
                if(Invoke != null)
                    Invoke.Invoke();
            }
        }

        public List<UIHelper> UIElements = new List<UIHelper>();

        public void UpdateUI()
        {
            Extensions.Execute.UIThread(() =>
            {
                foreach (var x in UIElements)
                {

                }
            });

            UIElements.Clear();
        }

        public void Toggle(object Window, bool Show = true)
        {
            Test();
        }

        public void Test()
        {
            var dispatcher = Application.Current.Dispatcher;

            if (dispatcher.CheckAccess())
            {
                System.Windows.MessageBox.Show("HEJ");
            }

            else
            {
                dispatcher.Invoke(() =>
                {
                    System.Windows.MessageBox.Show("anee");
                });
            }
        }
    }
}
