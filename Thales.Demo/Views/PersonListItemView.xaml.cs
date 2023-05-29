using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Thales.Demo.Views
{
    public partial class PersonListItemView : UserControl
    {
        public static readonly DependencyProperty AssignedRoleProperty =
            DependencyProperty.Register("AssignedRole", typeof(object), typeof(PersonListItemView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object AssignedRole
        {
            get { return (object)GetValue(AssignedRoleProperty); }
            set { SetValue(AssignedRoleProperty, value); }
        }

        public static readonly DependencyProperty PersonItemDropCommandProperty =
            DependencyProperty.Register("PersonItemDropCommand", typeof(ICommand), typeof(PersonListItemView), new PropertyMetadata(null));

        public ICommand PersonItemDropCommand
        {
            get { return (ICommand)GetValue(PersonItemDropCommandProperty); }
            set { SetValue(PersonItemDropCommandProperty, value); }
        }

        public PersonListItemView()
        {
            InitializeComponent();
        }

        private void ListViewItem_Drop(object sender, DragEventArgs e)
        {
            ((Grid)sender).Background = new SolidColorBrush(Colors.Transparent);
            AssignedRole = e.Data.GetData(DataFormats.Serializable);
            PersonItemDropCommand?.Execute(null);
        }

        private void Grid_DragEnter(object sender, DragEventArgs e)
        {
            ((Grid)sender).Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#d9d9d9");
        }

        private void Grid_DragLeave(object sender, DragEventArgs e)
        {
            ((Grid)sender).Background = new SolidColorBrush(Colors.Transparent);
        }
    }
}
