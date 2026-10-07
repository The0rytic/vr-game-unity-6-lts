using UnityEngine;


public class GrabChecker : MonoBehaviour
{
    [SerializeField] private GameObject UFO;
    
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private UFOAnimation UFOAnim;
    public bool isGrab {get; private set;}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        UFOAnim = GetComponent<UFOAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrab = grabInteractable.isSelected;

        if (isGrab)
        {
            // UFOAnim.StartCoroutine(PlayAnimation());
        }  
    }
}
