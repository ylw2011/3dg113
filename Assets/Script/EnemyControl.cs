using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    public Transform player; // ���a?�H
    public float viewDistance = 10.0f; // ?���S??�w?10?��
    public float meleeDistance = 2.0f; // ��?�Z��?2?��
    public float walkSpeed = 3.5f; // ??�t��
    private NavMeshAgent agent; // AI?��N�z

    private Animator animator; // ??���

    void Start()
    {
        // ?��?��N�z�M??���
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = walkSpeed;

        if (agent == null)
            Debug.LogError("No NavMeshAgent attached to GameObject");
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ?���a�b?���S??�AAI?�¦V���a��?
        if (distanceToPlayer <= viewDistance && distanceToPlayer > meleeDistance)
        {
            agent.SetDestination(player.position);
            animator.SetBool("walk", true);
        }
        else if (distanceToPlayer <= meleeDistance)
        {
            animator.SetBool("walk", false);
            animator.SetTrigger("attack1");
            agent.SetDestination(transform.position); // Stay in place while attacking
        }
        else
        {
            animator.SetBool("walk", false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeDistance);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �p�G�I�쪱�a
        {
            GlobalSet.life--;
            //player.GetComponent<PlayerHealth>().TakeDamage(1); // ?���a��HP�y��1??�`
            player.GetComponent<Animator>().SetTrigger("hit"); // �D?���a��"hit"??
        }
    }
}