using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDashBoard : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _SteeringText,_SpeedText;
    // Start is called before the first frame update
    void Start()
    {
        _SteeringText.text = CarController.steeringAxis.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        _SteeringText.text = CarController.steeringAxis.ToString("0.00");
        _SpeedText.text=" Speed "+ CarController.speed.ToString("00.00");
    }
}
