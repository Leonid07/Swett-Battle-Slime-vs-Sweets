using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOad : MonoBehaviour
{
    public float time;

    private void Start()
    {
        StartCoroutine(qwe());
    }
    IEnumerator qwe()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
