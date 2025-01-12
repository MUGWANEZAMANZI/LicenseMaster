using UnityEngine;

public class SmothRotation : MonoBehaviour
{

    [SerializeField] public float rotationSpeed = 150f;
    [SerializeField] public float bounceFrequency = .3f;
    [SerializeField] public float bounceAmplitude = .01f;

    public Vector3 startPosition;
    public Vector3 newPosition;
    
    private void Awake()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        Vector3 newPosition = startPosition;
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        newPosition.y = Mathf.Sin(Time.time * bounceFrequency) * bounceAmplitude;
        transform.localPosition = startPosition + newPosition;
    }
}
