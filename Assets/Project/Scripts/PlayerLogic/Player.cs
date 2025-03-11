using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Planer _planer;
    private PlaneWalker _walker;
    
    private Camera _camera;

    [SerializeField] private GameObject WIN_Icon;

    void Start()
    {
        _planer = GetComponent<Planer>();
        _walker = GetComponent<PlaneWalker>();
        
        _camera = Camera.main;
    }

    void Update()
    {
        if (_planer.Plane != ' ' && _planer.PlaneVector != ' ') _walker.SetPlane(_planer.Plane, _planer.PlaneVector);
        if (_planer.DirPlane != ' ' && _planer.DirVector != ' ')
            _walker.SetDirection(_planer.DirPlane, _planer.DirVector);
        if (_planer.LinePlane != ' ' && _planer.LineVector != ' ')
            _walker.SetLine(_planer.LinePlane, _planer.LineVector);

        RaycastHit HallHit;
        Ray HallRay = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(HallRay, out HallHit))
        {
            if (HallHit.transform.tag == "WhiteBox")
            {
                
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Finish") StartCoroutine(Finish());
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(0.3f);
        WIN_Icon.SetActive(true);
        WIN_Icon.transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.1f);
        WIN_Icon.transform.DOScale(new Vector3(8, 8, 8), 0.1f);
        yield return new WaitForSeconds(0.5f);
        WIN_Icon.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        yield return new WaitForSeconds(0.4f);
        WIN_Icon.SetActive(false);
        SceneManager.LoadScene("StartScene");
    }
}
