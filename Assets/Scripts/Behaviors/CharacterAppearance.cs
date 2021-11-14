using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAppearance : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    CharacterData currentCharacter;

    Coroutine animationRoutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool CheckForAnimation()
    {
        if(animationRoutine != null)
        {
            return true;
        }
        return false;
    }

    public void SwapSprites(CharacterData newCharacter)
    {
        currentCharacter = newCharacter;
        Stand();
    }

    public void SetDirection(float axis)
    {
        if(axis < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(axis > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public void Stand()
    {
        if (animationRoutine != null)
        {
            StopCoroutine(animationRoutine);
            animationRoutine = null;
        }

        spriteRenderer.sprite = currentCharacter.spriteSheet.spriteSheet[currentCharacter.GetAnimationByName("Walk").spriteIndexes[0]];
    }

    public void StartAnimation(string animationName)
    {
        AnimationData animation = currentCharacter.GetAnimationByName(animationName);
        WaitForSeconds delay = new WaitForSeconds(animation.durationInSeconds / animation.spriteIndexes.Length);

        if(animationRoutine != null)
        {
            StopCoroutine(animationRoutine);
            animationRoutine = null;
        }

        animationRoutine = StartCoroutine(Animate(delay, animation));


    }

    IEnumerator Animate(WaitForSeconds delay, AnimationData animation)
    {
        for(int i = 0; i < animation.spriteIndexes.Length; i++)
        {
            if (animation.spriteIndexes[i] < currentCharacter.spriteSheet.spriteSheet.Length)
            {
                spriteRenderer.sprite = currentCharacter.spriteSheet.spriteSheet[animation.spriteIndexes[i]];
                yield return delay;
            }
        }

        animationRoutine = StartCoroutine(Animate(delay, animation));
    }
}
