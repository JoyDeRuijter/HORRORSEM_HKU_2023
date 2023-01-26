using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeakerProfile", menuName = "ScriptableObjects/Dialogue/SpeakerProfile", order = 2)]
public class SpeakerProfile : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite avatar;
}
