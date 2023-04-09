using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

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
                var pc_list = db.PC.ToList();
                foreach (var pc in pc_list)
                {
                    if(pc.Status == "Забронирован")
                    {
                        if (pc.RentDate.Value.AddHours(Convert.ToDouble(pc.Hours.Value)) < DateTime.Now && pc.RentDate.Value.AddMinutes(15) > DateTime.Now)
                        {
                            pc.Hours = null;
                            pc.ClientId = null;
                            pc.RentDate = null;
                            pc.Status = "Ожидание";
                        }
                        else if (pc.RentDate.Value.AddHours(Convert.ToDouble(pc.Hours.Value)) < DateTime.Now && pc.RentDate.Value.AddMinutes(15) < DateTime.Now)
                        {
                            pc.Hours = null;
                            pc.ClientId = null;
                            pc.RentDate = null;
                            pc.Status = "Свободен";
                        }
                    }
                    else if(pc.Status == "Ожидание")
                    {
                        if (pc.RentDate.Value.AddHours(Convert.ToDouble(pc.Hours.Value)) < DateTime.Now && pc.RentDate.Value.AddMinutes(15) < DateTime.Now)
                        {
                            pc.Hours = null;
                            pc.ClientId = null;
                            pc.RentDate = null;
                            pc.Status = "Свободен";
                        }
                    }
                }
                db.SaveChanges();
                pcGrid.ItemsSource = pc_list;
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
