using UnityEngine;

public class GameController : MonoBehaviour
{
    public DungeonController DungeonController;
    public FollowCamera FollowCamera;

    public GameObject PlayerPrefab;
    private Player player;
    public Player Player => player;

    private void Start()
    {
        StartNewGame();
    }

    void StartNewGame()
    {
        CreatePlayer();
        DungeonController.CreateNewDungeon();
        DungeonController.MakeCurrentRoom();
    }

    void CreatePlayer()
    {
        GameObject playerGO = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        player = playerGO.GetComponent<Player>();
        DungeonController.player = player;

        FollowCamera.Target = playerGO.transform;
    }
}
