using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Data")]
public class EnemyData : ScriptableObject
{
	public float attackDistance;
	public float targetDistance;

	[Space]
	public string moveAnimation;
	public string idleAnimation;
	public string attackAnimation;
}