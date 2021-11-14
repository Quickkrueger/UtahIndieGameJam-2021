using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOverTime : MonoBehaviour
{
    public void Disable(float time)
    {
        StartCoroutine(DisableAsync(time));
    }

    IEnumerator DisableAsync(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
