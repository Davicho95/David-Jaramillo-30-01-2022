using ListaTareas.Data;
using ListaTareas.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListaTareas
{
    public partial class App : Application
    {
        private static DatabaseContext context;

        public static DatabaseContext Context
        {
            get
            {
                if (context == null)
                {

                    var dbPath = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "TareasDB.db3");

                    context = new DatabaseContext(dbPath);

                }

                return context;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TareasPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
