using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
	
	[SerializeField] private ContainerCounter containerCounter;
	[SerializeField] private Animator animator;

	private string OPEN_CLOSE = "OpenClose";

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}
	
	private void Start()
	{
		containerCounter.OnPlayerGrabbed += CounterContainerVisual_OnPlayerGrabbed;
	}

	private void CounterContainerVisual_OnPlayerGrabbed(object sender, EventArgs e)
	{
		Debug.Log("Animating Counter");
		animator.SetTrigger(OPEN_CLOSE);
	}
}
