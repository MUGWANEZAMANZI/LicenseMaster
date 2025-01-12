using UnityEngine;
using TMPro;

public class ParkedScreen : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI levelTimeText;
    [SerializeField] public TextMeshProUGUI objectivesText;

    void OnEnable()
    {
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