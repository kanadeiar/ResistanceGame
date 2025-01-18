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

    private static GameStage[] _gameStages = new GameStage[5];
    private static int _stage;
    private static Player[] _selectTeam = [];

    public bool IsNotFound => _current is null;

    public bool IsHypermedia => _request.IsHtmx();

    public bool IsSelectTeam => _game == GameState.SelectTeam;

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
            each.Role = PlayerRole.Resistance;
        }

        var data = GameData.Data[all.Length];

        while (all.Count(p => p.Role == PlayerRole.Spy) < data.SpyCount)
        {
            var index = _random.Next(all.Length);
            all[index].Role = PlayerRole.Spy;
        }

        for (var i = 0; i < 5; i++)
        {
            var count = data.Teams[i];
            _gameStages[i] = new GameStage
            {
                Number = i + 1,
                Count = count
            };
        }

        InitTeam();
    }

    private void InitTeam()
    {
        var stage = _gameStages[_stage];
        _selectTeam = new Player[stage.Count];
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

    public GameWebModel Model()
    {
        return GameWebModel.Create(_id, _current, PlayersRepository.GetById(_leaderId), PlayersRepository.All, _selectTeam);
    }

    public StagesWebModel StagesModel() =>
        new()
        {
            GameStages = _gameStages,
        };

    public bool HasOldData()
    {
        if (_current?.IsNeedUpdate == false) return true;

        _current!.IsNeedUpdate = false;
        return false;
    }
}