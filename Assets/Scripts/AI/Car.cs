using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour
{
    [SerializeField] public List<Transform> waypoints;
    [SerializeField] public float waypointProximityDistance = 50f; // Distance to waypoint before deactivated
    private NavMeshAgent agent;
    private int currentWaypoint = 0;
    public float distanceToWaypoint;

    [SerializeField] private float currentSpeed = 35f;

    private float normalAcceleration = 35; // To store the normal acceleration
    [SerializeField] private float alteredAcceleration = 50f; // Temporary acceleration value
    private bool isAccelerating = false;

    public void Start()
    {
        currentWaypoint = 0;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = currentSpeed;
    }

    public void Update()
    {
        distanceToWaypoint = Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position);
        SetDestinationToCurrentWaypoint();
    }

    public void SetDestinationToCurrentWaypoint()
    {
        if (distanceToWaypoint <= waypointProximityDistance)
        {
            // Change to the altered acceleration for 2 seconds
            if (!isAccelerating)
            {
                StartCoroutine(TemporaryAccelerationChange());
            }

            currentWaypoint++;
        }

        if (currentWaypoint == waypoints.Count) currentWaypoint = 0;
        agent.SetDestination(waypoints[currentWaypoint].transform.position);
    }

    private IEnumerator TemporaryAccelerationChange()
    {
        isAccelerating = true;
        agent.acceleration = alteredAcceleration; // Set the temporary acceleration

        // Wait for 2 seconds
        yield return new WaitForSeconds(0.75f);

        agent.acceleration = normalAcceleration; // Reset to normal acceleration
        isAccelerating = false;
    }
}
