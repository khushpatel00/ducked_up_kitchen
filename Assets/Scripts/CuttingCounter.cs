using UnityEngine;

public class CuttingCounter : BaseCounter
{
	[SerializeField] private KitchenObjectSO cutKitchenObjectSO;
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

	public override void InteractAlternate(CharacterScript player)
	{
		if (HasKitchenObject())
		{
			GetKitchenObject().DestroySelf();
			Debug.Log("Generating KitchenObject" + kitchenObject);
			
			
			// kitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
			
			Transform kitchenObjectTransform = Instantiate(cutKitchenObjectSO.prefab);
			kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
		}
	}

}
