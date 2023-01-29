using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueObject", menuName = "ScriptableObjects/Dialogue/DialogueObject", order = 1)]
public class DialogueObject : ScriptableObject
{
    public SpeakerProfile speaker;
    [TextArea] public string[] dialogue;
    [HideInInspector] public bool isDone; 
}
