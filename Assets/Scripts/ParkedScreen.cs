using UnityEngine;
using TMPro;
using AK.Wwise;

public class ParkedScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public TextMeshProUGUI levelTimeText;
    [SerializeField] public TextMeshProUGUI objectivesText;

    [Header("Wwise")]
    [SerializeField] public AK.Wwise.Event TriggerAlert;

    void OnEnable()
    {
        TriggerAlert.Post(gameObject);
        Time.timeScale = 0;
        // Set Level Time Text
        float time = LevelManager.instance.levelTime;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        levelTimeText.text = string.Format("Level Time - {0:00}:{1:00}", minutes, seconds);

        // Set Objective Text
        objectivesText.text = LevelManager.instance.objectivesFound + "/" + LevelManager.instance.parkingZones.Count + " Parking Spots found.";
    }

    public void Coninue() 
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}