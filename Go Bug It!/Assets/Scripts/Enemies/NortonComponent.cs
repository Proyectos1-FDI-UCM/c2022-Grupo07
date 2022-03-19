using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NortonComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private int _range = 4;
    private bool _exploded = false;
    #endregion

    #region references
    private Transform _myTransform;
    private GameObject _myPlayer;
    [SerializeField] private GameObject _myRango;
    #endregion

    #region properties
    [HideInInspector] public bool _neutralized = false;
    #endregion

    #region methods


    private Animator anim;
    
    public bool Warning()
    {
        if (Vector2.Distance(_myPlayer.transform.position, transform.position) < _range  && !_neutralized && !_exploded )
        {
            Debug.Log("zona de peligro con Norton");
            _exploded = true;
            return true;
        }
        return false;
    }
    public void nortonRespawn()
    {
        Debug.Log("Llamada Norton Respawn");
        
        /*Invoke("sleep", 20);
        _exploded = false;
        gameObject.active = true;
        //GetComponent<SpriteRenderer>().enabled = true;*/

    }
    
    public void Explode()
    {
        _myRango.SetActive(true);
        Destroy(gameObject, 1.0f);

    }

    public void sleep()
    {

    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myPlayer = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Warning())
        {
            Debug.Log("Explosiona norton");
            anim.SetBool("Dead", true);   //se activaría la animacion de explotar del NOrton
 
        }
    }
}
