using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
	[SerializeField] private KitchenObjectSO kitchenObjectSO;
	[SerializeField] private Transform CounterTop;
	private KitchenObject kitchenObject;

	public override void Interact(CharacterScript player)
	{
		if (player.GetKitchenObject() != null) // player has a object
		{
			if (kitchenObject != null) ClearKitchenObject(kitchenObject.gameObject); // clear out previous instance of KitchenObject
			player.GetKitchenObject().SetKitchenObjectParent(this);
			kitchenObject = player.GetKitchenObject();
			player.ClearKitchenObjectRefrence();
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
