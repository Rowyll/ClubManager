using System;
using System.Windows;

namespace ClubManager
{
    /// <summary>
    /// Окно для добавления новых клиентов
    /// </summary>
    public partial class ClientAdd : Window
    {
        public ClientAdd()
        {
            InitializeComponent();
            using (ClubManagerEntities db = new ClubManagerEntities())
            {
                string userTitle = db.Staff.Find(CurrentUser.currentUserId).JobTitle;
                UserTitle.Content = userTitle;
            }
        }
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            ClientList clientList = new ClientList();
            clientList.Show();
            this.Hide();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            using (ClubManagerEntities db = new ClubManagerEntities())
            {
                try
                {
                    db.Clients.Add(new Clients { FullName = nameField.Text, Age = Convert.ToInt16(ageField.Text), Hours = Convert.ToInt16(hoursField.Text), Discount = 0 });
                    db.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены");
                }
                catch{
                    MessageBox.Show("Неверные данные");
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
