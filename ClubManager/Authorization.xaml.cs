﻿using System;
using System.Windows;

namespace ClubManager
{
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (ClubManagerEntities db = new ClubManagerEntities())
            {
                if (Login.Text != "" && Password.Password != "")
                {
                    foreach (Staff staff in db.Staff)
                    {
                        if (Login.Text == staff.Login && Password.Password == staff.Password)
                        {
                            CurrentUser.currentUserId = staff.IdStaff;
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            this.Hide();
                            return;
                        }
                    }
                    MessageBox.Show("Неверные данные");
                }
                else
                    MessageBox.Show("Введите данные");
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}