using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    public FollowCamera FollowCamera;

    public GameObject PlayerPrefab;
    private Player player;
    public Player Player => player;

    // Verificare se valido in quanto non viene scritto durante il video
    public enum eGameState
    {
        PlayerTurn,
        MonsterTurn
    }

    public eGameState GameState;

    private void Start()
    {
        StartNewGame();
    }

    void StartNewGame()
    {
        CreatePlayer();
        DungeonController.Instance.CreateNewDungeon();
    }

    // Verificare se valido in quanto non viene scritto durante il video
    public void GoToState(eGameState state)
    {
        GameState = state;
    }

    void CreatePlayer()
    {
        GameObject playerGO = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        player = playerGO.GetComponent<Player>();

        FollowCamera.Target = playerGO.transform;
    }
}
