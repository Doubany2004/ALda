using Microsoft.EntityFrameworkCore;
using EpicerieAlda.Models;
using EpicerieAlda.Data;
namespace EpicerieAlda.Dao
{
    public class ClientDao:IClientDao
    {
        private readonly AldaDbContext _db;

        // permet de donner l'accès à la base de données via le contexte AldaDbContext
        public ClientDao(AldaDbContext db)
        {
            _db = db;
        }

        // ajouter client a la base donnee
        public async Task AjouterClientAsync(Client client)
        {
            await _db.Clients.AddAsync(client);
            await _db.SaveChangesAsync();
        }

    }
}
