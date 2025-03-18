using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AICarController : MonoBehaviour
{
    public Transform[] waypoints; // Set waypoints in the Inspector
    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    private bool isParking = false;

    public float parkingTime = 3f; // Time spent parking

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToNextWaypoint();
    }

    void Update()
    {
        if (!isParking && !agent.pathPending && agent.remainingDistance < 1f)
        {
            StartCoroutine(ParkAndMove());
        }
    }

    IEnumerator ParkAndMove()
    {
        isParking = true;
        agent.isStopped = true; // Stop the car
        yield return new WaitForSeconds(parkingTime); // Wait
        agent.isStopped = false; // Resume movement
        isParking = false;
        MoveToNextWaypoint();
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        agent.destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}
