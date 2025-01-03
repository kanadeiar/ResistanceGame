using Htmx;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ResistanceGame.Data;
using ResistanceGame.Web.Models;

namespace ResistanceGame.Web.Hypermedia;

public class RegistrationHypermedia
{
    private readonly HttpRequest _request;
    private readonly ModelStateDictionary _modelState;
    private readonly RegistrationWebModel _model;

    public bool IsHtmx => _request.IsHtmx();

    public bool IsInvalid => !_modelState.IsValid;

    public RegistrationHypermedia(HttpRequest request, ModelStateDictionary modelState, RegistrationWebModel model)
    {
        _request = request;
        _modelState = modelState;
        _model = model;

        _model.Validate(_modelState);
    }

    public int RegisterNewMember()
    {
        var member = _model.Map();
        var id = PlayersRepository.Add(member);

        return id;
    }
}