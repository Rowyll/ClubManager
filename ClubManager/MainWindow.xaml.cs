using System;
using System.Windows;

namespace ClubManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (ClubManagerEntities db = new ClubManagerEntities())
            {
                string[] nameArray = db.Staff.Find(CurrentUser.currentUserId).FullName.Split(' ');
                string userTitle = db.Staff.Find(CurrentUser.currentUserId).JobTitle;
                UserName.Content = $"Добро пожаловать, {nameArray[1]}!";
                UserTitle.Content = userTitle;
            }
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }

        private void ClientButton_Click(object sender, RoutedEventArgs e)
        {
            ClientList clientList = new ClientList();
            clientList.Show();
            this.Hide();
        }

        private void PcButton_Click(object sender, RoutedEventArgs e)
        {
            PcList pcList = new PcList();
            pcList.Show();
            this.Hide();
        }

        private void RentButton_Click(object sender, RoutedEventArgs e)
        {
            RentList rentList = new RentList();
            rentList.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        
    }
}
