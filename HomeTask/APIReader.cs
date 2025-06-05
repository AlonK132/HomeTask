using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTask
{
    internal abstract class APIReader
    {
        protected string url;
        protected APIReader(string url)
        {
            this.url = url;
        }
        protected async Task<string> fetchRawData()
        {
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(url); // loading data from the url
                response.EnsureSuccessStatusCode(); //checking that the load is finished with success
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching data from {url}: {e.Message}");
                return "";
            }

        }
        public async Task<List<User>> fetchUsers() {
            string data = await fetchRawData();
            if (data == null || data == "") {
                return new List<User>();
            }  
            else
            {
                return parseUsers(data); //each reader have its own way to read the data ferom the url, so I used an abstract method. 
            }
        }
        protected abstract List<User> parseUsers(string rawData);
        


    }
}
