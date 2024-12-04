using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    public Transform player; // 玩家?象
    public float viewDistance = 10.0f; // ?野范??定?10?位
    public float meleeDistance = 2.0f; // 攻?距离?2?位
    public float walkSpeed = 3.5f; // ??速度
    private NavMeshAgent agent; // AI?航代理

    private Animator animator; // ??控制器

    void Start()
    {
        // ?取?航代理和??控制器
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = walkSpeed;

        if (agent == null)
            Debug.LogError("No NavMeshAgent attached to GameObject");
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ?玩家在?野范??，AI?朝向玩家移?
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
        if (other.CompareTag("Player")) // 如果碰到玩家
        {
            GlobalSet.life--;
            //player.GetComponent<PlayerHealth>().TakeDamage(1); // ?玩家的HP造成1??害
            player.GetComponent<Animator>().SetTrigger("hit"); // 触?玩家的"hit"??
        }
    }
}