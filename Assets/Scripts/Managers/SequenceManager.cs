using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{ 
    private Queue<Sequence> sequences = new Queue<Sequence>();

    private void Awake()
    {
        InitializeSequences();
    }

    private void Update()
    {
        if (sequences.Count == 0)
            return;

        for (int i = 0; i < sequences.Count; i++)
        {
            if (sequences.Peek().IsTriggered() && sequences.Peek().state == SequenceState.WAITING)
            {
                sequences.Peek().state = SequenceState.RUNNING;
                StartCoroutine(sequences.Peek().Run());
            }

            if (sequences.Peek().state == SequenceState.DONE)
                sequences.Dequeue();        
        }
    }

    private void InitializeSequences()
    {
        FirstSequence firstSequence = new FirstSequence();
        sequences.Enqueue(firstSequence);
        SecondSequence secondSequence = new SecondSequence();
        sequences.Enqueue(secondSequence);
    }
}
