using UnityEngine;

public class TrashCounter : BaseCounter
{
	public override void Interact(CharacterScript player)
	{
		Destroy(player.GetKitchenObject().gameObject);
		player.ClearKitchenObjectRefrence();
		// player.ClearKitchenObject(player.GetKitchenObject().gameObject);
	}
}
