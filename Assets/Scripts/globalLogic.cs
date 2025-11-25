using UnityEngine;

public class globalLogic : MonoBehaviour
{
	public GameObject enemy;
	public GameObject summonPlace;
	public float spawnRate = 6;
	public GameObject touchControls;
	public GameObject gameOverScreen;
	public GameObject player;
	public PlayerLogic playerLogic;

	private float spawnTimer = 0;

	private void Start()
	{
		player = GameObject.Find("Player");
	    playerLogic = GameObject.Find("Player").GetComponent<PlayerLogic>();
		touchControls.SetActive(Application.isMobilePlatform);
		spawnTimer = spawnRate;
	}

	void Update()
	{
		spawnTimer += Time.deltaTime;
		if (spawnTimer > spawnRate)
		{
			SummonEnemy();
			spawnTimer = 0;
		}
	}

	public void resetGame()
	{
		player.transform.position = Vector3.up;
		playerLogic.health = playerLogic.maxHealth;
		gameOverScreen.SetActive(false);
	}

	public void SummonEnemy()
	{
		Instantiate(enemy, summonPlace.transform.position, summonPlace.transform.rotation);
	}
}
