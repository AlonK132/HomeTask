using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeTask
{
    internal class APIManager
    {
        private List<APIReader> readers = new List<APIReader>();
        private string fileType; // will save json/csv
        private string filePath; //store the path to the directory
        public int numberOfUsers { get; set; }
        public APIManager(List<string[]> apiInfo, string fileType, string filePath)
        {
            this.filePath = filePath;
            this.fileType = fileType;
            foreach (var info in apiInfo)
            {
                string apiName = "HomeTask." + info[0] + "API";
                Type type = Type.GetType(apiName); // finds the class by the api name. by this way we can add more classes of api and dosnt need to change this code.
                if (type != null) {
                    APIReader reader = (APIReader)Activator.CreateInstance(type, info[1]);
                    readers.Add(reader);
                }
                else
                {
                    Console.WriteLine("Error while creating " + apiName);
                }

            }

        }
        public async Task loadAllUsers()
        {
            List<User> allUsers = new List<User>();

            foreach (var reader in readers)
            {
                try
                {
                    var users = await reader.fetchUsers(); // using the reader to create the user classes.
                    allUsers.AddRange(users);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load users from reader: {ex.Message}");
                }
            }
            this.numberOfUsers = allUsers.Count; // update the number of users created. 
            string fullPath = this.filePath + System.IO.Path.DirectorySeparatorChar + "users." + fileType.ToLower(); // create a path apropriate to file type(json/csv)
            filePath = fullPath;
            if (this.fileType == "CSV") //chosing the right method/
            {
                saveAsCsv(allUsers);
            }
            else if(this.fileType == "JSON")
            {
                saveAsJson(allUsers);
            }
        }
        private void saveAsCsv(List<User> users) {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("Id,FirstName,LastName,Email");

                foreach (var user in users)
                {
                    sb.AppendLine($"{user.id},{user.firstName},{user.lastName},{user.email}");
                }

                File.WriteAllText(filePath, sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save CSV: {ex.Message}");
            }
        

    }
        private void saveAsJson(List<User> users) {
            try
            {
                string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save JSON: {ex.Message}");
            }
        }


    }
}
