using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Primitives;

namespace WpfApp3
{ 
    class TriStateCheckComboBox
    {
        public static bool? GetSelectorItemSelected(DependencyObject d)
        {
            return (bool?)d.GetValue(SelectorItemSelectedProperty);
        }

        public static void SetSelectorItemSelected(DependencyObject d, bool? newVal)
        {
            d.SetValue(SelectorItemSelectedProperty, newVal);
        }


        public static readonly DependencyProperty SelectorItemSelectedProperty =
            DependencyProperty.RegisterAttached("SelectorItemSelected", typeof(bool?),
                typeof(TriStateCheckComboBox), new PropertyMetadata(SelectorItemSelectedChanged));

        private static void SelectorItemSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("SelectorItemSelectedChanged");
            SelectorItem selectorItem = (SelectorItem)d;
            IList triState = GetTriStateValues(d);

            if (triState == null) return;
            RefreshTriState(selectorItem, triState);
        }


        public static IList GetSelectedValues(DependencyObject d)
        {
            return (IList)d.GetValue(SelectedValuesProperty);
        }

        public static void SetSelectedValues(DependencyObject d, IList newVal)
        {
            d.SetValue(SelectedValuesProperty, newVal);
        }


        public static readonly DependencyProperty SelectedValuesProperty =
            DependencyProperty.RegisterAttached("SelectedValues", typeof(IList),
                typeof(TriStateCheckComboBox), new PropertyMetadata());


        public static IList GetTriStateValues(DependencyObject d)
        {
            return (IList)d.GetValue(TriStateValuesProperty);
        }

        public static void SetTriStateValues(DependencyObject d, IList newVal)
        {
            d.SetValue(TriStateValuesProperty, newVal);
        }


        public static readonly DependencyProperty TriStateValuesProperty =
            DependencyProperty.RegisterAttached("TriStateValues", typeof(IList),
                typeof(TriStateCheckComboBox), new PropertyMetadata(TriStateValuesChanged));

        private static void TriStateValuesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("TriStateValues Changed");

            IList triState = (IList)e.NewValue;

            SelectorItem selectorItem = (SelectorItem)d;

            selectorItem.PreviewMouseUp += (s, me) =>
            {
                System.Diagnostics.Debug.WriteLine("PreviewMouseUp");
                if (selectorItem.IsSelected == null)
                {
                    //CheckComboBox check = FindParent<CheckComboBox>(selectorItem);
                    GetSelectedValues(d).Add(selectorItem.DataContext);

                    me.Handled = true;
                }
            };


            INotifyCollectionChanged notifyCol = triState as INotifyCollectionChanged;
            notifyCol.CollectionChanged += (sender, colArgs) =>
            {
                if(colArgs.Action == NotifyCollectionChangedAction.Add)
                {
                    RefreshTriState(selectorItem, triState);
                }
            };

            RefreshTriState(selectorItem, triState);
        }

        static T FindParent<T>(DependencyObject d) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(d);

            if (parent == null) return null;
            System.Diagnostics.Debug.WriteLine("parent " + parent.GetType());
            if (parent is T) return (T)parent;
            return FindParent<T>(parent);
        }

        static void RefreshTriState(SelectorItem selectorItem, IList triState)
        {
            object obj = selectorItem.DataContext;
            foreach (object o in triState)
            {
                if (obj.Equals(o))
                {
                    selectorItem.IsSelected = null;
                    //Dispatcher.CurrentDispatcher.BeginInvoke((Action)delegate {
                    //    selectorItem.IsSelected = null;
                    //}//, DispatcherPriority.ApplicationIdle);
                }
            }
        }
    }
}
