using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [HideInInspector] public bool isOpen;

    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject currentDialogue;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private DialogueManager dialogueManager;

    private TypeWriterEffect typeWriterEffect;
    private ProfileHandler profileHandler;

    private void Awake()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        profileHandler = GetComponent<ProfileHandler>();
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        isOpen = true;
        dialogueBox.SetActive(true);
        profileHandler.UpdateProfile(dialogueObject);
        profileHandler.ShowProfile();
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.dialogue.Length; i++)
        {
            string dialogue = dialogueObject.dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            if (i == dialogueObject.dialogue.Length - 1) break;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typeWriterEffect.Run(dialogue, textLabel);

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
    }
}
