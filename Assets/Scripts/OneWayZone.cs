using UnityEngine;

public class OneWayZone : MonoBehaviour
{
    [SerializeField] public CarController player;
    private bool playerEnteredZone = true;

    void Update()
    {
        DrawArrow.ForDebug(transform.position, transform.forward * 30, Color.red, 5);
        if (!playerEnteredZone) return;
        if (Vector3.Dot(player.GetComponent<Rigidbody>().velocity.normalized, Vector3.forward) > .5f)
        {
            player.wrongWay = true;
        } else {
            player.wrongWay = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerEnteredZone = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        player.wrongWay = false;
    }
}