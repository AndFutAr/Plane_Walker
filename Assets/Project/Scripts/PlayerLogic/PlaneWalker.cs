using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlaneWalker : MonoBehaviour
{
    [SerializeField] private int direction;
    [SerializeField] private bool isUsed = false;
    public bool IsUsed() => isUsed;
    
    [SerializeField] private float moveSpeed = 2f;
    
    void Update()
    {
        if (direction == 0 && !isUsed)
        {
            if (Input.GetKey(KeyCode.W)) transform.position += transform.forward * moveSpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.S)) transform.position -= transform.forward * moveSpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.A)) transform.position -= transform.right * moveSpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.D)) transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
        else { if(!isUsed){ isUsed = true; StartCoroutine(StepToPlane());} }
    }

    public void CheckWay(int r) => direction = r;

    IEnumerator StepToPlane()
    {
        while (isUsed)
        {
            Vector3 playerPos = transform.position;

            Vector3 dirUp = transform.up;
            Vector3 dirForward = transform.forward;
            Vector3 dirRight = transform.right;
            if (direction == -1)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    transform.DOMove(transform.position + transform.forward * 0.1f, 0.1f);
                    yield return new WaitForSeconds(0.1f);
                    transform.DORotateQuaternion(Quaternion.LookRotation(-dirUp, dirForward), 0.5f);
                    yield return new WaitForSeconds(0.5f);
                    transform.DOMove(transform.position + transform.forward * 0.2f, 0.2f);
                    yield return new WaitForSeconds(0.2f);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    transform.DOMove(transform.position - transform.forward * 0.1f, 0.1f);
                    yield return new WaitForSeconds(0.1f);
                    transform.DORotateQuaternion(Quaternion.LookRotation(dirUp, -dirForward), 0.5f);
                    yield return new WaitForSeconds(0.5f);
                    transform.DOMove(transform.position - transform.forward * 0.2f, 0.2f);
                    yield return new WaitForSeconds(0.2f);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    transform.DOMove(transform.position - transform.right * 0.1f, 0.1f);
                    yield return new WaitForSeconds(0.1f);
                    transform.DORotateQuaternion(Quaternion.LookRotation(dirForward, -dirRight), 0.5f);
                    yield return new WaitForSeconds(0.5f);
                    transform.DOMove(transform.position - transform.right * 0.2f, 0.2f);
                    yield return new WaitForSeconds(0.2f);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    transform.DOMove(transform.position + transform.right * 0.1f, 0.1f);
                    yield return new WaitForSeconds(0.1f);
                    transform.DORotateQuaternion(Quaternion.LookRotation(dirForward, dirRight), 0.5f);
                    yield return new WaitForSeconds(0.5f);
                    transform.DOMove(transform.position + transform.right * 0.2f, 0.2f);
                    yield return new WaitForSeconds(0.2f);
                }
            }

            if (direction == 1)
            {
                if (Input.GetKey(KeyCode.W))
                    transform.DORotateQuaternion(Quaternion.LookRotation(dirUp, -dirForward), 0.5f);
                else if (Input.GetKey(KeyCode.S))
                    transform.DORotateQuaternion(Quaternion.LookRotation(-dirUp, dirForward), 0.5f);
                else if (Input.GetKey(KeyCode.A))
                    transform.DORotateQuaternion(Quaternion.LookRotation(dirForward, dirRight), 0.5f);
                else if (Input.GetKey(KeyCode.D))
                    transform.DORotateQuaternion(Quaternion.LookRotation(dirForward, -dirRight), 0.5f);
                yield return new WaitForSeconds(0.5f);
            }

            direction = 0;
            var rx = Math.Round((transform.rotation.eulerAngles.x + 10) / 90);
            var ry = Math.Round((transform.rotation.eulerAngles.y + 10) / 90);
            var rz = Math.Round((transform.rotation.eulerAngles.z + 10) / 90);
            int RX = (int)rx * 90;
            int RY = (int)ry * 90;
            int RZ = (int)rz * 90;
            transform.rotation = Quaternion.Euler(RX, RY, RZ);

            isUsed = false;
        }
    }
}