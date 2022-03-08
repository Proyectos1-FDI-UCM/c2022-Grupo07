using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimationController : MonoBehaviour
{

    #region references
    [SerializeField] private Sprite[] _spritesFull;
    [SerializeField] private Sprite[] _spritesEmpty;
    private Image _image;
    #endregion

    #region parameters
    [SerializeField] private int _spritesPerFrame;
    private int index = 0;
    private int frame = 0;
    #endregion

    #region properties
    private bool loop = true;
    private bool destroyOnEnd = false;
    private bool full = true;
    #endregion

    #region methods
    public void UpdateStatus(bool update)
    {
        full = update;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (full)
        {
            if (!loop && index == _spritesFull.Length) return;
            frame++;
            if (frame < _spritesPerFrame) return;
            _image.sprite = _spritesFull[index];
            frame = 0;
            index++;
            if (index >= _spritesFull.Length)
            {
                if (loop) index = 0;
                if (destroyOnEnd) Destroy(gameObject);
            }
        }
        else
        {
            if (!loop && index == _spritesEmpty.Length) return;
            frame++;
            if (frame < _spritesPerFrame) return;
            _image.sprite = _spritesEmpty[index];
            frame = 0;
            index++;
            if (index >= _spritesEmpty.Length)
            {
                if (loop) index = 0;
                if (destroyOnEnd) Destroy(gameObject);
            }
        }
        
    }
}
