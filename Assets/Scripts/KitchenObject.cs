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
		// checks were done before reaching here
		// if (this.kitchenObjectParent != null)
		// 	this.kitchenObjectParent.ClearKitchenObject();


		this.kitchenObjectParent = kitchenObjectParent;
		
		if(kitchenObjectParent.HasKitchenObject())
			Debug.LogError("KitchenObject already present");

		kitchenObjectParent.SetKitchenObject(this);
		transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform(); 
		transform.localPosition = Vector3.zero;
	}
	public void DestroyKitchenObject(GameObject gameObject)
	{
		Debug.LogWarning("Destroying this GameObject");
		Destroy(gameObject);
	}

    internal void DestroyKitchenObject(object equipedKitchenObject)
    {
        throw new NotImplementedException();
    }
}
