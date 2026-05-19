
// App.xaml.cs 

using CarApp.Core.Repositories;

using CarApp.Wpf.ViewModels;

using CarApp.Wpf.Views;
using System.Windows;



public partial class App : Application

{

    protected override void OnStartup(StartupEventArgs e)

    {
        MessageBox.Show("OnStartup er startet!");
        base.OnStartup(e);

        ICarRepository repository = new InMemoryCarRepository();

        var viewModel = new CarViewModel(repository);

        var view = new CarView();

        view.DataContext = viewModel;
        MessageBox.Show("OnStartup er her!");
        view.Show();

    }

}

    