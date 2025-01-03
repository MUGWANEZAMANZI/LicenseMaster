using UnityEngine;

public class CollisionViolation : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelManager.instance.CollisionViolation();
        }
    }
}