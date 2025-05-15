using DiplomProject.Models;
using DiplomProject.Services;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;

namespace DiplomProject.ViewModels
{
    public class ClientsViewModel : ViewModelBase
    {
        private readonly ClientService _clientService;
        private ObservableCollection<Client> _clients = new();

        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set => this.RaiseAndSetIfChanged(ref _clients, value);
        }

        public ReactiveCommand<Unit, Unit> LoadClientsCommand { get; }

        public ClientsViewModel(ClientService clientService)
        {
            _clientService = clientService;
            LoadClientsCommand = ReactiveCommand.CreateFromTask(LoadClientsAsync);
        }

        private async Task LoadClientsAsync()
        {
            var clients = await _clientService.GetClientsAsync();
            Clients = new ObservableCollection<Client>(clients);
        }
    }
}