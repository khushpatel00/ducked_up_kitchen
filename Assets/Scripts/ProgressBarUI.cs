using System;
using UnityEngine;

public class ProgressBarUI : MonoBehaviour
{

	[SerializeField] public UnityEngine.UI.Image barImage;
	[SerializeField] public CuttingCounter cuttingCounter;
	
	private void Start()
	{
		cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
		barImage.fillAmount = 0;
	}

	private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChanged_EventArgs e)
	{
		barImage.fillAmount = e.progressNormalized;
	}
}