using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeTask
{
    internal class randomuserAPI : APIReader
    {
        public randomuserAPI(string url) :base(url) { }
        protected override List<User> parseUsers(string rawData)
        {
            List<User> users = new List<User>();

            try
            {
                using JsonDocument doc = JsonDocument.Parse(rawData);
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("results", out JsonElement results))
                {
                    int index = 1;

                    foreach (JsonElement userElement in results.EnumerateArray())
                    {
                        string first = userElement.GetProperty("name").GetProperty("first").GetString();
                        string last = userElement.GetProperty("name").GetProperty("last").GetString();
                        string email = userElement.GetProperty("email").GetString();
                        string sourceId = $"randomuser-{index++}";
                        users.Add(new User(first, last, email, sourceId));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing randomuser response: {ex.Message}");
            }

            return users;
        }
    }
}
