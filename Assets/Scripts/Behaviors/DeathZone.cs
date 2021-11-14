using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Vector3Data spawnPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController player = collision.GetComponent<CharacterController>();
        if(player != null)
        {
            player.DeathSequence(spawnPoint.value);
        }
    }
}
