using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    public GameObject[] cars;
    public string[] carNames;
    public TextMeshProUGUI carText;
    public Button next;
    public Button prev;
    int index;

    void Start()
    {
        index = PlayerPrefs.GetInt("carIndex", 0); // Default to 0 if no saved value exists
        UpdateSelection();
    }

    void Update()
    {
        next.interactable = index < cars.Length - 1;
        prev.interactable = index > 0;
    }

    public void Next()
    {
        if (index < cars.Length - 1)
        {
            index++;
        }
        else
        {
            index = 0; // Loop back to the first car
        }
        UpdateSelection();
    }

    public void Prev()
    {
        if (index > 0)
        {
            index--;
        }
        else
        {
            index = cars.Length - 1; // Loop back to the last car

        }
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(i == index); // Activate only the selected car
        } 

        if(carNames != null && carNames.Length > index){
            carText.text = carNames[index];
        }


        // Save the selected car index
        PlayerPrefs.SetInt("carIndex", index);
        PlayerPrefs.Save();
    }

    public void Race()
    {
        SceneManager.LoadScene("Level2");
    }
}
