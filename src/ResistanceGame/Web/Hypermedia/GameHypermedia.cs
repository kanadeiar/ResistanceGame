using Htmx;
using ResistanceGame.Data;
using ResistanceGame.Model;
using ResistanceGame.Web.Models;

namespace ResistanceGame.Web.Hypermedia;

public class GameHypermedia
{
    private static GameState _game = GameState.Init;

    private readonly int _id;
    private readonly HttpRequest _request;
    private readonly Player? _current;

    public bool IsNotFound => _current is null;

    public bool IsHypermedia => _request.IsHtmx();

    public GameHypermedia(HttpRequest request, int id)
    {
        _request = request;
        _id = id;

        _current = PlayersRepository.GetById(_id);
    }

    private static int _counter;

    public GameWebModel Model() => GameWebModel.Create(_id, _current, _counter++);

    public bool HasOldData()
    {
        if (_current?.IsNeedUpdate == false) return true;

        _current!.IsNeedUpdate = false;
        return false;
    }
}