using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private Transform level, thisCube;
    [SerializeField] private GameObject cubeCollider;
    void Start()
    {
        thisCube = this.transform;
        GameObject col = Instantiate(cubeCollider, thisCube.position, thisCube.rotation);
        col.transform.parent = level;
        col.transform.localScale = new Vector3(1, 1, 1);
        col.transform.parent = thisCube;
    }
}
