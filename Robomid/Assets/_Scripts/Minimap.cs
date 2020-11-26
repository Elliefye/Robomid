using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform[,] minimapRooms;
    [SerializeField]
    private GameObject roomObject;
    [SerializeField]
    private float spriteOffset = 6;
    private DungeonGeneration generator;
    [SerializeField]
    private Camera MinimapCamera;
    public bool NewDungeon = false;

    private MinimapRoom currentRoom;

    public static Minimap Instance = null;

    void Start()
    {
        if(Instance == null)
        {
            generator = GameObject.FindGameObjectWithTag("Dungeon").GetComponent<DungeonGeneration>();
            minimapRooms = new Transform[generator.NumberOfRooms + 1, generator.NumberOfRooms + 1];
            DontDestroyOnLoad(gameObject);
            Instance = this;
            PopulateMinimap();
        }
        else if (Instance.NewDungeon == false)
        {
            Vector2Int coord = Instance.generator.GetCurrentRoom().RoomCoordinate;
            //Debug.Log("room c.:" + coord);
            Instance.AllignCameraWithRoom(coord);
            Instance.minimapRooms[coord.x, coord.y].GetComponent<MinimapRoom>().Active = true;
            Instance.currentRoom.Active = false;
            Instance.currentRoom.Discovered = true;
            Instance.currentRoom = Instance.minimapRooms[coord.x, coord.y].GetComponent<MinimapRoom>();
            Destroy(gameObject);
        }
        else
        {
            Instance.NewDungeon = false;
            Destroy(gameObject);
        }
    }

    public void PopulateMinimap()
    {
        for(int row = 0; row < minimapRooms.GetLength(1); row++)
        {
            for(int column = 0; column < minimapRooms.GetLength(0); column++)
            {
                GameObject newRoom = GameObject.Instantiate(roomObject, gameObject.transform);
                newRoom.transform.position = new Vector3(row * spriteOffset, column * -1 * spriteOffset);
                minimapRooms[row, column] = newRoom.transform;
                MinimapRoom newRoomScript = newRoom.GetComponent<MinimapRoom>();
                newRoomScript.coordinate = new Vector2Int(row, column);
                /*if(generator.RoomExists(newRoomScript.coordinate))
                {
                    newRoomScript.Discovered = true;
                }*/

                if(row == (minimapRooms.GetLength(0) - 1) / 2 - 1 && column == (minimapRooms.GetLength(0) - 1) / 2 - 1)
                {
                    MinimapCamera.transform.localPosition = new Vector3(newRoom.transform.position.x, newRoom.transform.position.y, newRoom.transform.position.z - 1);
                    newRoomScript.Active = true;
                    currentRoom = newRoomScript;
                }
            }
        }
    }

    public void AllignCameraWithRoom(Vector2Int coordinates)
    {
        Transform roomTransform = Instance.minimapRooms[coordinates.x, coordinates.y];
        MinimapCamera.transform.localPosition = new Vector3(roomTransform.position.x, roomTransform.position.y, roomTransform.position.z - 1);
    }

    public string PrintMinimap()
    {
        string full = "";
        for (int rowIndex = 0; rowIndex < minimapRooms.GetLength(1); rowIndex++)
        {
            string row = "";
            for (int columnIndex = 0; columnIndex < minimapRooms.GetLength(0); columnIndex++)
            {
                if (!minimapRooms[columnIndex, rowIndex].GetComponent<MinimapRoom>().Discovered)
                {
                    row += "X";
                }
                else
                {
                    row += "R";
                }
            }
            full += row + "\n";
        }
        return full;
    }

    public void ResetMinimap()
    {
        for (int row = 0; row < minimapRooms.GetLength(1); row++)
        {
            for (int column = 0; column < minimapRooms.GetLength(0); column++)
            {
                MinimapRoom roomScript = minimapRooms[row, column].GetComponent<MinimapRoom>();

                roomScript.Active = false;
                roomScript.Discovered = false;

                if (row == (minimapRooms.GetLength(0) - 1) / 2 - 1 && column == (minimapRooms.GetLength(1) - 1) / 2 - 1)
                {
                    AllignCameraWithRoom(new Vector2Int(row, column));
                    roomScript.Active = true;
                    currentRoom = roomScript;
                }
            }
        }
        NewDungeon = true;
    }
}
