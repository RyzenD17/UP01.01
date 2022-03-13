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

namespace UP01._01
{
    /// <summary>
    /// Логика взаимодействия для StockPage.xaml
    /// </summary>
    public partial class StockPage : Page
    {
        List<Material> MaterialStart = BaseClass.Base.Material.ToList();
        PageChanges pc = new PageChanges();
        List<Material> MaterialFilterSort;

        public StockPage()
        {
            InitializeComponent();
            LVStock.ItemsSource = BaseClass.Base.Material.ToList();
            List<MaterialType> MT = BaseClass.Base.MaterialType.ToList();
            CBFilter.Items.Add("Все записи");
            for(int i =0;i<MT.Count();i++)
            {
                CBFilter.Items.Add(MT[i].Title);
            }
            CBFilter.SelectedIndex = 0;
            CBSorting.Items.Add("Все записи");
            CBSorting.Items.Add("Наименование");
            CBSorting.Items.Add("Остаток на складе");
            CBSorting.Items.Add("Стоимость");
            CBSorting.SelectedIndex = 0;
            DataContext = pc;
            pc.CountPage = 15;
            pc.Countlist = MaterialFilters.Count;
            LVStock.ItemsSource = MaterialFilters.Skip(0).Take(pc.CountPage).ToList();
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            string typename = "";
            List<Material> ML = BaseClass.Base.Material.Where(x => x.ID == index).ToList();
            foreach (Material s in ML)
            {
                    typename += s.MaterialType.Title + " | " + s.Title;
            }
            tb.Text = typename;
        }

        private void TextBlock_Loaded_1(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            string suppliers = "Поставщики:  ";
            List<MaterialSupplier> MS = BaseClass.Base.MaterialSupplier.Where(x => x.MaterialID == index).ToList();
            List<Supplier> S = BaseClass.Base.Supplier.Where(x => x.ID == index).ToList();
            foreach(MaterialSupplier s in MS)
            {
                foreach (Supplier t in S)
                {
                    suppliers += s.Supplier.Title+", ";
                }
            }
            if(suppliers!="Поставщики:  ")
            {
               tb.Text = suppliers.Substring(0,suppliers.Length-2);
            }
            else
            {
                suppliers += "-";
                tb.Text = suppliers;
            }
           
        }

        List<Material> MaterialFilters;

        private void Filters()
        {
            int index = CBFilter.SelectedIndex;
            if(index!=0)
            {
                MaterialFilters = MaterialStart.Where(x => x.MaterialTypeID == index).ToList();
            }
            else
            {
                MaterialFilters = MaterialStart;
            }

            if(!string.IsNullOrWhiteSpace(TBFilter.Text))
            {
                MaterialFilters = MaterialFilters.Where(x => x.Title.ToLower().Contains(TBFilter.Text.ToLower())).ToList();
            }
            LVStock.ItemsSource = MaterialFilters;
            TBlCount.Text = "Количество записей - " + MaterialFilters.Count() + " из " + MaterialStart.Count() ;
        }

        private void TBFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filters();
            LVStock.ItemsSource = MaterialFilters.Skip(0).Take(pc.CountPage).ToList();
        }

        private void CBFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filters();
            LVStock.ItemsSource = MaterialFilters.Skip(0).Take(pc.CountPage).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(CBSorting.SelectedIndex==0)
            {
                MaterialFilters.Sort((x, y) => x.ID.CompareTo(y.ID));
                LVStock.Items.Refresh();
            }
            if (CBSorting.SelectedIndex == 1)
            {
                MaterialFilters.Sort((x, y) => x.Title.CompareTo(y.Title));
                LVStock.Items.Refresh();
            }
            if (CBSorting.SelectedIndex == 2)
            {
                MaterialFilters.Sort((x, y) => Convert.ToInt32(x.CountInStock).CompareTo(Convert.ToInt32(y.CountInStock)));
                LVStock.Items.Refresh();
            }
            if (CBSorting.SelectedIndex == 3)
            {
                MaterialFilters.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                LVStock.Items.Refresh();
            }
            LVStock.ItemsSource = MaterialFilters.Skip(0).Take(pc.CountPage).ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (CBSorting.SelectedIndex == 0)
            {
                MaterialFilters.Sort((x, y) => x.ID.CompareTo(y.ID));
                MaterialFilters.Reverse();
                LVStock.Items.Refresh();
            }
            if (CBSorting.SelectedIndex == 1)
            {
                MaterialFilters.Sort((x, y) => x.Title.CompareTo(y.Title));
                MaterialFilters.Reverse();
                LVStock.Items.Refresh();
            }
            if (CBSorting.SelectedIndex == 2)
            {
                MaterialFilters.Sort((x, y) => Convert.ToInt32(x.CountInStock).CompareTo(Convert.ToInt32(y.CountInStock)));
                MaterialFilters.Reverse();
                LVStock.Items.Refresh();
            }
            if (CBSorting.SelectedIndex == 3)
            {
                MaterialFilters.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                MaterialFilters.Reverse();
                LVStock.Items.Refresh();
            }
            LVStock.ItemsSource = MaterialFilters.Skip(0).Take(pc.CountPage).ToList();
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new CreateOrUpdate());
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;
            int id = Convert.ToInt32(B.Uid);
            Material MaterialUpdate = BaseClass.Base.Material.FirstOrDefault(x => x.ID == id);
            FrameClass.MainFrame.Navigate(new CreateOrUpdate(MaterialUpdate));
        }

        private void LVStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(LVStock.SelectedIndex>-1)
            {
                ChangeCount.Visibility = Visibility.Visible;
            }
            else
            {
                ChangeCount.Visibility = Visibility.Collapsed;
            }
                  
        }

        private void ChangeCount_Click(object sender, RoutedEventArgs e)
        {
            var list = LVStock.SelectedItems;
            double max = 1;
            foreach (Material m in list)
            {
                if (m.MinCount > max)
                {
                    max = m.MinCount;
                }
            }
            ChangeMinCountWindow window = new ChangeMinCountWindow(max);
           

            window.ShowDialog();
            if(window.Count>0)
            {
                foreach (Material m in list)
                {
                    m.MinCount = window.Count;
                }
                LVStock.Items.Refresh();
            }
        }

        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)  // обработка нажатия на один из Textblock в меню с номерами страниц
        {
            TextBlock tb = (TextBlock)sender;
            switch (tb.Uid)  // определяем, куда конкретно было сделано нажатие
            {
                case "prev":
                    pc.CurrentPage--;
                    break;
                case "next":
                    pc.CurrentPage++;
                    break;
                default:
                    pc.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            LVStock.ItemsSource = MaterialFilters.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();  // оображение записей постранично с определенным количеством на каждой странице
            // Skip(pc.CurrentPage* pc.CountPage - pc.CountPage) - сколько пропускаем записей
            // Take(pc.CountPage) - сколько записей отображаем на странице
        }
    }
}
