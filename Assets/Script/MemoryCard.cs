using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    [SerializeField] private SceneController controller;

    private void Start()
    {
        if (!cardBack)
        {
            cardBack = null;
            Debug.Log("Add card back to MemoryCard");
        }
        if (!controller)
        {
            controller = null;
            Debug.Log("Add SceneController to GameObject");
        }
    }
    private int _id;
    public int id
    {
        get { return _id; }
    }

    // public method
    public void SetCart(int id, Sprite image)//give id and image card
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void OnMouseDown() {
        
        if (cardBack.activeSelf && controller.canReveal)
        {
            cardBack.SetActive(false);
            controller.CardRevealed(this);
        }
    }

    // This method activates cover card
    public void Unreveal()
    {
        cardBack.SetActive(true);
    }
}
