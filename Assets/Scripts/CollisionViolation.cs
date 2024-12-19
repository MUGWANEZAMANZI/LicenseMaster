using UnityEngine;

public class CollisionViolation : MonoBehaviour
{

    private CarController player;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelManager.instance.CollisionViolation();
        }
    }
}