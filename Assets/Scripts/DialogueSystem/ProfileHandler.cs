using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileHandler : MonoBehaviour
{
    [SerializeField] private RectTransform profileBox;
    [SerializeField] private new TMP_Text name;
    [SerializeField] private Image icon;
    private SpeakerProfile currentProfile;

    public void UpdateProfile(DialogueObject dialogueObject)
    {
        if (dialogueObject.speaker != currentProfile || currentProfile == null)
        {
            currentProfile = dialogueObject.speaker;
            name.text = currentProfile.name;
            icon.sprite = currentProfile.avatar;
        }
    }

    public void ShowProfile()
    {
        profileBox.gameObject.SetActive(true);
    }

    public void HideProfile()
    {
        profileBox.gameObject.SetActive(false);
    }
}
