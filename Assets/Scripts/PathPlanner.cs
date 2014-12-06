// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections;




public class PathPlanner
{

	private Vector3 currentGoal;
	private PathPlannerStates state;
	private float vel;
	private Queue goalQueue;

	//For follow
	private Vector3 prevPos;

	public enum PathPlannerStates {
		NOP,
		READY,
		PROCESSING,
		FINISH
	}

	public PathPlanner ()
	{
		this.goalQueue = new Queue ();
		this.vel = 2;
		state = PathPlannerStates.NOP;

	}

	public void addGoal(Vector3 goal)
	{

	

		this.goalQueue.Enqueue (goal);


		if( state == PathPlannerStates.NOP || state == PathPlannerStates.FINISH )
		{
			this.state = PathPlannerStates.READY;
		
		}

	}

	void setGoal (Vector3 goal)
	{
		this.currentGoal = goal;

	}

	public Vector3 followTarget(Vector3 currentPosition, Vector3 targetPosition, out float moveHorizontal,out float moveVertical)
	{
		if( Vector3.Distance(this.prevPos,targetPosition) > 10 )
		{
			this.prevPos = targetPosition;
			this.addGoal(targetPosition);
		}
	

		return this.processTrajectory (currentPosition, out moveHorizontal, out moveVertical);
	}
	
	public Vector3 processTrajectory(Vector3 currentPosition, out float moveHorizontal,out float moveVertical)
	{
		moveVertical = 0;
		moveHorizontal = 0;
		//Vector2 movement = new Vector2(0,0);
		Vector3 updatedPos = currentPosition;

		if( this.state == PathPlannerStates.READY )
		{

			if( this.goalQueue.Count > 0 )
			{
				currentGoal = (Vector3)this.goalQueue.Dequeue();
				this.state = PathPlannerStates.PROCESSING;
			
			}
			else
			{
				this.state = PathPlannerStates.FINISH;
			
			}


		}
		
		if( state == PathPlannerStates.PROCESSING )
		{
	
			Vector3 direction = (currentGoal - currentPosition);

			float magnitude = Vector3.Magnitude(direction);
			direction.Normalize();
			Vector3 yAxis = new Vector3(0,1,0);
			Vector3 xAxis = new Vector3(1,0,0);
			
			if( magnitude > 0.1 )
			{

				updatedPos = Vector3.MoveTowards(currentPosition,currentGoal,vel);


				//transform.position += direction*vel*Time.deltaTime;
				moveVertical = Vector3.Dot (direction,yAxis);//*vel*Time.deltaTime
				moveHorizontal = Vector3.Dot (direction,xAxis);
				
				if (Mathf.Abs (moveVertical) > Mathf.Abs (moveHorizontal))
					moveHorizontal = 0;
				else if (Mathf.Abs (moveHorizontal) > Mathf.Abs (moveVertical))
					moveVertical = 0;
				else {
					moveVertical = 0;
					moveHorizontal = 0;
				}

				//movement.Set (moveHorizontal,moveVertical);
			}
			else
			{
				this.state = PathPlannerStates.READY;

			}


			Debug.Log("H: " + moveHorizontal + " V: " + moveVertical);
		}

		return updatedPos;
	}
	public Vector3 getCurrGoal()
	{
		return this.currentGoal;
	}

	public PathPlannerStates getState()
	{
		return this.state;
	}
}


