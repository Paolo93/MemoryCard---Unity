using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public Animator animator;
    private int levelToLoad;

    public void FadeToLevel (int levelIndex)
    {
        int CurrentLevel = PlayerPrefs.GetInt("ReachLevel", 1);
        PlayerPrefs.SetInt("ReachLevel", CurrentLevel);
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
