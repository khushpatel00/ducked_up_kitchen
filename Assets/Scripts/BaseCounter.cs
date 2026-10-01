using UnityEngine;

public class BaseCounter : MonoBehaviour
{
    public virtual void Interact(CharacterScript player)
	{
		Debug.LogError("Invalid Call: BaseCounter.Interact()");
	}
    public virtual void InteractAlternate(CharacterScript player)
	{
		Debug.LogError("Invalid Call: BaseCounter.InteractAlternate()");
	}
}