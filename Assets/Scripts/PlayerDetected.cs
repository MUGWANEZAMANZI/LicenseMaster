using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetected : MonoBehaviour
{
    public static string detectedTag;
    public static bool isDetected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isDetected = true;
            detectedTag = gameObject.tag;
        }
    }
    private void OntriggerExit(Collider other)
    {
        if (gameObject.tag == "Player")
        {
            isDetected = false;
            detectedTag = "";
            Debug.Log("Left");
        }
    }
}
