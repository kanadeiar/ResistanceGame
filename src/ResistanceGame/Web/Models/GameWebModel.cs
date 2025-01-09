using ResistanceGame.Model;

namespace ResistanceGame.Web.Models;

public class GameWebModel
{
    public static GameWebModel Create(int id, Player? current, int counter)
    {
        return new GameWebModel
        {
            Id = id,
            Current = current ?? new(),
            Counter = counter
        };
    }

    public int Id { get; init; }
    public required Player Current { get; init; }
    public int Counter { get; init; }
}