﻿using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof(SphereCollider))]
public class Magnet : MonoBehaviour
{
	#region Singleton class: Magnet

	public static Magnet Instance;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

	#endregion

	[SerializeField] float magnetForce;
    public Animator animator;
	public bool isGameOver=false;

    //to store objects inside magnetic field
    List<Rigidbody> affectedRigidbodies = new List<Rigidbody> ();
	Transform magnet;
	public static Magnet instance;

	
	void Start ()
	{
		if (instance==null)
		{
			instance = this;
		}
		else
		{
			Destroy (instance);
		}
		magnet = transform;
		affectedRigidbodies.Clear ();
	}

	void FixedUpdate ()
	{
		if (!Game.isGameover && Game.isMoving) {
			foreach (Rigidbody rb in affectedRigidbodies) {
				rb.AddForce ((magnet.position - rb.position) * magnetForce * Time.fixedDeltaTime);
			}
		}
	}

	//Object enters Magnetic field
	void OnTriggerEnter (Collider other)
	{
		string tag = other.tag;

		if (!Game.isGameover && (tag.Equals ("Obstacle") || tag.Equals ("Object"))) {
			AddToMagnetField (other.attachedRigidbody);
            
        }

		/*if (!Game.isGameover && tag.Equals("Object"))
		{
            animator.SetTrigger("isFun");
        }*/

	}

	//Object exits Magnetic field
	void OnTriggerExit (Collider other)
	{
		string tag = other.tag;

		if (!Game.isGameover && (tag.Equals ("Obstacle") || tag.Equals ("Object"))) {
			RemoveFromMagnetField (other.attachedRigidbody);
		}
		/*if (tag.Equals("Obstacle"))
		{
			isGameOver = true;
		}*/
		/*if (!Game.isGameover && tag.Equals("Object"))
		{
			animator.SetBool("isTrigger", false);
		}*/
	}

	public void AddToMagnetField (Rigidbody rb)
	{
		affectedRigidbodies.Add (rb);
	}

	public void RemoveFromMagnetField (Rigidbody rb)
	{
		affectedRigidbodies.Remove (rb);
	}
}
