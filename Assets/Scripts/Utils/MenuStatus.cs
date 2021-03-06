﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuStatus
{

	private Dictionary<string,bool> menuStatus;
	private Dictionary<string,List<string>> menuProblem;

	public MenuStatus ()
	{
		this.menuStatus = new Dictionary<string,bool> ()
		{
			{"Pause", false},
			{"Control", false},
			{"Feedback", false},
			{"Inventory", false},
			{"Map", false},
			{"Quest", false}
		};
	}

	/// <summary>
	/// Open the specified menu.
	/// </summary>
	/// <param name="menu">Menu.</param>
	public void open(string menu){
		menuStatus [menu] = true;
		//Debug.Log (menu + " " + menuStatus [menu].ToString());
	}

	/// <summary>
	/// Close the specified menu.
	/// </summary>
	/// <param name="menu">Menu.</param>
	public void close(string menu){
		menuStatus [menu] = false;
		//Debug.Log (menu + " " + menuStatus [menu].ToString());
	}

	/// <summary>
	/// Check the rules over the open menu problem.
	/// </summary>
	/// <returns><c>true</c>, if problem was opened, <c>false</c> otherwise.</returns>
	/// <param name="menu">Menu.</param>
	public bool openProblem(string menu){
		bool problem = false;

		//when the feedback is open, no key should work to open a other menu
		if (menuStatus ["Feedback"]) {
			problem = true;
		} else {
			switch (menu) {
			//Feedback não é necessário
			case "Pause":
				// need to do test with map (map was disabled in 25/10/2016)
				if(menuStatus ["Inventory"] || menuStatus ["Quest"] /*|| menuStatus ["Map"]*/){
					problem = true;
					Debug.Log (menu);
				}
				break;
			case "Inventory":
				// need to do test with map (map was disabled in 25/10/2016)
				if (menuStatus ["Pause"] /*|| menuStatus ["Map"]*/) {
					problem = true;
					Debug.Log (menu);
				}
				break;
			case "Quest":
				// need to do test with map (map was disabled in 25/10/2016)
				if (menuStatus ["Pause"] /*|| menuStatus ["Map"]*/) {
					problem = true;
					Debug.Log (menu);
				}
				break;
			case "Map": // need to do test with map (map was disabled in 25/10/2016)
//					if(menuStatus ["Pause"] || menuStatus ["Inventory"] || menuStatus ["Quest"]){
//						problem = true;
//					}
				break;
			}
		}
			
		return problem;
	}
}

