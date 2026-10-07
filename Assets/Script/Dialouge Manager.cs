using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public InputActionReference leftGrabAction;
    public InputActionReference rightGrabAction; // Add this
    public GameObject canvas;

    [TextArea]
    public string[] lines;
    private int currentLine;
    private bool dialogueActive;
    private bool canvasOpened = false; // tracks if ever opened

    void Start()
    {
        canvas.SetActive(false); // starts hidden
        StartDialogue();
    }

    void OnEnable()
    {
        leftGrabAction.action.Enable();
        leftGrabAction.action.performed += OnLeftGrab;

        rightGrabAction.action.Enable();
        rightGrabAction.action.performed += OnRightGrab;
    }

    void OnDisable()
    {
        leftGrabAction.action.performed -= OnLeftGrab;
        leftGrabAction.action.Disable();

        rightGrabAction.action.performed -= OnRightGrab;
        rightGrabAction.action.Disable();
    }

    void OnRightGrab(InputAction.CallbackContext ctx)
    {
        if (canvasOpened) return; // once open, ignore all future presses
        canvasOpened = true;
        canvas.SetActive(true);
    }

    void OnLeftGrab(InputAction.CallbackContext ctx)
    {
        if (!dialogueActive) return;
        NextLine();
    }

    public void StartDialogue()
    {
        currentLine = 0;
        dialogueActive = true;
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = lines[currentLine];
    }

    void NextLine()
    {
        currentLine++;
        if (currentLine >= lines.Length)
        {
            EndDialogue();
            return;
        }
        dialogueText.text = lines[currentLine];
    }

    void EndDialogue()
    {
        dialogueActive = false;
        dialogueText.gameObject.SetActive(false);
    }
}