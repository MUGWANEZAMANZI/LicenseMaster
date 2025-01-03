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

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Road")
        {
            player.wheelsOffRoad.RemoveAll(wheel => wheel == this.name);
            return;
        }
        Debug.Log(name + " is offroad!");
        if (!player.wheelsOffRoad.Exists(wheel => wheel == name)) player.wheelsOffRoad.Add(name);
    }
}