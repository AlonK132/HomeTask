using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeTask
{
    internal class dummyjsonAPI : APIReader
    {
        public dummyjsonAPI(string url) : base(url) { }
        protected override List<User> parseUsers(string rawData)
        {
            List<User> users = new List<User>();

            try
            {
                using JsonDocument doc = JsonDocument.Parse(rawData);
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("users", out JsonElement userArray))
                {
                    int index = 1;

                    foreach (JsonElement userElement in userArray.EnumerateArray())
                    {
                        string first = userElement.GetProperty("firstName").GetString();
                        string last = userElement.GetProperty("lastName").GetString();
                        string email = userElement.GetProperty("email").GetString();
                        string sourceId = $"dummyjson-{index++}";

                        users.Add(new User(first, last, email, sourceId));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing dummyjson response: {ex.Message}");
            }

            return users;
        }
    }
}
