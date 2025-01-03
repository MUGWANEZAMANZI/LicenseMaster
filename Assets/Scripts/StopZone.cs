using UnityEngine;

public class StopZone : MonoBehaviour
{
    private bool playerHasStopped = false;
    private bool playerHasEnteredStopZone = true;

    void Update()
    {
        if ( !playerHasEnteredStopZone ) return;

        if (CarController.speed <= .5 ) playerHasStopped = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerHasStopped = false;
            playerHasEnteredStopZone = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (!playerHasStopped) LevelManager.instance.StopZoneViolation();
    }


}