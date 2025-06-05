namespace HomeTask
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            List<string[]> apiInfo = new List<string[]>();
            string url1 = "https://randomuser.me/api/?results=500";
            string url2 = "https://jsonplaceholder.typicode.com/users";
            string url3 = "https://dummyjson.com/users";
            string url4 = "https://reqres.in/api/users";
            apiInfo.Add(["randomuser", url1]);
            apiInfo.Add(["jsonplaceholder", url2]);
            apiInfo.Add(["dummyjson", url3]);
            apiInfo.Add(["reqres", url4]);
            // the info about the url better be saved in some file. Its implements this way due to lack of time.
            APIManager manager;
            Console.WriteLine("Welcome to the system!");
            while (true)
            {
                Console.WriteLine("Please enter a path");
                string path = Console.ReadLine();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    Console.WriteLine("Directory did not exist. Creating a new one");
                }
                Console.WriteLine("Which file format will you like to use? JSON/CSV");
                string format = Console.ReadLine();
                while(format != "JSON" && format != "CSV")
                {
                    Console.WriteLine("Invalid format, please enter again. To exit enter exit");
                    format = Console.ReadLine();
                    if(format == "exit")
                        return;
                }
                manager = new APIManager(apiInfo, format, path);
                await manager.loadAllUsers();
                Console.WriteLine("Number of Users created:" + manager.numberOfUsers);
                return;
            }
        }
    }
}