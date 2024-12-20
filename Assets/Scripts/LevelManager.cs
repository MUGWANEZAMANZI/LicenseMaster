using System;
using System.Collections;
using System.Drawing;
using JetBrains.Annotations;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [Header("Level Info")]
    [SerializeField] public int points = 100;
    [SerializeField] public int violations = 0;
    [SerializeField] public float elapsedTime = 0f;
    [SerializeField] public float currentZoneSpeedLimit = 30f;

    [Header("References")]
    [SerializeField] public CarController player;
    public static LevelManager instance;
    private static int speedLimitDeduction = 5;
    private static int oneWayDeduction = 10;
    private static int roundAboutDection = 5;
    private static int timeDeduction = 10;
    private static int offRoadDeduction = 5;
    private static int collisionDeduction = 10;


    // Private Vars
    private bool speedLimitViolationCoroutine = false;
    private bool offRoadViolationCoroutine = false;


    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("[" + name + "] Missing player reference on this GameOjbect.");
        }
    }

    void Update()
    {
        // Speed Check
        if (CarController.speed > currentZoneSpeedLimit && !speedLimitViolationCoroutine) StartCoroutine(SpeedLimitViolation());
        if (player.offRoad && !offRoadViolationCoroutine) StartCoroutine(OffRoadViolation());
    }

    private IEnumerator OffRoadViolation()
    {
        offRoadViolationCoroutine = true;
        // TODO Display Warning on UI
        yield return new WaitForSeconds(5f);
        if (player.offRoad)
        {
            points -= speedLimitDeduction;
            violations++;
            // TODO display visual and update HUD elements
        }
        offRoadViolationCoroutine = false;
    }

    private IEnumerator SpeedLimitViolation()
    {
        speedLimitViolationCoroutine = true;
        // TODO Display Warning on UI
        yield return new WaitForSeconds(3f);
        if (CarController.speed > currentZoneSpeedLimit)
        {
            points -= speedLimitDeduction;
            violations++;
            // TODO display visual and update HUD elements
        }
        speedLimitViolationCoroutine = false;
    }

    public void CollisionViolation()
    {
        points -= collisionDeduction;
        violations++;
    }
}