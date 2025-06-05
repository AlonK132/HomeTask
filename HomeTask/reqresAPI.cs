using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeTask
{
    internal class reqresAPI : APIReader
    {
        public reqresAPI(string url) : base(url) { }
        protected override List<User> parseUsers(string rawData)
        {
            List<User> users = new List<User>();

            try
            {
                using JsonDocument doc = JsonDocument.Parse(rawData);
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("error", out JsonElement error))
                {
                    Console.WriteLine($"Reqres API error: {error.GetString()}");
                    return users;
                }
                if (root.TryGetProperty("data", out JsonElement dataArray))
                {
                    int index = 1;

                    foreach (JsonElement userElement in dataArray.EnumerateArray())
                    {
                        string first = userElement.GetProperty("first_name").GetString();
                        string last = userElement.GetProperty("last_name").GetString();
                        string email = userElement.GetProperty("email").GetString();
                        string sourceId = $"reqres-{index++}";

                        users.Add(new User(first, last, email, sourceId));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing reqres response: {ex.Message}");
            }

            return users;
        }
    }
}
