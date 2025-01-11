using ResistanceGame.Model;

namespace ResistanceGame.Web.Models;

public class GameWebModel
{
    public static GameWebModel Create(int id, Player? current, Player? leader)
    {
        return new GameWebModel
        {
            Id = id,
            Current = current ?? new(),
            Leader = leader ?? new(),
        };
    }

    public int Id { get; init; }
    public required Player Current { get; init; }
    public Player Leader { get; init; }
}