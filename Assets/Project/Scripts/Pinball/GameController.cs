using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _ball, parent;
    private int k = 20;
    
    void Start() => StartCoroutine(Spawner());
    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            float randX = Random.Range(-2.0f, 2.0f);
            var ball = Instantiate(_ball, new Vector3(randX, 5.5f, 3.052442f),Quaternion.identity);
            ball.transform.SetParent(parent.transform);
            k--;
            if(k <= 0) break;
        }
    }
}
