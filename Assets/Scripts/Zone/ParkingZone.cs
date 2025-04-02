using UnityEngine;

public class ParkingZone : MonoBehaviour
{
    private bool playerHasEnteredZone = false;

    void Update()
    {
        //Debug.Log(playerHasEnteredZone);
        if (playerHasEnteredZone && Mathf.Abs(CarController.speed) < .5) 
        {
            if (LevelManager.instance.isInfinite == false)
            {
                LevelManager.instance.CompleteObjective();
                gameObject.SetActive(false);
            }
            else
            {
                LevelManager.instance.EndlessObjective();
                gameObject.SetActive(false);
            }
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