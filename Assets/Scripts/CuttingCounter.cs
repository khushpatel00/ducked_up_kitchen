using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
	public event EventHandler<OnProgressChanged_EventArgs> OnProgressChanged;
	public class OnProgressChanged_EventArgs : EventArgs
	{
		public float progressNormalized;
	}
	[SerializeField] private CuttingRecipeSO[] CuttingRecipeSOs;
	[SerializeField] private GameObject CounterVisual;
	[SerializeField] private ProgressBarUI progressBarUI;
	private int cuttingProgress;
	private CuttingRecipeSO currentCuttingRecipe;

	public override void Interact(CharacterScript player)
	{
		cuttingProgress = 0;
		if (!HasKitchenObject())
		{
			// no KitchenObject here
			if (player.HasKitchenObject() && isCarryingRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
			{
				player.GetKitchenObject().SetKitchenObjectParent(this);
				player.ClearKitchenObjectRefrence();
				if (currentCuttingRecipe == null) _ = GetKitchenObjectForCuttingRecipe(GetKitchenObject().GetKitchenObjectSO());
				cuttingProgress = 0;
				OnProgressChanged?.Invoke(this, new OnProgressChanged_EventArgs
				{
					progressNormalized = cuttingProgress / GetRequiredCuts(currentCuttingRecipe)
				});
				progressBarUI.SetIsVisible(true);
			} // else { //  player doesnt have anything  }
		}
		else
		{
			// theres KitchenObject here
			if (!player.HasKitchenObject())
			{
				kitchenObject.SetKitchenObjectParent(player);
				SetKitchenObject(null);
				cuttingProgress = 0;
				OnProgressChanged?.Invoke(this, new OnProgressChanged_EventArgs
				{
					progressNormalized = cuttingProgress / GetRequiredCuts(currentCuttingRecipe)
				});
				progressBarUI.SetIsVisible(false);

			} // else { // player already has KitchenObject }
		}
	}

	public override void InteractAlternate(CharacterScript player)
	{
		if (HasKitchenObject())
		{
			KitchenObjectSO outputKitchenObject = GetKitchenObjectForCuttingRecipe(GetKitchenObject().GetKitchenObjectSO());

			cuttingProgress++;
			float currentCuttingProgress = (float)cuttingProgress / GetRequiredCuts(currentCuttingRecipe);
			OnProgressChanged?.Invoke(this, new OnProgressChanged_EventArgs
			{
				progressNormalized = currentCuttingProgress
			});
			if (!(currentCuttingProgress > 1))
				CounterVisual.GetComponent<Animator>().SetTrigger("Cut");
			
			// progressBarUI.SetIsVisible(!(currentCuttingProgress == 0 || currentCuttingProgress >= 1));
			
			Debug.Log(cuttingProgress + " " + GetRequiredCuts(currentCuttingRecipe) + " " + (float)cuttingProgress / GetRequiredCuts(currentCuttingRecipe));

			if (outputKitchenObject != null && cuttingProgress >= GetRequiredCuts(currentCuttingRecipe))
			{
				GetKitchenObject().DestroySelf();
				KitchenObject.Instance.SpawnKitchenObject(outputKitchenObject, this);
			}
			// else
			// {
			// 	cuttingProgress++;
			// 	OnProgressChanged?.Invoke(this, new OnProgressChanged_EventArgs
			// 	{
			// 		progressNormalized = (float)cuttingProgress / GetRequiredCuts(currentCuttingRecipe),
			// 	});
			// 	Debug.Log(cuttingProgress + " " + GetRequiredCuts(currentCuttingRecipe) + " " + (float)cuttingProgress / GetRequiredCuts(currentCuttingRecipe));

			// }
		}
	}

	public KitchenObjectSO GetKitchenObjectForCuttingRecipe(KitchenObjectSO kitchenObjectSO)
	{
		foreach (CuttingRecipeSO cuttingRecipe in CuttingRecipeSOs)
		{
			if (cuttingRecipe.input == kitchenObjectSO)
			{
				currentCuttingRecipe = cuttingRecipe;
				return cuttingRecipe.output;
			}
		}
		return null;
	}

	public int GetRequiredCuts(CuttingRecipeSO cuttingRecipeSO)
	{
		return cuttingRecipeSO.requiredCuts;
	}

	public bool isCarryingRecipe(KitchenObjectSO kitchenObjectSO)
	{
		foreach (CuttingRecipeSO cuttingRecipe in CuttingRecipeSOs)
		{
			if (cuttingRecipe.input == kitchenObjectSO) return true;
		}
		return false;
	}

}
