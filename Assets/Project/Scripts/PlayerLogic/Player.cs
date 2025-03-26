using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Planer _planer;
    private PlaneWalker _walker;
    private Camera _camera;
    [SerializeField] private float shield;
    private bool _isDead;

    [SerializeField] private GameObject[] _boxes;
    [SerializeField] private Transform[] _rayFromCamera;
    
    [SerializeField] private Transform _room;
    [SerializeField] private GameObject WIN_Icon, OverMenu;

    void Start()
    {
        _planer = GetComponent<Planer>();
        _walker = GetComponent<PlaneWalker>();
        _camera = Camera.main;
        
        _boxes = GameObject.FindGameObjectsWithTag("WhiteBox");
        
        OverMenu.SetActive(true);
        OverMenu.SetActive(false);
    }

    void Update()
    {
        if(!_walker.IsUsed()) _walker.CheckWay(_planer.DropRaycast());

        RaycastHit[] Hits;
        Hits = Physics.RaycastAll(_camera.transform.position, _camera.transform.forward, 2.47f);
        for (int i = 0; i < _boxes.Length; i++)
        {
            bool isAvai = true;
            for (int j = 0; j < Hits.Length; j++)
            {
                if (_boxes[i] == Hits[j].collider.gameObject && Hits[j].collider.tag == "WhiteBox") isAvai = false;
            }

            RaycastHit HallHit;
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out HallHit, 2.47f))
            {
                if (isAvai && HallHit.transform != _boxes[i].transform.parent) _boxes[i].SetActive(true);
                else _boxes[i].SetActive(false);
            }
        }

        if (shield > 0) shield -= Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Finish") StartCoroutine(Finish());
        if (collision.collider.tag == "BlackBox" && shield <= 0 && !_isDead)
        {
            _isDead = true;
            OverMenu.SetActive(true);
            OverMenu.transform.localScale = new Vector3(0, 0, 0);
            StartCoroutine(GameOver());
        }
    }

    public void OnShield()
    {
        shield = 1;
        _isDead = false;
        PlaneWalker walker = GetComponent<PlaneWalker>();
        walker.enabled = true;
    }

    IEnumerator GameOver()
    {
        while (true)
        {
            _camera.transform.SetParent(_room);
            PlaneWalker walker = GetComponent<PlaneWalker>();
            walker.enabled = false;
            yield return new WaitForSeconds(0.1f);
            OverMenu.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
            yield return new WaitForSeconds(0.3f);
            break;
        }
    }

    IEnumerator Finish()
    {
        while (true)
        {
            WIN_Icon.SetActive(true);
            WIN_Icon.transform.localScale = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(0.1f);
            WIN_Icon.transform.DOScale(new Vector3(8, 8, 8), 0.1f);
            yield return new WaitForSeconds(0.3f);
            WIN_Icon.transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            yield return new WaitForSeconds(0.2f);
            WIN_Icon.SetActive(false);
            SceneManager.LoadScene("StartScene");
            break;
        }
    }
}