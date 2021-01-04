using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

	public float speed = 3f; // player movement speed
	private Vector3 change; // player movement direction
	private Rigidbody2D rb2d;
	private Animator anim;
	private bool isAlive = true;

    // Start is called before the first frame update
    void Start(){
		anim = gameObject.GetComponentInChildren<Animator>();
		if (gameObject.GetComponent<Rigidbody2D>() != null) {
			rb2d = GetComponent<Rigidbody2D>();
		}
    }

    // Update is called once per frame
    void Update(){

		if (isAlive == true){
			change = Vector3.zero;
			change.x = Input.GetAxisRaw("Horizontal");
			change.y = Input.GetAxisRaw("Vertical");
			UpdateAnimationAndMove();

			if (Input.GetAxis ("Horizontal") > 0){
				Vector3 newScale = transform.localScale;
				newScale.x = 1.0f;
				transform.localScale = newScale;
			}
			else if (Input.GetAxis ("Horizontal") < 0){
				Vector3 newScale =transform.localScale;
				newScale.x = -1.0f;
				transform.localScale = newScale;
			}

			if (Input.GetKey(KeyCode.Space)){
				anim.SetTrigger("Attack"); 
			}
		} //else playerDie();
    }


	void UpdateAnimationAndMove() {
		if (isAlive == true){
			if (change!=Vector3.zero) {
				rb2d.MovePosition(transform.position + change * speed * Time.deltaTime);
				//MoveCharacter();
				//anim.SetFloat("moveX", change.x);
				//anim.SetFloat("moveY", change.y);
				anim.SetBool("Walk", true);
			} else {
				anim.SetBool("Walk", false);
			}
		}
	}

	public void playerHit(){
		if (isAlive == true){
			anim.SetTrigger("Hurt"); 
		}
	}

	public void playerDie(){
		anim.SetTrigger("Dead"); 
		if (isAlive == true) {
			isAlive = false;
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
			gameObject.GetComponent<Collider>().enabled = false;
		}
	}

}
