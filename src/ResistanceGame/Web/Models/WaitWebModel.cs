using ResistanceGame.Data;
using ResistanceGame.Model;

namespace ResistanceGame.Web.Models;

public class WaitWebModel
{
    public static WaitWebModel Create(int id, Player? current, bool isMayBeStart)
    {
        return new WaitWebModel
        {
            Id = id,
            Current = current ?? new(),
            All = PlayersRepository.All,
            IsMayBeStart = isMayBeStart,
        };
    }

    public int Id { get; init; }
    public required Player Current { get; init; }
    public IEnumerable<Player> All { get; init; } = Array.Empty<Player>();
    public bool IsMayBeStart { get; init; }
}