using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSprite : MonoBehaviour
{
    [SerializeField] private Sprite _spriteVariantOne;
    [SerializeField] private Sprite _spriteVariantTwo;
    
    private Image _image;
    

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void ToggleSpriteToOne(bool toOne)
    {
        _image.sprite = toOne ? _spriteVariantOne : _spriteVariantTwo;
    }

    public void SetSprites(Sprite spriteOne, Sprite spriteTwo)
    {
        _spriteVariantOne = spriteOne;
        _spriteVariantTwo = spriteTwo;
    }
}
