using System;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
	[SerializeField] private KitchenObjectSO kitchenObjectSO;
	private IKitchenObjectParent kitchenObjectParent;
	public IKitchenObjectParent GetKitchenObjectParent() 
	{
		return kitchenObjectParent;
	}
	public KitchenObjectSO GetKitchenObjectSO()
	{
		return kitchenObjectSO;
	}
	public void SetKitchenObjectParent (IKitchenObjectParent kitchenObjectParent)
	{
		this.kitchenObjectParent = kitchenObjectParent;
		kitchenObjectParent.SetKitchenObject(this);
		transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform(); 
		transform.localPosition = Vector3.zero;
	}
	public void DestroyKitchenObject(GameObject specifiedGameObject)
	{
		Debug.LogWarning("Destroying this GameObject");
		kitchenObjectParent.ClearKitchenObject(specifiedGameObject);
		Destroy(specifiedGameObject);
	}
	public void DestroySelf()
	{
		kitchenObjectParent.ClearSelfKitchenObject();
		Destroy(gameObject);
	}
	public KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent currentKitchenObjectParent)
	{
		Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
		KitchenObject currentKitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
		currentKitchenObject.SetKitchenObjectParent(currentKitchenObjectParent);
		return currentKitchenObject;
	}
}
