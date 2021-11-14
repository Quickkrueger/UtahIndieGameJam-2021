using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Vector3Data spawnPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CharacterController>() != null)
        {
            spawnPoint.SetValue(transform.position);
        }
    }
}
