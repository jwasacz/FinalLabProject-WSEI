using Newtonsoft.Json;
using WebAPI.Entities;

namespace WebApplicationAdmin.API
{
    public class ApiService : IApiInterface
    {
        string url = "https://localhost:7107/api/"; 
        static readonly HttpClient client = new HttpClient();



        public async Task<List<Refuel>> GetAllRefuels()
        {
            List<Refuel> refuels = new List<Refuel>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(url + "Refuel/GetAllRefuels");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                refuels = JsonConvert.DeserializeObject<List<Refuel>>(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return refuels;
        }

    }
}






        //login
        // public async Task <string> Login()
        // {
        //    try
        //  {
        //login
        //hasło

        //HttpResponseMessage response = await client.PostAsync(url);//doda login
        //response.EnsureSuccessStatusCode();
        // string responseBody = await response.Content.ReadAsStringAsync();
        // return null;

        // }
        // catch (Exception ex )
        //  {

        //   throw;

        //  return null;

        //  }


        // } 



  