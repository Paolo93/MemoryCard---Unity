using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{

    public float speed;
    Renderer bgRenderer;

    public void Start()
    {
        bgRenderer = GetComponent<Renderer>();
    }
  
    public void Update()
    {
        Vector2 offset = new Vector2(Time.time * speed, 0 );

        bgRenderer.material.mainTextureOffset = offset;

    }

    
}
