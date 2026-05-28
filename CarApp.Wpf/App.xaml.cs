
// App.xaml.cs 

using CarApp.Core.Repositories;

using CarApp.Wpf.ViewModels;

using CarApp.Wpf.Views;
using System.Windows;


namespace CarApp.Wpf
{

    public partial class App : Application

    {

        protected override void OnStartup(StartupEventArgs e)

        {

            base.OnStartup(e);


            ICarRepository carRepo = new InMemoryCarRepository();

            ITripRepository tripRepo = new InMemoryTripRepository();


            var mainViewModel = new MainViewModel(carRepo, tripRepo);

            var mainView = new MainView();

            mainView.DataContext = mainViewModel;

            mainView.Show();

        }

    }

}