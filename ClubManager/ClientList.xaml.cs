using System;
using System.Linq;
using System.Windows;

namespace ClubManager
{
    /// <summary>
    /// Окно для отображения всех клиентов
    /// </summary>
    public partial class ClientList : Window
    {
        public ClientList()
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
                foreach (var client in db.Clients)
                {
                    try
                    {
                        if (client.Hours >= 50)
                        {
                            client.Discount = 10;
                        }
                        else
                        {
                            client.Discount = 0;
                        }
                        var lastClientRent = db.Rents.Where(x => x.ClientId == client.IdClient).Max();
                        if (lastClientRent.RentDate.AddHours(lastClientRent.Hours) > DateTime.Now && lastClientRent.Hours >= 5 && client.Discount < 15)
                        {
                            client.Discount += 15;
                        }
                        else if ((lastClientRent.RentDate.AddHours(lastClientRent.Hours) < DateTime.Now || lastClientRent.Hours < 5) && client.Discount >= 15)
                        {
                            client.Discount -= 15;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                db.SaveChanges();
                var clients = db.Clients.ToList();
                clientGrid.ItemsSource = clients;
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            ClientEdit clientEdit = new ClientEdit();
            clientEdit.Show();
            this.Hide();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ClientAdd clientAdd = new ClientAdd();
            clientAdd.Show();
            this.Hide();
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
