using System.Collections.Generic;
using UnityEngine;

public class StopZone : MonoBehaviour
{
    private bool playerHasStopped = false;
    private bool playerHasEnteredZone = false;

    // List of AI Car to track (Assign these in the Inspector)
    [SerializeField]private List<GameObject> aiCar = new List<GameObject>();

    // List to track AI  currently inside the trigger
    private List<GameObject> objectsInTrigger = new List<GameObject>();

    void Update()
    {
        if ( !playerHasEnteredZone ) return;

        if (CarController.speed <= .5 ) playerHasStopped = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerHasStopped = false;
            playerHasEnteredZone = true;
        }
        // Check if the AI  has the correct tag
        else if (aiCar.Contains(collider.gameObject))
        {
            // Add to the list if it's not already there
            if (!objectsInTrigger.Contains(collider.gameObject))
            {
                objectsInTrigger.Add(collider.gameObject);
                SetObjectActive(collider.gameObject, true);  // Activate the boolean
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (!playerHasStopped) LevelManager.instance.StopZoneViolation();
        objectsInTrigger.Remove(collider.gameObject);
        SetObjectActive(collider.gameObject, false);
    }

    private void SetObjectActive(GameObject obj, bool state)
    {
        // Try to get the script and set the boolean
        Car script = obj.GetComponent<Car>();
        if (script != null)
        {
            script.hasStopped = state;
        }
    }
}