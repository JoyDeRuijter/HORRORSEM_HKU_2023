using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomSet", menuName = "ScriptableObjects/RoomSet", order = 1)]
public class RoomSet : ScriptableObject
{
    public string setName;
    public Sprite[] roomSprites = new Sprite[2];
    public Light[] lights;
}
