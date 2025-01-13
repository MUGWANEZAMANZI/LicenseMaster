using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
  [SerializeField] public List<Transform> waypoints;
  [SerializeField] public float waypointProximityDistance = 50f; // Distance to waypoint before deactivated
  private NavMeshAgent agent;
  private int currentWaypoint = 0;
  private float distanceToWaypoint;
  public void Start()
  {
    currentWaypoint = 0;
    agent = gameObject.GetComponent<NavMeshAgent>();
    agent.speed = 35f;
  }

  public void Update()
  {
    distanceToWaypoint = Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position);
    SetWaypoint();
  }

  public void SetWaypoint()
  {
    if (distanceToWaypoint <= waypointProximityDistance) currentWaypoint++;

    if (currentWaypoint == waypoints.Count) currentWaypoint = 0;
    agent.SetDestination(waypoints[currentWaypoint].transform.position);
  }
}