using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public Transform player; // Ссылка на объект игрока
    public float stopDistance = 1.5f; // Расстояние, на котором противник остановится перед игроком

    public ParticleSystem deadParticle;

    private NavMeshAgent navAgent; // Компонент NavMeshAgent

    public SkinnedMeshRenderer skinnedMeshRenderer;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("player").transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance)
        {
            // Начать преследование игрока
            navAgent.isStopped = false;
            navAgent.SetDestination(player.position);
        }
        else
        {
            // Остановить преследование
            navAgent.isStopped = true;
        }
    }

    public void DeadEnemy()
    {
        navAgent.isStopped = false;
        ParticleSystem ps = Instantiate(deadParticle, transform.position, Quaternion.identity);
        Debug.Log(ps.name);
        ps.Play();
        Destroy(gameObject);
    }
}
