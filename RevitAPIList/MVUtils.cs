using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RevitAPIList
{
    public class MVUtils
    {
        public static System.Windows.Controls.Grid CreateGrid()
        {
            System.Windows.Controls.Grid grid = new System.Windows.Controls.Grid()
            {
                Name = "ViewGrid"
            };

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            return grid;

        }
        public static void CreateRow(System.Windows.Controls.Grid grid, int sheetNumber, List<ViewPlan> viewNames, int rowNumber)
        {
            grid.RowDefinitions.Add(new RowDefinition());
            Label label = new Label()
            {
                Content = sheetNumber
            };
            grid.Children.Add(label);
            System.Windows.Controls.Grid.SetRow(label, rowNumber);
            System.Windows.Controls.Grid.SetColumn(label, 1);
            string SelectedView = $"{{Binding SelectedView+{rowNumber}}}";
            ComboBox comboBox = new ComboBox()
            {
                ItemsSource = viewNames,
                SelectedValue = SelectedView,
                DisplayMemberPath = "Name",
            };
            grid.Children.Add(comboBox);
            System.Windows.Controls.Grid.SetRow(comboBox, rowNumber);
            System.Windows.Controls.Grid.SetColumn(label, 2);
        }

    }
}
