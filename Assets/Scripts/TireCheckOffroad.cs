using Unity.VisualScripting;
using UnityEngine;

public class TireCheckOffroad : MonoBehaviour
{

    private CarController player;

    void Awake()
    {
        // This is probably bad? But I didnt want to deal with setting the reference manualy.
        player = transform.parent.gameObject
                .transform.parent.gameObject
                .transform.parent.gameObject.GetComponent<CarController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // if (name == "FrontRightWheel")
        // {
            Debug.Log(name + "Is colliding with - " + collision.gameObject.tag);
        // }
        if (collision.gameObject.tag != "Terrain")
        {
            if (player.wheelsOffRoad.Exists(wheel => wheel == name)) player.wheelsOffRoad.Remove(name);
            return;
        }
        Debug.Log(name + " tire is offroad!");
        if (!player.wheelsOffRoad.Exists(wheel => wheel == name)) player.wheelsOffRoad.Add(name);
    }
}