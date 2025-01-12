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
    Execute,
    Result,
    Final,
    End,
}