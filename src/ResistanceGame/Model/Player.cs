namespace ResistanceGame.Model;

public class Player
{
    public static Player Create(string? name)
    {
        return new Player
        {
            Name = name,
        };
    }

    public int Id { get; set; }
    public string? Name { get; private init; }
    public bool IsReady { get; set; }
    public bool IsPlay { get; set; }

    public PlayerRole Role { get; set; } = PlayerRole.None;

    public bool? Vote { get; set; }

    public bool Continue { get; set; }

    public bool? IsSuccess { get; set; }

    public bool IsNeedUpdate { get; set; }
}