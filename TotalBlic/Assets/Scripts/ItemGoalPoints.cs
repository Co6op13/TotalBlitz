using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGoalPoints : MonoBehaviour
{
    [SerializeField] private int amountGoalpoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MyEventManager.SendPlayertakegoalPoints(amountGoalpoints);
        gameObject.SetActive(false);
    }
}
