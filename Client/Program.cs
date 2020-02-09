using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static HttpClient client = new HttpClient();
        private readonly static string url = "http://localhost:4798/api/works";

        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:4798/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            RunAsync().GetAwaiter().GetResult();

        }


        static async Task<List<Work>> GetAllWorksAsync()
        {
            var path = "http://localhost:4798/api/works";
            List<Work> works = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                works = await response.Content.ReadAsAsync<List<Work>>();
            }
            return works;
        }

        static async Task<Work> GetWorkAsync(string workID)
        {
            var path = "http://localhost:4798/api/works" + $"/{workID}";
            Work work = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                work = await response.Content.ReadAsAsync<Work>();
            }
            return work;
        }

        static async Task<HttpStatusCode> CreateWorkAsync(Work work)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                url, work);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        static async Task<HttpStatusCode> DeleteWorkAsync(string ID)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                url + $"/{ID}");
            return response.StatusCode;
        }

        static void ShowWork(Work work)
        {
            Console.WriteLine($"ID: {work.ID}\nComposerID:{work.ComposerID}\nEraID: {work.EraID}\nTitle: {work.Title}\nDescription: {work.Description}\nYear: {work.Year}");
        }

        static void ShowAllWorks(List<Work> works)
        {
            foreach (var work in works)
            {
                Console.WriteLine($"ID: {work.ID}\nComposerID:{work.ComposerID}\nEraID: {work.EraID}\nTitle: {work.Title}\nDescription: {work.Description}\nYear: {work.Year}");
                Console.WriteLine();
            }
        }

        static async Task RunAsync()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Work App Interface!\n\nJust simply enter any of the following commands:\n- get - to find a work by ID \n- getAll - to show all works\n- del - to delete a work\n- add - to add a work\n- q - to exit");
            Console.WriteLine();
            client.BaseAddress = new Uri("http://localhost:4798/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            await Input();
        }

        public static async Task Input()
        {
            for (; ; )
            {
                Console.WriteLine("Enter command...");
                string command = Console.ReadLine();
                string input;
                string getByID = "get";
                string getAll = "getAll";
                string add = "add";
                string del = "del";
                string txt = "txt";
                string q = "q";
                string[] commands = { getByID, getAll, add, del, txt, q };
                if (command == getByID)
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Enter ID of work:");
                        input = Console.ReadLine();
                        Console.WriteLine();
                        var work = await GetWorkAsync(input);
                        ShowWork(work);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (command == getAll)
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Showing all works:");
                        Console.WriteLine();
                        var works = await GetAllWorksAsync();
                        ShowAllWorks(works);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                if (command == add)
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Enter data to insert a work:");
                        Console.WriteLine();
                        Console.WriteLine($"Enter ComposerID:");
                        var composerID = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine($"Enter EraID: ");
                        var eraID = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine($"Enter Title:");
                        var title = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine($"Enter Description:");
                        var description = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine($"Enter Year:");
                        var year = Console.ReadLine();
                        
                        var work = new Work
                        {
                            ComposerID = int.Parse(composerID),
                            EraID = int.Parse(eraID),
                            Title = title,
                            Description = description,
                            Year = int.Parse(year)
                        };
                        var statusCode = await CreateWorkAsync(work);
                        Console.WriteLine($"Created (HTTP Status = {(int)statusCode})");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (command == del)
                {
                    Console.Clear();
                    Console.WriteLine("Enter ID of work to delete:");
                    input = Console.ReadLine();
                    var statusCode = await DeleteWorkAsync(input);
                    Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
                }
                if (command == "q")
                {
                    Console.WriteLine();
                    Console.WriteLine("Closing program...");
                    break;
                }
                if (!commands.Contains(command))
                {
                    Console.WriteLine("Wrong command!");
                    Console.WriteLine();
                }
            }
        }
    }
}
