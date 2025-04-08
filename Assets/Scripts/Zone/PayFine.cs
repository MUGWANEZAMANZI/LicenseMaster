using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayFine : MonoBehaviour
{
    private bool playerHasEnteredZone = false;
    public BoxCollider boxCollider;

    void Update()
    {
        //Debug.Log(playerHasEnteredZone);
        if (playerHasEnteredZone && Mathf.Abs(CarController.speed) < .5)
        {
            if (LevelManager.instance.penaltyFinePaid == false)
            {
                LevelManager.instance.PayFine();
                StartCoroutine(ToggleCollider());
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerHasEnteredZone = true;
        }
    }

    private IEnumerator ToggleCollider()
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(0.75f);
        boxCollider.enabled = true;
    }
}
