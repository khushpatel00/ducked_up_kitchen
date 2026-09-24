using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{

	[SerializeField] private KitchenObjectSO kitchenObjectSO;
	[SerializeField] private Transform CounterTop;
	private KitchenObject kitchenObject;
	private GameObject currentGameObject;

	public override void Interact(CharacterScript player)
	{
		if (player.HasKitchenObject())
			player.ClearSelfKitchenObject();
		Transform objectTransform = Instantiate(kitchenObjectSO.prefab, CounterTop);
		// objectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
		objectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
		// currentGameObject = objectTransform.gameObject;
		// player.SetKitchenObject(kitchenObject);
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
