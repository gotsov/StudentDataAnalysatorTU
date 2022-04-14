using System;
using System.Collections.Generic;
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

namespace StudentDataAnalysator.UserControls
{
    /// <summary>
    /// Interaction logic for ViewExcelView.xaml
    /// </summary>
    public partial class ViewExcelView : UserControl
    {
        public ViewExcelView()
        {
            InitializeComponent();
        }

        private void DataGrid_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
