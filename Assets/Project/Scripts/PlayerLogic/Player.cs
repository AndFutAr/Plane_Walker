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
    [SerializeField] private Transform _room;
    
    [SerializeField] private Transform _delBox;

    [SerializeField] private GameObject WIN_Icon, OverMenu;

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
        Vector3 fwd = transform.TransformDirection(Vector3.down);
        
        if (Physics.Raycast(_camera.transform.position, fwd, out HallHit, 2.47f))
        {
            Debug.DrawLine(_camera.transform.position, HallHit.point, Color.red);
            if(HallHit.collider.gameObject.tag == "Player" && _delBox != null) { _delBox.gameObject.SetActive(true); _delBox = null; }
            else if(HallHit.collider.gameObject.tag == "WhiteBox")
            { 
                if(_delBox != null && _delBox != HallHit.transform) _delBox.gameObject.SetActive(true);
                _delBox = HallHit.collider.gameObject.transform;
                _delBox.gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Finish") StartCoroutine(Finish());
        if (collision.collider.tag == "BlackBox") GameOver();
    }

    public void GameOver()
    {
        _camera.transform.SetParent(_room);
        gameObject.SetActive(false);
        OverMenu.SetActive(true);
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
