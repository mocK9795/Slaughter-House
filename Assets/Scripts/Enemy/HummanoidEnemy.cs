using UnityEngine;
using UnityEngine.AI;

public class HummanoidEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
	public EnemyData enemyData;
	public Animator animator;
	public Weapon weapon;

	enum State {inAttack, inMotion, inIdle}
	State state;

	private void Update()
	{
		float distance = Vector3.Distance(transform.position, PlayerLogic.Instance.transform.position);
		if (distance < enemyData.attackDistance && state != State.inAttack)
		{
			agent.isStopped = true;
			animator.SetTrigger(enemyData.attackAnimation);
			state = State.inAttack;
		}
		else if (distance < enemyData.targetDistance && state != State.inMotion)
		{
			agent.SetDestination(PlayerLogic.Instance.transform.position);
			animator.SetTrigger(enemyData.moveAnimation);
			state = State.inMotion;
		}
		else if (state != State.inIdle)
		{
			agent.isStopped = true;
			animator.SetTrigger(enemyData.idleAnimation);
			state = State.inIdle;
		}
	}
}
