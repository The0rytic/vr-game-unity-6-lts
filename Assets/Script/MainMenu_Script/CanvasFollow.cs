using UnityEngine;

public class CanvasFollow : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(transform.position + (transform.position - cam.position));
    }
}