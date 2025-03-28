
using System.Collections;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    // Current state of the traffic light
    [Header("States")]
    [SerializeField] private TrafficLightState currentState;

    // UI or material colors for the traffic light.
    [Header("Lights")]
    public Light redLight;
    public Light yellowLight;
    public Light greenLight;

    // Timers for each light
    [Header("Timer")]
    [SerializeField] private float timer;
    [Header("LightDuration")]
    public float redTime = 5f;
    public float yellowTime = 2f;
    public float greenTime = 10f;

    // Enum to represent the three states
    private enum TrafficLightState
    {
        Red,
        Yellow,
        Green
    }


    void Start()
    {
        // Initialize with red light and reset timer
        currentState = TrafficLightState.Red;
        timer = redTime;
    }

    private void Update()
    {
        // Count down the timer
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SwitchState();
        }
    }

    private void SwitchState()
    {
        switch (currentState)
        {
            case TrafficLightState.Red:
                currentState = TrafficLightState.Yellow;
                timer = yellowTime;
                break;
            case TrafficLightState.Yellow:
                currentState = TrafficLightState.Green;
                timer = greenTime;
                break;
            case TrafficLightState.Green:
                currentState = TrafficLightState.Red;
                timer = redTime;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (currentState == TrafficLightState.Red)
            {
                LevelManager.instance.RedLightViolation();
            }
        }
    }
}
