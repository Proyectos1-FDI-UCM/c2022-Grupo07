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
    private Animator _myAnimator;
    #endregion
    #region methods
    IEnumerator DestroyAttack(float attackDuration, GameObject attackToDestroy)
    {
        yield return new WaitForSeconds(attackDuration);
        Destroy(attackToDestroy);
        _myAnimator.SetInteger("Attack", -1);
        yield return new WaitForSeconds(0.4f);
        _myAnimator.SetInteger("Attack", -2);
        yield return new WaitForSeconds(0.7f);
        _myAnimator.SetInteger("Attack", -1);

    }
    private void AvastAttack()
    {
        GameObject currentAvastattack= Instantiate(Avast[Random.Range(0, Avast.GetLength(0))],_boss.transform);
        StartCoroutine(DestroyAttack(attackDuration, currentAvastattack));
    }
    private void NortonAttack()
    {
        GameObject currentNortonattack = Instantiate(Norton[Random.Range(0, Norton.GetLength(0))],_boss.transform);
        StartCoroutine(DestroyAttack(attackDuration, currentNortonattack));
    }
    private void WDefenderAttack()
    {
        GameObject currentDefenderattack = Instantiate(WDefender[Random.Range(0, WDefender.GetLength(0))],_boss.transform);
        StartCoroutine(DestroyAttack(attackDuration, currentDefenderattack));
    }
    private void McAfeeAttack()
    {
        GameObject currentMcAfeeattack = Instantiate(McAfee[Random.Range(0, McAfee.GetLength(0))], _boss.transform);
        StartCoroutine(DestroyAttack(attackDuration, currentMcAfeeattack));
    }

    #endregion
    #region parameters
    [SerializeField] private float attackDuration;
    [SerializeField] private float timeToAttack;
    [SerializeField] private float _gracePeriod;
    private int _rnd;
    private bool _freezeTimeChoosing = true;
    #endregion
    #region properties
    private float _elapsedCoolDown;
    #endregion

    // Update is called once per frame
    private void Start()
    {
        _myAnimator = GetComponent<Animator>();
        _myAnimator.SetInteger("Attack", -1);
         _rnd = -2;
    }
    void Update()
    {
        _elapsedCoolDown += Time.deltaTime;
        if(_elapsedCoolDown>=timeToAttack)
        {
            //
            if (_freezeTimeChoosing)
            {
                _rnd = Random.Range(0, 4);
                _freezeTimeChoosing = false;
            }
            switch (_rnd)
            {
                case 0: _myAnimator.SetInteger("Attack", 0); break;
                case 1: _myAnimator.SetInteger("Attack", 1); break;
                case 2: _myAnimator.SetInteger("Attack", 2); break;
                case 3: _myAnimator.SetInteger("Attack", 3); break;
            }

            if (_elapsedCoolDown >= timeToAttack + _gracePeriod )
            {
                if (_rnd == 0) AvastAttack();
                else if (_rnd == 1) McAfeeAttack();
                else if (_rnd == 2) NortonAttack();
                else WDefenderAttack();
                _freezeTimeChoosing = true;
                _elapsedCoolDown = 0;
                
            }
            
        }
    }
}
