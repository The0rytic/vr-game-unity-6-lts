using UnityEngine;

[System.Serializable]
public class SFXClips
{
    [Header("Dialogue")]
    public AudioClip dialogueOpen;
    public AudioClip dialogueClose;
    public AudioClip dialogueTyping;
    public AudioClip dialogueNext;

    [Header("Clue")]
    public AudioClip clueFound;
    public AudioClip clueConnect;

    [Header("Evidence Board")]
    public AudioClip boardOpen;
    public AudioClip boardClose;
    public AudioClip solveButton;

    [Header("Result")]
    public AudioClip starEarn;
    public AudioClip caseCorrect;
    public AudioClip caseWrong;

    [Header("UI")]
    public AudioClip buttonClick;
    public AudioClip buttonHover;

    [Header("Footsteps")]
    public AudioClip footSteps;
}