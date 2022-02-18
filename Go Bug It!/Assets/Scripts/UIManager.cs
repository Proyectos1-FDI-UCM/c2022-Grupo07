using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    #region references
    [SerializeField] private GameObject _lifeUI;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;
    #endregion

    #region parameters
    private Image[] _hearts;
    private int _life = 4;
    #endregion

    #region methods

    // Actualiza la vida del jugador
    public void UpdatePlayerLife(int life, bool powerup)
    {
        if (life <= 3 && life >= 1)
        {
            if (powerup == false) _hearts[life - 1].sprite = _emptyHeart;
            else _hearts[life - 1].sprite = _fullHeart;
        }
    }
    #endregion

    // Initializes own references
    private void Awake()
    {
        // Inicializar vida
        _hearts = new Image[_life];
        for (int i = 0; i < _life; i++)
        {
            _hearts[i]= _lifeUI.transform.GetChild(i).GetComponent<Image>();
            _hearts[i].sprite = _fullHeart;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
