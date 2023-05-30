using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{

    [SerializeField] private Room[] roomPrefabs;
    [SerializeField] private Room StartRoom;
    [SerializeField] private int countRoom;
    [SerializeField] [Range(3, 15)] private int maxRoomOnX, maxRoomOnY;
    [SerializeField] private float roomWidth, roomHeight;
    [SerializeField] private Transform parentObject;

    private Room[,] map;
    private int countRooms;

    void Start()
    {
        CheckMaxCountRoomsForODD();

        map[maxRoomOnX / 2, maxRoomOnY / 2] = StartRoom;
        for (int i = 0; i < countRoom; i++)
        {
            PlaceOneRoom();
        }
        //StartCoroutine(SpawnRooms());
        //Invoke("Stop", 4f);
    }

    private void CheckMaxCountRoomsForODD()
    {
        maxRoomOnX = maxRoomOnX % 2 == 0 ? maxRoomOnX - 1 : maxRoomOnX;
        maxRoomOnY = maxRoomOnY % 2 == 0 ? maxRoomOnX - 1 : maxRoomOnY;
        map = new Room[maxRoomOnX, maxRoomOnY];
    }
    private void PlaceOneRoom()
    {
        HashSet<Vector2Int> vacantPlace = new HashSet<Vector2Int>();
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == null)
                    continue;

                if ((x > 0) && (map[x - 1, y] == null))
                    vacantPlace.Add(new Vector2Int(x - 1, y));

                if ((x < map.GetLength(0) - 1) && (map[x + 1, y] == null))
                    vacantPlace.Add(new Vector2Int(x + 1, y));

                if ((y > 0) && (map[x, y - 1] == null))
                    vacantPlace.Add(new Vector2Int(x, y - 1));

                if ((y < map.GetLength(1) - 1) && (map[x, y + 1] == null))
                    vacantPlace.Add(new Vector2Int(x, y + 1));
            }
        }
        Room newRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)]);
        newRoom.transform.SetParent(parentObject);
        Debug.Log(vacantPlace.Count);
        int limit = 100;
        while (limit-- > 0)
        {
            newRoom.RotateRoom180();
            Vector2Int pos = vacantPlace.ElementAt(Random.Range(0, vacantPlace.Count));
            Vector3 globalPosition = new Vector3(
                (pos.x - maxRoomOnX / 2) * roomWidth,
                (pos.y - maxRoomOnY / 2) * roomHeight,
                0f);

            if (ConnectRoomToEnother(newRoom, pos))
            {
                newRoom.transform.position = globalPosition;
                map[pos.x, pos.y] = newRoom;
                return;
            }
        }

        Destroy(newRoom.gameObject);
    }


    private bool ConnectRoomToEnother(Room room, Vector2Int position)
    {
        int maxX = map.GetLength(0) - 1;
        int maxY = map.GetLength(1) - 1;

        List<Vector2Int> neighboursRoom = new List<Vector2Int>();

        if ((room.DoorTop != null) && (position.y < maxY) && map[position.x, position.y + 1]?.DoorBottom != null)
            neighboursRoom.Add(Vector2Int.up);

        if ((room.DoorBottom != null) && (position.y > 0) && map[position.x, position.y - 1]?.DoorTop != null)
            neighboursRoom.Add(Vector2Int.down);

        if ((room.DoorRight != null) && (position.x < maxX) && map[position.x + 1, position.y]?.DoorLeft != null)
            neighboursRoom.Add(Vector2Int.right);

        if ((room.DoorLeft != null) && (position.x > 0) && map[position.x - 1, position.y]?.DoorLeft != null)
            neighboursRoom.Add(Vector2Int.left);

        if (neighboursRoom.Count == 0) return false;

        Vector2Int selectDirection = neighboursRoom[Random.Range(0, neighboursRoom.Count)];
        Room selectRoom = map[position.x + selectDirection.x, position.y + selectDirection.y];

        if (selectDirection == Vector2Int.up)
        {
            room.DoorTop.SetActive(false);
            selectRoom.DoorBottom.SetActive(false);
        }
        else if (selectDirection == Vector2Int.down)
        {
            room.DoorBottom.SetActive(false);
            selectRoom.DoorTop.SetActive(false);
        }
        else if (selectDirection == Vector2Int.right)
        {
            room.DoorRight.SetActive(false);
            selectRoom.DoorLeft.SetActive(false);
        }
        else if (selectDirection == Vector2Int.left)
        {
            room.DoorLeft.SetActive(false);
            selectRoom.DoorRight.SetActive(false);
        }
        return true;
    }


    //private void Stop()
    //{
    //    StopCoroutine(SpawnRooms());
    //}

    //private IEnumerator SpawnRooms()
    //{
    //    yield return new WaitForSeconds(3f);
    //    countRooms = Random.Range(0, 4);
    //    List<int> directionRoom = new List<int> { 0, 1, 2, 3 };
    //    for (int i = 0; i < countRooms; i++)
    //    {
    //        int dir = Random.Range(0, directionRoom.Count);

    //        switch (dir)
    //        {
    //            case 0:
    //                Instantiate(roomPrefabs, thisRoom.spawnPoint[0].position, thisRoom.spawnPoint[0].rotation);
    //                break;
    //            case 1:
    //                Instantiate(roomPrefabs, thisRoom.spawnPoint[1].position, thisRoom.spawnPoint[1].rotation);
    //                break;
    //            case 2:
    //                Instantiate(roomPrefabs, thisRoom.spawnPoint[2].position, thisRoom.spawnPoint[2].rotation);
    //                break;
    //            case 3:
    //                Instantiate(roomPrefabs, thisRoom.spawnPoint[3].position, thisRoom.spawnPoint[3].rotation);
    //                break;
    //        }
    //    }
    //    yield return new WaitForSeconds(3f);

    //}
}
