using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolationManager : MonoBehaviour
{
    [Header("ViolationManager")]
    public CarController carController;
    public PlayerDetected detected;
    public GameObject[] speedLmitDetectors;
    public bool violatedInViolationManager = false;
    public int punishedWith = 0;
    public UIManager uiManager; //UIManager instance
    public Finance finance; //Fiunanace Class instance
    // Start is called before the first frame update
    void Start()
    {
        carController = FindObjectOfType<CarController>();
        UIManager uiManager = FindObjectOfType<UIManager>();
        Finance finance = FindObjectOfType<Finance>();
    }

    // Update is called once per frame
    void Update()
    {
        if(carController != null)
        {
            if (PlayerDetected.isDetected) {
                SpeedLimitViolation();
                   
            }

        }
        else
        {
            Debug.Log("Car Controller Script is missing"); 
        }
        //Debug.Log(violatedInViolationManager);
    }

    public void SpeedLimitViolation()
    {
        float currentSpeed = CarController.speed;
        //Debug.Log(currentSpeed);
        string violated = (PlayerDetected.detectedTag);
        
        if( currentSpeed > 60f && violated == "60")
        {
            violatedInViolationManager= true;
           
        }
        if (violatedInViolationManager)
        {
            violatedInViolationManager = false;
            punishedWith = finance.Violation("Speeding");
            uiManager.SpeedLimit(currentSpeed, punishedWith);
            
        }

    }
    

}
