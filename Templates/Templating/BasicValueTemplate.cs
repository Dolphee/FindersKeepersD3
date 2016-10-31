using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using PropertyChanged;
using FindersKeepers.Templates.Templating.FKTemplates;

namespace FindersKeepers.Templates.Templating
{
    [ImplementPropertyChanged]
    public class BasicValueTemplate : UserControl, IDesignUpdate
    {
        public object DataObject { get;set; }
        public T TryGetDataObject<T>() where T : class
        {
            return Extensions.TryInvoke<T>(() =>
            {
                return (T)DataObject;
            });
        }

        public virtual void CollectionChanged() { }
        public BasicValueTemplate() { }

        public BasicValueTemplate(object DataObj)
        {
            DataContext = this;
            DataObject = DataObj;
        }

        public void ChangeValue(object sender, MouseButtonEventArgs e)
        {
            BindingExpression be = BindingOperations.GetBindingExpression((FrameworkElement)sender, ((DependencyProperty)Image.SourceProperty));
            Extensions.SetNewValue<bool>(be.ParentBinding.Path.Path, this, false);
        }
    }
}
