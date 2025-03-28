using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs; // Assign the same cars used in CarSelection
    public Transform spawnPoint; // Assign a spawn position in the scene

    void Start()
    {
        int selectedCarIndex = PlayerPrefs.GetInt("carIndex", 0); // Default to first car
        GameObject selectedCar = Instantiate(carPrefabs[selectedCarIndex], spawnPoint.position, spawnPoint.rotation);

        selectedCar.GetComponent<CarController1>().enabled = true; // Enable the script only for the chosen car
    }
}
