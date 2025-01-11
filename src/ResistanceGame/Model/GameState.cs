namespace ResistanceGame.Model;

public enum PlayerRole
{
    None,
    Peace,
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