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
using System.Windows.Shapes;

namespace UP01._01
{
    /// <summary>
    /// Логика взаимодействия для ChangeMinCountWindow.xaml
    /// </summary>
    public partial class ChangeMinCountWindow : Window
    {
        public ChangeMinCountWindow(double max)
        {
            InitializeComponent();
            TBCount.Text = Convert.ToString(max);
        }

        public int Count
        {
            get
            {
                return Convert.ToInt32(TBCount.Text);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
