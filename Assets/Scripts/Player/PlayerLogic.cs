using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public LayerMask raycastMask;
    public float interactionDistance;
    public static RaycastHit raycast;

    public float health;
    public float maxHealth;

    public static PlayerLogic Instance;

	private void Start() {
		Instance = this;
	}
}
