using System.Collections;
using UnityEngine;

public class NumberController : MonoBehaviour {
	public Sprite[] numberSprites;

	public void RenderNumber (float number) {
		string[] numberList = number.ToString().Split();

		print(numberList);
	}
}
