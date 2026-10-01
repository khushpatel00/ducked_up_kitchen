using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{

	[SerializeField] private Transform CounterTop;
	private KitchenObject kitchenObject; 
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
			// there is a KitchenObject
			
		}
	}

	public void ClearKitchenObject(GameObject gameObject)
	{
		throw new System.NotImplementedException();
	}

	public Transform GetKitchenObjectFollowTransform()
	{
		return CounterTop;
	}
	public void SetKitchenObject(KitchenObject kitchenObject)
	{
		this.kitchenObject = kitchenObject;
	}
	public KitchenObject GetKitchenObject()
	{
		return kitchenObject;
	}
	public bool HasKitchenObject()
	{
		return kitchenObject != null;
	}
}
