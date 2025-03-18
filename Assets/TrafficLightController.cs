
using System.Collections;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public Light redLight;
    public Light yellowLight;
    public Light greenLight;

    public float redTime = 5f;
    public float yellowTime = 2f;
    public float greenTime = 5f;

    void Start()
    {
        StartCoroutine(TrafficLightSequence());
    }

    IEnumerator TrafficLightSequence()
    {
        while (true)
        {
            // Red light on
            SetLightState(true, false, false);
            yield return new WaitForSeconds(redTime);

            // Green light on
            SetLightState(false, false, true);
            yield return new WaitForSeconds(greenTime);

            // Yellow light on
            SetLightState(false, true, false);
            yield return new WaitForSeconds(yellowTime);
        }
    }

    void SetLightState(bool red, bool yellow, bool green)
    {
        redLight.enabled = red;
        yellowLight.enabled = yellow;
        greenLight.enabled = green;
    }
}
