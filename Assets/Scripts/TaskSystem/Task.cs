using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskObject", menuName = "ScriptableObjects/Tasks/TaskObject", order = 1)]
public class Task : ScriptableObject
{
    public new string name;
    [TextArea] public string taskDescription;
    [HideInInspector] public bool isCompleted = false;
}
