using DiplomProject.Data;
using DiplomProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DiplomProject.Services
{
    public class ClientService
    {
        private readonly AppDbContext _dbContext;
        private readonly EncryptionService _encryptionService;

        public ClientService(AppDbContext dbContext, EncryptionService encryptionService)
        {
            _dbContext = dbContext;
            _encryptionService = encryptionService;
        }

        public async Task AddClientAsync(Client client)
        {
            client.Phone = _encryptionService.Encrypt(client.Phone);
            client.Email = _encryptionService.Encrypt(client.Email);
            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ObservableCollection<Client>> GetClientsAsync()
        {
            var clients = await _dbContext.Clients.ToListAsync();
            foreach (var client in clients)
            {
                client.Phone = _encryptionService.Decrypt(client.Phone);
                client.Email = _encryptionService.Decrypt(client.Email);
            }
            return new ObservableCollection<Client>(clients);
        }
    }
}
