using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float ViewDistance = 3f;
    public float MeleeDistance = 1f;
    public float WalkSpeed = 1f;
    public int InitHP=3;
    public int InitAP=1;
    NavMeshAgent agent;
    private Animator animator;
    private float CurrHP;
    private float CurrAP;
    bool touch = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.stoppingDistance = MeleeDistance;
        agent.speed = WalkSpeed;
        CurrHP = InitHP;
        CurrAP = InitAP;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && CurrHP > 0)
        {
            if (touch && animator.GetCurrentAnimatorStateInfo(0).IsName("attack1")) //target.gameObject.GetComponent<Gamekit3D.PlayerController>().m_InAttack
            {
                agent.isStopped = true;
                animator.SetBool("walk", false);
                touch = false;
                CurrHP--;
                Debug.Log(target.name + " Hit " + gameObject.name + ", left " + CurrHP);
                if (CurrHP <= 0)
                {
                    target = null;
                    animator.SetTrigger("die");
                    //GlobalSet.enemycount--;
                }
                else
                    animator.SetTrigger("hit");
            }
            else
            {
                float distance = Vector3.Distance(target.position, transform.position);
                if (distance <= ViewDistance)
                {
                    if (distance <= MeleeDistance)
                    {
                        agent.isStopped = true;
                        animator.SetBool("walk", false);
                        facetarget();
                        animator.SetTrigger("attack1");
                    }
                    else
                    {
                        agent.isStopped = false;
                        animator.SetBool("walk", true);
                        agent.SetDestination(target.position);
                    }
                }
                else
                {
                    agent.isStopped = true;
                    animator.SetBool("walk", false);
                }
            }
        }
    }

    void facetarget()
    {
        Vector3 dire = (target.position - transform.position).normalized;
        Quaternion lookr = Quaternion.LookRotation(new Vector3(dire.x, 0, dire.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookr, Time.deltaTime * 3f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ViewDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MeleeDistance);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "attack1") touch = true;
        Debug.Log("Collider Enter " + other.name);
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == "attack1") touch = false;
        Debug.Log("Collider Enter " + other.name);
    }
}
