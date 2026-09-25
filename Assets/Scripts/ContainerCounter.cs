using System;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{

	public event EventHandler OnPlayerGrabbed;
	[SerializeField] private KitchenObjectSO kitchenObjectSO;
	[SerializeField] private Transform CounterTop;
	private KitchenObject kitchenObject;

	public override void Interact(CharacterScript player)
	{
		if (player.HasKitchenObject())
			player.ClearSelfKitchenObject();
		Transform objectTransform = Instantiate(kitchenObjectSO.prefab, CounterTop);
		objectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
		Debug.Log("Interacting");
		OnPlayerGrabbed?.Invoke(this, EventArgs.Empty);
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
