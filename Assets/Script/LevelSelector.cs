using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{

    public Button[] levelButtons;
    SceneController sceneController;

    private void Awake()
    {
        
    }

    private void Start()
    {
        int ReachLevel = PlayerPrefs.GetInt("ReachLevel", 1);
        //int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i + 1 > ReachLevel)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void PlayLevel(int Level)
    {
        SceneManager.LoadScene(Level);
    }



}
