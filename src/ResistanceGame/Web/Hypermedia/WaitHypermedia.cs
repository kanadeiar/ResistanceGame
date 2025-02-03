using Htmx;
using ResistanceGame.Data;
using ResistanceGame.Model;
using ResistanceGame.Web.Models;

namespace ResistanceGame.Web.Hypermedia;

public class WaitHypermedia
{
    private readonly int _id;
    private readonly HttpRequest _request;
    private readonly Player? _current;

    public bool IsNotFound => _current is null;

    public bool IsHypermedia => _request.IsHtmx();

    private bool isMayBeStart =>
        PlayersRepository.All.Count() >= 3 &&
        PlayersRepository.All.Count() <= 10 &&
        PlayersRepository.All.All(p => p.IsReady) &&
        !IsGameStarted;

    public bool IsGameStarted =>
        PlayersRepository.All.Any(p => p.IsPlay);

    public WaitHypermedia(HttpRequest request, int id)
    {
        _request = request;
        _id = id;

        _current = PlayersRepository.GetById(_id);
    }

    public void SwitchReady()
    {
        if (_current == null) return;

        _current.IsReady = !_current.IsReady;
        PlayersRepository.SetNeedUpdate();
    }

    public void Start()
    {
        if (!isMayBeStart) return;
        foreach (var each in PlayersRepository.All)
        {
            each.IsPlay = true;
        }
        PlayersRepository.SetNeedUpdate();
    }

    public WaitWebModel Model() => WaitWebModel.Create(_id, _current, isMayBeStart);

    public bool HasOldData()
    {
        if (_current?.IsNeedUpdate == false) return true;

        _current!.IsNeedUpdate = false;
        return false;
    }
}