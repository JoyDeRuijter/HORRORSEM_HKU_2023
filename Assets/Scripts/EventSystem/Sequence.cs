using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sequence
{
    public int ID;
    public SequenceState state;
    public abstract bool IsTriggered();
    public abstract IEnumerator Run();

    protected GameManager gameManager;
}

public enum SequenceState { WAITING, RUNNING, DONE }