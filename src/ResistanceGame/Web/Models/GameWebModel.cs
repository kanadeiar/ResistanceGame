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
            All = all.ToArray(),
            SelectTeam = selectTeam.ToArray(),
        };
    }

    public int Id { get; init; }
    public required Player Current { get; init; }
    public required Player Leader { get; init; }
    public IEnumerable<Player> All { get; init; } = [];
    public IEnumerable<Player> SelectTeam { get; init; } = [];
    public bool IsMayBeConfirmTeam { get; set; }
    public bool IsMayBeVote { get; set; }
    public bool IsVoteOfTeam { get; set; }
    public bool IsShowResultOfVote { get; set; }
    public bool IsTeamSuccess { get; set; }
    public bool IsExecute { get; set; }
    public bool IsShowResultOfExecute { get; set; }
    public bool IsExecuteIsBeSuccess { get; set; }
    public bool IsFinal { get; set; }
    public bool IsResistanceWin { get; set; }
    public bool IsShow { get; set; }
}