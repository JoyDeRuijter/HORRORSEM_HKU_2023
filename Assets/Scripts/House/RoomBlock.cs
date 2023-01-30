using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using System.Collections;

public class RoomBlock : MonoBehaviour
{
    [HideInInspector] public bool isScary;
    [HideInInspector] public bool playerIsHere;

    public int ID;
    public RoomSet roomSet;
    public Light2D roomLight;

    private SpriteRenderer spriteRenderer;
    private Sprite currentSprite;

    private void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        InitializeSprite();
    }

    private void Update()
    {
        UpdateSprite();
    }

    private void InitializeSprite()
    {
        currentSprite = spriteRenderer.sprite;
        if (currentSprite != roomSet.roomSprites[0])
        {
            spriteRenderer.sprite = roomSet.roomSprites[0];
            currentSprite = spriteRenderer.sprite;
        }
    }

    private void UpdateSprite()
    {
        if (isScary && currentSprite != roomSet.roomSprites[1])
        {
            spriteRenderer.sprite = roomSet.roomSprites[1];
            currentSprite = spriteRenderer.sprite;
        }

        else if (!isScary && currentSprite != roomSet.roomSprites[0])
        {
            spriteRenderer.sprite = roomSet.roomSprites[0];
            currentSprite = spriteRenderer.sprite;
        }
    }

    public void TurnLightOnSlow()
    {
        DOTween.To(() => roomLight.intensity, x => roomLight.intensity = x, 1f, 0.5f);
    }

    public void TurnLightOn()
    { 
        roomLight.intensity = 1f;
    }

    public void TurnLightOff()
    { 
        roomLight.intensity = 0f;
    }

    public IEnumerator FlickerLight()
    {
        TurnLightOn();
        yield return new WaitForSeconds(0.1f);
        TurnLightOff();
        yield return new WaitForSeconds(0.1f);
        TurnLightOn();
        yield return new WaitForSeconds(0.1f);
        TurnLightOff();
        yield return new WaitForSeconds(0.1f);
        TurnLightOn();
        yield return new WaitForSeconds(0.1f);
        TurnLightOff();
    }
}
