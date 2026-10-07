using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.SceneManagement;
using System.Diagnostics;
public class UFOAnimation : MonoBehaviour
{
    public GameObject UFO;
    public GameObject Flash;
    public GameObject LightBeam;
    public XRGrabInteractable grabInteractable;
    public SceneLoader SceneLoader;
    public AudioSource source;
    public AudioClip clip;

    void Awake()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
    }
    void OnGrab(BaseInteractionEventArgs args)
    {
        StartCoroutine(PlayAnimation());
        source.PlayOneShot(clip);
    }

    IEnumerator PlayAnimation()
    {
        UFO.transform.DOMove(new Vector3(0, 112, 10), 5f).SetEase(Ease.OutQuart);
        yield return new WaitForSecondsRealtime(7f);
        Flash.transform.DOScaleX(5.6f, 3f).SetEase(Ease.OutExpo);
        yield return new WaitForSecondsRealtime(3f);
        LightBeam.transform.DOLocalMoveZ(5f, 2f).SetEase(Ease.OutExpo);
        yield return new WaitForSecondsRealtime(2f);
        LightBeam.transform.DOScaleZ(0.7f, 0.5f).SetEase(Ease.OutExpo);
        yield return new WaitForSecondsRealtime(0.5f);
        SceneLoader.LoadNextLevel(); 
    }


}
