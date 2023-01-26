using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [HideInInspector] public bool isUsingStairsOrLadder;

    [SerializeField] private float speed;

    private BoxCollider2D bc;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        HandleHorizontalMovement();
    }

    // Coroutine that moves the player up or down a ladder or set of stairs
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
            transform.DOLocalMove(highPos, 1f);
            yield return new WaitForSeconds(1f);
            transform.DOLocalMove(lowPos, 2.5f);
            yield return new WaitForSeconds(2.5f);
        }

        transform.parent = null;
        bc.isTrigger = false;
        rb.isKinematic = false;

        isUsingStairsOrLadder = false;
    }

    // Handles the horizontal input and uses it to move the player horizontally
    private void HandleHorizontalMovement()
    {
        if (!isUsingStairsOrLadder)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveHorizontal * speed, 0);
        }
    }
}
