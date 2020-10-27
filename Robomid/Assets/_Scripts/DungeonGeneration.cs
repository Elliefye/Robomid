using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGeneration : MonoBehaviour
{

    [SerializeField]
    private int numberOfRooms;
    [SerializeField]
    public string roomNamePrefix;

    [SerializeField]
    private int numberOfObstacles;
    [SerializeField]
    private Vector2Int[] possibleObstacleSizes;

    [SerializeField]
    private int numberOfEnemies;
    [SerializeField]
    private GameObject[] possibleEnemies;

    [SerializeField]
    private GameObject goalPrefab;

    [SerializeField]
    private TileBase obstacleTile;

    private Room[,] rooms;

    private Room currentRoom;

    [HideInInspector]
    public GameObject roomObject;

    private static DungeonGeneration instance = null;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            currentRoom = GenerateDungeon();
        }
        else
        {
            string roomPrefabName = instance.currentRoom.PrefabName();
            roomObject = (GameObject)Instantiate(Resources.Load(roomPrefabName));
            Tilemap tilemap = roomObject.GetComponentInChildren<Tilemap>();
            instance.currentRoom.AddPopulationToTilemap(tilemap, instance.obstacleTile);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        string roomPrefabName = currentRoom.PrefabName();
        roomObject = (GameObject)Instantiate(Resources.Load(roomPrefabName));
        Tilemap tilemap = roomObject.GetComponentInChildren<Tilemap>();
        currentRoom.AddPopulationToTilemap(tilemap, obstacleTile);
    }

    private Room GenerateDungeon()
    {
        int gridSize = 3 * numberOfRooms;

        rooms = new Room[gridSize, gridSize];

        Vector2Int initialRoomCoordinate = new Vector2Int((gridSize / 2) - 1, (gridSize / 2) - 1);

        Queue<Room> roomsToCreate = new Queue<Room>();
        roomsToCreate.Enqueue(new Room(initialRoomCoordinate.x, initialRoomCoordinate.y, roomNamePrefix));
        List<Room> createdRooms = new List<Room>();
        while (roomsToCreate.Count > 0 && createdRooms.Count < numberOfRooms)
        {
            Room currentRoom = roomsToCreate.Dequeue();
            rooms[currentRoom.roomCoordinate.x, currentRoom.roomCoordinate.y] = currentRoom;
            createdRooms.Add(currentRoom);
            AddNeighbors(currentRoom, roomsToCreate);
        }

        int maximumDistanceToInitialRoom = 0;
        Room finalRoom = null;
        foreach (Room room in createdRooms)
        {
            List<Vector2Int> neighborCoordinates = room.NeighborCoordinates();
            foreach (Vector2Int coordinate in neighborCoordinates)
            {
                Room neighbor = rooms[coordinate.x, coordinate.y];
                if (neighbor != null)
                {
                    room.Connect(neighbor);
                }
            }
            room.PopulateObstacles(numberOfObstacles, possibleObstacleSizes);
            room.PopulatePrefabs(numberOfEnemies, possibleEnemies);

            int distanceToInitialRoom = Mathf.Abs(room.roomCoordinate.x - initialRoomCoordinate.x) + Mathf.Abs(room.roomCoordinate.y - initialRoomCoordinate.y);
            if (distanceToInitialRoom > maximumDistanceToInitialRoom)
            {
                maximumDistanceToInitialRoom = distanceToInitialRoom;
                finalRoom = room;
            }
        }

        GameObject[] goalPrefabs = { goalPrefab };
        finalRoom.PopulatePrefabs(1, goalPrefabs);

        return rooms[initialRoomCoordinate.x, initialRoomCoordinate.y];
    }

    private void AddNeighbors(Room currentRoom, Queue<Room> roomsToCreate)
    {
        List<Vector2Int> neighborCoordinates = currentRoom.NeighborCoordinates();
        List<Vector2Int> availableNeighbors = new List<Vector2Int>();
        foreach (Vector2Int coordinate in neighborCoordinates)
        {
            if (rooms[coordinate.x, coordinate.y] == null)
            {
                availableNeighbors.Add(coordinate);
            }
        }

        int numberOfNeighbors = Random.Range(1, availableNeighbors.Count);

        for (int neighborIndex = 0; neighborIndex < numberOfNeighbors; neighborIndex++)
        {
            float randomNumber = Random.value;
            float roomFrac = 1f / availableNeighbors.Count;
            Vector2Int chosenNeighbor = new Vector2Int(0, 0);
            foreach (Vector2Int coordinate in availableNeighbors)
            {
                if (randomNumber < roomFrac)
                {
                    chosenNeighbor = coordinate;
                    break;
                }
                else
                {
                    roomFrac += 1f / availableNeighbors.Count;
                }
            }
            roomsToCreate.Enqueue(new Room(chosenNeighbor, roomNamePrefix));
            availableNeighbors.Remove(chosenNeighbor);
        }
    }

    public void MoveToRoom(Room room)
    {
        currentRoom = room;
    }

    public Room CurrentRoom()
    {
        return currentRoom;
    }

    public void ResetDungeon()
    {
        currentRoom = GenerateDungeon();
    }

}
