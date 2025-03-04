using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    public GameObject[] cars;
    public GameObject[] names;
    public Button next;
    public Button prev;
    int index, index2;

    void Start()
    {
        index = PlayerPrefs.GetInt("carIndex", 0); // Default to 0 if no saved value exists
        UpdateSelection();
    }

    void Update()
    {
        next.interactable = index < cars.Length - 1;
        next.interactable = index2 < names.Length - 1;
        prev.interactable = index > 0;
        prev.interactable = index2 > 0;
    }

    public void Next()
    {
        if (index < cars.Length - 1 || index2 < cars.Length -1)
        {
            index++;
            index2++;
        }
        else
        {
            index = 0; // Loop back to the first car
            index2 = 0;
        }
        UpdateSelection();
    }

    public void Prev()
    {
        if (index > 0 || index2 > 0)
        {
            index--;
            index2--;
        }
        else
        {
            index = cars.Length - 1; // Loop back to the last car
            index = names.Length - 1;
        }
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(i == index); // Activate only the selected car
            names[i].SetActive(i == index);
        }
        for(int i = 0; i < names.Length; i++)
        {
            names[i].SetActive(i == index2);
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
