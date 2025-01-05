using ResistanceGame.Model;

namespace ResistanceGame.Web.Models;

public class WaitWebModel
{
    public int Id { get; init; }
    public required Player Current { get; init; }
    public IEnumerable<Player> All { get; init; } = Array.Empty<Player>();
    public bool IsMayBeStart { get; init; }
}