using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace WpfCascadePicker.Controls
{
    public class CascadePicker : MenuBase
    {
        private static readonly Type _typeofSelf = typeof(CascadePicker);
        static CascadePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofSelf, new FrameworkPropertyMetadata(_typeofSelf));
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CascadePicker), new PropertyMetadata(string.Empty));



        public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register(
            "IsDropDownOpen", typeof(bool), typeof(CascadePicker), new PropertyMetadata(false));

        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        public static readonly DependencyProperty MaxDropDownHeightProperty = DependencyProperty.Register(
            "MaxDropDownHeight", typeof(double), typeof(CascadePicker), new PropertyMetadata(SystemParameters.PrimaryScreenHeight / 3.0));
        public double MaxDropDownHeight
        {
            get
            {
                return (double)GetValue(MaxDropDownHeightProperty);
            }
            set
            {
                SetValue(MaxDropDownHeightProperty, value);
            }
        }
        
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
        public CascadePicker()
        {
            AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(MenuItem_Click));
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var clickedMenuItem = e.OriginalSource as MenuItem;
            if (clickedMenuItem != null)
            {
                List<string> hierarchy = GetParentHierarchy(clickedMenuItem);
                Text = GetHierarchicalPath(hierarchy);
            }
            IsDropDownOpen = false;
        }
        private string GetHierarchicalPath(List<string> hierarchy)
        {
            hierarchy.Reverse();
            return string.Join(" / ", hierarchy);
        }
        private List<string> GetParentHierarchy(MenuItem item)
        {
            var hierarchy = new List<string>();

            var headerObject = item.Header;
            var propertyInfo = headerObject.GetType().GetProperty(DisplayMemberPath);
            if (propertyInfo != null)
            {
                var displayValue = propertyInfo.GetValue(headerObject, null) as string;
                if (!string.IsNullOrEmpty(displayValue))
                    hierarchy.Add(displayValue);
            }
            var parent = VisualTreeHelper.GetParent(item);
            if (parent != null)
            {
                var stackPanel = parent as StackPanel;
                if (stackPanel != null)
                {
                    var menuItemParent = stackPanel.TemplatedParent as MenuItem;
                    if (menuItemParent != null)
                    {
                        var parentHierarchy = GetParentHierarchy(menuItemParent);
                        hierarchy.AddRange(parentHierarchy);
                    }
                }
            }
            return hierarchy;
        }
    }
}
