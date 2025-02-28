using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController1 : MonoBehaviour
{
    public WheelCollider[] wheels;
    public float maxTorque = 200f;
    public float maxSteer = 20f;
    public float brakeTorque = 2000f;
    private float verticalInput;
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getInputs();

        if (wheels == null || wheels.Length == 0)
        {
            Debug.LogError("Wheels array is empty! Assign WheelColliders in the Inspector.");
            return;
        }

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].motorTorque = maxTorque * verticalInput;
            wheels[i].steerAngle = maxSteer * horizontalInput;
            if (Input.GetKey(KeyCode.Space))
            {
                wheels[i].brakeTorque = brakeTorque;
            }
            else
            {
                wheels[i].brakeTorque = 0;
            }
        }


       
    }
    void getInputs()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }
}

