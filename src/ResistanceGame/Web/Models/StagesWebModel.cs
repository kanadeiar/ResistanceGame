using ResistanceGame.Model;

namespace ResistanceGame.Web.Models;

public class StagesWebModel
{
    public GameStage[] GameStages { get; init; } = new GameStage[5];
}