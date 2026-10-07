using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] Vector3 rotationAxis = Vector3.up;

    void Update()
    {
        transform.Rotate(rotationAxis.normalized * rotationSpeed * Time.deltaTime);
    }
}