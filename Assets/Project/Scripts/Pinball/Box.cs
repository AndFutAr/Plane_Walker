using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Box : MonoBehaviour
{
    [SerializeField] private float time = 0;
    [SerializeField] private TMP_Text Timer;
    
    [SerializeField] private int points = 25;
    [SerializeField] private TMP_Text pointsText;
    void Update()
    {
        pointsText.text = points.ToString();
        Timer.text = time.ToString();
        time += Time.deltaTime;
        
        if (points <= 0) SceneManager.LoadScene("StartScene");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player") { points -= 5; Destroy(other.gameObject); }
    }
}
