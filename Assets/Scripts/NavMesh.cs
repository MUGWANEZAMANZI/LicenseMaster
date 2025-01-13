using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class NavMesh : MonoBehaviour
{
 //   public List<GameObject> productionQueue = new List<GameObject>();
   // public Transform target;
    public GameObject [] wpt;
    private NavMeshAgent agent;
  
    bool _continue=false;
    int i =0;
    public GameObject Leadcar;
    private float timer = 12f;
    public   float distStop=50f;
  
   public float distance;
    public void Start()
    {
         i=0;
   //  foreach (GameObject wpt in productionQueue)
   wpt[1].SetActive(false);
   wpt[2].SetActive(false);
   wpt[3].SetActive(false);
    wpt[4].SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        agent.GetComponent<NavMeshAgent>().speed = 35f;
    }
    
public void GoToNext()
{
//foreach (GameObject wpt in productionQueue)
//{
distance = Vector3.Distance(wpt[i].transform.position,Leadcar.transform.position);               
if(distance <= distStop){
 wpt[i].gameObject.SetActive(false);

 i++;
   }


   
if(i==wpt.Length-1)
i=0;
//wpt[i].SetActive(true);
agent.SetDestination(wpt[i].transform.position);
/*if(i==4&&distance <= 250)
  { 
    wpt[1].SetActive(true);
   wpt[2].SetActive(false);
   wpt[3].SetActive(false);
    wpt[4].SetActive(false);
     _continue=false;
  }
  */
}

 
    public void Update()
    {
     
          GoToNext();
      
    }
}