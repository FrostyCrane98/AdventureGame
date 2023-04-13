using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum eTileID
    {
        Empty,
        DoorUp,
        DoorDown,
        DoorLeft,
        DoorRight,
        FloorUp,
        FloorDown
    }
    public eTileID ID;

    public Vector2Int Position;

}