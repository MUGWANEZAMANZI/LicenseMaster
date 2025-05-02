using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDetectObject : MonoBehaviour
{
    //REMEMBER to put two trigger box, one in front and other behind the ai car.
    //Make sure other objects have box collider trigger on in order this to work.
    //Change box collider value depend on how much responsinse you want to car to have. (i.e: make the box small if you want to car to start stopping when too close to other).
    [SerializeField] AiCar aiCarScript;
    [SerializeField] TrafficLightController trafficLightControllerScript;

    private void OnTriggerEnter(Collider other)
    {
        //Use this to detect any object within in box. Make sure the triggerd BOX COLLIDERS above the ground.
        Debug.Log("Something has entered the BoxCollider!");

        //Use this to detect any object with Car tag
        if (other.gameObject.tag == "AiCar")
        {
            Debug.Log("CAR!");
            aiCarScript.hasStopped = true;
        }
        else if (other.gameObject.tag == "StopSign")
        {
            if (trafficLightControllerScript.redLight)
            {
                Debug.Log("StopSign!");
                aiCarScript.hasStopped = true;
            }
            else if (trafficLightControllerScript.greenLight)
            {
                aiCarScript.hasStopped = false;
            }
        }
    }
}
