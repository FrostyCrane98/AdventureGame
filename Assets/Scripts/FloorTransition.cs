using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTransition
{
    public Room TargetRoom;
    public Vector2Int TilePosition;

    public FloorTransition(Room _room, Vector2Int _tilePos)
    {
        TargetRoom = _room;
        TilePosition = _tilePos;
    }
}
