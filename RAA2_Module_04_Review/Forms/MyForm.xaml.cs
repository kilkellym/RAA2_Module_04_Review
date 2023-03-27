using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace RAA2_Module_04_Review
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class MyForm : Window
    {
        ObservableCollection<Level> LevelList { get; set; }
        ExternalEvent EventSet;
        ExternalEvent EventReset;

        public MyForm(List<Level> _levels, ExternalEvent _eventReset, ExternalEvent _eventSet)
        {
            InitializeComponent();

            LevelList = new ObservableCollection<Level>(_levels);

            cmbLevels.ItemsSource = LevelList;
            cmbLevels.DisplayMemberPath = "Name";
            EventReset = _eventReset;
            EventSet = _eventSet;

            //cmbLevels.SelectedIndex = 0;

        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            Globals.SelectedLevel = cmbLevels.SelectedItem as Level;
            SetSelectedColor();
            SetSelectedCategories();

            EventSet.Raise();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            SetSelectedCategories();

            EventReset.Raise();
        }

        private void SetSelectedCategories()
        {
            List<string> selectedCats = new List<string> ();

            if (cbxCatColumns.IsChecked == true)
                selectedCats.Add("Columns");
            if (cbxCatFraming.IsChecked == true)
                selectedCats.Add("Framing");
            if (cbxCatWalls.IsChecked == true)
                selectedCats.Add("Walls");

            Globals.SelectedCats = selectedCats;
        }

        private void SetSelectedColor()
        {
            if (rbColorBlue.IsChecked == true)
                Globals.SelectedColor = "Blue";
            if (rbColorGreen.IsChecked == true)
                Globals.SelectedColor = "Green";
            if (rbColorRed.IsChecked == true)
                Globals.SelectedColor = "Red";
            if (rbColorYellow.IsChecked == true)
                Globals.SelectedColor = "Yellow";
        }
    }
}
