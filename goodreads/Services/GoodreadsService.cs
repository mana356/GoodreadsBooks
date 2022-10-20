using goodreads.Repository.Entities;
using goodreads.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;

namespace goodreads.Services
{
    public class GoodreadsService : IGoodreadsService
    {
        
        public async Task FindAndUpdateBookDetails()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55587/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("api-key", "11111111");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync("api/Department/1");
                if (response.IsSuccessStatusCode)
                {
                    
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
        }

        
    }
}
