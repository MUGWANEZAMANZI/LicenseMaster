using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolationManager : MonoBehaviour
{
    [Header("ViolationManager")]
    public CarController carController;
    public Detected detected;
    public GameObject[] speedLmitDetectors;
    public static bool violatedInViolationManager = false;
    public static int punishedWith = 0;
    // Start is called before the first frame update
    void Start()
    {
        carController = FindObjectOfType<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(carController != null)
        {
            if (Detected.isDetected) SpeedLimitViolation();
        }
        else
        {
            Debug.Log("Car Controller Script is missing"); 
        }
        //Debug.Log(violatedInViolationManager);
    }

    public static void SpeedLimitViolation()
    {
        float currentSpeed = CarController.speed;
        string violated = (Detected.detectedTag);
        
        if( currentSpeed > 60f && violated == "60")
        {
            violatedInViolationManager= true;
           
        }
        if (violatedInViolationManager)
        {
            punishedWith = Finance.Violation("Speeding");
            UIManager.SpeedLimit(currentSpeed, punishedWith);
        }

    }
    

}
