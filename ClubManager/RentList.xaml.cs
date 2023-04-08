using System;
using System.Linq;
using System.Windows;

namespace ClubManager
{
    /// <summary>
    /// Окно для отображения арендованных компьютеров
    /// </summary>
    public partial class RentList : Window
    {
        public RentList()
        {
            InitializeComponent();
            using (ClubManagerEntities db = new ClubManagerEntities())
            {
                string userTitle = db.Staff.Find(CurrentUser.currentUserId).JobTitle;
                UserTitle.Content = userTitle;
            }
            LoadData();
        }

        public void LoadData()
        {
            using (ClubManagerEntities db = new ClubManagerEntities())
            { 
                var rents = db.Rents.ToList();
                rentGrid.ItemsSource = rents;
            }
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        
    }
}
