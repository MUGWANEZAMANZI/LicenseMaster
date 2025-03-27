using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    public Button message;
    public GameObject viewMessage,pauseObject, PauseWindowObject;
    public GameObject needle, steeringWheel;
    public float maxRotation = 45f;
    public float rotationSpeed = 100f;
    public static string m;


    [Header("Speed Lmit")]
    public static GameObject messagePrefab;
    public static Transform contentPanel;
    // Start is called before the first frame update
    void Start()
    {
        viewMessage.SetActive(false);
        steeringWheel.transform.localEulerAngles = new Vector3(0, 0, 0);
        needle.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        m = "kk";//messagePrefab.GetComponent<TextMeshProUGUI>().text = message;;;;
        Debug.Log(m);   

        if (viewMessage.activeSelf && Input.anyKeyDown)
        {
            Close();
        }
            SteeringWheelRotation();
        
    }

    //Animating the steering Wheel
    public void SteeringWheelRotation() { 
        float horizontal = Input.GetAxis("Vertical");
        float vertical = Input.GetAxis("Horizontal");
        float targetRotation = -horizontal * maxRotation;
        float targetRotationSpeed = -vertical * maxRotation;
        float currentRotation = steeringWheel.transform.localEulerAngles.z; // Access transform
        float currentRotationNeedle = needle.transform.localEulerAngles.z; // Access transform
        float newRotation = Mathf.MoveTowardsAngle(currentRotation, targetRotationSpeed, rotationSpeed * Time.deltaTime);
        float newRotationNeedle = Mathf.MoveTowardsAngle(currentRotationNeedle, targetRotation, rotationSpeed * Time.deltaTime);

        steeringWheel.transform.localEulerAngles = new Vector3(0 ,0, newRotation);
        needle.transform.localEulerAngles = new Vector3(0, 0, newRotationNeedle);
    }

    public void ViewMessages()
    {
        viewMessage.SetActive(true);
       
    }
    public void Close()
    {
        if (Input.anyKeyDown)
        {
            viewMessage.SetActive(false);
        }
    }
        public void Pause()
    {
        pauseObject.SetActive(false);
        PauseWindowObject.SetActive(true);
    }

    public static void SpeedLimit(float speedYouHave, float fine)
    {
        string message= $"🚨 Speeding Alert!\n" +
                         $"Driver: Vietkong\n" +
                         $"Speed: {speedYouHave} km/h\n" +
                         $"Fine: ${fine}\n" +
                         $"Pay within 10 minutes!";
        //viewMessage.GetCompenent<TextMeshProUGUI>().text = message;
        //GameObject newMessage = Instantiate(messagePrefab, contentPanel);
        //newMessage.GetComponent<TextMeshProUGUI>().text = message;

    }

}
