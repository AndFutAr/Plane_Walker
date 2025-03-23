using UnityEngine;

public class Planer : MonoBehaviour
{
    public int DropRaycast()
    {
        RaycastHit HallHit;
        
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        
        Vector3 fwdW = transform.TransformDirection(Vector3.forward); 
        Vector3 fwdA = transform.TransformDirection(Vector3.left); 
        Vector3 fwdS = transform.TransformDirection(Vector3.back); 
        Vector3 fwdD = transform.TransformDirection(Vector3.right);

        if(Input.GetKey(KeyCode.W))
        {
            if (Physics.Raycast(transform.position, fwdW, out HallHit, 0.1f)) if(HallHit.transform.tag == "WhiteBox" || HallHit.transform.tag == "Untagged") return 1;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            if (Physics.Raycast(transform.position, fwdA, out HallHit, 0.1f)) if(HallHit.transform.tag == "WhiteBox" || HallHit.transform.tag == "Untagged") return 1;
        }   
        else if(Input.GetKey(KeyCode.S))
        {
            if (Physics.Raycast(transform.position, fwdS, out HallHit, 0.1f)) if(HallHit.transform.tag == "WhiteBox" || HallHit.transform.tag == "Untagged") return 1;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            if (Physics.Raycast(transform.position, fwdD, out HallHit, 0.1f)) if(HallHit.transform.tag == "WhiteBox" || HallHit.transform.tag == "Untagged") return 1;
        }
        Debug.DrawRay(transform.position, dwn * 0.3f, Color.red);
        if (!Physics.Raycast(transform.position, dwn, out HallHit, 0.3f)) return -1;
        return 0;
    }
}