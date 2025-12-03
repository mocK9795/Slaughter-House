using UnityEngine.AI;
using UnityEngine;

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
		UpdateHummanoidState(distance);
	}

	void UpdateHummanoidState(float distance)
	{
		if (distance < enemyData.attackDistance)
		{
			if (state == State.inAttack) return;

			animator.SetTrigger(enemyData.attackAnimation);
			agent.isStopped = true;
			state = State.inAttack;
		}
		else if (distance < enemyData.targetDistance)
		{
			if (state == State.inMotion) return;

			agent.SetDestination(PlayerLogic.Instance.transform.position);
			animator.SetTrigger(enemyData.moveAnimation);
			agent.isStopped = false;
			state = State.inMotion;
		}
		else
		{
			if (state == State.inIdle) return;

			animator.SetTrigger(enemyData.idleAnimation);
			agent.isStopped = true;
			state = State.inIdle;
		}
	}
}
