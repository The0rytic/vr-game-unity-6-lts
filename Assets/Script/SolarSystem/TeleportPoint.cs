using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    private Vector3 offset;
    private Transform planet;

    void Start()
    {
        planet = transform.parent;
        offset = transform.position - planet.position;
        transform.SetParent(null); // lepas dari parent saat play
    }

    void LateUpdate()
    {
        transform.position = planet.position + offset;
        transform.rotation = Quaternion.identity;
    }
}