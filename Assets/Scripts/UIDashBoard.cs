using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;

using UnityEngine.UI;
using System.ComponentModel;
public class UIDashBoard : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _SteeringText,_SpeedText;
    float _dec1, _dec2,_dec3,_dec4,valueComponent;
     public  Slider slider1,slider2;
   public float val, val2;
    // Start is called before the first frame update
    void Start()
    {
        CarController.steeringAxis = 0f;
        _SteeringText.text = CarController.steeringAxis.ToString();
        

    }
    public void ConvertDecimal1()
    {
        val = CarController.steeringAxis;
        if (val >= 0)
        {
            _dec1 = val;
            slider1.value.Equals(_dec1);
        }
        if (val < 0)
        {
            _dec2 = Mathf.Abs(val);
            slider1.value.Equals(_dec2);
        }
       
    }
    public void ConvertDecimal2()
    { val2= CarController.steeringAxis; 
        if (val2 >= 0)
        {
            _dec3 = val2;
            slider2.value.Equals(_dec3);
        }
        if (val2 < 0)
        {
            _dec4 = Mathf.Abs(val2);
            slider2.value.Equals(_dec4);
        }

    }

    // Update is called once per frame
    void Update()
    {
        _SteeringText.text = CarController.steeringAxis.ToString("0.00");
        _SpeedText.text=" Speed "+ CarController.speed.ToString("00.00");
        ConvertDecimal1();
        ConvertDecimal2();


    }
}
