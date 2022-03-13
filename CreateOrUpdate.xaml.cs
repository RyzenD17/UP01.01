using Microsoft.Win32;
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
    /// Логика взаимодействия для CreateOrUpdate.xaml
    /// </summary>
    public partial class CreateOrUpdate : Page
    {
        bool flag;
        string path;
        Material mat = new Material();
        MaterialSupplier matsup = new MaterialSupplier();
        List<MaterialSupplier> MatSupList = BaseClass.Base.MaterialSupplier.ToList();

        public CreateOrUpdate()
        {
            InitializeComponent();
            flag = true;
            List<MaterialType> MT = BaseClass.Base.MaterialType.ToList();
            for (int i = 0; i < MT.Count(); i++)
            {
                CBMaterialType.Items.Add(MT[i].Title);
            }
            List<Supplier> SUP = BaseClass.Base.Supplier.ToList();
            for(int i=0;i<SUP.Count;i++)
            {
                CBSupplier.Items.Add(SUP[i].Title);
            }

            Delete.Visibility=Visibility.Collapsed;
        }

        public CreateOrUpdate(Material MaterialUpdate)
        {
            InitializeComponent();
            List<MaterialType> MT = BaseClass.Base.MaterialType.ToList();
            for (int i = 0; i < MT.Count(); i++)
            {
                CBMaterialType.Items.Add(MT[i].Title);
            }
            List<Supplier> SUP = BaseClass.Base.Supplier.ToList();
            for (int i = 0; i < SUP.Count; i++)
            {
                CBSupplier.Items.Add(SUP[i].Title);
            }
            mat = MaterialUpdate;
            TBTitle.Text = mat.Title;
            CBMaterialType.SelectedIndex = mat.MaterialTypeID - 1;
            TBCountInStock.Text = Convert.ToString(mat.CountInStock);
            TBUnit.Text = mat.Unit;
            TBCountInPack.Text = Convert.ToString(mat.CountInPack);
            TBMinCount.Text = Convert.ToString(mat.MinCount);
            TBCost.Text = Convert.ToString(mat.Cost);
            TBDescription.Text = mat.Description;

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new StockPage());
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string title = TBTitle.Text;
                int type = CBMaterialType.SelectedIndex+1;
                double instock = Convert.ToDouble(TBCountInStock.Text);
                string unit = TBUnit.Text;
                int inpack = Convert.ToInt32(TBCountInPack.Text);
                double mincount = Convert.ToDouble(TBMinCount.Text);
                decimal cost = Convert.ToDecimal(TBCost.Text);
                string description = TBDescription.Text;
                string image = path;
                if(cost<0||mincount<0)
                {
                    MessageBox.Show("Запись не добавлена!", "Добавление записи");
                }
                else
                {
                    mat.Title = title; mat.MaterialTypeID = type; mat.CountInStock = instock; mat.Unit = unit; mat.CountInPack = inpack; mat.MinCount = mincount; mat.Cost = cost; mat.Description = description; mat.Image = image;
                    MessageBoxResult result = MessageBox.Show("Вы уверены что хотите добавить новую запись?", "Добавление записи", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (flag == true)
                        {
                            BaseClass.Base.Material.Add(mat);
                        }
                        BaseClass.Base.SaveChanges();
                        MessageBox.Show("Данные записаны!", "Добавление записи");
                        FrameClass.MainFrame.Navigate(new StockPage());
                    }
                }

            }
            catch
            {
                MessageBox.Show("Запись не добавлена!", "Добавление записи");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int id = mat.ID;
            Material MaterialDelete = BaseClass.Base.Material.FirstOrDefault(x => x.ID == id);
            ProductMaterial PM = BaseClass.Base.ProductMaterial.FirstOrDefault(x => x.MaterialID == id);
            if(PM==null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить запись?", "Удаление записи", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    BaseClass.Base.Material.Remove(MaterialDelete);
                    BaseClass.Base.SaveChanges();
                    FrameClass.MainFrame.Navigate(new StockPage());
                    MessageBox.Show("Запись удалена!", "Удаление записи");
                }
            }
            else
            {
                MessageBox.Show("Удаление записи не возможно!", "Удаление записи");
            }
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            LBSupplier.Items.Add(CBSupplier.SelectedItem);
        }

        private void DeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            LBSupplier.Items.Remove(CBSupplier.SelectedItem);
        }

        private void BtnChangeImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog(); 
            OFD.ShowDialog();
            path = OFD.FileName;
            int n = path.IndexOf("material");
            path = path.Substring(n);
        }
    }
}
