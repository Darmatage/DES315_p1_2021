using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public GameObject hitEffectAnim;

	//if the bullet hits a collider, play the explosion animation, then destroy the effect and the bullet
	void OnCollisionEnter2D(Collision2D collision){
		GameObject animEffect = Instantiate (hitEffectAnim, transform.position, Quaternion.identity);
		Destroy(animEffect, 0.5f);
		Destroy(gameObject);
	}
}
