using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] public Transform[] SpawnPoint;
    [SerializeField] public GameObject DoorTop;
    [SerializeField] public GameObject DoorRight;
    [SerializeField] public GameObject DoorBottom;
    [SerializeField] public GameObject DoorLeft;


    public void RotateRoom180()
    {
        transform.Rotate(0, 0, 180);

        GameObject tmp = DoorTop;
        DoorTop = DoorBottom;
        DoorBottom = tmp;
        tmp = DoorLeft;
        DoorLeft = DoorRight;
        DoorRight = tmp;
    }
}
