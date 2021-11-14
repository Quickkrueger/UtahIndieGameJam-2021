using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public Vector3Data spawnpoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController player = collision.GetComponent<CharacterController>();

        if(player != null && !player.characterControls.isDurable)
        {
            player.DeathSequence(spawnpoint.value);
        }

    }
}
