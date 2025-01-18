namespace ResistanceGame.Model;

public record GameData(int SpyCount, int[] Teams)
{
    public static Dictionary<int, GameData> Data = new Dictionary<int, GameData>
    {
        {0, new GameData(1, [1, 1, 1, 1, 1])},
        {1, new GameData(1, [1, 1, 1, 1, 1])},
        {2, new GameData(1, [1, 1, 1, 1, 1])},
        {3, new GameData(1, [1, 1, 1, 1, 1])},
        {4, new GameData(1, [1, 1, 1, 1, 1])},
        {5, new GameData(2, [2, 3, 2, 3, 3])},
        {6, new GameData(2, [2, 3, 4, 3, 4])},
        {7, new GameData(3, [2, 3, 3, 4, 4])},
        {8, new GameData(3, [3, 4, 4, 5, 5])},
        {9, new GameData(3, [3, 4, 4, 5, 5])},
        {10, new GameData(4, [3, 4, 4, 5, 5])},
    };
}