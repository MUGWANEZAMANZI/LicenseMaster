using UnityEngine;

public class ParkingZone : MonoBehaviour
{
    private bool playerHasEnteredZone = false;

    void Update()
    {
        if (playerHasEnteredZone && Mathf.Abs(CarController.speed) < .5) 
        {
            LevelManager.instance.CompleteObjective();
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerHasEnteredZone = true;
        }
    }
}