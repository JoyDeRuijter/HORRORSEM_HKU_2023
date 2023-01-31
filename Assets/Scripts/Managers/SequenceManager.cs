using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{ 
    [HideInInspector] public Queue<Sequence> sequences = new Queue<Sequence>();

    private void Awake()
    {
        InitializeSequences();
    }

    private void Update()
    {
        if (sequences.Count != 0)
            StartCoroutine(RunSequences());
    }

    private void InitializeSequences()
    {
        FirstSequence firstSequence = new FirstSequence();
        sequences.Enqueue(firstSequence);
        SecondSequence secondsequence = new SecondSequence();
        sequences.Enqueue(secondsequence);
    }

    private IEnumerator RunSequences()
    {
        if (sequences.Count >= 1 && sequences.Peek().IsTriggered() && sequences.Peek().state == SequenceState.WAITING)
        {
            sequences.Peek().state = SequenceState.RUNNING;
            StartCoroutine(sequences.Peek().Run());
            yield return new WaitUntil(() => sequences.Peek().state == SequenceState.DONE);
            sequences.Dequeue();
        }

        if (sequences.Count >= 1 && sequences.Peek().state == SequenceState.DONE)
            sequences.Dequeue();
    }
}
