using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ThrowSpear : MonoBehaviour
{
    public GameObject player;
    public Transform cam;
    public Transform attachPoint;
    public GameObject objectToThrow;
    public GameObject attackPoint;
    public int totalThrows;
    public float throwCooldown;
   // public GameObject enemy;
    public KeyCode throwKey = KeyCode.Mouse2;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    void Start()
    {

     //   player.transform.LookAt(enemy.transform);
        readyToThrow = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
            Throw();
    }
    public void Throw()
    {


        readyToThrow = false;
        //instantiate object
        GameObject projectile = Instantiate(objectToThrow, attackPoint.transform.position, cam.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.transform.Rotate(90f, 0f, 0f);
        //calculate direction
        Vector3 forceDirection = cam.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attachPoint.transform.position).normalized;

        }
        //Debug.DrawRay(transform.position,forceDirection,Color.red,50);   

        //add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
        totalThrows--;
        Invoke(nameof(ResetThrow), throwCooldown);
    }
    public void ResetThrow()
    {
        readyToThrow = true;
    }
}