using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_attack_controller : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject [] Avast;
    [SerializeField] private GameObject [] Norton;
    [SerializeField] private GameObject [] WDefender;
    [SerializeField] private GameObject[] McAfee;
    [SerializeField] private GameObject _boss;
    #endregion
    #region methods
    IEnumerator DestroyAttack(float attackDuration, GameObject attackToDestroy)
    {
        yield return new WaitForSeconds(attackDuration);
        Destroy(attackToDestroy);

    }
    public void AvastAttack()
    {
        GameObject currentAvastattack= Instantiate(Avast[Random.Range(0, Avast.GetLength(0))],_boss.transform);
        StartCoroutine(DestroyAttack(attackDuration, currentAvastattack));
    }
    public void NortonAttack()
    {
        GameObject currentNortonattack = Instantiate(Norton[Random.Range(0, Norton.GetLength(0))],_boss.transform);
        StartCoroutine(DestroyAttack(attackDuration, currentNortonattack));
    }
    public void WDefenderAttack()
    {
        GameObject currentDefenderattack = Instantiate(WDefender[Random.Range(0, WDefender.GetLength(0))],_boss.transform);
        StartCoroutine(DestroyAttack(attackDuration, currentDefenderattack));
    }
    public void McAfeeAttack()
    {
        GameObject currentMcAfeeattack = Instantiate(McAfee[Random.Range(0, McAfee.GetLength(0))], _boss.transform);
        StartCoroutine(DestroyAttack(attackDuration, currentMcAfeeattack));
    }

    #endregion
    #region parameters
    [SerializeField] private float attackDuration;
    [SerializeField] private float timeToAttack;
    #endregion
    #region properties
    private float _elapsedCoolDown;
    #endregion

    // Update is called once per frame
    void Update()
    {
        _elapsedCoolDown += Time.deltaTime;
        Debug.Log(_elapsedCoolDown);
        if(_elapsedCoolDown>=timeToAttack)
        {
            int rnd = Random.Range(0, 4);
            if (rnd == 0) AvastAttack();
            else if (rnd == 1) NortonAttack();
            else if (rnd == 2) McAfeeAttack();
            else WDefenderAttack();
            _elapsedCoolDown = 0;
        }
    }
}
