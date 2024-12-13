using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField] public GameObject lookTarget = null;
	[SerializeField] public GameObject moveTarget = null;
	[SerializeField] public float speed = 1.5f;

	void FixedUpdate()
	{
		// Always look at the look target (player/car)
		transform.LookAt(lookTarget.transform);
		// Calculate how far the camera should move this update
		float moveStep = Mathf.Abs(Vector3.Distance(transform.position, moveTarget.transform.position) * speed); 
		// Move the camera
		transform.position = Vector3.MoveTowards(transform.position, moveTarget.transform.position, moveStep * Time.deltaTime);

	}
}
