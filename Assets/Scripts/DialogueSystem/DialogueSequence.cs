using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSequence", menuName = "ScriptableObjects/Dialogue/DialogueSequence", order = 2)]
public class DialogueSequence : ScriptableObject
{
    public string sequenceName;
    public DialogueObject[] dialogueObjects;
}
