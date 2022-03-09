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
    #endregion

    #region properties
    [HideInInspector] public bool _neutralized = false;
    #endregion

    #region methods


    private Animator anim;
    
    public bool Explode()
    {
        if (Vector2.Distance(_myPlayer.transform.position, transform.position) < _range  && !_neutralized && !_exploded )
        {
            _exploded = true;
            return true;
        }
        return false;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myPlayer = GameObject.FindGameObjectWithTag("Player");
        anim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Explode())
        {
            //Instantiate();   //anim.setBool("explodeNorton", true);   se activaría la animacion de explotar del NOrton

        }
    }
}
