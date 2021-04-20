namespace Munters_Giphy
{
    public static class GiphyInfo
    {
        const string url = "https://api.giphy.com/v1/gifs/";
        const string apiKey = "KhGB5M2fGEfh37C7T2spT8dwpL0iZaIo";

        public static GiphyResponse GetTrending()
        {
            string urlParameters = $"trending?api_key={apiKey}&limit=10&rating=g";
            var response = APICall.RunAsync<GiphyResponse>(url, urlParameters).GetAwaiter().GetResult();
            return response;
        }

        public static GiphyResponse GetBySearch(string searchTerm = " ")
        {
            string urlParameters = $"search?api_key={apiKey}&q={searchTerm}&limit=10&offset=0&rating=g&lang=en";
            var response = APICall.RunAsync<GiphyResponse>(url, urlParameters).GetAwaiter().GetResult();
            return response;
        }
    }
}
