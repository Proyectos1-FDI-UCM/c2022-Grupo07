using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimationController : MonoBehaviour
{

    #region references
    [SerializeField] private Sprite[] _sprites;
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
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!loop && index == _sprites.Length) return;
        frame++;
        if (frame < _spritesPerFrame) return;
        _image.sprite = _sprites[index];
        frame = 0;
        index++;
        if (index >= _sprites.Length)
        {
            if (loop) index = 0;
            if (destroyOnEnd) Destroy(gameObject);
        }
    }
}
