using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float ViewDistance=7f;
    public float MeleeDistance = 2f;
    public float WalkSpeed = 1f;
    public int InitHP;
    public int InitAP;
    NavMeshAgent agent;
    private Animator ta;
    private float CurrHP;
    private float CurrAP;
    bool touch = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ta = GetComponent<Animator>();
        agent.stoppingDistance = MeleeDistance;
        agent.speed = WalkSpeed;
        CurrHP=InitHP;
        CurrAP = InitAP;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && CurrHP>0)
        {
            if (touch)
            {
                agent.isStopped = true;
                ta.SetBool("walk", false);
                CurrHP--;
                Debug.Log(target.name + " Hit " + gameObject.name + ", left " + CurrHP);
                if (CurrHP <= 0)
                {
                    ta.SetTrigger("die");
                    //GlobalSet.enemycount--;
                }
                else
                    ta.SetTrigger("hit");
            }
            else
            {
                float distance = Vector3.Distance(target.position, transform.position);
                if (distance <= ViewDistance)
                {
                    if (distance <= MeleeDistance)
                    {
                        agent.isStopped = true;
                        ta.SetBool("walk", false);
                        facetarget();
                        ta.SetTrigger("attack1");
                    }
                    else
                    {
                        agent.isStopped = false;
                        ta.SetBool("walk", true);
                        agent.SetDestination(target.position);
                    }
                }
                else
                {
                    agent.isStopped=true;                    
                    ta.SetBool("walk", false);
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
        if (other.name == target.name) touch = true;
        Debug.Log("Collider Enter "+other.name);
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == target.name) touch = false;
        Debug.Log("Collider Enter "+other.name);
    }
}
