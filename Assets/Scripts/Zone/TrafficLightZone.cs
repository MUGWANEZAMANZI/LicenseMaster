using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightZone : MonoBehaviour
{
    // Enum to represent the three states
    private enum TrafficLightState
    {
        Red,
        Yellow,
        Green
    }

    // Current state of the traffic light
    [Header("States")]
    [SerializeField] private TrafficLightState currentState;

    // Timers for each light
    [Header("Timer")]
    [SerializeField] private float timer;

    // Time durations for each light state
    [Header("LightDuration")]
    [SerializeField] private const float redDuration = 10f;
    [SerializeField] private const float yellowDuration = 5f;
    [SerializeField] private const float greenDuration = 15f;

    // UI or material colors for the traffic light (Optional, I just put it here incase for now.)
    [Header("Materials")]
    public Renderer redLight;
    public Renderer yellowLight;
    public Renderer greenLight;

    void Start()
    {
        // Initialize with red light and reset timer
        currentState = TrafficLightState.Red;
        timer = redDuration;
        SetTrafficLightStateMaterial();
    }

    void Update()
    {
        // Count down the timer
        timer -= Time.deltaTime;

        // If the timer reaches zero, switch to the next state
        if (timer <= 0f)
        {
            SwitchState();
            SetTrafficLightStateMaterial();
        }
    }

    // Switch between the traffic light states
    private void SwitchState()
    {
        switch (currentState)
        {
            case TrafficLightState.Red:
                currentState = TrafficLightState.Yellow;
                timer = yellowDuration;
                break;
            case TrafficLightState.Yellow:
                currentState = TrafficLightState.Green;
                timer = greenDuration;
                break;
            case TrafficLightState.Green:
                currentState = TrafficLightState.Red;
                timer = redDuration;
                break;
        }
    }

    // Set the corresponding traffic light state colors
    private void SetTrafficLightStateMaterial()
    {
        //// Turn off all lights first
        //redLight.material.color = Color.black;
        //yellowLight.material.color = Color.black;
        //greenLight.material.color = Color.black;

        //// Turn on the current light based on the state
        //switch (currentState)
        //{
        //    case TrafficLightState.Red:
        //        redLight.material.color = Color.red;
        //        break;
        //    case TrafficLightState.Yellow:
        //        yellowLight.material.color = Color.yellow;
        //        break;
        //    case TrafficLightState.Green:
        //        greenLight.material.color = Color.green;
        //        break;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (currentState== TrafficLightState.Red)
            {
                LevelManager.instance.RedLightViolation();
            }
        }
    }
}
