namespace JiaoMaCupScoreRecorder.Client.Models;

public class ScoreDataModel
{
    public string DiscordId { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string Point { get; set; } = string.Empty;

    public decimal TotalScore { get; set; }

    public List<decimal> Scores { get; set; } = new();

    public List<decimal> Scores2 { get; set; } = new();

    public List<string> ImageUrls { get; set; } = new();
}