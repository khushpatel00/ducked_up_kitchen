using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
	[SerializeField] private KitchenObjectSO kitchenObjectSO;
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
	public void ClearKitchenObject(GameObject gameObject)
	{
		kitchenObject.DestroyKitchenObject(gameObject);
		kitchenObject = null;
	}
	public bool HasKitchenObject()
	{
		return kitchenObject != null;
	}
}
