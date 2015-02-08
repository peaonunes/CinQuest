﻿using UnityEngine;
using System.Collections.Generic;

public class Point{
	public float x, y;
	public bool horizontalFirst;

	public Point(float x, float y){
		this.x = x;
		this.y = y;
		this.horizontalFirst = false;
	}

	public Point(){}
}