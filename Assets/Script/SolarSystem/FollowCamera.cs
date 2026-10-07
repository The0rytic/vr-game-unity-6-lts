using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform cam;

    [SerializeField] float distance = 2f;
    [SerializeField] float followSpeed = 10f;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        Vector3 forward = cam.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 targetPosition = cam.position + forward * distance;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );

        transform.LookAt(transform.position + (transform.position - cam.position));
    }
}