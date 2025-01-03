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

    public int Id => _id;

    public bool IsNotFound => _current is null;

    public bool IsHtmx => _request.IsHtmx();

    public WaitHypermedia(HttpRequest request, int id)
    {
        _request = request;
        _id = id;

        _current = PlayersRepository.GetById(_id);
    }

    public WaitWebModel Model()
    {
        return new WaitWebModel
        {
            Id = _id,
            Current = _current ?? new Player(),
            All = PlayersRepository.All,
        };
    }

    public bool HasOldData()
    {
        if (_current?.IsNeedUpdate == false) return true;

        _current!.IsNeedUpdate = false;
        return false;
    }
}