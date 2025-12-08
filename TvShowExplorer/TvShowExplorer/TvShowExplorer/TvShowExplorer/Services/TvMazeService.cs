using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
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
            var url = $"/search/shows?q={Uri.EscapeDataString(query)}";

            try
            {
                var results = await _http.GetFromJsonAsync<List<TvMazeSearchResult>>(url);
                return results ?? new List<TvMazeSearchResult>();
            }
            catch
            {
                throw;
            }
        }

        public async Task<TvMazeShow?> GetShowByIdAsync(int id)
        {
            var url = $"/shows/{id}";

            try
            {
                var show = await _http.GetFromJsonAsync<TvMazeShow>(url);
                return show;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<TvMazeEpisode>> GetEpisodesForShowAsync(int showId)
        {
            var url = $"/shows/{showId}/episodes";

            try
            {
                var episodes = await _http.GetFromJsonAsync<List<TvMazeEpisode>>(url);
                return episodes ?? new List<TvMazeEpisode>();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets a page of shows (250 per page). Page is 0-indexed.
        /// </summary>
        public async Task<List<TvMazeShow>> GetShowsByPageAsync(int page = 0)
        {
            var url = $"/shows?page={page}";

            try
            {
                var shows = await _http.GetFromJsonAsync<List<TvMazeShow>>(url);
                return shows ?? new List<TvMazeShow>();
            }
            catch
            {
                throw;
            }
        }
    }
}
