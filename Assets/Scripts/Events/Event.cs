using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    public Event(int _id) => ID = _id;

    public int ID { get; private set; }
    public EventState State { get; private set; } = EventState.WAITING;

    protected GameManager gameManager = GameManager.Instance;
   
    public virtual void Run()
    {
        State = EventState.RUNNING;
    }

    public void SetToDone()
    { 
        State = EventState.DONE;
    }

    public IEnumerator Wait(float _seconds) { yield return new WaitForSeconds(_seconds); }
}

public enum EventState { WAITING, RUNNING, DONE}