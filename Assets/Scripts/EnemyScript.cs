using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public MeshRenderer enemyRenderer;
    public NavMeshAgent agent;
    public Material hitMaterial;
    public Material normalMaterial;
	public float health = 100;
    public int damage = 1;
	public float hitCoolDown = 0.1f;
    public float attackKnockback = 10f;
	public float knockbackAmplifier = 0.9f;
    public float speed = 4;
    public float debuffSpeed = 1;

    private Vector3 knockback = Vector3.zero;
    private float hitTimer = 0;
    private bool isHit = false;

    void Start()
    {
        
    }

    void Update()
    {
        knockback *= knockbackAmplifier;
        if (knockback.magnitude < 0.2)
        {
            knockback = Vector3.zero;
            agent.speed = speed;
        }

        transform.position += knockback * Time.deltaTime;

        if (isHit)
        {
            hitTimer += Time.deltaTime;
        }
        else { return; }
        if (hitTimer > hitCoolDown) { 
            isHit = false;
            enemyRenderer.material = normalMaterial;
        }
    }

    public void Attack(float damage, Vector3 forward, float knockbackForce)
    {
        if (isHit) { return; }
        isHit = true;
        hitTimer = 0;

        health -= damage;
        
        knockback = forward * knockbackForce;
        agent.speed = debuffSpeed;

        enemyRenderer.material = hitMaterial;

        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}
