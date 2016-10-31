using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace FindersKeepers
{
    public class INotifyList<T> : ObservableCollection<T>
    {
        private bool suppressNotification;

        public INotifyList() { }

        public INotifyList(IEnumerable<T> items)
            : base(items)
        {
        }

        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnCollectionChangedMultiItem(
            NotifyCollectionChangedEventArgs e)
        {
            var handlers = CollectionChanged;
            if (handlers == null)
                return;

            handlers(this, e);
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (suppressNotification)
                return;

            base.OnCollectionChanged(e);
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }

        public void AddRange(IEnumerable<T> items)
        {
            if (items == null)
                return;

            suppressNotification = true;

            var itemList = items.ToList();

            foreach (var item in itemList)
            {
                Add(item);
            }
            suppressNotification = false;

            OnCollectionChangedMultiItem(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new List<T>(items)));
        }

        public void AddRange(params T[] items)
        {
            AddRange((IEnumerable<T>)items);
        }

        public void ReplaceWithRange(IEnumerable<T> items)
        {
            Items.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            AddRange(items);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            suppressNotification = true;

            var removableItems = items.Where(x => Items.Contains(x)).ToList();

            foreach (var item in removableItems)
            {
                Remove(item);
            }

            suppressNotification = false;

        }
    }
}
