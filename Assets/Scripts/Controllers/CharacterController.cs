using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public class CharacterController : MonoBehaviour
{
    public CollectionSO collectedCharacters;
    public CharacterData currentCharacterData;
    public ControlsData characterControls;
    public CharacterAppearance characterAppearance;

    public UnityEvent StartSwap;
    public UnityEvent EndSwap;

    private Rigidbody2D characterRB;
    private CapsuleCollider2D characterCollider;

    private Coroutine groundChecker;
    private Coroutine climbChecker;
    private Coroutine walkPressed;

    private InputActionMap characterInputs;

    private bool attacking = false;

    private void Start()
    {

        characterRB = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<CapsuleCollider2D>();
        characterInputs = characterControls.inputs;

        collectedCharacters.OnSelectedChanged += SwapToNewCharacter;

        collectedCharacters.SetSelectedCollectable(currentCharacterData);

        characterAppearance.SwapSprites(currentCharacterData);

        InitializeControls();
    }

    void InitializeControls()
    {
        for (int i = 0; i < characterInputs.actions.Count; i++)
        {
            switch (characterInputs.actions[i].name)
            {
                case "Walk":
                    characterInputs.actions[i].performed += Walk;
                    characterInputs.actions[i].canceled += Walk;
                    characterInputs.actions[i].Enable();
                    break;
                case "Jump":
                    characterInputs.actions[i].performed += Jump;
                    characterInputs.actions[i].canceled += Jump;
                    characterInputs.actions[i].Enable();
                    break;
                case "Attack":
                    characterInputs.actions[i].performed += Attack;
                    characterInputs.actions[i].canceled += Attack;
                    characterInputs.actions[i].Enable();
                    break;
                case "Fly":
                    characterInputs.actions[i].performed += Fly;
                    characterInputs.actions[i].canceled += Fly;
                    characterInputs.actions[i].Enable();
                    break;
                case "Swap Character":
                    characterInputs.actions[i].performed += SwapCharacter;
                    characterInputs.actions[i].canceled += SwapCharacter;
                    characterInputs.actions[i].Enable();
                    break;
            }

        }
    }

    private void Walk(CallbackContext value)
    {

        float axis = value.ReadValue<float>();

        if (value.performed &&  Mathf.Abs(axis) > 0f)
        {
            characterAppearance.StartAnimation(value.action.name);
            attacking = false;

            walkPressed = StartCoroutine(WalkPressed(axis));
            if (characterControls.canClimb)
            {
                climbChecker = StartCoroutine(CheckForClimb(axis));
            }

        }
        else if(value.canceled)
        {
            characterAppearance.Stand();

            StopCoroutine(walkPressed);
            characterRB.velocity = new Vector2(0f, characterRB.velocity.y);

            if (climbChecker != null)
            {
                StopCoroutine(climbChecker);
            }

            climbChecker = null;
            groundChecker = StartCoroutine(CheckIfGrounded());
        }

    }

    IEnumerator WalkPressed(float axis)
    {
        characterRB.velocity = new Vector2(currentCharacterData.characterStats.speed * axis, characterRB.velocity.y);
        characterAppearance.SetDirection(axis);

        yield return new WaitForSeconds(Time.deltaTime * 4);

        characterAppearance.SetDirection(axis);

        if (!characterAppearance.CheckForAnimation())
        {
            characterAppearance.StartAnimation("Walk");
        }

        walkPressed = StartCoroutine(WalkPressed(axis));

    }

    private void Jump(CallbackContext value)
    {
         if (characterControls.HasMoreJumps() && !characterControls.canFly)
        {
            float axis = value.ReadValue<float>();
            if (axis > 0f)
            {
                attacking = false;
                characterAppearance.StartAnimation(value.action.name);

                characterRB.velocity = new Vector2(characterRB.velocity.x, Mathf.Clamp(characterRB.velocity.y, 0f, float.MaxValue));
                characterRB.AddForce(Vector2.up * currentCharacterData.characterStats.jumpHeight);
                if (groundChecker == null)
                {
                    groundChecker = StartCoroutine(CheckIfGrounded());
                }
            }
            else if(groundChecker != null)
            {
                characterRB.velocity = new Vector2(characterRB.velocity.x, Mathf.Clamp(characterRB.velocity.y, float.MinValue, 0f));
                characterControls.UseJump();
            }
        }
               
    }

    private void Attack(CallbackContext value)
    {
        if(value.performed && currentCharacterData.GetAnimationByName(value.action.name) != null)
        {
            characterAppearance.StartAnimation(value.action.name);
            attacking = true;
        }
        if (value.canceled)
        {
            characterAppearance.Stand();
            attacking = false;
        }
    }

    private void Fly(CallbackContext value)
    {
        if (characterControls.canFly && value.performed)
        {
            characterRB.AddForce(Vector2.up * currentCharacterData.characterStats.jumpHeight);
            attacking = false;
        }
    }

    private void SwapCharacter(CallbackContext value)
    {

        if(value.performed && collectedCharacters.collectables.Count > 1)
        {
            attacking = false;
            StartSwap.Invoke();
        }
        else if(value.canceled)
        {
            EndSwap.Invoke();
        }
    }

    private void SwapToNewCharacter(CollectableSO newCharacter)
    {
        currentCharacterData = (CharacterData)newCharacter;
        characterControls.canClimb = currentCharacterData.characterStats.climbs;
        characterControls.canFly = currentCharacterData.characterStats.flies;
        characterControls.canClimb = currentCharacterData.characterStats.climbs;
        characterControls.maxJumps = currentCharacterData.characterStats.numJumps;
        characterControls.ResetJumps();
        characterAppearance.SwapSprites(currentCharacterData);

        Vector2 size = GetComponent<SpriteRenderer>().sprite.bounds.size;
        
        if(currentCharacterData.name != "Human")
        {
            characterCollider.direction = CapsuleDirection2D.Horizontal;
        }
        else
        {
            characterCollider.direction = CapsuleDirection2D.Vertical;
        }

        characterCollider.size = size;

        EndSwap.Invoke();
    }

    IEnumerator CheckIfGrounded()
    {
        bool groundFlag1 = false;
        if (Mathf.Abs(characterRB.velocity.y) < 0.01f)
        {
            groundFlag1 = true;
        }
        yield return new WaitForFixedUpdate();
        if (groundFlag1 && Mathf.Abs(characterRB.velocity.y) < 0.01f)
        {
            groundFlag1 = false;
            characterControls.ResetJumps();
            characterAppearance.Stand();
            groundChecker = null;
        }
        else
        {
            groundFlag1 = false;
            groundChecker = StartCoroutine(CheckIfGrounded());
        }
    }

    IEnumerator CheckForClimb(float axis)
    {
        bool climbFlag1 = false;
        if(Mathf.Abs(characterRB.velocity.x) < 0.01f)
        {
            climbFlag1 = true;
        }
        yield return new WaitForFixedUpdate();
        if (climbFlag1 && Mathf.Abs(characterRB.velocity.x) < 0.01f)
        {
            characterRB.velocity = new Vector2(characterRB.velocity.x, 5f);
            characterControls.ResetJumps();
            if (groundChecker != null)
            {
                StopCoroutine(groundChecker);
                groundChecker = null;
            }
        }

        climbChecker = StartCoroutine(CheckForClimb(axis));
    }

    public void DeathSequence(Transform respawnLocation)
    {
        transform.position = respawnLocation.position;
        collectedCharacters.NextCollectable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (attacking)
        {

        }
    }

    private void OnDestroy()
    {

        collectedCharacters.OnSelectedChanged -= SwapToNewCharacter;

        for (int i = 0; i < characterInputs.actions.Count; i++)
        {
            switch (characterInputs.actions[i].name)
            {
                case "Walk":
                    characterInputs.actions[i].performed -= Walk;
                    characterInputs.actions[i].canceled -= Walk;
                    break;
                case "Jump":
                    characterInputs.actions[i].performed -= Jump;
                    characterInputs.actions[i].canceled -= Jump;
                    break;
                case "Attack":
                    characterInputs.actions[i].performed -= Attack;
                    characterInputs.actions[i].canceled -= Attack;
                    break;
                case "Fly":
                    characterInputs.actions[i].performed -= Fly;
                    characterInputs.actions[i].canceled -= Fly;
                    break;
                case "Swap Character":
                    characterInputs.actions[i].performed -= SwapCharacter;
                    characterInputs.actions[i].canceled -= SwapCharacter;
                    break;
            }

        }
    }
}
