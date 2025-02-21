using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    //This function rotates the Cars in the scene
    void Update()
    {
        transform.Rotate(0, 0.5f, 0);
    }
}
