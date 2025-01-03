namespace ResistanceGame.Model;

public class Player
{
    public int Id { get; set; }
    public string? Name { get; init; }
    public bool IsReady { get; set; }
    public bool IsNeedUpdate { get; set; }
}