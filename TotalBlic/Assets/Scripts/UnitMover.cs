using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private Seeker seeker;
    [SerializeField] private Transform target;
    [SerializeField] private float nextWaipointDistance;
    [SerializeField] private float delayUpdatepath;
    [SerializeField] private float moveTime = 0.5f;
    [SerializeField] private Path path;
    [SerializeField] private int currentWaypoint = 0;
    [SerializeField] private bool isGoalReached = false;
    [SerializeField] private bool isCanMove = true;
    [SerializeField] private float distanceToObstacle;
    [SerializeField] private LayerMask enotherUnit;
    [SerializeField] private float coefficientMoveDawn, coefficientMoveHorizontal;
    Vector3 startPosition;
    Vector3 nextPosition;
    float targetTime;
    [SerializeField] private List<Vector3> test;
    private float currentMoveTime;

    private void OnEnable()
    {
        MyEventManager.OnSetTargetForSelectedUnits.AddListener(SetTarget); ;
    }
    void Start()
    {
        seeker = GetComponent<Seeker>();
        nextPosition = transform.position;
        UpdatePath();
        //InvokeRepeating("UpdatePath", 0f, delayUpdatepath);
    }


    private void SetTarget(Vector2 target)
    {
        this.target.position = RoundingPosition(target);
        this.target.gameObject.SetActive(true);
        isCanMove = true;
        UpdatePath();
    }
    void UpdatePath()
    {
        if (target != null && seeker.IsDone())
        {
            seeker.StartPath(RoundingPosition(transform.position), target.position, OnPathComplite);
        }
    }

    void OnPathComplite(Path path)
    {
        if (!path.error)
        {
            this.path = path;
            currentWaypoint = 0;
        }
    }

    void SetPositionToMove(Vector3 move)
    {
        currentMoveTime = moveTime;
        if (move == Vector3.down)
        {
            currentMoveTime *= coefficientMoveDawn;
        }
        if (move.x != 0)
        {
            currentMoveTime *= coefficientMoveHorizontal;
        }
        targetTime = Time.time + currentMoveTime;
        startPosition = RoundingPosition(transform.position);
        nextPosition = startPosition + move;
        if (!(startPosition.x == nextPosition.x || startPosition.y == nextPosition.y))
        { 
            //Debug.Log("jopa ----------------------------------" + transform.position+ " " + startPosition + "  " + nextPosition + "  " + RoundingPosition(nextPosition) +  "   " + currentWaypoint);
            int p = UnityEngine.Random.Range(0, 2);
            if (p == 0)
                nextPosition.x = startPosition.x;
            else
                nextPosition.y = startPosition.y;
          //  Debug.Log("jopa ----------------------------------" + transform.position + " " + startPosition + "  " + nextPosition + "  " + RoundingPosition(nextPosition) + "   " + currentWaypoint);
           // Debug.Break();
        }

        nextPosition = RoundingPosition(nextPosition);
    }

    private Vector2 RoundingPosition(Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }

    public void SetTarget(Vector3 target)
    {
        
        this.target.position = target;

    }

    private void Move()
    {
        transform.position = Vector3.Lerp(startPosition, nextPosition, 1 - (targetTime - Time.time) / currentMoveTime);
    }

    private void Update()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
            return;

        //if (path.vectorPath.Count <= 2)
        //    isCanMove = false;
        test = path.vectorPath;
        if (Vector2.Distance(transform.position, nextPosition) < nextWaipointDistance && (path.vectorPath.Count > 2)) //(Vector2.Distance(transform.position, nextPosition) < nextWaipointDistance &&
        {
            Debug.Log(path.vectorPath.Count + "  " + currentWaypoint);
            transform.position = nextPosition;
            GetNextWaypoint();
            var dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            SetPositionToMove(dir);
        }
    }

    private void FixedUpdate()
    {
        if (targetTime > Time.time)
        {
            Move();
        }
    }

    private bool CheckObstacle(Vector2 direction)
    {
        Debug.Log("checkObstacle");
        return AccessoryMetods.CheckObstacle(transform.position, direction, distanceToObstacle, enotherUnit);
    }

    private void GetNextWaypoint()
    {
        float distance = Vector2.Distance(transform.position, nextPosition);
        if (distance < nextWaipointDistance && path.vectorPath.Count - 1 > currentWaypoint)
        {
            Debug.Log(distance);
            currentWaypoint++;
        }
    }
}