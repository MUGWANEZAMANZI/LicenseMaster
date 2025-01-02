using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
   // [SerializeField] Transform[] waypoints;
  //  [SerializeField] private int destinationPoint;
  //  public GameObject stop1;
  // public GameObject stop2;
    bool _continue=true;
    private float timer = 12f;
    public void Start()
    {
        // stop1.SetActive(true);
        // stop2.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        GetComponent<NavMeshAgent>().speed = 15f;
    }
/*(    }
public void GoToNextPoint()
    {
        if (waypoints.Length == 0)
            return;

        var NextPoint = Random.Range(0, waypoints.Length);
        destinationPoint = (destinationPoint + NextPoint) % waypoints.Length;
    }
*/
 /*  public void OnTriggerEnter(Collider other)
    {
        stop2.SetActive(true);
       // if (other.gameObject.CompareTag("Player")) {
           _continue = false;
         
       //   agent.transform.position = agent.transform.position;
       // }
    }
 */
    public void Update()
    {
        if (_continue)
        {
            agent.SetDestination(player.position);
           // stop1.SetActive(false);
        }
        timer -= Time.deltaTime;
        if (timer <=0 && !_continue)
        {
           // stop2.SetActive(false);
            agent.SetDestination(player.position);
        }
      
    }
}