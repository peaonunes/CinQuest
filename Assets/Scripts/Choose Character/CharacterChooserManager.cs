﻿using UnityEngine;
using System.Collections;

public class CharacterChooserManager : MonoBehaviour {

	public GameObject[] optionsPanels;
	public GameObject leftArrow;
	public GameObject rightArrow;
	public GameObject selected;

	private int currentOptionPanel = 0;
	private static GameObject selectedCharacter;


	void Start () {
		leftArrow.SetActive (false);
	}
		
	public static void selectCharacter(GameObject character) {
		selectedCharacter = character;
	}

	public void nextCharacters() {
		leftArrow.SetActive (true);
		selected.SetActive (false);
		selectedCharacter = null;

		if (currentOptionPanel + 1 < optionsPanels.Length) {
			StartCoroutine(MoveFunction(optionsPanels [currentOptionPanel], new Vector3(-1065, 0, 0)));

			currentOptionPanel++;

			StartCoroutine(MoveFunction(optionsPanels [currentOptionPanel], new Vector3(0, 0, 0)));

			if (currentOptionPanel == optionsPanels.Length -1) {
				rightArrow.SetActive (false);
			}
		}
	}

	public void previousCharacters() {
		rightArrow.SetActive (true);
		selected.SetActive (false);
		selectedCharacter = null;

		if (currentOptionPanel - 1 >= 0) {
			StartCoroutine(MoveFunction(optionsPanels [currentOptionPanel], new Vector3(1065, 0, 0)));

			currentOptionPanel--;

			StartCoroutine(MoveFunction(optionsPanels [currentOptionPanel], new Vector3(0, 0, 0)));

			if (currentOptionPanel == 0) {
				leftArrow.SetActive (false);
			}
		}
	}

	IEnumerator MoveFunction(GameObject panel, Vector3 newPosition)
	{
		float timeSinceStarted = 0f;
		while (true)
		{
			timeSinceStarted += Time.deltaTime * 2.0f;
			panel.GetComponent<RectTransform> ().localPosition = 
				Vector3.Lerp(panel.GetComponent<RectTransform> ().localPosition, newPosition, timeSinceStarted);

			if (panel.GetComponent<RectTransform> ().localPosition == newPosition)
			{
				yield break;
			}
				
			yield return null;
		}
	}
}
