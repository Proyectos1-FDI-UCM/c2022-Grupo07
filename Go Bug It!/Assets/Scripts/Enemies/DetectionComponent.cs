using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionComponent : MonoBehaviour
{
    #region references
    private NortonComponent _myNorton;
    private McAfeeComponent _myMcAfee;
    [SerializeField] private bool _isMcAfee = false;
    #endregion
    
    #region methods
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Si se detecta al jugador
        PlayerLifeComponent _player = collision.gameObject.GetComponent<PlayerLifeComponent>();
        if (_player != null)
        {
            //Si soy un McAfee disparo y si soy Norton me activo, si no estoy neutralizado
            if (_isMcAfee && !NeuState()) _myMcAfee.Shoot();
            else if (!_isMcAfee) _myNorton.Activated();
        }
    }
    #endregion

    public bool NeuState()
    {
        //Este metodo devuelve el estado de neutralizacion del McAfee, para saber si llamar al metodo disparo
        return _myMcAfee._neutralized;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Dependiendo de si el enemigo es McAfee o no asignamos las variables
        if (_isMcAfee) _myMcAfee = GetComponentInParent<McAfeeComponent>();
        else _myNorton = GetComponentInParent<NortonComponent>();
    }
}
