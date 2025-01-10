using ResistanceGame.Model;

namespace ResistanceGame.Web.Models;

public class GameWebModel
{
    public static GameWebModel Create(int id, Player? current, bool isLeader)
    {
        return new GameWebModel
        {
            Id = id,
            Current = current ?? new(),
            IsLeader = isLeader,
        };
    }

    public int Id { get; init; }
    public required Player Current { get; init; }
    public bool IsLeader { get; init; }
}