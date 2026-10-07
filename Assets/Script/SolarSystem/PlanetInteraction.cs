using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlanetInteraction : MonoBehaviour
{
    [SerializeField] string planetName;
    [TextArea] [SerializeField] string planetFact;
    [SerializeField] bool isBumi = false;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;

    void Start()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnPlanetSelected);
    }

    void OnPlanetSelected(SelectEnterEventArgs args)
    {
        PlanetUIManager.Instance.SelectPlanet(planetName);
        // StopOrbits dihapus dari sini, biar gak dobel
    }
}