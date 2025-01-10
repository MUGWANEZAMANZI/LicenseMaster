using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerControls;
    [Header("Controls")]
    [SerializeField] private string left = "TurnLeft";
    [SerializeField] private string right = "TurnRight";
    [SerializeField] private string throttle = "Throttle";
    [SerializeField] private string reverse = "Reverse";
    [SerializeField] private string handbreak = "Handbreak";
    [SerializeField] private string radio = "Radio";
    [SerializeField] private string leftBlinker = "LeftBlinker";
    [SerializeField] private string rightBlinker = "RightBlinker";
    private InputAction turnLeftAction;
    private InputAction turnRightAction;
    private InputAction throttleAction;
    private InputAction reverseAction;
    private InputAction handbreakAction;
    private InputAction radioToggleAction;
    private InputAction leftBlinkerAction;
    private InputAction rightBlinkerAction;

    public bool TurnLeftInput { get; private set; }
    public bool TurnRightInput { get; private set; }
    public bool ThrottleInput { get; private set; }
    public bool ReverseInput { get; private set; }
    public bool HandbrakeInput { get; private set; }
    public bool RadioInput { get; private set; }
    public bool LeftBlinkerInput { get; private set; }
    public bool RightBlinkerInput { get; private set; }


    public static PlayerControls instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        turnLeftAction = playerControls.FindActionMap("Default").FindAction(left);
        turnRightAction = playerControls.FindActionMap("Default").FindAction(right);
        throttleAction = playerControls.FindActionMap("Default").FindAction(throttle);
        reverseAction = playerControls.FindActionMap("Default").FindAction(reverse);
        handbreakAction = playerControls.FindActionMap("Default").FindAction(handbreak);
        radioToggleAction = playerControls.FindActionMap("Default").FindAction(radio);
        leftBlinkerAction = playerControls.FindActionMap("Default").FindAction(leftBlinker);
        rightBlinkerAction = playerControls.FindActionMap("Default").FindAction(rightBlinker);

        RegisterInputActions();
    }

    private void RegisterInputActions()
    {
        turnLeftAction.performed += context => TurnLeftInput = true;
        turnLeftAction.canceled += context => TurnLeftInput = false;

        turnRightAction.performed += context => TurnRightInput = true;
        turnRightAction.canceled += context => TurnRightInput = false;

        throttleAction.performed += context => ThrottleInput = true;
        throttleAction.canceled += context => ThrottleInput = false;

        reverseAction.performed += context => ReverseInput = true;
        reverseAction.canceled += context => ReverseInput = false;

        handbreakAction.performed += context => HandbrakeInput = true;
        handbreakAction.canceled += context => HandbrakeInput = false;

        radioToggleAction.performed += context => RadioInput = true;
        radioToggleAction.canceled += context => RadioInput = false;

        leftBlinkerAction.performed += context => LeftBlinkerInput = true;
        leftBlinkerAction.canceled += context => LeftBlinkerInput = false;

        rightBlinkerAction.performed += context => RightBlinkerInput = true;
        rightBlinkerAction.canceled += context => RightBlinkerInput = false;
    }

    private void OnEnable()
    {
        turnLeftAction.Enable();
        turnRightAction.Enable();
        throttleAction.Enable();
        reverseAction.Enable();
        handbreakAction.Enable();
        radioToggleAction.Enable();
        leftBlinkerAction.Enable();
        rightBlinkerAction.Enable();
    }
    private void OnDisable()
    {
        turnLeftAction.Disable();
        turnRightAction.Disable();
        throttleAction.Disable();
        reverseAction.Disable();
        handbreakAction.Disable();
        radioToggleAction.Disable();
        leftBlinkerAction.Disable();
        rightBlinkerAction.Disable();
    }

}