
using Microsoft.EntityFrameworkCore;
using EpicerieAlda.Models;
namespace EpicerieAlda.Dao
{
    public interface IClientDao
    {
       Task AjouterClientAsync(Client client);
    }
}
