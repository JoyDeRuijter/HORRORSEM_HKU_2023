using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [HideInInspector] public bool isOpen;

    [SerializeField] private TMP_Text textLabel;
    public DialogueObject currentDialogue;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private DialogueManager dialogueManager;

    private TypeWriterEffect typeWriterEffect;
    private ProfileHandler profileHandler;

    private void Awake()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        profileHandler = GetComponent<ProfileHandler>();
    }

    public void ShowDialogue(DialogueObject _dialogueObject)
    {
        currentDialogue = _dialogueObject;
        isOpen = true;
        dialogueBox.SetActive(true);
        profileHandler.UpdateProfile(_dialogueObject);
        profileHandler.ShowProfile();
        StartCoroutine(StepThroughDialogue(_dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject _dialogueObject)
    {
        _dialogueObject.isDone = false;
        for (int i = 0; i < _dialogueObject.dialogue.Length; i++)
        {
            string dialogue = _dialogueObject.dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            if (i == _dialogueObject.dialogue.Length - 1)
                break;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        yield return new WaitForSeconds(1f);
        dialogueManager.pressedSpace = false;
        _dialogueObject.isDone = true;
    }

    private IEnumerator RunTypingEffect(string _dialogue)
    {
        typeWriterEffect.Run(_dialogue, textLabel);

        while (typeWriterEffect.isRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
                typeWriterEffect.Stop();
        }
    }

    public void CloseDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        profileHandler.HideProfile();
        currentDialogue = null;
    }
}
