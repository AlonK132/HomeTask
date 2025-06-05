using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeTask
{
    internal class jsonplaceholderAPI : APIReader
    {
        public jsonplaceholderAPI(string url) : base(url) { }
        protected override List<User> parseUsers(string rawData)
        {
            List<User> users = new List<User>();

            try
            {
                using JsonDocument doc = JsonDocument.Parse(rawData);
                JsonElement root = doc.RootElement;

                if (root.ValueKind == JsonValueKind.Array)
                {
                    int index = 1;

                    foreach (JsonElement userElement in root.EnumerateArray())
                    {
                        string fullName = userElement.GetProperty("name").GetString();
                        string[] parts = fullName?.Split(' ') ?? new string[] { "", "" };

                        string first = parts.Length > 0 ? parts[0] : "";
                        string last = parts.Length > 1 ? parts[1] : "";

                        string email = userElement.GetProperty("email").GetString();
                        string sourceId = $"jsonplaceholder-{index++}";

                        users.Add(new User(first, last, email, sourceId));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing jsonplaceholder response: {ex.Message}");
            }

            return users;
        }
    }
}
