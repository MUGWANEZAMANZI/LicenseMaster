using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Violations 
{
    Speed,
    OneWay,
    Collision,
    Stop,
    OffRoad
}

public class LevelManager : MonoBehaviour
{

    [Header("Level Info")]
    [SerializeField] public float levelTime = 0;
    [SerializeField] public int points = 100;
    [SerializeField] public int violations = 0;
    [SerializeField] public float elapsedTime = 0f;
    [SerializeField] public float currentZoneSpeedLimit = 30f;
    [SerializeField] public List<Violations> incuredViolations = new List<Violations>();

    [Header("Level Params")]
    [SerializeField] public float violationWarningTime;

    [Header("References")]
    [SerializeField] public CarController player;
    [SerializeField] public Canvas HudOverlay;
    [SerializeField] public GameObject violationTextPrefab;

    [Header("Wwise")]
    [SerializeField] public AK.Wwise.Event LevelStart;

    public static LevelManager instance;
    private static int speedLimitDeduction = 5;
    private static int oneWayDeduction = 10;
    private static int roundAboutDection = 5;
    private static int timeDeduction = 10;
    private static int offRoadDeduction = 5;
    private static int stopZoneDeduction = 10;
    private static int collisionDeduction = 10;

    public bool speedLimitViolationCoroutine = false;
    public bool offRoadViolationCoroutine = false;
    public bool oneWayViolationCoroutine = false;
    public bool collisionViolationCoroutine = false;
    public bool stopViolationCoroutine = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        if (player == null)
        {
            Debug.LogError("[" + name + "] Missing player reference on this GameOjbect.");
        }
        
        LevelStart.Post(gameObject);
    }

    void Update()
    {
        // Speed Check
        if (CarController.speed > currentZoneSpeedLimit && !speedLimitViolationCoroutine) StartCoroutine(SpeedLimitViolation());
        if (player.offRoad && !offRoadViolationCoroutine) StartCoroutine(OffRoadViolation());
        if (player.wrongWay && !oneWayViolationCoroutine) StartCoroutine(OneWayViolation());
        levelTime += Time.deltaTime;
    }

    private IEnumerator OneWayViolation()
    {
        Debug.Log(message: "Hit!");
        oneWayViolationCoroutine = true;
        GameObject warning = Instantiate(violationTextPrefab, new Vector3(HudOverlay.transform.position.x, 300, HudOverlay.transform.position.z), HudOverlay.transform.rotation, HudOverlay.transform);
        warning.GetComponent<ViolationDialogWarning>().text.text = "Wrong Way";
        yield return new WaitForSeconds(violationWarningTime);
        if (player.wrongWay)
        {
            points -= oneWayDeduction;
            incuredViolations.Add(Violations.OneWay);
        }
        oneWayViolationCoroutine = false;
        Destroy(warning);
    }

    private IEnumerator OffRoadViolation()
    {
        offRoadViolationCoroutine = true;
        GameObject warning = Instantiate(violationTextPrefab, new Vector3(HudOverlay.transform.position.x, 300, HudOverlay.transform.position.z), HudOverlay.transform.rotation, HudOverlay.transform);
        warning.GetComponent<ViolationDialogWarning>().text.text = "Return to Road";
        yield return new WaitForSeconds(violationWarningTime);
        if (player.offRoad)
        {
            points -= speedLimitDeduction;
            incuredViolations.Add(Violations.OffRoad);
        }
        offRoadViolationCoroutine = false;
        Destroy(warning);
    }

    private IEnumerator SpeedLimitViolation()
    {
        speedLimitViolationCoroutine = true;
        GameObject warning = Instantiate(violationTextPrefab, new Vector3(HudOverlay.transform.position.x, 300, HudOverlay.transform.position.z), HudOverlay.transform.rotation, HudOverlay.transform);
        warning.GetComponent<ViolationDialogWarning>().text.text = "Decrease Speed";
        yield return new WaitForSeconds(violationWarningTime);
        if (CarController.speed > currentZoneSpeedLimit)
        {
            points -= speedLimitDeduction;
            incuredViolations.Add(Violations.Speed);
        }
        speedLimitViolationCoroutine = false;
        Destroy(warning);
    }

    private IEnumerator CollisionViolationDebounce()
    {
        collisionViolationCoroutine = true;
        GameObject warning = Instantiate(violationTextPrefab, new Vector3(HudOverlay.transform.position.x, 300, HudOverlay.transform.position.z), HudOverlay.transform.rotation, HudOverlay.transform);
        warning.GetComponent<ViolationDialogWarning>().text.text = "Avoid Collisions";
        yield return new WaitForSeconds(1f);
        Destroy(warning);
        collisionViolationCoroutine = false;
    }

    private IEnumerator StopViolationDebounce()
    {
        stopViolationCoroutine = true;
        GameObject warning = Instantiate(violationTextPrefab, new Vector3(HudOverlay.transform.position.x, 300, HudOverlay.transform.position.z), HudOverlay.transform.rotation, HudOverlay.transform);
        warning.GetComponent<ViolationDialogWarning>().text.text = "Failed Stop";
        yield return new WaitForSeconds(1f);
        Destroy(warning);
        stopViolationCoroutine = false;
    }

    public void StopZoneViolation()
    {
        if (stopViolationCoroutine) return;
        StartCoroutine(StopViolationDebounce());
        points -= stopZoneDeduction;
        incuredViolations.Add(Violations.Stop);
    }

    public void CollisionViolation()
    {
        if (collisionViolationCoroutine) return;
        StartCoroutine(CollisionViolationDebounce());
        points -= collisionDeduction;
        incuredViolations.Add(Violations.Collision);
    }
}   