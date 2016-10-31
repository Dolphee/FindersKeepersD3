using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace FindersKeepers.Templates.Overlay
{
    public class ItemControl<T> : OverlayHelper<T> where T : UserControl
    {
        public ItemControl()
        {
            FKControls = new ObservableCollection<FKControl>();

            D3RenderControl = Activator.CreateInstance<T>();
            D3RenderControl.DataContext = this;
        }
    }
}
