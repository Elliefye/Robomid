﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class DungeonGeneration : MonoBehaviour
{
    [SerializeField]
    private Guid Id;
    [SerializeField]
    public int NumberOfRooms;
    [SerializeField]
    public string RoomNamePrefix;

    [SerializeField]
    private int NumberOfObstacles;
    [SerializeField]
    private Vector2Int[] PossibleObstacleSizes;

    [SerializeField]
    private int NumberOfEnemies;
    [SerializeField]
    private GameObject[] PossibleEnemies = null;
    [SerializeField]
    private GameObject GoalPrefab;

    [SerializeField]
    private TileBase ObstacleTile;

    private Room[,] Rooms;

    private Room CurrentRoom;

    [HideInInspector]
    public GameObject RoomObject;

    public static DungeonGeneration Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            CurrentRoom = GenerateDungeon();
            foreach(var room in Rooms)
            {
                if (room != null)
                {
                    foreach (var entry in room.PopulationToGameObject)
                    {
                        DontDestroyOnLoad(entry.Value);
                    }
                }
            }
            GameObject floorEffects = GameObject.FindGameObjectWithTag("FloorEffects");
            //floorEffects.GetComponent<FloorEffectDisplay>().FirstTrigger = true;
        }
        else
        {
            string roomPrefabName = Instance.CurrentRoom.PrefabName();
            RoomObject = (GameObject)Instantiate(Resources.Load($"Levels/{roomPrefabName}"));
            Tilemap tilemap = RoomObject.GetComponentInChildren<Tilemap>();
            Instance.CurrentRoom.AddPopulationToTilemap(tilemap, Instance.ObstacleTile);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        string roomPrefabName = CurrentRoom.PrefabName();

        RoomObject = (GameObject)Instantiate(Resources.Load($"Levels/{roomPrefabName}"));
        Tilemap tilemap = RoomObject.GetComponentInChildren<Tilemap>();
        CurrentRoom.AddPopulationToTilemap(tilemap, ObstacleTile);
    }

    private Room GenerateDungeon()
    {
        Rooms = new Room[1 + NumberOfRooms, 1 + NumberOfRooms];

        Vector2Int initialRoomCoordinate = new Vector2Int((NumberOfRooms / 2) - 1, (NumberOfRooms / 2) - 1);

        Queue<Room> roomsToCreate = new Queue<Room>();
        roomsToCreate.Enqueue(new Room(initialRoomCoordinate.x, initialRoomCoordinate.y, RoomNamePrefix));
        List<Room> createdRooms = new List<Room>();
        while (roomsToCreate.Count > 0 && createdRooms.Count < NumberOfRooms)
        {
            Room currentRoom = roomsToCreate.Dequeue();
            Rooms[currentRoom.RoomCoordinate.x, currentRoom.RoomCoordinate.y] = currentRoom;
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
                Room neighbor = null;
                neighbor = Rooms[coordinate.x, coordinate.y];
                if (neighbor != null)
                {
                    room.Connect(neighbor);
                }
            }
            room.PopulateObstacles(NumberOfObstacles, PossibleObstacleSizes);
            room.PopulatePrefabs(NumberOfEnemies, PossibleEnemies);

            int distanceToInitialRoom = Mathf.Abs(room.RoomCoordinate.x - initialRoomCoordinate.x) + Mathf.Abs(room.RoomCoordinate.y - initialRoomCoordinate.y);
            if (distanceToInitialRoom > maximumDistanceToInitialRoom)
            {
                maximumDistanceToInitialRoom = distanceToInitialRoom;
                finalRoom = room;
            }
        }

        var goalPrefabs = new GameObject[] { GoalPrefab };

        if (finalRoom == null)
        {
            finalRoom = createdRooms.FirstOrDefault();
        }
        finalRoom.PopulatePrefabs(1, goalPrefabs);

        foreach(var room in createdRooms)
        {
            string roomPrefabName = room.PrefabName();
            RoomObject = (GameObject)Resources.Load($"Levels/{roomPrefabName}");
            Tilemap tilemap = RoomObject.GetComponentInChildren<Tilemap>();
            room.AddPopulationToTilemap(tilemap, Instance.ObstacleTile);
        }

        return Rooms[initialRoomCoordinate.x, initialRoomCoordinate.y];
    }

    private void AddNeighbors(Room currentRoom, Queue<Room> roomsToCreate)
    {
        List<Vector2Int> neighborCoordinates = currentRoom.NeighborCoordinates();
        List<Vector2Int> availableNeighbors = new List<Vector2Int>();
        foreach (Vector2Int coordinate in neighborCoordinates)
        {
            try
            {
                if (Rooms[coordinate.x, coordinate.y] == null)
                {
                    availableNeighbors.Add(coordinate);
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Debug.LogError(ex);
            }
        }

        int numberOfNeighbors = UnityEngine.Random.Range(1, availableNeighbors.Count);

        for (int neighborIndex = 0; neighborIndex < numberOfNeighbors; neighborIndex++)
        {
            float randomNumber = UnityEngine.Random.value;
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
            roomsToCreate.Enqueue(new Room(chosenNeighbor, RoomNamePrefix));
            availableNeighbors.Remove(chosenNeighbor);
        }
    }

    public void MoveToRoom(Room room)
    {
        CurrentRoom = room;
    }

    public Room GetCurrentRoom()
    {
        return CurrentRoom;
    }

    public void ResetDungeon()
    {
        foreach (var room in Rooms)
        {
            if (room != null)
            {
                foreach (var entry in room.PopulationToGameObject)
                {
                    Destroy(entry.Value);
                }
            }
        }
        CurrentRoom = GenerateDungeon();
        foreach (var room in Rooms)
        {
            if (room != null)
            {
                foreach (var entry in room.PopulationToGameObject)
                {
                    DontDestroyOnLoad(entry.Value);
                }
            }
        }
    }

    //Reuse when creating map Ui
    public string PrintGrid()
    {
        string gridStr = "\n";

        for (int rowIndex = 0; rowIndex < Rooms.GetLength(1); rowIndex++)
        {
            string row = "";
            for (int columnIndex = 0; columnIndex < Rooms.GetLength(0); columnIndex++)
            {
                if (Rooms[columnIndex, rowIndex] == null)
                {
                    row += "X";
                }
                else
                {
                    row += "R";
                }
            }
            gridStr += row + "\n";
        }
        return gridStr;
    }

    public bool RoomExists(Vector2Int coordinates)
    {
        if (Rooms[coordinates.x, coordinates.y] == null)
        {
            return false;
        }
        else return true;
    }
}
