namespace TvShowExplorer.Models
{
    public class TvMazeSearchResult
    {
        public double Score { get; set; }
        public TvMazeShow Show { get; set; } = default!;
    }

    public class TvMazeShow
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Summary { get; set; }
        public string[]? Genres { get; set; }
        public TvMazeRating? Rating { get; set; }
        public TvMazeImage? Image { get; set; }
        public TvMazeNetwork? Network { get; set; }
    }

    public class TvMazeRating
    {
        public double? Average { get; set; }
    }

    public class TvMazeImage
    {
        public string? Medium { get; set; }
        public string? Original { get; set; }
    }

    public class TvMazeNetwork
    {
        public string? Name { get; set; }
    }

    public class TvMazeEpisode
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Season { get; set; }
        public int? Number { get; set; }
        public string? Airdate { get; set; }
        public string? Summary { get; set; }
    }
}
