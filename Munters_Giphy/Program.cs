using System;
using System.Collections.Concurrent;

namespace Munters_Giphy
{
    class Program
    {
        private static string _startMessage;
        private static bool _closeProgram = false;
        private static ConcurrentDictionary<string, GiphyRecord[]> _caching;

        static void Main(string[] args)
        {
            _caching = new ConcurrentDictionary<string, GiphyRecord[]>();

            _startMessage = "press 1 to get the top 10 trending on Giphy." + Environment.NewLine + 
                "Press 2 to search Giphy with a search term (up to 10 results)" + Environment.NewLine +
                "Press 'q' to exit";
            Startup();
        }

        private static void Startup()
        {
            if (!_closeProgram)
            {
                Console.WriteLine(_startMessage);
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        GetTrending();
                        break;
                    case "2":
                        GetBySearchTerm();
                        break;
                    case "q":
                        Console.WriteLine("goodbye");
                        _closeProgram = true;
                        break;
                    default:
                        Console.WriteLine("Wrong input, try again");
                        break;
                }
                Console.WriteLine(Environment.NewLine);
                Startup();
            }
        }

        private static void GetTrending()
        {
            string key = DateTime.Today.ToString() + "_Trending";
            var giphyRecords = GetFromCache(key);

            if (giphyRecords == null)
            {
                var response = GiphyInfo.GetTrending();
                giphyRecords = response.GiphyRecords;
                AddToCache(key, giphyRecords);
            }

            PrintResults(giphyRecords);
        }

        private static void GetBySearchTerm()
        {
            Console.WriteLine(Environment.NewLine + "Please enter a search term");
            string searchTerm = Console.ReadLine();


            string key = searchTerm + "_searchTerm";
            var giphyRecords = GetFromCache(key);

            if (giphyRecords == null)
            {
                var response = GiphyInfo.GetBySearch(searchTerm);
                giphyRecords = response.GiphyRecords;
                AddToCache(key, giphyRecords);
            }

            PrintResults(giphyRecords);
        }

        private static void PrintResults(GiphyRecord[] giphyRecords)
        {
            foreach (var record in giphyRecords)
            {
                Console.WriteLine($"{record.ID,-10} {record.Url,-40}");
            }
        }

        private static GiphyRecord[] GetFromCache(string key)
        {
            _caching.TryGetValue(key, out GiphyRecord[] records);
            return records;
        }

        private static void AddToCache(string key, GiphyRecord[] value)
        {
            _caching.GetOrAdd(key, value);
        }
    }
}
