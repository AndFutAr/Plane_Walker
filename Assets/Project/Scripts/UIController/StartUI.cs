using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    private Camera _cam;
    private bool isUsed = false;
    [SerializeField] private int thisLevel = 1, maxLevel = 1;
    
    [SerializeField] private Transform plObject;
    [SerializeField] private Transform setBox, backBox;
    [SerializeField] private Transform Levels;
    [SerializeField] private Transform[] level;
    [SerializeField] private GameObject[] levelText;

    [SerializeField] private AudioSource _phonSound;
    [SerializeField] private GameObject _soundHandler;

    void Awake()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            var pos = _soundHandler.transform.localPosition;
            _soundHandler.transform.localPosition = new Vector3(PlayerPrefs.GetFloat("volume") - 1, pos.y, pos.z);
        }
        else _soundHandler.transform.localPosition = new Vector3(-0.5f, -1f, 0);
    }
    void Start() => _cam = Camera.main;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit HallHit;
            Ray HallRay = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(HallRay, out HallHit))
            {
                if (HallHit.transform.tag == "StartGame")
                {
                    if (!isUsed)
                    {
                        isUsed = true;
                        StartCoroutine(StartGame());
                    }
                }

                if (HallHit.transform.tag == "BackLevel")
                {
                    if (!isUsed)
                    {
                        isUsed = true;
                        StartCoroutine(BackFromLevel());
                    }
                }
                if (HallHit.transform.tag == "OpenSettings")
                {
                    if (!isUsed)
                    {
                        isUsed = true;
                        StartCoroutine(OpenSettings());
                    }
                }

                if (HallHit.transform.tag == "BackMenu")
                {
                    if (!isUsed)
                    {
                        isUsed = true;
                        StartCoroutine(BackToMenu());
                    }
                }
                if(HallHit.transform.tag == "LastLevel") if (!isUsed && thisLevel > 1) { isUsed = true; StartCoroutine(LastLevel()); }
                if(HallHit.transform.tag == "NextLevel") if (!isUsed && thisLevel < maxLevel) { isUsed = true; StartCoroutine(NextLevel()); }

                if (HallHit.transform.tag == "ChooseLevel0") if (!isUsed) { isUsed = true; StartCoroutine(ChooseLevel(0)); }
                if (HallHit.transform.tag == "ChooseLevel1") if (!isUsed) { isUsed = true; StartCoroutine(ChooseLevel(1)); }
                if (HallHit.transform.tag == "ChooseLevel2") if (!isUsed) { isUsed = true; StartCoroutine(ChooseLevel(2)); }
                if (HallHit.transform.tag == "ChooseLevel3") if (!isUsed) { isUsed = true; StartCoroutine(ChooseLevel(3)); }
                if (HallHit.transform.tag == "ChooseLevel4") if (!isUsed) { isUsed = true; StartCoroutine(ChooseLevel(4)); }
                if (HallHit.transform.tag == "ChooseLevel5") if (!isUsed) { isUsed = true; StartCoroutine(ChooseLevel(5)); }

                if (HallHit.transform.tag == "QuatGame")
                {
                    if (!isUsed)
                    {
                        isUsed = true;
                        StartCoroutine(QuitGame());
                    }
                }

                if (HallHit.transform.tag == "Slider") _soundHandler.transform.position = HallHit.transform.position;
            }
        }
        _phonSound.volume = _soundHandler.transform.localPosition.x + 1;
    }

    IEnumerator StartGame()
    {
        while (isUsed)
        {
            yield return new WaitForSeconds(0.1f);
            plObject.DOMove(new Vector3(0, plObject.position.y, plObject.position.z), 0.5f);
            yield return new WaitForSeconds(0.5f);
            plObject.DOMove(new Vector3(plObject.position.x, plObject.position.y, 0.35f), 0.2f);
            yield return new WaitForSeconds(0.2f);
            plObject.DOMove(new Vector3(plObject.position.x, 2, plObject.position.z), 0.4f);
            yield return new WaitForSeconds(0.4f);
            plObject.DOMove(new Vector3(2, plObject.position.y, plObject.position.z), 0.5f);
            yield return new WaitForSeconds(0.5f);
            plObject.DOMove(new Vector3(plObject.position.x, 2.35f, plObject.position.z), 0.5f);
            yield return new WaitForSeconds(0.5f);
            plObject.DOMove(new Vector3(3.35f, plObject.position.y, plObject.position.z), 0.5f);
            _cam.transform.DOMove(new Vector3(4, _cam.transform.position.y, _cam.transform.position.z), 0.75f);
            yield return new WaitForSeconds(0.75f);
            plObject.DOMove(new Vector3(plObject.position.x, 0.65f, plObject.position.z), 0.5f);
            yield return new WaitForSeconds(0.1f);
            plObject.DOMove(new Vector3(plObject.position.x, 0.65f, plObject.position.z), 0.4f);
            yield return new WaitForSeconds(0.4f);
            
            isUsed = false;
        }
    }
    IEnumerator BackFromLevel()
    {
        while (isUsed)
        {
            yield return new WaitForSeconds(0.1f);
            plObject.DOMove(new Vector3(plObject.position.x, 2.35f, plObject.position.z), 0.5f);
            yield return new WaitForSeconds(0.5f);
            plObject.DOMove(new Vector3(2, plObject.position.y, plObject.position.z), 0.5f);
            _cam.transform.DOMove(new Vector3(0, _cam.transform.position.y, _cam.transform.position.z), 0.75f);
            yield return new WaitForSeconds(0.5f);
            plObject.DOMove(new Vector3(plObject.position.x, 2f, plObject.position.z), 0.5f);
            yield return new WaitForSeconds(0.5f);
            plObject.DOMove(new Vector3(0, plObject.position.y, plObject.position.z), 0.5f);
            yield return new WaitForSeconds(0.5f);
            plObject.DOMove(new Vector3(plObject.position.x, 0.65f, plObject.position.z), 0.5f);
            yield return new WaitForSeconds(0.5f);
            plObject.DOMove(new Vector3(plObject.position.x, plObject.position.y, 0), 0.2f);
            yield return new WaitForSeconds(0.2f);
            plObject.DOMove(new Vector3(-1.3f, plObject.position.y, plObject.position.z), 0.5f);
            yield return new WaitForSeconds(0.1f);
            
            isUsed = false;
        }
    }

    IEnumerator LastLevel()
    {
        while (isUsed)
        {
            levelText[thisLevel - 1].SetActive(false);
            level[thisLevel - 1].DOMove(new Vector3(4, 1, 1), 0.4f);
            yield return new WaitForSeconds(0.4f);
            Levels.DOMove(new Vector3(Levels.transform.position.x, Levels.transform.position.y - 1, Levels.transform.position.z), 0.6f);
            yield return new WaitForSeconds(0.6f);
            thisLevel--;
            level[thisLevel - 1].DOMove(new Vector3(4, 1, 0), 0.4f);
            yield return new WaitForSeconds(0.4f);
            levelText[thisLevel - 1].SetActive(true);
            isUsed = false;
        }
    }
    IEnumerator NextLevel()
    {
        while (isUsed)
        {
            levelText[thisLevel - 1].SetActive(false);
            level[thisLevel - 1].DOMove(new Vector3(4, 1, 1), 0.4f);
            yield return new WaitForSeconds(0.4f);
            Levels.DOMove(new Vector3(Levels.transform.position.x, Levels.transform.position.y + 1, Levels.transform.position.z), 0.6f);
            yield return new WaitForSeconds(0.6f);
            thisLevel++;
            level[thisLevel - 1].DOMove(new Vector3(4, 1, 0), 0.4f);
            yield return new WaitForSeconds(0.4f);
            levelText[thisLevel - 1].SetActive(true);
            isUsed = false;
        }
    }
    IEnumerator OpenSettings()
    {
        while (isUsed)
        {
            yield return new WaitForSeconds(0.5f);
            plObject.DOMove(new Vector3(setBox.position.x, plObject.position.y, setBox.position.z), 0.5f);
            yield return new WaitForSeconds(0.5f);
            setBox.DOMove(new Vector3(setBox.position.x, -3, setBox.position.z), 0.5f);
            plObject.DOMove(new Vector3(plObject.position.x, -2.35f, plObject.position.z), 0.5f);
            yield return new WaitForSeconds(0.1f);
            backBox.DOMove(new Vector3(backBox.position.x, backBox.position.y, backBox.position.z - 1), 0.5f);
            _cam.transform.DOMove(new Vector3(_cam.transform.position.x, _cam.transform.position.y - 2.5f, _cam.transform.position.z), 0.5f);
            isUsed = false;
        }
    }
    IEnumerator BackToMenu()
    {
        while (isUsed)
        {
            yield return new WaitForSeconds(0.5f);
            _cam.transform.DOMove(new Vector3(_cam.transform.position.x, _cam.transform.position.y + 2.5f, _cam.transform.position.z), 0.5f);
            backBox.DOMove(new Vector3(backBox.position.x, backBox.position.y, backBox.position.z + 1), 0.5f);
            yield return new WaitForSeconds(0.1f);
            plObject.DOMove(new Vector3(plObject.position.x, 0.65f, plObject.position.z), 0.5f);
            setBox.DOMove(new Vector3(setBox.position.x, 0, setBox.position.z), 0.5f);
            yield return new WaitForSeconds(0.5f);
            plObject.DOMove(new Vector3(-1.3f, plObject.position.y, plObject.position.z), 0.5f);
            
            PlayerPrefs.SetFloat("volume", _soundHandler.transform.localPosition.x + 1);
            isUsed = false;
        }
    }

    IEnumerator ChooseLevel(int level)
    {
        while (isUsed)
        {
            _cam.transform.DOMove(new Vector3(_cam.transform.position.x, _cam.transform.position.y, -0.75f), 0.5f);
            yield return new WaitForSeconds(0.5f);
            switch (level)
            {
                case 0: SceneManager.LoadScene("TestLevel"); break;
                case 1: SceneManager.LoadScene("level_1"); break;
                case 2: SceneManager.LoadScene("level_2"); break;
                case 3: SceneManager.LoadScene("level_3"); break;
                case 4: SceneManager.LoadScene("level_4"); break;
                case 5: SceneManager.LoadScene("level_5"); break;
            }
        }
    }

    IEnumerator QuitGame()
    {
        while (isUsed)
        {
            yield return new WaitForSeconds(0.1f);
            plObject.DOMove(new Vector3(plObject.position.x - 1, plObject.position.y, plObject.position.z), 0.5f);
            yield return new WaitForSeconds(0.3f);
            isUsed = false;
            Application.Quit();
        }
    }
}
