using Htmx;
using ResistanceGame.Data;
using ResistanceGame.Model;
using ResistanceGame.Web.Models;

namespace ResistanceGame.Web.Hypermedia;

public class GameHypermedia
{
    private static Random _random = new ();
    private static GameState _game = GameState.Init;

    private readonly int _id;
    private readonly HttpRequest _request;
    private readonly Player? _current;
    private static int _leaderId = -1;

    public bool IsNotFound => _current is null;

    public bool IsHypermedia => _request.IsHtmx();

    public bool IsSelectTeam => _game == GameState.SelectTeam;

    public bool IsLeader => _leaderId == _current?.Id;

    public bool IsEnd => _game == GameState.End;

    public GameHypermedia(HttpRequest request, int id)
    {
        _request = request;
        _id = id;

        _current = PlayersRepository.GetById(_id);
        if (_game == GameState.Init)
        {
            _game = GameState.SelectTeam;
            InitNewGame();
            SelectRandomLeader();
            PlayersRepository.SetNeedUpdate();
        }
    }

    private void InitNewGame()
    {
        var all = PlayersRepository.All.ToArray();
        foreach (var each in PlayersRepository.All)
        {
            each.Role = PlayerRole.Peace;
        }

        var spyCount = all.Length switch
        {
            5 => 2,
            6 => 2,
            7 => 3,
            8 => 3,
            9 => 3,
            10 => 4,
            _ => 1,
        };
        while (all.Count(p => p.Role == PlayerRole.Spy) < spyCount)
        {
            var index = _random.Next(all.Length);
            all[index].Role = PlayerRole.Spy;
        }
    }

    private void SelectRandomLeader()
    {
        var all = PlayersRepository.All.ToArray();
        var newLeaderId = -1;
        while (newLeaderId == _leaderId)
        {
            var index = _random.Next(all.Length);
            newLeaderId = all[index].Id;
        }

        _leaderId = newLeaderId;
    }

    public GameWebModel Model() => GameWebModel.Create(_id, _current, PlayersRepository.GetById(_leaderId));

    public bool HasOldData()
    {
        if (_current?.IsNeedUpdate == false) return true;

        _current!.IsNeedUpdate = false;
        return false;
    }
}