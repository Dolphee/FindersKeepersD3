using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using FindersKeepers.DebugWorker;

namespace FindersKeepers.Templates.Overlay
{
    public abstract class OverlayHelper<E>
    {
        private Dictionary<int, FKControl> Mapping = new Dictionary<int, FKControl>();
        public ObservableCollection<FKControl> FKControls { get; set; }
        public List<FKControl> ItemsToAdd = new List<FKControl>();
        public List<FKControl> ItemsToRemove = new List<FKControl>();
        public HashSet<string> ActiveControls = new HashSet<string>();
        public E D3RenderControl;

        public virtual void TryUpdate()
        {
            foreach (FKControl c in FKControls)
            {
                try
                {
                    c.UpdateControl();
                }

                catch (Exception e)
                {
                    DebugWriter.Write(e);
                }
            }

            if (ItemsToAdd.Count != 0)
            {
                Extensions.Execute.UIThread(() => ItemsToAdd.ForEach(x => { FKControls.Add(x); ActiveControls.Add(x.Name); }));
                ItemsToAdd.Clear();
            }

            if (ItemsToRemove.Count != 0)
            {
                Extensions.Execute.UIThread(() => ItemsToRemove.ForEach(x => { FKControls.Remove(x); ActiveControls.Remove(x.Name); }));
                ItemsToRemove.Clear();
            }
        }

        public void TryRefreshControl<X>() where X : class
        {
            foreach (X f in Search<X>(typeof(X)))
                (f as IFKControl).IUpdate();
        }

        public UserControl CreateControl<T>(string Name, bool initialization = false, object Params = null) where T : IFKControl
        {
            if (ActiveControls.Contains(Name))
                return null;

            UserControl Control = default(UserControl);

            Extensions.Execute.UIThread(() =>
            {
                if (Params == null)
                    Control = (UserControl)Extensions.CreatePage(typeof(T));
                else
                    Control = (UserControl)Extensions.CreatePage(typeof(T), Params);

                if (!initialization)
                    ItemsToAdd.Add(new FKControl((IFKControl)Control, Name));
                else
                    FKControls.Add(new FKControl((IFKControl)Control, Name));
            });

            return Control;
        }

        public T Search<T>() where T : class
        {
            var Items = FKControls.Where(x => x.Control is T).Select(x => x.Control);

            if (Items.Count() != 0)
                return (T)Items.SingleOrDefault();

            return (T)FKControls.Where(x => x.Controller is T).Select(x => x.Controller).SingleOrDefault();
        }

        public object Search<T>(string Name) where T : class
        {
            Extensions.TryInvoke<T>(() =>
            {
                if(FKControls.ToList().Exists(x => x.Control.GetType() == typeof(T)))
                    return (T)FKControls.Where(x => x.Control.GetType() == typeof(T)).Select(x => x.Control).FirstOrDefault();

                return null;
            });

            return null;
        }

        public void Delete<T>() where T : class
        {
            var Items = FKControls.Where(x => x.Control is T).Select(x => x).Concat(FKControls.Where(x => x.Controller is T).Select(x => x));

            if (Items.Count() == 0)
                return;
          
             ItemsToRemove.Add(Items.FirstOrDefault());
        }

        public void Delete<T>(string Name) where T : class
        {
            var Items = FKControls.Where(x => x.Name == Name).SingleOrDefault();

            if (Items == null)
                return;

            ItemsToRemove.Add(Items);
        }

        public IEnumerable<T> Search<T>(Type type) where T : class
        {
            return FKControls.Where(x => x.Control is T).Select(x => x.Control as T)
            .Concat(FKControls.Where(x => x.Controller is T).Select(x => x.Controller as T));
        }

        public T CreateWPFControl<T>(string Name, bool initialization = false) where T : IFKWPF
        {
            IFKWPF ClassControl = default(T);

            Extensions.TryInvoke(() =>
            {
                Extensions.Execute.UIThread(() =>
                {
                    ClassControl = (IFKWPF)Extensions.CreatePage(typeof(T));

                    if(!initialization)
                        ItemsToAdd.Add(new FKControl(ClassControl.Control, Name, ClassControl));
                    else
                        FKControls.Add(new FKControl(ClassControl.Control, Name, ClassControl));
                });
            });

            return (T)ClassControl;
        }

    }
}
