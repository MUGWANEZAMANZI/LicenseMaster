using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDetectObject : MonoBehaviour
{
    //REMEMBER to put two trigger box, one in front and other behind the ai car.
    //Make sure other objects have box collider trigger on in order this to work.
    //Change box collider value depend on how much responsinse you want to car to have. (i.e: make the box small if you want to car to start stopping when too close to other).
    [SerializeField] AiCar aiCarScript;

    private void OnTriggerEnter(Collider other)
    {
        //Once it detect another trigger box. Slow AiCar down.
        Debug.Log("Something has entered the BoxCollider!");
    }
}
