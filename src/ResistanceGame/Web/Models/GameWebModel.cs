using ResistanceGame.Model;

namespace ResistanceGame.Web.Models;

public class GameWebModel
{
    public static GameWebModel Create(int id, Player? current, Player? leader, IEnumerable<Player> all)
    {
        return new GameWebModel
        {
            Id = id,
            Current = current ?? new(),
            Leader = leader ?? new(),
            All = all
        };
    }

    public int Id { get; init; }
    public required Player Current { get; init; }
    public required Player Leader { get; init; }
    public IEnumerable<Player> All { get; init; } = [];
    public IEnumerable<Player> SelectedTeam { get; init; } = [];
    public bool IsShow { get; set; }
}