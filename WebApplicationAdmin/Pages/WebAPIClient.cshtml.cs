using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationAdmin.API;
using WebAPI.Entities;

namespace WebApplicationAdmin.Pages
{
    public class WebAPIClientModel : PageModel
    {
        private readonly IApiInterface _apiService;

        public WebAPIClientModel(IApiInterface apiService)
        {
            _apiService = apiService;
        }

        public List<Refuel> Refuels { get; set; }

        public async Task OnGetAsync()
        {
            Refuels = await _apiService.GetAllRefuels();
        }
    }
}