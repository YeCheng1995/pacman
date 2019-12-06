
using UnityEngine;
using System.Collections;
//Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
//This line should always be present at the top of scripts which use pathfinding
using Pathfinding;
public class AstarAI : MonoBehaviour
{
    //The point to move to
    public Transform target;

    private Seeker seeker;
    private CharacterController controller;

    //The calculated path
    public Path path;

    //The AI's speed per second
    public float speed = 100;

    public float turnSpeed = 10;
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 0.5f;

    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();

        seeker.pathCallback += OnPathComplete;
        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("1 " + p.error);
        if (!p.error)
        {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }
    }

     void OnDisable()
    {
        seeker.pathCallback -= OnPathComplete;
    }
    public void FixedUpdate()
    {
        if (path == null)
        {
            //We have no path to move after yet
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            Debug.Log("End Of Path Reached");
            return;
        }

        //Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        controller.SimpleMove(dir);

        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
}