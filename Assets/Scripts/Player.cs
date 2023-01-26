using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private bool isUsingStairsOrLadder;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!isUsingStairsOrLadder)
        { 
            float moveHorizontal = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveHorizontal * speed, 0);
        }
    }

    public IEnumerator UseStairsOrLadder(GameObject _roomObject, bool _isGoingUp, Vector2 _lowPosition, Vector2 _highPosition)
    {
        isUsingStairsOrLadder = true;
        Vector3 lowPos = new Vector3(_lowPosition.x, _lowPosition.y, 0f);
        Vector3 highPos = new Vector3(_highPosition.x, _highPosition.y, 0f);
        
        rb.isKinematic = true;
        bc.isTrigger = true;
        transform.parent = _roomObject.transform;

        if (_isGoingUp)
        {
            transform.DOLocalMove(lowPos, 1f);
            yield return new WaitForSeconds(1f);
            transform.DOLocalMove(highPos, 2.5f);
            yield return new WaitForSeconds(2.5f);
        }
        else
        {
            transform.DOMove(highPos, 1f);
            yield return new WaitForSeconds(1f);
            transform.DOMove(lowPos, 2.5f);
            yield return new WaitForSeconds(2.5f);
        }

        transform.parent = null;
        bc.isTrigger = false;
        rb.isKinematic = false;

        isUsingStairsOrLadder = false;
    }
}
