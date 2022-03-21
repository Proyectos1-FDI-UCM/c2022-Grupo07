using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_attack_controller : MonoBehaviour
{
    #region references
    [SerializeField] GameObject [] Avast;
    [SerializeField] GameObject [] Norton;
    [SerializeField] GameObject [] WDefender;
    [SerializeField] GameObject[] McAfee;
    #endregion
    #region methods
    IEnumerator DestroyAttack(float attackDuration, GameObject attackToDestroy)
    {
        yield return new WaitForSeconds(attackDuration);
        Destroy(attackToDestroy);

    }
    public void AvastAttack()
    {
        GameObject currentAvastattack= Instantiate(Avast[Random.Range(0, Avast.GetLength(0) - 1)]);
        StartCoroutine(DestroyAttack(attackDuration, currentAvastattack));
    }
    public void NortonAttack()
    {
        GameObject currentNortonattack = Instantiate(Norton[Random.Range(0, Norton.GetLength(0) - 1)]);
        StartCoroutine(DestroyAttack(attackDuration, currentNortonattack));
    }
    public void WDefenderAttack()
    {
        GameObject currentDefenderattack = Instantiate(WDefender[Random.Range(0, WDefender.GetLength(0) - 1)]);
        StartCoroutine(DestroyAttack(attackDuration, currentDefenderattack));
    }
    public void McAfeeAttack()
    {
        GameObject currentMcAfeeattack = Instantiate(McAfee[Random.Range(0, McAfee.GetLength(0) - 1)]);
        StartCoroutine(DestroyAttack(attackDuration, currentMcAfeeattack));
    }

    #endregion
    #region parameters
    [SerializeField] private float attackDuration;
    [SerializeField] private float timeToAttack;
    #endregion
    #region properties

    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }
}
