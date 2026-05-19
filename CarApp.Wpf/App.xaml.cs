
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
            System.Diagnostics.Debug.WriteLine($"App Starter!");
            ICarRepository repository = new InMemoryCarRepository();

            var viewModel = new CarViewModel(repository);

            var view = new CarView();

            view.DataContext = viewModel;

            view.Show();

        }

    }

}