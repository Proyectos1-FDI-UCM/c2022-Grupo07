using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesComponent : MonoBehaviour
{
    #region references
    private Animator _myAnimator;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.OnCollectiblePicked(NameCheck(gameObject.name));
        _myAnimator.SetTrigger("Picked");
        Destroy(gameObject, 0.59f);
    }

    // Devuelve un índice según el coleccionable que se recoge
    public int NameCheck(string name)
    {
        if (name == "Python") return 0;
        else if (name == "C#") return 1;
        else if (name == "Java") return 2;
        else return 3;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myAnimator = GetComponent<Animator>();
    }
}
