using System;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class ProgressBarUI : MonoBehaviour
{

	[SerializeField] public UnityEngine.UI.Image barImage;
	[SerializeField] public CuttingCounter cuttingCounter;
	
	private void Start()
	{
		cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
		barImage.fillAmount = 0;
		
		SetIsVisible(false);
	}

	private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChanged_EventArgs e)
	{
		barImage.fillAmount = e.progressNormalized;
		SetIsVisible(!(e.progressNormalized == 0 || e.progressNormalized > 1));
	}
	
	public void SetIsVisible(bool isShown)
	{
		GetComponent<Canvas>().gameObject.SetActive(isShown);
	}
}