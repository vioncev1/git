using UnityEngine;

public class Main : MonoBehaviour
{
    public static IGameStrategy GameStrategy { get; set; }
    public static int FieldWidth { get; set; } = 0;
    public static int FieldHeight { get; set; } = 0;
    public static Transform[,] Grid { get; set; } = null;
    public static int Score { get; set; } = 0;
    public static bool isGameOver = false;

    public enum GameState
    {
        InGame,
        Ended,
    }

    public static GameState gameState = GameState.Ended;

    public static Vector2 RoundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool InsideBorder(Vector2 pos)
    {
        return (int)pos.x >= 0 && (int)pos.x < FieldWidth && (int)pos.y >= 0;
    }

    public static void DeleteRow(int y)
    {
        for (int x = 0; x < FieldWidth; ++x)
        {
            Destroy(Grid[x, y].gameObject);
            Grid[x, y] = null;
        }

        GameStrategy.IncScore();
    }

    public static void DecreaseRow(int y)
    {
        for (int x = 0; x < FieldWidth; ++x)
        {
            if (Grid[x, y] != null)
            {
                // Move one towards bottom
                Grid[x, y - 1] = Grid[x, y];
                Grid[x, y] = null;

                // Update Block position
                Grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void DecreaseRowsAbove(int y)
    {
        for (int i = y; i < FieldHeight; ++i)
        {
            DecreaseRow(i);
        }
    }

    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < FieldWidth; ++x)
        {
            if (Grid[x, y] == null)
            {
                return false;
            }
        }

        return true;
    }

    public static void DeleteFullRows()
    {
        GameStrategy.DeleteFullRows();
    }

    public static void CheckGameOver()
    {
        for (int x = 0; x < FieldWidth; ++x)
        {
            if (Grid[x, FieldHeight - 1] != null)
            {
                isGameOver = true;
                break;
            }
        }
    }

    void Start()
    {
    }

    void Update()
    {
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(5, 14, 100, 100), "Score: " + Score);

        if (gameState == GameState.InGame)
        {
            return;
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 60, 200, 50), "Режим #1"))
        {
            GameStrategy = FindObjectOfType<GameMode1>();
            GameStrategy.SetParams();

            FindObjectOfType<Spawner>().SpawnNext();

            gameState = GameState.InGame;
        }
        else if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 10, 200, 50), "Режим #2"))
        {
            GameStrategy = FindObjectOfType<GameMode2>();
            GameStrategy.SetParams();

            FindObjectOfType<Spawner>().SpawnNext();

            gameState = GameState.InGame;
        }
    }
}
