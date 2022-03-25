using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollecctiblesComponent : MonoBehaviour
{
    #region properties
    private int[] _myCollecionables;
    #endregion
    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int posición;
        posición = NameCheck(this.gameObject.name);
        _myCollecionables[posición] = 1;
        Destroy(this.gameObject);
    }

    public int NameCheck(string name)
    {       
        if (name == "C#")
        {
            Debug.Log("C#");
            return 0;
        }
        else if (name == "Python")
        {
            Debug.Log("Python");
            return 1;
        }
        else if (name == "C++")
        {
            Debug.Log("C++");
            return 2;
        }
        else 
        {
            Debug.Log("Java");
            return 3;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCollecionables = new int[4];
        for(int i = 0; i < _myCollecionables.Length; i++)
        {
            _myCollecionables[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
