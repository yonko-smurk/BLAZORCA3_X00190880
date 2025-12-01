using System.Net.Http;
using System.Net.Http.Json;
using TvShowExplorer.Models;

namespace TvShowExplorer.Services
{
    public class TvMazeService
    {
        private readonly HttpClient _http;

        public TvMazeService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TvMazeSearchResult>> SearchShowsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<TvMazeSearchResult>();

            var encoded = Uri.EscapeDataString(query);
            var url = $"/search/shows?q={encoded}";

            try
            {
                var result = await _http.GetFromJsonAsync<List<TvMazeSearchResult>>(url);
                return result ?? new List<TvMazeSearchResult>();
            }
            catch
            {
                // Let the UI show a friendly error message
                throw;
            }
        }

        public async Task<List<TvMazeEpisode>> GetEpisodesForShowAsync(int showId)
        {
            var url = $"/shows/{showId}/episodes";

            try
            {
                var result = await _http.GetFromJsonAsync<List<TvMazeEpisode>>(url);
                return result ?? new List<TvMazeEpisode>();
            }
            catch
            {
                throw;
            }
        }
    }
}
