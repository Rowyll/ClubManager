using System;
using System.Linq;
using System.Windows;

namespace ClubManager
{
    /// <summary>
    /// Окно для бронирования компьютеров
    /// </summary>
    public partial class PcRent : Window
    {
        public PcRent()
        {
            InitializeComponent();
            using (ClubManagerEntities db = new ClubManagerEntities())
            {
                string userTitle = db.Staff.Find(CurrentUser.currentUserId).JobTitle;
                UserTitle.Content = userTitle;
                rentDateField.Text = DateTime.Now.ToString();
            }
        }
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            PcList pcList = new PcList();
            pcList.Show();
            this.Hide();
        }

        private void TextField_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            using (ClubManagerEntities db = new ClubManagerEntities())
            {
                try
                {
                    int clientId = Convert.ToInt16(clientIdField.Text);
                    int pcId = Convert.ToInt16(pcIdField.Text);
                    var currentPC = db.PC.Where(x => x.IdPC == pcId).FirstOrDefault();
                    var currentClient = db.Clients.Where(x => x.IdClient == clientId).FirstOrDefault();
                    var price = 0;
                    switch (Convert.ToInt16(currentPC.Type))
                    {
                        case 1:
                            price = 100;
                            break;
                        case 2:
                            price = 150;
                            break;
                        case 3:
                            price = 200;
                            break;
                    }
                    if(Convert.ToInt16(hoursField.Text) >=5)
                    {
                        priceField.Text = Math.Round(price * Convert.ToDouble(hoursField.Text) * (1 - 0.15 - Convert.ToDouble(currentClient.Discount) / 100)).ToString();
                    }
                    else
                    {
                        priceField.Text = Math.Round(price * Convert.ToDouble(hoursField.Text) * (1 - Convert.ToDouble(currentClient.Discount) / 100)).ToString();
                    }
                }
                catch
                {
                    return;
                }
            }
                
        }

        private void ButtonRent_Click(object sender, RoutedEventArgs e)
        {
            using (ClubManagerEntities db = new ClubManagerEntities())
            {
                int pcId = Convert.ToInt16(pcIdField.Text);
                var currentPC = db.PC.Where(x => x.IdPC == pcId).FirstOrDefault();
                if (currentPC.Status == "Свободен")
                {
                    db.Rents.Add(new Rents { ClientId = Convert.ToInt16(clientIdField.Text), IdPC = Convert.ToInt16(pcIdField.Text), AdminId = CurrentUser.currentUserId, RentDate = DateTime.Parse(rentDateField.Text), Hours = Convert.ToInt16(hoursField.Text), Price = Convert.ToInt16(priceField.Text) });
                    int currentClientId = Convert.ToInt16(clientIdField.Text);
                    int currentPcId = Convert.ToInt16(pcIdField.Text);
                    var currentClient = db.Clients.Where(x => x.IdClient == currentClientId).FirstOrDefault();
                    currentClient.Hours += Convert.ToInt16(hoursField.Text);
                    currentPC.Status = "Забронирован";
                    currentPC.RentDate = DateTime.Parse(rentDateField.Text);
                    currentPC.Hours = Convert.ToInt16(hoursField.Text);
                    currentPC.ClientId = Convert.ToInt16(clientIdField.Text);
                    db.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены");
                }
                else
                {
                    MessageBox.Show("Компьютер занят или находится в ожидании");
                }

            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
