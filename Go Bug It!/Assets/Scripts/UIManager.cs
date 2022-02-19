using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    #region references
    // Sección de vida en la UI
    [SerializeField] private GameObject _lifeUI;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;
    // Sección de tiempo en la UI
    [SerializeField] private GameObject _timeUI;
    private Text _timeText;
    [SerializeField] private Sprite _timer;
    #endregion

    #region parameters
    private Image[] _hearts = new Image[4];
    #endregion

    #region methods
    // Actualiza la vida del jugador
    public void UpdatePlayerLife(int life, bool powerup)
    {
        if (life <= 3 && life >= 0)
        {
            if (powerup == false) _hearts[life].sprite = _emptyHeart;
            else _hearts[life].sprite = _fullHeart;
        }
    }

    // Actualizar cronometro
    public void UpdateTime(int time)
    {
        if (time >= 100) _timeText.text = "" + time;
        else if (time >= 10) _timeText.text = "0" + time;
        else _timeText.text = "00" + time;
    }
    #endregion

    // Initializes own references
    private void Awake()
    {
        // Inicializar vida
        for (int i = 0; i < _hearts.Length; i++)
        {
            _hearts[i] = _lifeUI.transform.GetChild(i).GetComponent<Image>();
            _hearts[i].sprite = _fullHeart;
        }

        // Inicializar temporizador
        _timeText = _timeUI.transform.GetChild(1).GetComponent<Text>();
        _timeUI.transform.GetChild(0).GetComponent<Image>().sprite = _timer;
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
