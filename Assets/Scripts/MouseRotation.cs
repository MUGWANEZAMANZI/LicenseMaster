

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Diagnostics.Contracts;

public class MouseRotation : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;
   // public LayerMask layerMask;
    //[SerializeField] Transform target;
    public GameObject Player;

    void Update()

    {
      
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
                Player.transform.LookAt(hitInfo.point);

        
    }

}