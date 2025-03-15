using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private int p = 1;
    [SerializeField] private bool isActive = false;
    
    public bool Active() => isActive;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(BeatBall());
    }

    IEnumerator BeatBall()
    {
        while (true)
        {
            isActive = true;
            transform.DORotate(new Vector3(0, 0, 120 * p), 0.1f);
            yield return new WaitForSeconds(0.2f);
            transform.DORotate(new Vector3(0, 0, 90 * p), 0.1f);
            yield return new WaitForSeconds(0.2f);
            isActive = false;
            break;
        }
    }
}
