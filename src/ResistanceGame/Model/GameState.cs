namespace ResistanceGame.Model;

public enum PlayerRole
{
    None,
    Resistance,
    Spy,
}

public enum GameState
{
    Init,
    SelectTeam,
    VoteOfTeam,
    ShowResultOfVote,
    Execute,
    Result,
    Final,
    End,
}