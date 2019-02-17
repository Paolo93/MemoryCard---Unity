using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float mainTimer = 60.0f;

    private float timer;
    Animator anim;
    TextMeshPro timerLabel;
   

    void Start()
    {
        timer = mainTimer;
        timerLabel = GetComponent<TextMeshPro>();
        anim = GetComponent<Animator>();
    }

  
    void Update()
    {
        anim.SetFloat("Pulse", timer);
        if(timer >= 0.0f)
        {
            timer -= Time.deltaTime;
            timerLabel.text = timer.ToString("F");
        } else if (timer <= 0.0f)
        {
            timerLabel.text = "0.00";
            timer = 0.0f;
           
            SceneController.GameOver();
        }
    }
}
