using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
	public List<Camera> cameras;

	public List<Material> cameraMat;


	// Use this for initialization
	void Start()
	{
		for (int i = 0; i < cameras.Count; i++)
		{
			if (cameras[i].targetTexture != null)
			{

				cameras[i].targetTexture.Release();
			}
			cameras[i].targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
			cameraMat[i].mainTexture = cameras[i].targetTexture;
		}
	}
}

