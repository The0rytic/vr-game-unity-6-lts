using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class LockCharacterController : MonoBehaviour
{
    private CharacterController cc;

    [SerializeField] private float height = 3f;
    [SerializeField] private Vector3 center = new Vector3(0, 0.4f, 0);

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void LateUpdate()
    {
        cc.height = height;
        cc.center = center;
    }
}