using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AccessoryMetods
{
    public static bool CheckVisible(Transform from, Transform target, float distance, LayerMask mask)
    {
        bool result = false;
        RaycastHit2D hit = Physics2D.Linecast(from.position, target.position, mask);
        if (hit.collider != null && Vector2.Distance(from.position, target.position) < distance)
        {
            if (hit.collider.gameObject == target.gameObject)
            {

                Debug.DrawLine(from.position, target.position);
                result = true;
            }
        }
        return result;
    }

    public static bool CheckVisible(Transform from, Transform target, LayerMask mask)
    {
        bool result = false;
        RaycastHit2D hit = Physics2D.Linecast(from.position, target.position, mask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject == target.gameObject)
            {

                Debug.DrawLine(from.position, target.position);
                result = true;
            }
        }
        return result;
    }

    public static bool CheckVisible(Vector2 from, Vector2 target, LayerMask mask)
    {
        bool result = false;
        RaycastHit2D hit = Physics2D.Linecast(from, target, mask);
        if (hit.collider != null)
        {
            Debug.DrawLine(from, target);
            result = true;
        }
        return result;
    }

    public static bool CheckVisible(Vector2 from, Vector2 target, LayerMask mask, out GameObject gameObject)
    {
        gameObject = null;
        bool result = false;
        RaycastHit2D hit = Physics2D.Linecast(from, target, mask);
        if (hit.collider != null)
        {
            Debug.DrawLine(from, target);
            result = true;
            gameObject = hit.collider.gameObject;
        }
        return result;
    }

    public static bool CheckObstacle(Vector2 from, Vector2 direction, float distance, LayerMask mask)
    {        
        bool result = false;
        RaycastHit2D hit = Physics2D.Raycast(from, direction, distance, mask);  //from, target, mask);
        if (hit.collider != null)
        {
            Debug.DrawRay(from, direction * direction);
            result = true;      
        }
        return result;
    }

    public static bool CheckDistance(Vector2 from, Vector2 target, LayerMask mask, out float distance, out Vector2 colliderPosition)
    {
        bool result = false;
        RaycastHit2D hit = Physics2D.Linecast(from, target, mask);
        colliderPosition = target;
        distance = Vector2.Distance(from, target); ;
        if (hit.collider != null)
        {
            // Debug.DrawLine(from, target);
            result = true;
            colliderPosition = hit.point;
            distance = Vector2.Distance(from, hit.point);
        }
        return result;
    }

    public static float GetAngleFromVectorFloat(Vector3 direction)
    {
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;

        return angle;
    }
    public static Vector3 GetVectorFromAngle(float angle)
    {
        // angle = 0 -> 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }


    public static int GetAngleFromVectorInt(Vector3 direction)
    {
        direction = direction.normalized;
        int angle = (int)(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        if (angle < 0) angle += 360;

        return angle;
    }

    public static Vector2 GetRandomPositionWithCheckVisible(Vector2 from, float searchRadius, LayerMask obstacle)
    {
        Vector2 newPosition = from;
        bool p = false;
        while (!p)
        {
            newPosition = from + new Vector2(Random.Range(-searchRadius, searchRadius), Random.Range(-searchRadius, searchRadius));
            if (!AccessoryMetods.CheckVisible(from, newPosition, obstacle))
            {
                p = true;
                break;
            }

        }
        return newPosition;
    }

    public static Vector2 GetPositionAvoidLightWithCheckVisible(Vector2 from, Vector2 directionToLight, LayerMask obstacle)
    {
        Vector2 newPosition = from;
        for (int i = 0; i < 3; i++)
        {
            //int d = Random.Range()
            //newPosition = Vector2.Perpendicular(directionToLight);
        }
        return newPosition - directionToLight * 3;
    }

}

