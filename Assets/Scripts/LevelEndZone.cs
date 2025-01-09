using UnityEngine;

public class LevelEndZone : MonoBehaviour
{
    [SerializeField] public GameObject levelEndScreen;
    private bool playerHasEnteredZone = false;
    
    void Start() 
    {
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