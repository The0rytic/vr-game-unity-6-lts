using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSFX : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip hoverSound; // isi di Inspector

    public void OnPointerEnter(PointerEventData eventData)
    {
        SFXManager.Instance.PlaySFX(hoverSound);
    }
}