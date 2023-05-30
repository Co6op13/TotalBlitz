using UnityEngine;
using UnityEngine.Events;

public static class MyEventManager
{

    public static UnityEvent<int> OnTakeGoalPoints = new UnityEvent<int>();
    public static UnityEvent<Vector2> OnSetTargetForSelectedUnits = new UnityEvent<Vector2>();
    public static void SendPlayertakegoalPoints(int goalPoints)
    {
        OnTakeGoalPoints.Invoke(goalPoints);
    }

    public static void SendSetTargetForSelectedunits( Vector2 position)
    {
        OnSetTargetForSelectedUnits.Invoke(position);
    }
    

}