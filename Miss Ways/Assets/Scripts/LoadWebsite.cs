using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWebsite : MonoBehaviour
{

	#region Public Methods

	public void Load (string website)
	{
		Application.OpenURL (website);
	}

	#endregion

}