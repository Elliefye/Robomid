using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room
{
    public Vector2Int RoomCoordinate;
    public Dictionary<string, Room> Neighbors;
    public string RoomNamePrefix;
    public bool IsCleared { get; private set; } = false;

    private string[,] Population;

    public Dictionary<Tuple<int, int>, GameObject> PopulationToGameObject;

    public Room(int xCoordinate, int yCoordinate, string roomNamePrefix)
    {
        RoomNamePrefix = roomNamePrefix;
        RoomCoordinate = new Vector2Int(xCoordinate, yCoordinate);
        Neighbors = new Dictionary<string, Room>();
        Population = new string[16, 12];
        for (int xIndex = 0; xIndex < Population.GetLength(0); xIndex += 1)
        {
            for (int yIndex = 0; yIndex < Population.GetLength(1); yIndex += 1)
            {
                Population[xIndex, yIndex] = string.Empty;
            }
        }
        PopulationToGameObject = new Dictionary<Tuple<int, int>, GameObject>();
    }

    public Room(Vector2Int roomCoordinate, string roomNamePrefix)
    {
        RoomNamePrefix = roomNamePrefix;
        Room room = this;
        room.RoomCoordinate = roomCoordinate;
        Neighbors = new Dictionary<string, Room>();
        Population = new string[16, 12];
        for (int xIndex = 0; xIndex < Population.GetLength(0); xIndex += 1)
        {
            for (int yIndex = 0; yIndex < Population.GetLength(1); yIndex += 1)
            {
                Population[xIndex, yIndex] = string.Empty;
            }
        }
        PopulationToGameObject = new Dictionary<Tuple<int, int>, GameObject>();
    }

    public List<Vector2Int> NeighborCoordinates()
    {
        // NESW ordering
        var neighborCoordinates = new List<Vector2Int>();
        if (RoomCoordinate.y - 1 >= 0)
        {
            neighborCoordinates.Add(new Vector2Int(RoomCoordinate.x, RoomCoordinate.y - 1));
        }
        neighborCoordinates.Add(new Vector2Int(RoomCoordinate.x + 1, RoomCoordinate.y));
        neighborCoordinates.Add(new Vector2Int(RoomCoordinate.x, RoomCoordinate.y + 1));
        if (RoomCoordinate.x - 1 >= 0)
        {
            neighborCoordinates.Add(new Vector2Int(RoomCoordinate.x - 1, RoomCoordinate.y));
        }

        return neighborCoordinates;
    }

    public void Connect(Room neighbor)
    {
        string direction = "";
        if (neighbor.RoomCoordinate.y < RoomCoordinate.y)
        {
            direction = "N";
        }
        if (neighbor.RoomCoordinate.x > RoomCoordinate.x)
        {
            direction = "E";
        }
        if (neighbor.RoomCoordinate.y > RoomCoordinate.y)
        {
            direction = "S";
        }
        if (neighbor.RoomCoordinate.x < RoomCoordinate.x)
        {
            direction = "W";
        }
        Neighbors.Add(direction, neighbor);
    }

    public string PrefabName()
    {
        string name = RoomNamePrefix;
        foreach (KeyValuePair<string, Room> neighborPair in Neighbors)
        {
            name += neighborPair.Key;
        }
        return name;
    }

    public Room Neighbor(string direction)
    {
        return Neighbors[direction];
    }

    public void PopulateObstacles(int numberOfObstacles, Vector2Int[] possibleSizes)
    {
        for (int obstacleIndex = 0; obstacleIndex < numberOfObstacles; obstacleIndex += 1)
        {
            int sizeIndex = UnityEngine.Random.Range(0, possibleSizes.Length);
            Vector2Int regionSize = possibleSizes[sizeIndex];
            List<Vector2Int> region = FindFreeRegion(regionSize);
            foreach (Vector2Int coordinate in region)
            {
                Population[coordinate.x, coordinate.y] = "Obstacle";
            }
        }
    }

    public void PopulatePrefabs(int numberOfPrefabs, GameObject[] possiblePrefabs)
    {
        for (int prefabIndex = 0; prefabIndex < numberOfPrefabs; prefabIndex += 1)
        {
            int choiceIndex = UnityEngine.Random.Range(0, possiblePrefabs.Length);
            GameObject prefab = possiblePrefabs[choiceIndex];
            List<Vector2Int> region = FindFreeRegion(new Vector2Int(1, 1));

            Population[region[0].x, region[0].y] = prefab.name;
            PopulationToGameObject[new Tuple<int, int>(region[0].x, region[0].y)] = prefab;
        }
    }

    private List<Vector2Int> FindFreeRegion(Vector2Int sizeInTiles)
    {
        List<Vector2Int> region = new List<Vector2Int>();
        do
        {
            region.Clear();

            Vector2Int centerTile = new Vector2Int(UnityEngine.Random.Range(1, Population.GetLength(0) - 1), UnityEngine.Random.Range(1, Population.GetLength(1) - 1));

            region.Add(centerTile);

            int initialXCoordinate = (centerTile.x - (int)Mathf.Floor(sizeInTiles.x / 2));
            int initialYCoordinate = (centerTile.y - (int)Mathf.Floor(sizeInTiles.y / 2));
            for (int xCoordinate = initialXCoordinate; xCoordinate < initialXCoordinate + sizeInTiles.x; xCoordinate += 1)
            {
                for (int yCoordinate = initialYCoordinate; yCoordinate < initialYCoordinate + sizeInTiles.y; yCoordinate += 1)
                {
                    region.Add(new Vector2Int(xCoordinate, yCoordinate));
                }
            }
        } while (!IsFree(region));
        return region;
    }

    private bool IsFree(List<Vector2Int> region)
    {
        foreach (Vector2Int tile in region)
        {
            if (Population[tile.x, tile.y] != string.Empty)
            {
                return false;
            }
        }
        return true;
    }

    public void AddPopulationToTilemap(Tilemap tilemap, TileBase obstacleTile)
    {
        for (int xIndex = 0; xIndex < Population.GetLength(0); xIndex += 1)
        {
            for (int yIndex = 0; yIndex < Population.GetLength(1); yIndex += 1)
            {
                if (Population[xIndex, yIndex] == "Obstacle")
                {
                    tilemap.SetTile(new Vector3Int(xIndex - 10, yIndex - 4, 0), obstacleTile);
                }
                else if (Population[xIndex, yIndex] != string.Empty && Population[xIndex, yIndex] != "Player" && 
                            PopulationToGameObject[new Tuple<int, int>(xIndex, yIndex)] != null)
                {
                    if(PopulationToGameObject[new Tuple<int, int>(xIndex, yIndex)].activeSelf)
                    {
                        GameObject prefab = UnityEngine.Object.Instantiate(PopulationToGameObject[new Tuple<int, int>(xIndex, yIndex)]);
                        prefab.transform.position = new Vector2(xIndex - 10 + 0.5f, yIndex - 4 + 0.5f);
                        PopulationToGameObject[new Tuple<int, int>(xIndex, yIndex)] = prefab;
                        prefab.SetActive(false);
                    }
                    else
                    {
                        PopulationToGameObject[new Tuple<int, int>(xIndex, yIndex)].SetActive(true);
                    }
                }
            }
        }
    }

    public void Clear()
    {
        IsCleared = true;
        for (int i = 0; i < Population.GetLength(0); i++)
        {
            for (int j = 0; j < Population.GetLength(1); j++)
            {
                GameObject gameObject;
                if (!PopulationToGameObject.TryGetValue(new Tuple<int, int>(i, j), out gameObject))
                {
                    continue;
                }

                if (gameObject == null || gameObject.CompareTag("Enemy"))
                {
                    Population[i, j] = string.Empty;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}

