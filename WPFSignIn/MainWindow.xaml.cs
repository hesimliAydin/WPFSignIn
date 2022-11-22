using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

namespace WPFSignIn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Human> humans;
        public MainWindow()
        {
            InitializeComponent();
            humans = new List<Human>();

            AddDay();
            AddMonth();
            AddYear();
            AddCountry();
        }
        
        private void AddDay()
        {
            for (int i = 1; i <= 31; i++)
            {
                DayComboB.Items.Add(i.ToString());
            }
        }

        private void AddMonth()
        {
            for (int i = 1; i <= 12; i++)
            {
                MonthComboB.Items.Add(i);
            }
        }

        private void AddYear()
        {
            for (int i = 1950; i < 2022; i++)
            {
                YearComboB.Items.Add(i.ToString());
            }
        }

        private void AddCountry()
        {
            var list = CultureInfo.GetCultures(CultureTypes.SpecificCultures).
                               Select(p => new RegionInfo(p.Name).EnglishName).
                               Distinct().OrderBy(s => s).ToList();

            CountryComboB.ItemsSource = list;
        }


        private void Profile_Photo_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filenames = e.Data.GetData(DataFormats.FileDrop, true) as string[];

                foreach (string fileName in filenames)
                {
                    profile.ImageSource = new BitmapImage(new Uri(fileName));
                    
                }
            }
        }

        private void SignInClick(object sender, RoutedEventArgs e)
        {
            Human human = new();
            if (string.IsNullOrWhiteSpace(FullNameTxt.Text))
            {
                MessageBox.Show("FullName is empty"," Validition", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (string.IsNullOrWhiteSpace(TitleTxt.Text))
            {
                MessageBox.Show("Title is empty", " Validition", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (string.IsNullOrWhiteSpace(EmailTxt.Text))
            {
                MessageBox.Show("Email is empty", " Validition", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (string.IsNullOrWhiteSpace(SloganTxt.Text))
            {
                MessageBox.Show("Slogan is empty", " Validition", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (string.IsNullOrWhiteSpace(RegionTxt.Text))
            {
                MessageBox.Show("Region is empty", " Validition", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (string.IsNullOrWhiteSpace(PostalTxt.Text))
            {
                MessageBox.Show("Postal is empty", " Validition", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (string.IsNullOrWhiteSpace(PhoneText.Text))
            {
                MessageBox.Show("Phone is empty", " Validition", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            

            humans.Add(human);

            var jsonString = System.Text.Json.JsonSerializer.Serialize(humans);
            File.WriteAllText("Human.json", jsonString);

        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Select a picture";
            ofd.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            if (ofd.ShowDialog() == true)
            {
                profile.ImageSource = new BitmapImage(new Uri(ofd.FileName));
                profile.Stretch = Stretch.UniformToFill;
            }

        }
    }
}
