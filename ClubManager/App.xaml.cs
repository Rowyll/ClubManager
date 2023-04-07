using System.Windows;

namespace ClubManager
{
    public partial class App : Application
    {

    }

    public static class CurrentUser
    {
        public static int currentUserId { get; set; }
    }

    public static class SelectedClient
    {
        public static int selectedClientId { get; set; }
    }
}