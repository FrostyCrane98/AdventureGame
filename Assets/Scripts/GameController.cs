using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
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
        DungeonController.Instance.CreateNewDungeon();
    }

    void CreatePlayer()
    {
        GameObject playerGO = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        player = playerGO.GetComponent<Player>();

        FollowCamera.Target = playerGO.transform;
    }
}
