using ResistanceGame.Model;

namespace ResistanceGame.Data;

public static class PlayersRepository
{
    private static List<Player> _players = new();
    private static int _lastId = 1;

    public static IEnumerable<Player> All => _players;

    public static Player? GetById(int id)
    {
        return All.FirstOrDefault(p => p.Id == id);
    }

    public static int Add(Player player)
    {
        player.Id = _lastId++;
        _players.Add(player);
        UpNeedUpdate();
        return player.Id;
    }

    public static void Remove(int id)
    {
        if (GetById(id) is not { } deleted) return;

        _players.Remove(deleted);
        UpNeedUpdate();
    }

    public static void UpNeedUpdate()
    {
        foreach (var each in All)
        {
            each.IsNeedUpdate = true;

        }
    }
}