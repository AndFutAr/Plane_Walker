using UnityEngine;

public class DotLogic : MonoBehaviour
{
    [SerializeField] private Transform targetDot;

    void Update()
    {
        transform.position = targetDot.position;
    }
}
