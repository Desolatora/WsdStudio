using System;
using System.Windows.Forms;

namespace WsdPreprocessingStudio.Core.UI
{
    public static class ListViewExtensions
    {
        public static ListView AddGroup(
            this ListView listView, string headerText, Action<ListViewGroup> groupAction = null)
        {
            var group = new ListViewGroup(headerText);

            listView.Groups.Add(group);
            groupAction?.Invoke(group);

            return listView;
        }

        public static ListView AddItem(
            this ListView listView, string itemText, params string[] subItemTexts)
        {
            var item = new ListViewItem(itemText);

            foreach (var subItemText in subItemTexts)
            {
                item.SubItems.Add(subItemText);
            }

            listView.Items.Add(item);

            return listView;
        }

        public static ListViewGroup AddItem(
            this ListViewGroup listViewGroup, string itemText, params string[] subItemTexts)
        {
            var item = new ListViewItem(itemText, listViewGroup);

            foreach (var subItemText in subItemTexts)
            {
                item.SubItems.Add(subItemText);
            }

            listViewGroup.ListView.Items.Add(item);

            return listViewGroup;
        }
    }
}
