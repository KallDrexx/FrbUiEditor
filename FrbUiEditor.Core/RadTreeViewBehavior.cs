using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace FrbUiEditor.Core
{
    /// <summary>
    /// This class adds functionality to the RadTreeView
    /// Set RightClickSelectsNode property to true if you want right click to select & focus the tree node
    /// Originally from http://shrinandvyas.blogspot.com/2013/05/wpf-telerik-radtreeview-select-node-on.html
    /// </summary>
    public class RadTreeViewBehavior
    {
        public static bool GetRightClickSelectsNode(DependencyObject obj)
        {
            if (ReferenceEquals(obj, null))
                return false;
            return (bool)obj.GetValue(RightClickSelectsNodeProperty);
        }

        public static void SetRightClickSelectsNode(DependencyObject obj, bool value)
        {
            if (ReferenceEquals(obj, null))
                return;
            obj.SetValue(RightClickSelectsNodeProperty, value);
        }

        public static readonly DependencyProperty RightClickSelectsNodeProperty =
            DependencyProperty.RegisterAttached("RightClickSelectsNode", typeof(bool), typeof(RadTreeViewBehavior), new UIPropertyMetadata(OnRightClickSelectsNodeChanged));

        private static void OnRightClickSelectsNodeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tree = d as RadTreeView;
            if (!ReferenceEquals(null, tree))
            {
                if ((bool)e.NewValue)
                {
                    tree.MouseRightButtonDown += OnTreeMouseRightButtonDown;
                }
                else
                {
                    tree.MouseRightButtonDown -= OnTreeMouseRightButtonDown;
                }
            }
        }

        private static void OnTreeMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.IsSelected = true;
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        private static RadTreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is RadTreeViewItem))
                source = VisualTreeHelper.GetParent(source);

            return source as RadTreeViewItem;
        }
    }
}
