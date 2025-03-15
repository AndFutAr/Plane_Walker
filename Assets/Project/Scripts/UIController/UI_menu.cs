using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_menu : MonoBehaviour
{
    [SerializeField] private GameObject _OverMenu, player;
    
    void Start() => _OverMenu.SetActive(false);
    
    public void Replay()
    {
        player.SetActive(true);
        Camera.main.transform.SetParent(player.transform);
        _OverMenu.SetActive(false);
    }
    public void ExitGame() => SceneManager.LoadScene("StartScene");
}
