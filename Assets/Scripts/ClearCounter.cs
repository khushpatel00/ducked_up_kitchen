using UnityEngine;

public class ClearCounter : BaseCounter
{
	[SerializeField] private KitchenObjectSO kitchenObjectSO;

	public override void Interact(CharacterScript player)
	{
		if (!HasKitchenObject())
		{
			// no KitchenObject here
			if (player.HasKitchenObject())
			{
				player.GetKitchenObject().SetKitchenObjectParent(this);
				player.ClearKitchenObjectRefrence();
			} // else { //  player doesnt have anything  }
		}
		else
		{
			// theres KitchenObject here
			if (!player.HasKitchenObject())
			{
				kitchenObject.SetKitchenObjectParent(player);
				SetKitchenObject(null);
			} // else { // player already has KitchenObject }
		}
	}
}
