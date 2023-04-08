using System;
using System.Linq;
using System.Windows;

namespace ClubManager
{
    /// <summary>
    /// Окно для отображения всех доступных компьютеров
    /// </summary>
    public partial class PcList : Window
    {
        public PcList()
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
                foreach(var currentPC in db.PC)
                {
                    if (currentPC.Status == "Забронирован")
                    {
                        var rentStart = currentPC.RentDate;
                        var rentHours = currentPC.Hours;
                        var addedTime = rentStart.Value.AddHours(rentHours.Value);
                        if (addedTime < DateTime.Now && addedTime.AddMinutes(10) > DateTime.Now)
                        {
                            currentPC.Status = "Ожидание";
                            currentPC.ClientId = null;
                            currentPC.RentDate = null;
                            currentPC.Hours = null;
                        }
                    }
                    else if (currentPC.Status == "Ожидание")
                    {
                        currentPC.Status = "Свободен";
                        currentPC.ClientId = null;
                        currentPC.RentDate = null;
                        currentPC.Hours = null;
                    }
                }
                db.SaveChanges();
                var pc = db.PC.ToList();
                pcGrid.ItemsSource = pc;
            }
        }

        private void ButtonRent_Click(object sender, RoutedEventArgs e)
        {
            PcRent pcRent = new PcRent();
            pcRent.Show();
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
