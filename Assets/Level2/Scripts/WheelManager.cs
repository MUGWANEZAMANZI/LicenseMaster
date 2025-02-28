using UnityEngine;

public class WheelAligner : MonoBehaviour
{
    public WheelCollider[] wheelColliders;
    public Transform[] wheelMeshes;

    void Update()
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            Vector3 position;
            Quaternion rotation;
            wheelColliders[i].GetWorldPose(out position, out rotation);

            if (wheelMeshes[i] != null)
            {
                wheelMeshes[i].position = position;
                wheelMeshes[i].rotation = rotation;
            }
        }
    }
}
