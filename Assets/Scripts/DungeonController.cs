using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    Dungeon currentDungeon;

    public TileSet TileSet;

    public int NoOfFloors;
    public Vector2Int RoomsPerFloor;
    public Vector2Int RoomSize;

    private int floorIndex = 0;
    public Floor CurrentFloor => currentDungeon.floors[floorIndex];
    private Vector2Int roomPosition;
    public Room CurrentRoom => currentDungeon.floors[floorIndex].Rooms[roomPosition.x, roomPosition.y];

    public void CreateNewDungeon()
    {
        currentDungeon = new Dungeon();

        for (int i=0; i < NoOfFloors; i++)
        {
            GameObject floorObj = new GameObject("Floor " + i);
            floorObj.transform.SetParent(transform);
            Floor floor = floorObj.AddComponent<Floor>();
            currentDungeon.floors.Add(floor);
            floor.Rooms = new Room[RoomsPerFloor.x, RoomsPerFloor.y];

            for (int x = 0; x < RoomsPerFloor.x; x++)
            {
                for (int y = 0; y < RoomsPerFloor.y; y++)
                {
                    GameObject roomObj = new GameObject("Room " + x + ", " + y);
                    roomObj.transform.SetParent(floorObj.transform);
                    Room room = roomObj.AddComponent<Room>();
                    floor.Rooms[x, y] = room;

                    room.RoomPosition = new Vector2Int(x, y);

                    room.Tiles = new Tile[RoomSize.x, RoomSize.y];

                    for (int tilex = 0; tilex < RoomSize.x; tilex++)
                    {
                        for (int tiley = 0; tiley < RoomSize.y; tiley++)
                        {
                            GameObject tileObj = new GameObject("Tile " + tilex + ", " + tiley);
                            tileObj.transform.SetParent(roomObj.transform);
                            room.Tiles[tilex, tiley] = tileObj.AddComponent<Tile>();
                            room.Tiles[tilex, tiley].Position = new Vector2Int(tilex, tiley);
                        }
                    }
                }
            }
        }
        // add door in appropriate places
        foreach (Floor floor in currentDungeon.floors)
        {
            for (int x = 0; x< floor.Rooms.GetLength(0); x++)
            {
                for (int y = 0; y< floor.Rooms.GetLength(1); y++)
                {
                    Room room = floor.Rooms[x, y];
                    if (room!= null)
                    {
                        if (RoomHasNeighbour(room, Vector2Int.up))
                        {
                            room.Tiles[room.Size.x / 2, room.Size.y - 1].ID = Tile.eTileID.DoorUp;
                        }
                        if (RoomHasNeighbour(room, Vector2Int.down))
                        {
                            room.Tiles[room.Size.x / 2, 0].ID = Tile.eTileID.DoorDown;
                        }
                        if (RoomHasNeighbour(room, Vector2Int.left))
                        {
                            room.Tiles[0, room.Size.y / 2].ID = Tile.eTileID.DoorLeft;
                        }
                        if (RoomHasNeighbour(room, Vector2Int.right))
                        {
                            room.Tiles[room.Size.x - 1, room.Size.y / 2].ID = Tile.eTileID.DoorRight;
                        }
                    }
                }
            }
        }
    }

    bool RoomHasNeighbour(Room _checkRoom, Vector2Int _direction)
    {
        Vector2Int testPos = _checkRoom.RoomPosition + _direction;

        if (testPos.x < 0 || testPos.y<0 || testPos.x >= CurrentFloor.Rooms.GetLength(0) || testPos.y >= CurrentFloor.Rooms.GetLength(1))
        {
            return false;
        }
        if (CurrentFloor.Rooms[testPos.x, testPos.y] == null)
        {
            return false;
        }

        return true;
    }

    public void MakeCurrentRoom()
    {
        for (int x=0; x < CurrentRoom.Size.x; x++)
        {
            for (int y = 0; y < CurrentRoom.Size.y; y++)
            {
                GameObject defaultTile = GameObject.Instantiate(TileSet.GetTilePrototype(TilePrototype.eTileID.Empty).PrefabObject, new Vector3(x, 0, y), Quaternion.identity);

                TilePrototype.eTileID id = TilePrototype.eTileID.Empty;
                switch (CurrentRoom.Tiles[x, y].ID)
                {
                    case Tile.eTileID.DoorUp:
                    case Tile.eTileID.DoorDown:
                    case Tile.eTileID.DoorLeft:
                    case Tile.eTileID.DoorRight:
                        id = TilePrototype.eTileID.Door;
                        break;

                    case Tile.eTileID.FloorDown:
                        id = TilePrototype.eTileID.FloorDown;
                        break;
                    
                    case Tile.eTileID.FloorUp:
                        id = TilePrototype.eTileID.FloorUp;
                        break;
                                
                }

                if (id != TilePrototype.eTileID.Empty)
                {
                    GameObject prefabObject = TileSet.GetTilePrototype(id).PrefabObject;
                    if (prefabObject != null)
                    {
                        GameObject newTileObject = GameObject.Instantiate(prefabObject, new Vector3(x, 0, y), Quaternion.identity);
                        newTileObject.transform.SetParent(CurrentRoom.Tiles[x, y].transform);
                    }
                    else
                    {
                        Debug.LogError("Missing GameObject For : " + id);
                    }


                }
            }
        }
    }
}
