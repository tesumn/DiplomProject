using DiplomProject.ViewModels;
using ReactiveUI;
using System;
using System.Reactive;

namespace DiplomProject.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentTab;
        public ViewModelBase CurrentTab
        {
            get => _currentTab;
            set => this.RaiseAndSetIfChanged(ref _currentTab, value);
        }

        public ReactiveCommand<ViewModelBase, Unit> SetCurrentTabCommand { get; }
        public DashboardViewModel Dashboard { get; }
        public ClientsViewModel Clients { get; }
        public OrdersViewModel Orders { get; }

        public MainWindowViewModel(
            DashboardViewModel dashboard,
            ClientsViewModel clients,
            OrdersViewModel orders)
        {
            Dashboard = dashboard;
            Clients = clients;
            Orders = orders;

            _currentTab = Dashboard;
            SetCurrentTabCommand = ReactiveCommand.Create<ViewModelBase>(vm => CurrentTab = vm);
        }
    }
}