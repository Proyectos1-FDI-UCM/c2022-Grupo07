using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextComponent : MonoBehaviour
{
    #region references
    private GameObject _myBanner;
    #endregion

    #region methods
    public void Appear(float duration)
    {
        StartCoroutine(ObjectBanner(duration));
    }

    IEnumerator ObjectBanner(float duration)
    {
        yield return new WaitForSeconds(duration);
        _myBanner.SetActive(false);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myBanner = gameObject;
    }
}
