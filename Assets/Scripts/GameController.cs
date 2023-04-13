using UnityEngine;

public class GameController : MonoBehaviour
{
    public DungeonController dungeonController;

    private void Start()
    {
        StartNewGame();
    }

    void StartNewGame()
    {
        dungeonController.CreateNewDungeon();
        dungeonController.MakeCurrentRoom();
    }
}
