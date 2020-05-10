using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

	public float bgSpeed;
	public Renderer bgRend;

	// Use this for initialization
	void Start()
	{
		Time.timeScale = 1f;
	}

	// Update is called once per frame
	void Update()
	{
		//gameObject.transform.localScale = new Vector3(Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight, gameObject.transform.position.z);
		if(CountdownController.instance != null && CountdownController.instance.CanStartGame())
		{
			bgRend.material.mainTextureOffset += new Vector2(bgSpeed * Time.deltaTime, 0f);
		} else
        {
            bgRend.material.mainTextureOffset += new Vector2(bgSpeed * Time.deltaTime, 0f);
        }
	}
}
