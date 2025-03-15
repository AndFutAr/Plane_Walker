using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlaneWalker : MonoBehaviour
{
    private Rigidbody rb;
    private Transform trans;

    [SerializeField] private char VerHere = ' ', VerDir = ' ';
    [SerializeField] private char StrHere = ' ', StrDir = ' ';
    [SerializeField] private char SideHere = ' ', SideDir = ' ';

    [SerializeField] private char nextVerHere = ' ', nextVerDir = ' ';
    [SerializeField] private char nextStrHere = ' ', nextStrDir = ' ';
    [SerializeField] private char nextSideHere = ' ', nextSideDir = ' ';

    [SerializeField] private float rotX = -90, rotY = 0, rotZ = 0;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float coolDown = 1f;
    public float Cooldown => coolDown;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();

        rotX = (int)transform.rotation.eulerAngles.x;
        rotY = (int)transform.rotation.eulerAngles.y;
        rotZ = (int)transform.rotation.eulerAngles.z;
    }

    public void SetPlane(char plane, char pVector)
    {
        VerHere = plane;
        VerDir = pVector;
    }

    public void SetDirection(char dir, char dVector)
    {
        StrHere = dir;
        StrDir = dVector;
    }

    public void SetLine(char line, char lVector)
    {
        SideHere = line;
        SideDir = lVector;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) transform.position += transform.forward * moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S)) transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.A)) transform.position -= transform.right * moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.D)) transform.position += transform.right * moveSpeed * Time.deltaTime;

        var FrezRotX = RigidbodyConstraints.FreezeRotationX;
        var FrezRotY = RigidbodyConstraints.FreezeRotationY;
        var FrezRotZ = RigidbodyConstraints.FreezeRotationZ;

        switch (VerHere)
        {
            case 'z': rb.constraints = RigidbodyConstraints.FreezePositionZ | FrezRotX | FrezRotY | FrezRotZ; break;
            case 'y': rb.constraints = RigidbodyConstraints.FreezePositionY | FrezRotX | FrezRotY | FrezRotZ; break;
            case 'x': rb.constraints = RigidbodyConstraints.FreezePositionX | FrezRotX | FrezRotY | FrezRotZ; break;
        }

        if (coolDown > 0) coolDown -= 2 * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E)) Debug.Log(transform.rotation.eulerAngles);
        else if (Input.GetKeyDown(KeyCode.Q)) Debug.Log(rotX + ", " + rotY + ", " + rotZ);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "ToXZ" && coolDown <= 0)
        {
            coolDown = 1f;
            
            if (Input.GetKey(KeyCode.W)) DefinePlane("txz", 'W');
            else if (Input.GetKey(KeyCode.A)) DefinePlane("txz", 'A');
            else if (Input.GetKey(KeyCode.S)) DefinePlane("txz", 'S');
            else if (Input.GetKey(KeyCode.D)) DefinePlane("txz", 'D');
            
            if (((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && SideHere == 'y') ||
                ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && StrHere == 'y'))
            {
                if (VerHere == 'x' && VerDir == '+')
                    transform.position = new Vector3(collision.transform.position.x - 0.2f, transform.position.y,
                        collision.transform.position.z);
                else if (VerHere == 'x' && VerDir == '-')
                    transform.position = new Vector3(collision.transform.position.x + 0.2f, transform.position.y,
                        collision.transform.position.z);
                else if (VerHere == 'z' && VerDir == '+')
                    transform.position = new Vector3(collision.transform.position.x, transform.position.y,
                        collision.transform.position.z - 0.2f);
                else if (VerHere == 'z' && VerDir == '-')
                    transform.position = new Vector3(collision.transform.position.x, transform.position.y,
                        collision.transform.position.z + 0.2f);
            }
            PlayerRotate();
            //  StartCoroutine(StepToPlane());
        }
        else if (collision.collider.tag == "FromXZ" && coolDown <= 0)
        {
            coolDown = 1f;
            
            if (Input.GetKey(KeyCode.W)) DefinePlane("fxz", 'W');
            else if (Input.GetKey(KeyCode.A)) DefinePlane("fxz", 'A');
            else if (Input.GetKey(KeyCode.S)) DefinePlane("fxz", 'S');
            else if (Input.GetKey(KeyCode.D)) DefinePlane("fxz", 'D');
            PlayerRotate();
            //StartCoroutine(StepToPlane());
        }
        else if (collision.collider.tag == "ToXY" && coolDown <= 0)
        {
            coolDown = 1f;
            
            if (Input.GetKey(KeyCode.W)) DefinePlane("txy", 'W');
            else if (Input.GetKey(KeyCode.A)) DefinePlane("txy", 'A');
            else if (Input.GetKey(KeyCode.S)) DefinePlane("txy", 'S');
            else if (Input.GetKey(KeyCode.D)) DefinePlane("txy", 'D');
            
            if (((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && SideHere == 'z') ||
                ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && StrHere == 'z'))
            {
                if (VerHere == 'x' && VerDir == '+')
                    transform.position = new Vector3(collision.transform.position.x - 0.2f,
                        collision.transform.position.y, transform.position.z);
                else if (VerHere == 'x' && VerDir == '-')
                    transform.position = new Vector3(collision.transform.position.x + 0.2f,
                        collision.transform.position.y, transform.position.z);
                else if (VerHere == 'y' && VerDir == '+')
                    transform.position = new Vector3(collision.transform.position.x,
                        collision.transform.position.y - 0.2f, transform.position.z);
                else if (VerHere == 'y' && VerDir == '-')
                    transform.position = new Vector3(collision.transform.position.x,
                        collision.transform.position.y + 0.2f, transform.position.z);
            }
            PlayerRotate();
            //StartCoroutine(StepToPlane());
        }
        else if (collision.collider.tag == "FromXY" && coolDown <= 0)
        {
            coolDown = 1f;
            
            if (Input.GetKey(KeyCode.W)) DefinePlane("fxy", 'W');
            else if (Input.GetKey(KeyCode.A)) DefinePlane("fxy", 'A');
            else if (Input.GetKey(KeyCode.S)) DefinePlane("fxy", 'S');
            else if (Input.GetKey(KeyCode.D)) DefinePlane("fxy", 'D');
            PlayerRotate();
            //StartCoroutine(StepToPlane());
        }
        else if (collision.collider.tag == "ToZY" && coolDown <= 0)
        {
            coolDown = 1f;

            if (Input.GetKey(KeyCode.W)) DefinePlane("tzy", 'W');
            else if (Input.GetKey(KeyCode.A)) DefinePlane("tzy", 'A');
            else if (Input.GetKey(KeyCode.S)) DefinePlane("tzy", 'S');
            else if (Input.GetKey(KeyCode.D)) DefinePlane("tzy", 'D');

            if (((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && SideHere == 'x') ||
                ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && StrHere == 'x'))
            {
                if (VerHere == 'z' && VerDir == '+')
                    transform.position = new Vector3(transform.position.x, collision.transform.position.y,
                        collision.transform.position.z - 0.2f);
                else if (VerHere == 'z' && VerDir == '-')
                    transform.position = new Vector3(transform.position.x, collision.transform.position.y,
                        collision.transform.position.z + 0.2f);
                else if (VerHere == 'y' && VerDir == '+')
                    transform.position = new Vector3(transform.position.x, collision.transform.position.y - 0.2f,
                        collision.transform.position.z);
                else if (VerHere == 'y' && VerDir == '-')
                    transform.position = new Vector3(transform.position.x, collision.transform.position.y + 0.2f,
                        collision.transform.position.z);
            }
            PlayerRotate();
            //StartCoroutine(StepToPlane());
        }
        else if (collision.collider.tag == "FromZY" && coolDown <= 0)
        {
            coolDown = 1f;

            if (Input.GetKey(KeyCode.W)) DefinePlane("fzy", 'W');
            else if (Input.GetKey(KeyCode.A)) DefinePlane("fzy", 'A');
            else if (Input.GetKey(KeyCode.S)) DefinePlane("fzy", 'S');
            else if (Input.GetKey(KeyCode.D)) DefinePlane("fzy", 'D');
            PlayerRotate();
            //StartCoroutine(StepToPlane());
        }
        transform.rotation = Quaternion.Euler(rotX, rotY, rotZ); 
    }

    IEnumerator StepToPlane()
    {
        while (true)
        {
            transform.DORotate(new Vector3(rotX, rotY, rotZ), 0.1f);
            yield return new WaitForSeconds(0.1f);
            Debug.Log(rotX + ", " + rotY + ", " + rotZ);
            break;
        }
    }

    void PlayerRotate()
    {
        if (nextVerHere == 'y' && nextVerDir == '+')
        {
            if (nextStrHere == 'x' && nextStrDir == '+' && nextSideHere == 'z' && nextSideDir == '+') { rotX = 0; rotY = 90; rotZ = 0; } 
            else if (nextStrHere == 'x' && nextStrDir == '-' && nextSideHere == 'z' && nextSideDir == '-') { rotX = 0; rotY = -90; rotZ = 0; } 
            else if (nextStrHere == 'z' && nextStrDir == '+' && nextSideHere == 'x' && nextSideDir == '-') { rotX = 0; rotY = 0; rotZ = 0; } 
            else if (nextStrHere == 'z' && nextStrDir == '-' && nextSideHere == 'x' && nextSideDir == '+') { rotX = 0; rotY = 180; rotZ = 0; } 
        }
        else if (nextVerHere == 'y' && nextVerDir == '-')
        {
            if (nextStrHere == 'z' && nextStrDir == '+' && nextSideHere == 'x' && nextSideDir == '+') { rotX = 180; rotY = 180; rotZ = 0; } 
            else if (nextStrHere == 'z' && nextStrDir == '-' && nextSideHere == 'x' && nextSideDir == '-') { rotX = 180; rotY = 0; rotZ = 0; } 
            else if (nextStrHere == 'x' && nextStrDir == '+' && nextSideHere == 'z' && nextSideDir == '-') { rotX = 180; rotY = -90; rotZ = 0; } 
            else if (nextStrHere == 'x' && nextStrDir == '-' && nextSideHere == 'z' && nextSideDir == '+') { rotX = 180; rotY = 90; rotZ = 0; } 
        }
        else if (nextVerHere == 'x' && nextVerDir == '+')
        {
            if (nextStrHere == 'z' && nextStrDir == '+' && nextSideHere == 'y' && nextSideDir == '+') { rotX = 0; rotY = 0; rotZ = 270; }
            else if (nextStrHere == 'z' && nextStrDir == '-' && nextSideHere == 'y' && nextSideDir == '-') { rotX = 180; rotY = 0; rotZ = 270; }
            else if (nextStrHere == 'y' && nextStrDir == '+' && nextSideHere == 'z' && nextSideDir == '-') { rotX = 270; rotY = 0; rotZ = 270; }
            else if (nextStrHere == 'y' && nextStrDir == '-' && nextSideHere == 'z' && nextSideDir == '+') { rotX = 90; rotY = 0; rotZ = 270; }
        }
        else if (nextVerHere == 'x' && nextVerDir == '-')
        {
            if (nextStrHere == 'y' && nextStrDir == '+' && nextSideHere == 'z' && nextSideDir == '+') { rotX = 270; rotY = 0; rotZ = 90; }
            else if (nextStrHere == 'y' && nextStrDir == '-' && nextSideHere == 'z' && nextSideDir == '-') { rotX = 90; rotY = 0; rotZ = 90; }
            else if (nextStrHere == 'z' && nextStrDir == '+' && nextSideHere == 'y' && nextSideDir == '-') { rotX = 0; rotY = 0; rotZ = 90; }
            else if (nextStrHere == 'z' && nextStrDir == '-' && nextSideHere == 'y' && nextSideDir == '+') { rotX = 180; rotY = 0; rotZ = 90; }
        }
        else if (nextVerHere == 'z' && nextVerDir == '+')
        {
            if (nextStrHere == 'y' && nextStrDir == '+' && nextSideHere == 'x' && nextSideDir == '+') { rotX = 270; rotY = 90; rotZ = 90; } 
            else if (nextStrHere == 'y' && nextStrDir == '-' && nextSideHere == 'x' && nextSideDir == '-') { rotX = 90; rotY = 90; rotZ = 90; } 
            else if (nextStrHere == 'x' && nextStrDir == '+' && nextSideHere == 'y' && nextSideDir == '-') { rotX = 0; rotY = 90; rotZ = 90; }
            else if (nextStrHere == 'x' && nextStrDir == '-' && nextSideHere == 'y' && nextSideDir == '+') { rotX = 180; rotY = 90; rotZ = 90; }  
        }
        else if (nextVerHere == 'z' && nextVerDir == '-')
        {
            if (nextStrHere == 'x' && nextStrDir == '+' && nextSideHere == 'y' && nextSideDir == '+') { rotX = 180; rotY = 270; rotZ = 90; }
            else if (nextStrHere == 'x' && nextStrDir == '-' && nextSideHere == 'y' && nextSideDir == '-') { rotX = 0; rotY = 270; rotZ = 90; }
            else if (nextStrHere == 'y' && nextStrDir == '+' && nextSideHere == 'x' && nextSideDir == '-') { rotX = 270; rotY = 270; rotZ = 90; } 
            else if (nextStrHere == 'y' && nextStrDir == '-' && nextSideHere == 'x' && nextSideDir == '+') { rotX = 90; rotY = 270; rotZ = 90; } 
        }
    }
    private void DefinePlane(string col, char s)
    {
        switch (col)
        {
            case "txz":
            {
                if (StrHere == 'y')
                {
                    nextStrHere = StrHere;
                    nextStrDir = StrDir;
                    if ((StrDir == '+' && s == 'D') || (StrDir == '-' && s == 'A')) 
                    {
                        if (VerHere == 'x' && VerDir == '-') { nextVerHere = 'z'; nextVerDir = '-'; }
                        else if (VerHere == 'z' && VerDir == '-') { nextVerHere = 'x'; nextVerDir = '+'; }
                        else if (VerHere == 'x' && VerDir == '+') { nextVerHere = 'z'; nextVerDir = '+'; }
                        else if (VerHere == 'z' && VerDir == '+') { nextVerHere = 'x'; nextVerDir = '-'; }
                    }
                    else if ((StrDir == '-' && s == 'D') || (StrDir == '+' && s == 'A')) 
                    {
                        if (VerHere == 'x' && VerDir == '-') { nextVerHere = 'z'; nextVerDir = '+'; }
                        else if (VerHere == 'z' && VerDir == '+') { nextVerHere = 'x'; nextVerDir = '+'; }
                        else if (VerHere == 'x' && VerDir == '+') { nextVerHere = 'z'; nextVerDir = '-'; }
                        else if (VerHere == 'z' && VerDir == '-') { nextVerHere = 'x'; nextVerDir = '-'; }
                    }
                    nextSideHere = VerHere;
                    if (s == 'D') { nextSideDir = VerDir; }
                    else if (s == 'A') { if (VerDir == '-') nextSideDir = '+'; else if (VerDir == '+') nextSideDir = '-'; }
                }
                if (SideHere == 'y')
                {
                    nextSideHere = 'y';
                    nextSideDir = SideDir;
                    if ((SideDir == '+' && s == 'W') || (SideDir == '-' && s == 'S')) 
                    {
                        if (VerHere == 'x' && VerDir == '-') { nextVerHere = 'z'; nextVerDir = '-'; }
                        else if (VerHere == 'z' && VerDir == '-') { nextVerHere = 'x'; nextVerDir = '+'; }
                        else if (VerHere == 'x' && VerDir == '+') { nextVerHere = 'z'; nextVerDir = '+'; }
                        else if (VerHere == 'z' && VerDir == '+') { nextVerHere = 'x'; nextVerDir = '-'; }
                    }
                    else if ((SideDir == '-' && s == 'W') || (SideDir == '+' && s == 'S')) 
                    {
                        if (VerHere == 'x' && VerDir == '-') { nextVerHere = 'z'; nextVerDir = '+'; }
                        else if (VerHere == 'z' && VerDir == '+') { nextVerHere = 'x'; nextVerDir = '+'; }
                        else if (VerHere == 'x' && VerDir == '+') { nextVerHere = 'z'; nextVerDir = '-'; }
                        else if (VerHere == 'z' && VerDir == '-') { nextVerHere = 'x'; nextVerDir = '-'; }
                    }
                    nextStrHere = VerHere;
                    if (s == 'W') { if (VerDir == '+') nextStrDir = '-'; else if (VerDir == '-') nextStrDir = '+'; }
                    else if (s == 'S') { nextStrDir = VerDir; }
                }
            }break;
            case "fxz":
            {
                if (StrHere == 'y')
                {
                    nextStrHere = StrHere;
                    nextStrDir = StrDir;
                    if ((StrDir == '+' && s == 'D') || (StrDir == '-' && s == 'A')) 
                    {
                        if (VerHere == 'x' && VerDir == '-') { nextVerHere = 'z'; nextVerDir = '+'; }
                        else if (VerHere == 'z' && VerDir == '+') { nextVerHere = 'x'; nextVerDir = '+'; }
                        else if (VerHere == 'x' && VerDir == '+') { nextVerHere = 'z'; nextVerDir = '-'; }
                        else if (VerHere == 'z' && VerDir == '-') { nextVerHere = 'x'; nextVerDir = '-'; }
                    }
                    else if ((StrDir == '-' && s == 'D') || (StrDir == '+' && s == 'A')) 
                    {
                        if (VerHere == 'x' && VerDir == '-') { nextVerHere = 'z'; nextVerDir = '-'; }
                        else if (VerHere == 'z' && VerDir == '-') { nextVerHere = 'x'; nextVerDir = '+'; }
                        else if (VerHere == 'x' && VerDir == '+') { nextVerHere = 'z'; nextVerDir = '+'; }
                        else if (VerHere == 'z' && VerDir == '+') { nextVerHere = 'x'; nextVerDir = '-'; }
                    }
                    nextSideHere = VerHere;
                    if (s == 'D') { if (VerDir == '-') nextSideDir = '+'; else if (VerDir == '+') nextSideDir = '-'; }
                    else if (s == 'A') { nextSideDir = VerDir; }
                }
                if (SideHere == 'y')
                {
                    nextSideHere = 'y';
                    nextSideDir = SideDir;
                    if ((SideDir == '+' && s == 'W') || (SideDir == '-' && s == 'S')) 
                    {
                        if (VerHere == 'x' && VerDir == '-') { nextVerHere = 'z'; nextVerDir = '+'; }
                        else if (VerHere == 'z' && VerDir == '+') { nextVerHere = 'x'; nextVerDir = '+'; }
                        else if (VerHere == 'x' && VerDir == '+') { nextVerHere = 'z'; nextVerDir = '-'; }
                        else if (VerHere == 'z' && VerDir == '-') { nextVerHere = 'x'; nextVerDir = '-'; }
                    }
                    else if ((SideDir == '-' && s == 'W') || (SideDir == '+' && s == 'S')) 
                    {
                        if (VerHere == 'x' && VerDir == '-') { nextVerHere = 'z'; nextVerDir = '-'; }
                        else if (VerHere == 'z' && VerDir == '-') { nextVerHere = 'x'; nextVerDir = '+'; }
                        else if (VerHere == 'x' && VerDir == '+') { nextVerHere = 'z'; nextVerDir = '+'; }
                        else if (VerHere == 'z' && VerDir == '+') { nextVerHere = 'x'; nextVerDir = '-'; }
                    }
                    nextStrHere = VerHere;
                    if (s == 'W') { nextStrDir = VerDir; }
                    else if (s == 'S') { if (VerDir == '+') nextStrDir = '-'; else if (VerDir == '-') nextStrDir = '+'; }
                }
            }break;
            
            case "txy":
            {              
                if (StrHere == 'z')
                {
                    nextStrHere = StrHere;
                    nextStrDir = StrDir;
                    if ((StrDir == '+' && s == 'D') || (StrDir == '-' && s == 'A')) 
                    {
                        if (VerHere == 'x' && VerDir == '-') { nextVerHere = 'y'; nextVerDir = '+'; }
                        else if (VerHere == 'y' && VerDir == '+') { nextVerHere = 'x'; nextVerDir = '+'; }
                        else if (VerHere == 'x' && VerDir == '+') { nextVerHere = 'y'; nextVerDir = '-'; }
                        else if (VerHere == 'y' && VerDir == '-') { nextVerHere = 'x'; nextVerDir = '-'; }    
                    }
                    else if ((StrDir == '-' && s == 'D') || (StrDir == '+' && s == 'A')) 
                    {
                        if (VerHere == 'x' && VerDir == '-') { nextVerHere = 'y'; nextVerDir = '-'; }
                        else if (VerHere == 'y' && VerDir == '-') { nextVerHere = 'x'; nextVerDir = '+'; }
                        else if (VerHere == 'x' && VerDir == '+') { nextVerHere = 'y'; nextVerDir = '+'; }
                        else if (VerHere == 'y' && VerDir == '+') { nextVerHere = 'x'; nextVerDir = '-'; }
                    }
                    nextSideHere = VerHere;
                    if (s == 'D') { nextSideDir = VerDir; }
                    else if (s == 'A') { if (VerDir == '-') nextSideDir = '+'; else if (VerDir == '+') nextSideDir = '-'; }
                }
                if (SideHere == 'z')
                {
                    nextSideHere = 'z';
                    nextSideDir = SideDir;
                    if ((SideDir == '+' && s == 'W') || (SideDir == '-' && s == 'S')) 
                    {
                        if (VerHere == 'x' && VerDir == '-') { nextVerHere = 'y'; nextVerDir = '+'; }
                        else if (VerHere == 'y' && VerDir == '+') { nextVerHere = 'x'; nextVerDir = '+'; }
                        else if (VerHere == 'x' && VerDir == '+') { nextVerHere = 'y'; nextVerDir = '-'; }
                        else if (VerHere == 'y' && VerDir == '-') { nextVerHere = 'x'; nextVerDir = '-'; }
                    }
                    else if ((SideDir == '-' && s == 'W') || (SideDir == '+' && s == 'S')) 
                    {
                        if (VerHere == 'x' && VerDir == '-') { nextVerHere = 'y'; nextVerDir = '-'; }
                        else if (VerHere == 'y' && VerDir == '-') { nextVerHere = 'x'; nextVerDir = '+'; }
                        else if (VerHere == 'x' && VerDir == '+') { nextVerHere = 'y'; nextVerDir = '+'; }
                        else if (VerHere == 'y' && VerDir == '+') { nextVerHere = 'x'; nextVerDir = '-'; }
                    }
                    nextStrHere = VerHere;
                    if (s == 'W') { if (VerDir == '+') nextStrDir = '-'; else if (VerDir == '-') nextStrDir = '+'; }
                    else if (s == 'S') { nextStrDir = VerDir; }
                }
            }break;
            case "fxy":
            {
                if (StrHere == 'z')
                {
                    nextStrHere = StrHere;
                    nextStrDir = StrDir;
                    if ((StrDir == '+' && s == 'D') || (StrDir == '-' && s == 'A')) 
                    {
                        if (VerHere == 'x' && VerDir == '-') { nextVerHere = 'y'; nextVerDir = '-'; }
                        else if (VerHere == 'y' && VerDir == '-') { nextVerHere = 'x'; nextVerDir = '+'; }
                        else if (VerHere == 'x' && VerDir == '+') { nextVerHere = 'y'; nextVerDir = '+'; }
                        else if (VerHere == 'y' && VerDir == '+') { nextVerHere = 'x'; nextVerDir = '-'; }
                    }
                    else if ((StrDir == '-' && s == 'D') || (StrDir == '+' && s == 'A')) 
                    {
                        if (VerHere == 'x' && VerDir == '-') { nextVerHere = 'y'; nextVerDir = '+'; }
                        else if (VerHere == 'y' && VerDir == '+') { nextVerHere = 'x'; nextVerDir = '+'; }
                        else if (VerHere == 'x' && VerDir == '+') { nextVerHere = 'y'; nextVerDir = '-'; }
                        else if (VerHere == 'y' && VerDir == '-') { nextVerHere = 'x'; nextVerDir = '-'; } 
                    }
                    nextSideHere = VerHere;
                    if (s == 'D') { if (VerDir == '-') nextSideDir = '+'; else if (VerDir == '+') nextSideDir = '-'; }
                    else if (s == 'A') { nextSideDir = VerDir; }
                }
                if (SideHere == 'z')
                {
                    nextSideHere = 'z';
                    nextSideDir = SideDir;
                    if ((SideDir == '+' && s == 'W') || (SideDir == '-' && s == 'S')) 
                    {
                        if (VerHere == 'x' && VerDir == '-') { nextVerHere = 'y'; nextVerDir = '-'; }
                        else if (VerHere == 'y' && VerDir == '-') { nextVerHere = 'x'; nextVerDir = '+'; }
                        else if (VerHere == 'x' && VerDir == '+') { nextVerHere = 'y'; nextVerDir = '+'; }
                        else if (VerHere == 'y' && VerDir == '+') { nextVerHere = 'x'; nextVerDir = '-'; }
                    }
                    else if ((SideDir == '-' && s == 'W') || (SideDir == '+' && s == 'S')) 
                    {
                        if (VerHere == 'x' && VerDir == '-') { nextVerHere = 'y'; nextVerDir = '+'; }
                        else if (VerHere == 'y' && VerDir == '+') { nextVerHere = 'x'; nextVerDir = '+'; }
                        else if (VerHere == 'x' && VerDir == '+') { nextVerHere = 'y'; nextVerDir = '-'; }
                        else if (VerHere == 'y' && VerDir == '-') { nextVerHere = 'x'; nextVerDir = '-'; }
                    }
                    nextStrHere = VerHere;
                    if (s == 'W') { nextStrDir = VerDir; }
                    else if (s == 'S') { if (VerDir == '+') nextStrDir = '-'; else if (VerDir == '-') nextStrDir = '+'; }
                }
            }break;
            
            case "tzy":
            {
                if (StrHere == 'x')
                {
                    nextStrHere = StrHere;
                    nextStrDir = StrDir;
                    if ((StrDir == '+' && s == 'D') || (StrDir == '-' && s == 'A')) 
                    {
                        if (VerHere == 'z' && VerDir == '-') { nextVerHere = 'y'; nextVerDir = '-'; }
                        else if (VerHere == 'y' && VerDir == '-') { nextVerHere = 'z'; nextVerDir = '+'; }
                        else if (VerHere == 'z' && VerDir == '+') { nextVerHere = 'y'; nextVerDir = '+'; }
                        else if (VerHere == 'y' && VerDir == '+') { nextVerHere = 'z'; nextVerDir = '-'; }
                    }
                    else if ((StrDir == '-' && s == 'D') || (StrDir == '+' && s == 'A')) 
                    {
                        if (VerHere == 'z' && VerDir == '-') { nextVerHere = 'y'; nextVerDir = '+'; }
                        else if (VerHere == 'y' && VerDir == '+') { nextVerHere = 'z'; nextVerDir = '+'; }
                        else if (VerHere == 'z' && VerDir == '+') { nextVerHere = 'y'; nextVerDir = '-'; }
                        else if (VerHere == 'y' && VerDir == '-') { nextVerHere = 'z'; nextVerDir = '-'; }
                    }
                    nextSideHere = VerHere;
                    if (s == 'D') { nextSideDir = VerDir; }
                    else if (s == 'A') { if (VerDir == '-') nextSideDir = '+'; else if (VerDir == '+') nextSideDir = '-'; }
                }
                if (SideHere == 'x')
                {
                    nextSideHere = 'x';
                    nextSideDir = SideDir;
                    if ((SideDir == '+' && s == 'W') || (SideDir == '-' && s == 'S')) 
                    {
                        if (VerHere == 'z' && VerDir == '-') { nextVerHere = 'y'; nextVerDir = '-'; }
                        else if (VerHere == 'y' && VerDir == '-') { nextVerHere = 'z'; nextVerDir = '+'; }
                        else if (VerHere == 'z' && VerDir == '+') { nextVerHere = 'y'; nextVerDir = '+'; }
                        else if (VerHere == 'y' && VerDir == '+') { nextVerHere = 'z'; nextVerDir = '-'; }
                    }
                    else if ((SideDir == '-' && s == 'W') || (SideDir == '+' && s == 'S')) 
                    {
                        if (VerHere == 'z' && VerDir == '-') { nextVerHere = 'y'; nextVerDir = '+'; }
                        else if (VerHere == 'y' && VerDir == '+') { nextVerHere = 'z'; nextVerDir = '+'; }
                        else if (VerHere == 'z' && VerDir == '+') { nextVerHere = 'y'; nextVerDir = '-'; }
                        else if (VerHere == 'y' && VerDir == '-') { nextVerHere = 'z'; nextVerDir = '-'; }
                    }
                    nextStrHere = VerHere;
                    if (s == 'W') { if (VerDir == '+') nextStrDir = '-'; else if (VerDir == '-') nextStrDir = '+'; }
                    else if (s == 'S') { nextStrDir = VerDir; }
                }
            }break;
            case "fzy":
            {
                if (StrHere == 'x') 
                {
                    nextStrHere = StrHere;
                    nextStrDir = StrDir;
                    if ((StrDir == '+' && s == 'D') || (StrDir == '-' && s == 'A')) 
                    {
                        if (VerHere == 'z' && VerDir == '-') { nextVerHere = 'y'; nextVerDir = '+'; }
                        else if (VerHere == 'y' && VerDir == '+') { nextVerHere = 'z'; nextVerDir = '+'; }
                        else if (VerHere == 'z' && VerDir == '+') { nextVerHere = 'y'; nextVerDir = '-'; }
                        else if (VerHere == 'y' && VerDir == '-') { nextVerHere = 'z'; nextVerDir = '-'; }
                    }
                    else if ((StrDir == '-' && s == 'D') || (StrDir == '+' && s == 'A')) 
                    {
                        if (VerHere == 'z' && VerDir == '-') { nextVerHere = 'y'; nextVerDir = '-'; }
                        else if (VerHere == 'y' && VerDir == '-') { nextVerHere = 'z'; nextVerDir = '+'; }
                        else if (VerHere == 'z' && VerDir == '+') { nextVerHere = 'y'; nextVerDir = '+'; }
                        else if (VerHere == 'y' && VerDir == '+') { nextVerHere = 'z'; nextVerDir = '-'; }
                    }
                    nextSideHere = VerHere;
                    if (s == 'D') { if (VerDir == '-') nextSideDir = '+'; else if (VerDir == '+') nextSideDir = '-'; }
                    else if (s == 'A') { nextSideDir = VerDir; }
                }
                if (SideHere == 'x')
                {
                    nextSideHere = 'x';
                    nextSideDir = SideDir;
                    if ((SideDir == '+' && s == 'W') || (SideDir == '-' && s == 'S')) 
                    {
                        if (VerHere == 'z' && VerDir == '-') { nextVerHere = 'y'; nextVerDir = '+'; }
                        else if (VerHere == 'y' && VerDir == '+') { nextVerHere = 'z'; nextVerDir = '+'; }
                        else if (VerHere == 'z' && VerDir == '+') { nextVerHere = 'y'; nextVerDir = '-'; }
                        else if (VerHere == 'y' && VerDir == '-') { nextVerHere = 'z'; nextVerDir = '-'; }
                    }
                    else if ((SideDir == '-' && s == 'W') || (SideDir == '+' && s == 'S')) 
                    {
                        if (VerHere == 'z' && VerDir == '-') { nextVerHere = 'y'; nextVerDir = '-'; }
                        else if (VerHere == 'y' && VerDir == '-') { nextVerHere = 'z'; nextVerDir = '+'; }
                        else if (VerHere == 'z' && VerDir == '+') { nextVerHere = 'y'; nextVerDir = '+'; }
                        else if (VerHere == 'y' && VerDir == '+') { nextVerHere = 'z'; nextVerDir = '-'; }
                    }
                    nextStrHere = VerHere;
                    if (s == 'W') { nextStrDir = VerDir; }
                    else if (s == 'S') { if (VerDir == '+') nextStrDir = '-'; else if (VerDir == '-') nextStrDir = '+'; }
                }
            }break;
        }
    }
}