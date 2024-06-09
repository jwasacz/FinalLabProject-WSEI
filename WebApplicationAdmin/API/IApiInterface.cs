
using WebAPI.Entities;

namespace WebApplicationAdmin.API
{
    public interface IApiInterface
    {
         Task<List<Refuel>> GetAllRefuels();

    }
}
