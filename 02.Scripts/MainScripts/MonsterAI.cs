using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : LivingEntity
{
    //Inspector
    private NavMeshAgent agent;
    private Animator anim;
    private AudioSource audioSource;

    private LivingEntity targetLe;
    private readonly Vector3 offset_raycast = new Vector3(0, .5f, 0);

    //Coroutine Timer
    private readonly WaitForSeconds setAI_ws = new WaitForSeconds(0.2f);
    private readonly WaitForSeconds animation_Attack = new WaitForSeconds(2.0f);
    private readonly WaitForSeconds animation_Attack2 = new WaitForSeconds(2.5f);

    //Attack
    private bool isTracking = false;

    public int type;
    public float attackRange;
    public bool isBoss = false;
    private bool isAttack = false;

    public AudioClip clipDie;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        base.SetState();
        agent.updateRotation = false;
    }

    private void Update()
    {
        //if (!PhotonNetwork.IsMasterClient)
        //    return;

        AnimatorFn();
        NavAgentRot();
        Debug.DrawRay(transform.position + new Vector3(0, .5f, 0), transform.forward * attackRange, Color.red);
    }

    void AnimatorFn()
    {
        anim.SetBool("isMove", agent.velocity != Vector3.zero);
    }

    void NavAgentRot()
    {
        if (agent.desiredVelocity.sqrMagnitude >= 0.1f * 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(agent.desiredVelocity),
                Time.deltaTime * 10.0f);
        }
    }

    IEnumerator OnTracking()
    {
        while (!dead)
        {
            if (targetLe.dead)
            {
                isTracking = false;
                GetComponentInChildren<MonsterSenser>().OnSenser();
                break;
            }

            if (!OnAttack())
            {
                agent.isStopped = false;
                agent.SetDestination(targetLe.transform.position);
            }
            else
            {
                agent.isStopped = true;
                int ran = Random.Range(0, 3);
                if (!isBoss)
                    ran = 0;
                switch (ran)
                {
                    case 0:
                        anim.SetTrigger("trigger_Attack");
                        yield return animation_Attack;
                        break;
                    case 1:
                        anim.SetTrigger("trigger_Attack2");
                        yield return animation_Attack;
                        break;
                    default:
                        anim.SetTrigger("trigger_Attack3");
                        yield return animation_Attack2;
                        break;
                }
            }
            yield return setAI_ws;
        }

    }

    private bool OnAttack()
    {
        return Physics.Raycast(transform.position + offset_raycast,
            transform.forward,
            attackRange,
            LayerMask.GetMask("Player"));
    }

    [PunRPC]
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public void OnSenser(Collider other)
    {
        // || !PhotonNetwork.IsMasterClient
        if (isTracking)
            return;

        if (other.CompareTag("Player"))
        {
            isTracking = true;
            targetLe = other.GetComponent<LivingEntity>();
            StartCoroutine(OnTracking());
        }
    }

    public override void Die()
    {
        base.Die();
        agent.isStopped = true;
        int randDie = Random.Range(-1, 1);
        if (randDie == 0)
            anim.SetTrigger("isDie_01");
        else
            anim.SetTrigger("isDie_02");

        GameObject.Find("Fogs").GetComponent<Fogs>().MonsterDie(type);
        audioSource.PlayOneShot(clipDie);
        Destroy(gameObject, 5);
    }

    //private void FindTarget()
    //{
    //    agent.isStopped = true;
    //    Collider[] cols =
    //        Physics.OverlapSphere(transform.position, 5f, LayerMask.GetMask("Player"));

    //    for (int i = 0; i < cols.Length; i++)
    //    {
    //        LivingEntity le = cols[i].GetComponent<LivingEntity>();
    //        if (le != null && !le.dead)
    //        {
    //            agent.isStopped = false;
    //            targetLe = le;
    //            break;
    //        }
    //    }
    //}
}
