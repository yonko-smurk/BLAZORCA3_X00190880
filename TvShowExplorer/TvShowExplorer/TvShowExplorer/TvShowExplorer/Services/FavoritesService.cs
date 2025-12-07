using Microsoft.JSInterop;
using System.Text.Json;

namespace TvShowExplorer.Services
{
    public class FavoritesService
    {
        private const string StorageKey = "tvshow_favorites";
        private readonly IJSRuntime _js;

        public FavoritesService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<List<int>> GetFavoritesAsync()
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
                return new List<int>();
            }
        }

        public async Task<bool> IsFavoriteAsync(int showId)
        {
            var favorites = await GetFavoritesAsync();
            return favorites.Contains(showId);
        }

        public async Task ToggleFavoriteAsync(int showId)
        {
            var favorites = await GetFavoritesAsync();

            if (favorites.Contains(showId))
            {
                favorites.Remove(showId);
            }
            else
            {
                favorites.Add(showId);
            }

            var json = JsonSerializer.Serialize(favorites);
            await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
        }
    }
}
