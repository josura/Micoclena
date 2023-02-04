using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapContainer : MonoBehaviour
{
    public SpriteRenderer spriteRend;
    public Sprite currentSprite;

    private void Start()
    {
        //Fetch the SpriteRenderer from the GameObject
        spriteRend = GetComponent<SpriteRenderer>();
        currentSprite = Resources.Load<Sprite>("Assets/sprited/Caps/testCap1.png");
        
    }

    public void updateSprite(Sprite spr)
    {
        currentSprite = spr;
    }

    // Update is called once per frame
    void Update()
    {
        spriteRend.sprite = currentSprite;
    }
}
