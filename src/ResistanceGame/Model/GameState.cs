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
    SelectLeader,
    SelectTeam,
    VoteOfTeam,
    Execute,
    Result,
    Final,
}