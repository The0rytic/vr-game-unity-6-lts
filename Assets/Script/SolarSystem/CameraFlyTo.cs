using UnityEngine;
using DG.Tweening;

public class CameraFlyTo : MonoBehaviour
{
    public static CameraFlyTo Instance;

    [Header("References")]
    [SerializeField] private Transform xrRig;

    private void Awake()
    {
        Instance = this;
    }

    public void FlyToPlanet(Transform teleportPoint)
    {
        if (teleportPoint == null)
        {
            Debug.LogError("Teleport point NULL!");
            return;
        }

        xrRig.DOLocalMove(teleportPoint.position, 2f).SetEase(Ease.InOutCubic);
        xrRig.rotation = teleportPoint.rotation;

        Debug.Log("Teleported to: " + teleportPoint.name);
    }
}