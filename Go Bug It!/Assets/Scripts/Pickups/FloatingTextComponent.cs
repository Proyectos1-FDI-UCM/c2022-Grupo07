using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextComponent : MonoBehaviour
{
    #region references
    private GameObject _myBanner;
    #endregion

    #region methods
    public void Appear()
    {
        StartCoroutine(ObjectBanner());
    }

    IEnumerator ObjectBanner()
    {
        yield return new WaitForSeconds(1.67f);
        _myBanner.SetActive(false);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myBanner = gameObject;
    }
}
