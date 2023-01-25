using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBlock : MonoBehaviour
{
    [HideInInspector] public bool isDark;

    public RoomSet roomSet;

    private SpriteRenderer spriteRenderer;
    private Sprite currentSprite;

    private void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        currentSprite = spriteRenderer.sprite;
        if (currentSprite != roomSet.roomSprites[0])
        { 
            spriteRenderer.sprite = roomSet.roomSprites[0];
            currentSprite = spriteRenderer.sprite;
        }
    }

    private void Update()
    {
        if (isDark && currentSprite != roomSet.roomSprites[1])
        {
            spriteRenderer.sprite = roomSet.roomSprites[1];
            currentSprite = spriteRenderer.sprite;
        }

        else if (!isDark && currentSprite != roomSet.roomSprites[0])
        {
            spriteRenderer.sprite = roomSet.roomSprites[0];
            currentSprite = spriteRenderer.sprite;
        }
    }
}
