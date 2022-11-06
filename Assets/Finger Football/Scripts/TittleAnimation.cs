using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FingerFootball
{
	//It is attached on the tittle inside the main menu. It is used to create a simple zoom in-zoom out animation
	public class TittleAnimation : MonoBehaviour
	{

		float scale = 1;
		bool up = true;

		void Update()
		{
			if (up)
			{
				scale += Time.deltaTime / 20;
				if (scale >= 1.05f)
				{
					up = false;
				}
			}
			else
			{
				scale -= Time.deltaTime / 20;
				if (scale <= 1f)
				{
					up = true;
				}
			}
			GetComponent<RectTransform>().localScale = new Vector2(scale, scale);
		}
	}
}