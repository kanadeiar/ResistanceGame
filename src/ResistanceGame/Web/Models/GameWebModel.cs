using ResistanceGame.Model;

namespace ResistanceGame.Web.Models;

public class GameWebModel
{
    public static GameWebModel Create(int id, Player? current, Player? leader, IEnumerable<Player> all, IEnumerable<Player> selectTeam)
    {
        return new GameWebModel
        {
            Id = id,
            Current = current ?? new(),
            Leader = leader ?? new(),
            All = all,
            SelectTeam = selectTeam,
        };
    }

    public int Id { get; init; }
    public required Player Current { get; init; }
    public required Player Leader { get; init; }
    public IEnumerable<Player> All { get; init; } = [];
    public IEnumerable<Player> SelectTeam { get; init; } = [];
    public bool IsMayBeConfirmTeam { get; set; }
    public bool IsShow { get; set; }
}