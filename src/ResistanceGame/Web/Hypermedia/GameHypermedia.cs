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
    private static bool? _lastIsSuccess;
    private static Player?[] _selectTeam = [];

    public bool IsNotFound => _current is null;

    public bool IsHypermedia => _request.IsHtmx();

    public bool IsSelectTeam => _game == GameState.SelectTeam;

    public bool IsVoteOfTeam => _game == GameState.VoteOfTeam;
    
    public bool MayBeConfirmTeam => _selectTeam.Length > 0 && _selectTeam.All(p => p is { });

    public bool VoteForTeamComplete => _game == GameState.VoteOfTeam && PlayersRepository.All.All(p => p.Vote != null);
    
    public bool IsShowResultOfVote => _game == GameState.ShowResultOfVote;

    public bool IsAllIsContinue => PlayersRepository.All.All(p => p.Continue);

    public bool IsExecute => _game == GameState.Execute;
    
    public bool IsShowResultOfExecute => _game == GameState.ShowResultOfExecute;

    public bool IsExecuteIsBeSuccess => _lastIsSuccess == true;

    public bool IsTeamSuccess
    {
        get
        {
            var needCount = PlayersRepository.All.Count() / 2;
            return PlayersRepository.All.Count(p => p.Vote == true) > needCount;
        }
    }

    public bool IsEnd => _game == GameState.End;

    public GameHypermedia(HttpRequest request, int id)
    {
        _request = request;
        _id = id;

        _current = PlayersRepository.GetById(_id);
        if (_game == GameState.Init)
        {
            _game = GameState.SelectTeam;
            initNewGame();
            selectRandomLeader();
            PlayersRepository.SetNeedUpdate();
        }

        if (VoteForTeamComplete)
        {
            _game = GameState.ShowResultOfVote;
            PlayersRepository.SetNeedContinue();
        }

        if (IsShowResultOfVote && IsAllIsContinue)
        {
            foreach (var each in PlayersRepository.All)
            {
                each.Continue = false;
            }
            if (IsTeamSuccess)
            {
                _game = GameState.Execute;
            }
            else
            {
                initTeam();
                selectRandomLeader();
                _game = GameState.SelectTeam;
            }
            PlayersRepository.SetNeedUpdate();
        }

        if (IsExecute && _selectTeam.All(p => p?.IsSuccess is not null))
        {
            if (_selectTeam.All(p => p?.IsSuccess == true))
            {
                _lastIsSuccess = true;
            }
            _gameStages[_stage].IsSuccess = _lastIsSuccess;
            _stage++;

            _game = GameState.ShowResultOfExecute;
            PlayersRepository.SetNeedUpdate();
        }

        // if (_gameStages.Count(p => p.IsSuccess == true) >= 3 || _gameStages.Count(p => p.IsSuccess == false) >= 3)
    }

    private void initNewGame()
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

        initTeam();
    }

    private static void initTeam()
    {
        var stage = _gameStages[_stage];
        _selectTeam = new Player[stage.Count];
        foreach (var each in PlayersRepository.All)
        {
            each.Vote = null;
            each.Continue = false;
        }
    }

    private static void selectRandomLeader()
    {
        var all = PlayersRepository.All.ToArray();
        var newLeaderId = -1;
        do
        {
            var index = _random.Next(all.Length);
            newLeaderId = all[index].Id;
        } while (newLeaderId == _leaderId);

        _leaderId = newLeaderId;
    }

    public void SelectTeamMember(int memberId, bool isSelect)
    {
        var selectedMember = PlayersRepository.GetById(memberId);

        if (isSelect)
        {
            for (var i = 0; i < _selectTeam.Length; i++)
            {
                if (_selectTeam[i] == selectedMember) break;
                if (_selectTeam[i] is not null) continue;

                _selectTeam[i] = selectedMember;
                PlayersRepository.SetNeedUpdate();
                break;
            }
        }
        else
        {
            for (var i = 0; i < _selectTeam.Length; i++)
            {
                if (_selectTeam[i] != selectedMember) continue;

                _selectTeam[i] = null;
                PlayersRepository.SetNeedUpdate();
                break;
            }
        }
    }

    public void ConfirmTeam()
    {
        if (!MayBeConfirmTeam) return;

        _game = GameState.VoteOfTeam;
        PlayersRepository.SetNeedUpdate();
    }

    public void VoteOfTeam(bool vote)
    {
        if (!IsVoteOfTeam) return;

        if (_current is { Vote: null })
        {
            _current.Vote = vote;
            PlayersRepository.SetNeedUpdate();
        }
    }

    public void Continue()
    {
        if (_current is not null)
        {
            _current.Continue = true;
        }
    }

    public void Execute(bool isSuccess)
    {
        if (_current is not null)
        {
            _current.IsSuccess = isSuccess;
        }
    }

    public GameWebModel Model()
    {
        var result = GameWebModel.Create(_id, _current, PlayersRepository.GetById(_leaderId), PlayersRepository.All, _selectTeam);
        result.IsMayBeConfirmTeam = MayBeConfirmTeam;
        result.IsMayBeVote = _current?.Vote == null;
        result.IsTeamSuccess = IsTeamSuccess;
        result.IsVoteOfTeam = IsVoteOfTeam;
        result.IsShowResultOfVote = IsShowResultOfVote;
        result.IsExecute = IsExecute;
        result.IsShowResultOfExecute = IsShowResultOfExecute;
        result.IsExecuteIsBeSuccess = IsExecuteIsBeSuccess;
        return result;
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