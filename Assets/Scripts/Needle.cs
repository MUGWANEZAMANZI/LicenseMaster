using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Needle : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _SteeringText, _SpeedText;
    public float value;
    public GameObject Speedneedle;
    public GameObject TurnNeedle;
    private float startPosition = 220f;
       private float endPosition;
    void Start()
    { 
        float x =CarController.steeringAxis; }


  public void  UpdateNeedle()
    {
       //  value = Mathf.Sin(Time.deltaTime)*100f;
       // var value = Mathf.Lerp(-1f, 1f, .02f);
      // float desiredPosition = startPosition - endPosition;
        //float temp = CarController.speed / 180;
      //  needle.transform.eulerAngles = new Vector3(0f, 0f,(startPosition - temp * desiredPosition));
     //   needle.transform.eulerAngles = new Vector3(0f, 0f, SinMovementZ());
    }
    public float SpeedSinMovementZ()
    {
        return transform.position.z -  CarController.speed;
    }

    public float TurnDirSinMovementZ()
    {
        return transform.position.z - 80*CarController.steeringAxis;
    }
    void Update()
    {
        value =(SpeedSinMovementZ());
        Speedneedle.transform.eulerAngles = new Vector3(0f, 0f, value+213f);
      //  _SteeringText.text = CarController.steeringAxis.ToString("0.00");
        _SpeedText.text =  CarController.speed.ToString("000.00");
        TurnNeedle.transform.eulerAngles = new Vector3(0f, 0f, TurnDirSinMovementZ() - 83.5f); ;    
    }
}