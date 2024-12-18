using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    [SerializeField] Transform[] waypoints;
    [SerializeField] private int destinationPoint;
    bool _continue=true;
  public  void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GetComponent<NavMeshAgent>().speed = 15f;
    }
public void GoToNextPoint()
    {
        if (waypoints.Length == 0)
            return;

        var NextPoint = Random.Range(0, waypoints.Length);
        destinationPoint = (destinationPoint + NextPoint) % waypoints.Length;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("other car")) {
            _continue = false;
          agent.transform.position = agent.transform.position;
        }
    }
    public void Update()
    {
        if (_continue) 
        agent.destination=(player.position);
      
    }
}