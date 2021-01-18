using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified version of the regular PlayerMove script. Essentially
// the same with modifications to work with oil. Simply copy
// the regular Player and replace the Player script with this one
// in a level that uses the Oil script.

public class OilPlayerMove : MonoBehaviour
{
    // OilPlayerMove variables
    public bool isTouchingOil = false;
    private Vector3 lastChange; // last player movement direction
    private Vector2 lastPosition; // last player position

    // PlayerMove variables
    public float speed = 3f; // player movement speed
    private Vector3 change; // player movement direction
	private Rigidbody2D rb2d;
	private Animator anim;
	private bool isAlive = true;

	private Renderer rend;

    // Start is called before the first frame update
    void Start(){
        lastPosition = rb2d.position;

        anim = gameObject.GetComponentInChildren<Animator>();
		rend = GetComponentInChildren<Renderer> ();

		if (gameObject.GetComponent<Rigidbody2D>() != null) {
			rb2d = GetComponent<Rigidbody2D>();
		}
    }

    // Update is called once per frame
    void FixedUpdate(){

        if (isAlive == true){

            if (lastPosition == rb2d.position)
            {
                isTouchingOil = false;
            }

            // if touching oil or hit a wall
            if (isTouchingOil == false || change == Vector3.zero)
            {
                change = Vector3.zero;
                change.x = Input.GetAxisRaw("Horizontal");
                change.y = Input.GetAxisRaw("Vertical");
                UpdateAnimationAndMove();

                if (Input.GetAxis("Horizontal") > 0)
                {
                    Vector3 newScale = transform.localScale;
                    newScale.x = 1.0f;
                    transform.localScale = newScale;
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    Vector3 newScale = transform.localScale;
                    newScale.x = -1.0f;
                    transform.localScale = newScale;
                }
            }
            else if (isTouchingOil == true)
            {
                UpdateAnimationAndMove();

                if (change.x > 0)
                {
                    Vector3 newScale = transform.localScale;
                    newScale.x = 1.0f;
                    transform.localScale = newScale;
                }
                else if (change.x < 0)
                {
                    Vector3 newScale = transform.localScale;
                    newScale.x = -1.0f;
                    transform.localScale = newScale;
                }
            }


            if (Input.GetKey(KeyCode.Space))
            {
                anim.SetTrigger("Attack");
            }

        } //else playerDie();
    }


    void UpdateAnimationAndMove() {
		if (isAlive == true){
			if (change!=Vector3.zero) {

                lastPosition = rb2d.position;

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
			StopCoroutine(ChangeColor());
			StartCoroutine(ChangeColor());
		}
	}

	public void playerDie(){
		anim.SetTrigger("Dead");
		if (isAlive == false) {
			//Debug.Log("I'm already dead");
		}
		else if (isAlive == true) {
			isAlive = false;
			gameObject.GetComponent<Collider2D>().enabled = false;
			//gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
		}
	}


	IEnumerator ChangeColor(){
		// color values are R, G, B, and alpha, each 0-255 divided by 100
		rend.material.color = new Color(2.0f, 1.0f, 0.0f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		rend.material.color = Color.white;
	}
}
