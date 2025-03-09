using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEffect : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) ToRotate(0);
        else if (Input.GetKey(KeyCode.S)) ToRotate(180);
        else if (Input.GetKey(KeyCode.A)) ToRotate(-90);    
        else if (Input.GetKey(KeyCode.D)) ToRotate(90);
    }

    public void ToRotate(int pos) => transform.rotation = Quaternion.Euler(0, pos, 0);
}
    