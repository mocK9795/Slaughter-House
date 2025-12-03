using UnityEngine;

public class ItemHolder : MonoBehaviour {
	public bool isPlayerItemHolder;
	public Transform itemPosition;
	public static Item activeItem;

	private void Update() {
		
		if (isPlayerItemHolder)
		PlayerItemHolderUpdate();
	}

	void PlayerItemHolderUpdate()
	{
		Item item = null;
		if (PlayerLogic.raycast.collider)
		{
			item = PlayerLogic.raycast.collider.GetComponentInParent<Item>();
		}


		if (activeItem && activeItem != item) activeItem.OnDrop();

		if (item == null) activeItem = null;
		else if (item != activeItem)
		{
			activeItem = item;
			activeItem.OnHold();
		}
	}
}
