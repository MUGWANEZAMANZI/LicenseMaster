using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

  [Header("Car Params")]
  [Range(20, 190)]
  public int maxSpeed = 90;
  [Range(10, 120)]
  public int maxReverseSpeed = 45;
  [Range(1, 10)]
  public int accelerationRate = 2;
  [Range(10, 45)]
  public int maxSteeringAngle = 27; // The maximum angle that the tires can reach while rotating the steering wheel.
  [Range(0.1f, 1f)]
  public float steeringSpeed = 0.5f;
  [Range(100, 600)]
  public int brakeForce = 350;
  [Range(1, 10)]
  public int decelerationMultiplier = 2;
  [Range(1, 10)]
  public int handbrakeDriftMultiplier = 5;
  [SerializeField] public bool leftBlinkerOn;
  [SerializeField] public bool rightBlinkerOn;
  [SerializeField] public bool offRoad;

  [SerializeField] public List<string> wheelsOffRoad = new List<string>();

  [Header("References")]
  [SerializeField] public Vector3 bodyMassCenter;
  [SerializeField] public Material blinkerOff;
  [SerializeField] public Material blinkerOn;
  [SerializeField] public GameObject LeftBlinker;
  [SerializeField] public GameObject RightBlinker;
  [SerializeField] public GameObject BrakeLight;

  [Header("Wheels")]
  public GameObject frontLeftMesh;
  public WheelCollider frontLeftCollider;
  public GameObject frontRightMesh;
  public WheelCollider frontRightCollider;
  public GameObject rearLeftMesh;
  public WheelCollider rearLeftCollider;
  public GameObject rearRightMesh;
  public WheelCollider rearRightCollider;

  [HideInInspector]
  public static float speed;
  [HideInInspector]
  public bool isDrifting;
  [HideInInspector]
  public bool isTractionLocked;

  private Rigidbody rb;
  public static float steeringAxis; // Represents the steering wheel. Values from -1 to 1.
  private float throttleAxis; // Used to know whether the throttle has reached the maximum value. It goes from -1 to 1.
  private float driftingAxis;
  private float localVelocityZ;
  private float localVelocityX;
  private bool deceleratingCar;
  private bool touchControlsSetup = false;
  private bool leftBlinkerCoroutine = false;
  private bool rightBlinkerCoroutine = false;

  WheelFrictionCurve FLwheelFriction;
  float FLWextremumSlip;
  WheelFrictionCurve FRwheelFriction;
  float FRWextremumSlip;
  WheelFrictionCurve RLwheelFriction;
  float RLWextremumSlip;
  WheelFrictionCurve RRwheelFriction;
  float RRWextremumSlip;

  void Start()
  {
    rb = gameObject.GetComponent<Rigidbody>();
    rb.centerOfMass = bodyMassCenter;

    //Initial setup to calculate the drift value of the car. This part could look a bit
    //complicated, but do not be afraid, the only thing we're doing here is to save the default
    //friction values of the car wheels so we can set an appropiate drifting value later.
    FLwheelFriction = new WheelFrictionCurve();
    FLwheelFriction.extremumSlip = frontLeftCollider.sidewaysFriction.extremumSlip;
    FLWextremumSlip = frontLeftCollider.sidewaysFriction.extremumSlip;
    FLwheelFriction.extremumValue = frontLeftCollider.sidewaysFriction.extremumValue;
    FLwheelFriction.asymptoteSlip = frontLeftCollider.sidewaysFriction.asymptoteSlip;
    FLwheelFriction.asymptoteValue = frontLeftCollider.sidewaysFriction.asymptoteValue;
    FLwheelFriction.stiffness = frontLeftCollider.sidewaysFriction.stiffness;
    FRwheelFriction = new WheelFrictionCurve();
    FRwheelFriction.extremumSlip = frontRightCollider.sidewaysFriction.extremumSlip;
    FRWextremumSlip = frontRightCollider.sidewaysFriction.extremumSlip;
    FRwheelFriction.extremumValue = frontRightCollider.sidewaysFriction.extremumValue;
    FRwheelFriction.asymptoteSlip = frontRightCollider.sidewaysFriction.asymptoteSlip;
    FRwheelFriction.asymptoteValue = frontRightCollider.sidewaysFriction.asymptoteValue;
    FRwheelFriction.stiffness = frontRightCollider.sidewaysFriction.stiffness;
    RLwheelFriction = new WheelFrictionCurve();
    RLwheelFriction.extremumSlip = rearLeftCollider.sidewaysFriction.extremumSlip;
    RLWextremumSlip = rearLeftCollider.sidewaysFriction.extremumSlip;
    RLwheelFriction.extremumValue = rearLeftCollider.sidewaysFriction.extremumValue;
    RLwheelFriction.asymptoteSlip = rearLeftCollider.sidewaysFriction.asymptoteSlip;
    RLwheelFriction.asymptoteValue = rearLeftCollider.sidewaysFriction.asymptoteValue;
    RLwheelFriction.stiffness = rearLeftCollider.sidewaysFriction.stiffness;
    RRwheelFriction = new WheelFrictionCurve();
    RRwheelFriction.extremumSlip = rearRightCollider.sidewaysFriction.extremumSlip;
    RRWextremumSlip = rearRightCollider.sidewaysFriction.extremumSlip;
    RRwheelFriction.extremumValue = rearRightCollider.sidewaysFriction.extremumValue;
    RRwheelFriction.asymptoteSlip = rearRightCollider.sidewaysFriction.asymptoteSlip;
    RRwheelFriction.asymptoteValue = rearRightCollider.sidewaysFriction.asymptoteValue;
    RRwheelFriction.stiffness = rearRightCollider.sidewaysFriction.stiffness;
  }

  void Update()
  {
    // Calculate speed of car and set local velocity for reference.
    speed = ((2 * Mathf.PI * frontLeftCollider.radius * frontLeftCollider.rpm * 60) / 1000) / 3; // Dividing by three for now just cause
    localVelocityX = transform.InverseTransformDirection(rb.velocity).x;
    localVelocityZ = transform.InverseTransformDirection(rb.velocity).z;

    // Accelerate
    if (Input.GetKey(KeyCode.W))
    {
      CancelInvoke("DecelerateCar");
      deceleratingCar = false;
      GoForward();
    }

    // Reverse/Brake
    if (Input.GetKey(KeyCode.S))
    {
      CancelInvoke("DecelerateCar");
      deceleratingCar = false;
      GoReverse();
    }

    // Left
    if (Input.GetKey(KeyCode.A)) TurnLeft();

    // Right
    if (Input.GetKey(KeyCode.D)) TurnRight();

    //Right Blinker
    if (Input.GetKeyUp(KeyCode.E))
    {
      rightBlinkerOn = !rightBlinkerOn;
      leftBlinkerOn = false;
    }

    // Left BLinker
    if (Input.GetKeyUp(KeyCode.Q))
    {
      leftBlinkerOn = !leftBlinkerOn;
      rightBlinkerOn = false;
    }

    // Coast
    if ((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))) ThrottleOff();

    if ((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) && !Input.GetKey(KeyCode.Space) && !deceleratingCar)
    {
      InvokeRepeating("DecelerateCar", 0f, 0.1f);
      deceleratingCar = true;
    }

    if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && steeringAxis != 0f) ResetSteeringAngle();

    // Brake Lights
    if (Input.GetKeyDown(KeyCode.S)) BrakeLight.GetComponent<MeshRenderer>().material = blinkerOn;
    if (Input.GetKeyUp(KeyCode.S)) BrakeLight.GetComponent<MeshRenderer>().material = blinkerOff;

    // Turn and rotate wheels
    AnimateWheelMeshes();
    if (leftBlinkerOn && !leftBlinkerCoroutine) StartCoroutine(LeftBlinkerFlash());
    if (rightBlinkerOn && !rightBlinkerCoroutine) StartCoroutine(RightBlinkerFlash());
  }

  private IEnumerator LeftBlinkerFlash()
  {
    leftBlinkerCoroutine = true;
    MeshRenderer blinker = LeftBlinker.GetComponent<MeshRenderer>();
    blinker.material = blinkerOn;
    yield return new WaitForSeconds(.5f);
    blinker.material = blinkerOff;
    yield return new WaitForSeconds(.5f);
    leftBlinkerCoroutine = false;
  }

  private IEnumerator RightBlinkerFlash()
  {
    rightBlinkerCoroutine = true;
    MeshRenderer blinker = RightBlinker.GetComponent<MeshRenderer>();
    blinker.material = blinkerOn;
    yield return new WaitForSeconds(.5f);
    blinker.material = blinkerOff;
    yield return new WaitForSeconds(.5f);
    rightBlinkerCoroutine = false;
  }

  public void TurnLeft()
  {
    steeringAxis = steeringAxis - (Time.deltaTime * 10f * steeringSpeed);
    if (steeringAxis > .95f) rightBlinkerOn = false;
    if (steeringAxis < -1f) steeringAxis = -1f;
    var steeringAngle = steeringAxis * maxSteeringAngle;
    frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, steeringAngle, steeringSpeed);
    frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, steeringAngle, steeringSpeed);
  }

  public void TurnRight()
  {
    steeringAxis = steeringAxis + (Time.deltaTime * 10f * steeringSpeed);
    if (steeringAxis > 1f) steeringAxis = 1f;
    if (steeringAxis < -.95f) leftBlinkerOn = false;
    var steeringAngle = steeringAxis * maxSteeringAngle;
    frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, steeringAngle, steeringSpeed);
    frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, steeringAngle, steeringSpeed);
  }

  public void ResetSteeringAngle()
  {
    // If reseting form a near max angle turn off blinkers
    if (Math.Abs(steeringAxis) > .95f)
    {
      leftBlinkerOn = false;
      rightBlinkerOn = false;
    }

    if (steeringAxis < 0f)
    {
      steeringAxis = steeringAxis + (Time.deltaTime * 10f * steeringSpeed);
    }
    else if (steeringAxis > 0f)
    {
      steeringAxis = steeringAxis - (Time.deltaTime * 10f * steeringSpeed);
    }
    if (Mathf.Abs(frontLeftCollider.steerAngle) < 1f)
    {
      steeringAxis = 0f;
    }
    var steeringAngle = steeringAxis * maxSteeringAngle;
    frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, steeringAngle, steeringSpeed);
    frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, steeringAngle, steeringSpeed);
  }

  public void AnimateWheelMeshes()
  {
    Quaternion FLWRotation;
    Vector3 FLWPosition;
    frontLeftCollider.GetWorldPose(out FLWPosition, out FLWRotation);
    frontLeftMesh.transform.position = FLWPosition;
    frontLeftMesh.transform.rotation = FLWRotation;

    Quaternion FRWRotation;
    Vector3 FRWPosition;
    frontRightCollider.GetWorldPose(out FRWPosition, out FRWRotation);
    frontRightMesh.transform.position = FRWPosition;
    frontRightMesh.transform.rotation = FRWRotation;

    Quaternion RLWRotation;
    Vector3 RLWPosition;
    rearLeftCollider.GetWorldPose(out RLWPosition, out RLWRotation);
    rearLeftMesh.transform.position = RLWPosition;
    rearLeftMesh.transform.rotation = RLWRotation;

    Quaternion RRWRotation;
    Vector3 RRWPosition;
    rearRightCollider.GetWorldPose(out RRWPosition, out RRWRotation);
    rearRightMesh.transform.position = RRWPosition;
    rearRightMesh.transform.rotation = RRWRotation;
  }

  public void GoForward()
  {
    if (Mathf.Abs(localVelocityX) > 4.5f)
    {
      isDrifting = true;
    }
    else
    {
      isDrifting = false;
    }
    throttleAxis = throttleAxis + (Time.deltaTime * 3f);

    if (throttleAxis > 1f) throttleAxis = 1f;

    if (localVelocityZ < -1f)
    {
      Brakes();
    }
    else
    {
      if (Mathf.RoundToInt(speed) < maxSpeed)
      {
        //Apply positive torque in all wheels to go forward if maxSpeed has not been reached.
        frontLeftCollider.brakeTorque = 0;
        frontLeftCollider.motorTorque = (accelerationRate * 50f) * throttleAxis;
        frontRightCollider.brakeTorque = 0;
        frontRightCollider.motorTorque = (accelerationRate * 50f) * throttleAxis;
        rearLeftCollider.brakeTorque = 0;
        rearLeftCollider.motorTorque = (accelerationRate * 50f) * throttleAxis;
        rearRightCollider.brakeTorque = 0;
        rearRightCollider.motorTorque = (accelerationRate * 50f) * throttleAxis;
      }
      else
      {
        // If speed is at max do not add any more torque.
        frontLeftCollider.motorTorque = 0;
        frontRightCollider.motorTorque = 0;
        rearLeftCollider.motorTorque = 0;
        rearRightCollider.motorTorque = 0;
      }
    }
  }

  public void GoReverse()
  {
    if (Mathf.Abs(localVelocityX) > 4.5f)
    {
      isDrifting = true;
    }
    else
    {
      isDrifting = false;
    }
    // The following part sets the throttle power to -1 smoothly.
    throttleAxis = throttleAxis - (Time.deltaTime * 3f);
    if (throttleAxis < -1f) throttleAxis = -1f;
    //If the car is still going forward, then apply brakes in order to avoid strange
    //behaviours. If the local velocity in the 'z' axis is greater than 1f, then it
    //is safe to apply negative torque to go reverse.
    if (localVelocityZ > 1f)
    {
      Brakes();
    }
    else
    {
      if (Mathf.Abs(Mathf.RoundToInt(speed)) < maxReverseSpeed)
      {
        //Apply negative torque in all wheels to go in reverse if maxReverseSpeed has not been reached.
        frontLeftCollider.brakeTorque = 0;
        frontLeftCollider.motorTorque = (accelerationRate * 50f) * throttleAxis;
        frontRightCollider.brakeTorque = 0;
        frontRightCollider.motorTorque = (accelerationRate * 50f) * throttleAxis;
        rearLeftCollider.brakeTorque = 0;
        rearLeftCollider.motorTorque = (accelerationRate * 50f) * throttleAxis;
        rearRightCollider.brakeTorque = 0;
        rearRightCollider.motorTorque = (accelerationRate * 50f) * throttleAxis;
      }
      else
      {
        //If the maxReverseSpeed has been reached, then stop applying torque to the wheels.
        // IMPORTANT: The maxReverseSpeed variable should be considered as an approximation; the speed of the car
        // could be a bit higher than expected.
        frontLeftCollider.motorTorque = 0;
        frontRightCollider.motorTorque = 0;
        rearLeftCollider.motorTorque = 0;
        rearRightCollider.motorTorque = 0;
      }
    }
  }

  public void ThrottleOff()
  {
    frontLeftCollider.motorTorque = 0;
    frontRightCollider.motorTorque = 0;
    rearLeftCollider.motorTorque = 0;
    rearRightCollider.motorTorque = 0;
  }

  public void DecelerateCar()
  {
    if (Mathf.Abs(localVelocityX) > 4.5f)
    {
      isDrifting = true;
    }
    else
    {
      isDrifting = false;
    }
    // The following part resets the throttle power to 0 smoothly.
    if (throttleAxis != 0f)
    {
      if (throttleAxis > 0f)
      {
        throttleAxis = throttleAxis - (Time.deltaTime * 10f);
      }
      else if (throttleAxis < 0f)
      {
        throttleAxis = throttleAxis + (Time.deltaTime * 10f);
      }
      if (Mathf.Abs(throttleAxis) < 0.15f)
      {
        throttleAxis = 0f;
      }
    }
    rb.velocity = rb.velocity * (1f / (1f + (0.025f * decelerationMultiplier)));
    // Since we want to decelerate the car, we are going to remove the torque from the wheels of the car.
    frontLeftCollider.motorTorque = 0;
    frontRightCollider.motorTorque = 0;
    rearLeftCollider.motorTorque = 0;
    rearRightCollider.motorTorque = 0;
    // If the magnitude of the car's velocity is less than 0.25f (very slow velocity), then stop the car completely and
    // also cancel the invoke of this method.
    if (rb.velocity.magnitude < 0.25f)
    {
      rb.velocity = Vector3.zero;
      CancelInvoke("DecelerateCar");
    }
  }

  public void Brakes()
  {
    frontLeftCollider.brakeTorque = brakeForce;
    frontRightCollider.brakeTorque = brakeForce;
    rearLeftCollider.brakeTorque = brakeForce;
    rearRightCollider.brakeTorque = brakeForce;
  }

  public void Handbrake()
  {
    CancelInvoke("RecoverTraction");
    driftingAxis = driftingAxis + (Time.deltaTime);
    float secureStartingPoint = driftingAxis * FLWextremumSlip * handbrakeDriftMultiplier;

    if (secureStartingPoint < FLWextremumSlip)
    {
      driftingAxis = FLWextremumSlip / (FLWextremumSlip * handbrakeDriftMultiplier);
    }
    if (driftingAxis > 1f)
    {
      driftingAxis = 1f;
    }
    if (Mathf.Abs(localVelocityX) > 4.5f)
    {
      isDrifting = true;
    }
    else
    {
      isDrifting = false;
    }
    if (driftingAxis < 1f)
    {
      FLwheelFriction.extremumSlip = FLWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
      frontLeftCollider.sidewaysFriction = FLwheelFriction;

      FRwheelFriction.extremumSlip = FRWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
      frontRightCollider.sidewaysFriction = FRwheelFriction;

      RLwheelFriction.extremumSlip = RLWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
      rearLeftCollider.sidewaysFriction = RLwheelFriction;

      RRwheelFriction.extremumSlip = RRWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
      rearRightCollider.sidewaysFriction = RRwheelFriction;
    }

    isTractionLocked = true;
  }

  public void RecoverTraction()
  {
    isTractionLocked = false;
    driftingAxis = driftingAxis - (Time.deltaTime / 1.5f);
    if (driftingAxis < 0f)
    {
      driftingAxis = 0f;
    }

    if (FLwheelFriction.extremumSlip > FLWextremumSlip)
    {
      FLwheelFriction.extremumSlip = FLWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
      frontLeftCollider.sidewaysFriction = FLwheelFriction;

      FRwheelFriction.extremumSlip = FRWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
      frontRightCollider.sidewaysFriction = FRwheelFriction;

      RLwheelFriction.extremumSlip = RLWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
      rearLeftCollider.sidewaysFriction = RLwheelFriction;

      RRwheelFriction.extremumSlip = RRWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
      rearRightCollider.sidewaysFriction = RRwheelFriction;

      Invoke("RecoverTraction", Time.deltaTime);

    }
    else if (FLwheelFriction.extremumSlip < FLWextremumSlip)
    {
      FLwheelFriction.extremumSlip = FLWextremumSlip;
      frontLeftCollider.sidewaysFriction = FLwheelFriction;

      FRwheelFriction.extremumSlip = FRWextremumSlip;
      frontRightCollider.sidewaysFriction = FRwheelFriction;

      RLwheelFriction.extremumSlip = RLWextremumSlip;
      rearLeftCollider.sidewaysFriction = RLwheelFriction;

      RRwheelFriction.extremumSlip = RRWextremumSlip;
      rearRightCollider.sidewaysFriction = RRwheelFriction;

      driftingAxis = 0f;
    }
  }

}