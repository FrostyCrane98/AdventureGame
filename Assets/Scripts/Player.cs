using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    AdventureGame controls;

    public float MoveSpeed;

    private int maxHealth;
    private int currentHealth;
    private int experience;
    private Quest[] quests;
    private Weapon heldWeapon;
    private Armour equipedArmour;
    private Item[] consumables;
    private float potionCooldown;

    private bool moving;
    private Vector2Int currentPosition;
    private Vector2Int targetPosition;

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void Awake()
    {
        controls = new AdventureGame();
        controls.Player.Move.performed += context => BeginMove(context.ReadValue<Vector2>());
    }

    public void SetPosition(Vector2Int _position)
    {
        currentPosition = _position;
        transform.position = new Vector3(currentPosition.x, 0, currentPosition.y);
    }

    private void BeginMove(Vector2 _direction)
    {
        Vector2Int direction = new Vector2Int((int)_direction.x, (int)_direction.y);
        Vector2Int position = currentPosition + direction;

        if (position.x < 0 || position.y < 0 
            || position.x >= DungeonController.Instance.CurrentRoom.Size.x 
            || position.y >= DungeonController.Instance.CurrentRoom.Size.y)
        {
            Debug.Log("Tried to move OOB");
            return;
        }

        Tile tile = DungeonController.Instance.CurrentRoom.Tiles[position.x, position.y];
        if (!moving && tile != null)
        {
            moving = true;
            targetPosition = position;
        }            
    }

    private void Update()
    {
        if (moving)
        {
            Vector3 targetPos = new Vector3(targetPosition.x, 0, targetPosition.y);
            if(Vector3.Distance(transform.position, targetPos) > float.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * MoveSpeed);
            }
            else
            {
                currentPosition = targetPosition;
                Tile tile = DungeonController.Instance.CurrentRoom.Tiles[currentPosition.x, currentPosition.y];
                
                // DungeonController.Instance.EnterTile(tile);

                moving = false;
            }
        }
    }
    void OnKill()
    {

    }

    void OnLevelUp()
    {

    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }
}
