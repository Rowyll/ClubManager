using System;
using System.Linq;
using System.Windows;

namespace ClubManager
{
    public partial class ClientEdit : Window
    {
        public ClientEdit()
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

        private void idField_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            using (ClubManagerEntities db = new ClubManagerEntities())
            {
                try
                {
                    int clientId = Convert.ToInt32(idField.Text);
                    var selectedClient = db.Clients.Where(x => x.IdClient == clientId).FirstOrDefault();
                    nameField.Text = selectedClient.FullName;
                    ageField.Text = selectedClient.Age.ToString();
                    hoursField.Text = selectedClient.Hours.ToString();
                }
                catch
                {
                    return;
                }
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            using (ClubManagerEntities db = new ClubManagerEntities())
            {
                int clientId = Convert.ToInt32(idField.Text);
                var selectedClient = db.Clients.Where(x => x.IdClient == clientId).FirstOrDefault();
                selectedClient.FullName = nameField.Text;
                selectedClient.Age = Convert.ToInt32(ageField.Text);
                selectedClient.Hours = Convert.ToInt32(hoursField.Text);
                db.SaveChanges();
                MessageBox.Show("Данные успешно сохранены");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
