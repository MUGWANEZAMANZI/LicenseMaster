using UnityEngine;

public class LevelEndZone : MonoBehaviour
{
    private GameObject levelEndScreen;
    private bool playerHasEnteredZone = false;
    
    void Start() 
    {
        levelEndScreen = GameObject.Find("LevelCompleteDialog");
    }

    void Update()
    {
        if (playerHasEnteredZone && Mathf.Abs(CarController.speed) < .5) 
        {
            levelEndScreen.SetActive(true);
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