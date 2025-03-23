using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_menu : MonoBehaviour
{
    [SerializeField] private GameObject _OverMenu, player, _camera3d, _room, skin;
    private Player _player;
    [SerializeField] private bool cam3d = false;
    
    [SerializeField] private AudioSource audio;
    [SerializeField] private Slider audio_slider;
    [SerializeField] private GameObject SetMenu;

    void Start()
    {
        _OverMenu.SetActive(false);
        _player = player.GetComponent<Player>();

        cam3d = false;
    }

    void Update()
    {
        audio.volume = audio_slider.value;
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (cam3d)
            {
                player.SetActive(true);
                skin.transform.SetParent(player.transform);
                _camera3d.SetActive(false);
                
                cam3d = false;
            }
            else
            {
                skin.transform.SetParent(_room.transform);
                player.SetActive(false);
                _camera3d.SetActive(true);
                
                cam3d = true;
            }
        }
    }
    public void OpenSetMenu() => SetMenu.SetActive(true);
    public void CloseSetMenu() => SetMenu.SetActive(false);
    public void Replay()
    {
        if (!cam3d)
        {
            player.SetActive(true);
            _player.OnShield();
            Camera.main.transform.SetParent(player.transform);
            _OverMenu.SetActive(false);
        }
    }
    public void ExitGame() => SceneManager.LoadScene("StartScene");
}
