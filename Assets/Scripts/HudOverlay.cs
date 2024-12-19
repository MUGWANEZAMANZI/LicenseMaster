using TMPro;
using UnityEngine;

public class HudOverlay : MonoBehaviour
{

    [Header("References")]
    [SerializeField] public TextMeshProUGUI speedText;
    [SerializeField] public TextMeshProUGUI scoreText;


    // Update is called once per frame
    void Start()
    {
        InvokeRepeating("UpdateGUI", 0, .1f);
    }

    void UpdateGUI()
    {
        speedText.text = "Speed: " + CarController.speed.ToString("0") + "Km/h";
        scoreText.text = "Score: " + LevelManager.instance.points;

        if (CarController.speed > LevelManager.instance.currentZoneSpeedLimit)
        {
            speedText.color = Color.red;
        }
        else
        {
            speedText.color = Color.white;
        }
    }
}
