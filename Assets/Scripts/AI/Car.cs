using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour
{
    [SerializeField] public List<Transform> waypoints;
    [SerializeField] public float waypointProximityDistance = 50f;
    private NavMeshAgent agent;
    private int currentWaypoint = 0;
    public float distanceToWaypoint;

    [SerializeField] private float setSpeed = 35f;
    private float normalAcceleration = 35;
    [SerializeField] private float alteredAcceleration = 50f;
    private bool isAccelerating = false;

    public float decelerationRate = 0.5f;
    private float currentSpeed; //This is to store the AI current speed.
    public bool hasStopped = false;
    public float parkingTime = 3f; // Time spent parking

    private void Start()
    {
        currentWaypoint = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = setSpeed;
        currentSpeed = agent.speed;
    }

    private void Update()
    {

        if (!hasStopped)
        {
            StartCoroutine(AccelerateToSpeed());
            distanceToWaypoint = Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position);
            SetDestinationToCurrentWaypoint();
        }
        else if (hasStopped)
        {
            StartCoroutine(DecelerateToStop());
            Debug.Log("STOP AI!");
        }
    }

    private void SetDestinationToCurrentWaypoint()
    {
        if (distanceToWaypoint <= waypointProximityDistance)
        {
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
        agent.acceleration = alteredAcceleration;
        yield return new WaitForSeconds(0.75f);
        agent.acceleration = normalAcceleration;
        isAccelerating = false;
    }

    private IEnumerator AccelerateToSpeed()
    {
        while (agent.speed < currentSpeed)
        {
            agent.speed += decelerationRate * Time.deltaTime;
            yield return null;
        }
        agent.speed = currentSpeed;
    }

    private IEnumerator DecelerateToStop()
    {
        while (agent.speed > 0.1f)
        {
            agent.speed -= decelerationRate * Time.deltaTime;
            yield return null;
        }
        agent.speed = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StopSign")
        {
            Debug.Log("STOP SIGN FOR AI");
            StartCoroutine(StopSigh());
        }
        else if (other.gameObject.tag == "AiParkZone")
        {
            Debug.Log("Parking FOR AI");
            StartCoroutine(Parking());
        }
    }

    private IEnumerator StopSigh()
    {
        hasStopped = true;
        yield return new WaitForSeconds(parkingTime); // Wait
        hasStopped = false;
    }

    private IEnumerator Parking()
    {
        // Check if the GameObject is close enough to the target
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) <= distanceToWaypoint)
        {
            // Stop and face the target
            //transform.position = waypoints[currentWaypoint].position;
            transform.LookAt(waypoints[currentWaypoint]); // Make the GameObject face the target
            hasStopped= true;
            yield return new WaitForSeconds(parkingTime); // Wait
            hasStopped= false;
        }
    }
}
