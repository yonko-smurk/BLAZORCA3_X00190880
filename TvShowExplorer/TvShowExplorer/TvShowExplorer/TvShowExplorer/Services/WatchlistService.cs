using Microsoft.JSInterop;
using System.Text.Json;

namespace TvShowExplorer.Services
{
    public class WatchlistService
    {
        private const string StorageKey = "tvshow_watchlist";
        private readonly IJSRuntime _js;

        public WatchlistService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<List<int>> GetWatchlistAsync()
        {
            var json = await _js.InvokeAsync<string?>("localStorage.getItem", StorageKey);

            if (string.IsNullOrWhiteSpace(json))
                return new List<int>();

            try
            {
                return JsonSerializer.Deserialize<List<int>>(json) ?? new List<int>();
            }
            catch
            {
                return [];
            }
        }

        public async Task<bool> IsInWatchlistAsync(int showId)
        {
            var watchlist = await GetWatchlistAsync();
            return watchlist.Contains(showId);
        }

        public async Task ToggleWatchlistAsync(int showId)
        {
            var watchlist = await GetWatchlistAsync();

            if (watchlist.Contains(showId))
                watchlist.Remove(showId);
            else
                watchlist.Add(showId);

            var json = JsonSerializer.Serialize(watchlist);
            await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
        }
    }
}