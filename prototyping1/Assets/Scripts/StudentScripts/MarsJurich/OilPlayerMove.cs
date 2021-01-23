using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified version of the regular PlayerMove script. Essentially
// the same with modifications to work with oil. Simply copy
// the regular Player and replace the Player script with this one
// in a level that uses the Oil script.
//
// You will also need to set the oil slip sound, which can be
// found in "Assets/StudentMedia/MarsJurich/oil_slip.wav", as
// well as add an AudioSource component to the Player object.

public class OilPlayerMove : MonoBehaviour
{
    // OilPlayerMove variables
    private bool isTouchingOil = false;
    private bool wasTouchingOil = false;
    private Vector3 lastChange; // last player movement direction
    private Vector2 lastPosition; // last player position

    public AudioClip slip;
    AudioSource audioSource;

    // PlayerMove variables
    public float speed = 3f; // player movement speed
    private Vector3 change; // player movement direction
	private Rigidbody2D rb2d;
	private Animator anim;
	private bool isAlive = true;

	private Renderer rend;

    // Start is called before the first frame update
    void Start(){
        lastPosition = new Vector2();

        audioSource = GetComponent<AudioSource>();

        anim = gameObject.GetComponentInChildren<Animator>();
		rend = GetComponentInChildren<Renderer> ();

		if (gameObject.GetComponent<Rigidbody2D>() != null) {
			rb2d = GetComponent<Rigidbody2D>();
		}
    }

    // Update is called once per frame
    void FixedUpdate(){
        if (isTouchingOil == true && wasTouchingOil == false)
        {
            audioSource.PlayOneShot(slip);
        }

        if (isAlive == true){

            lastChange = change;

            // set change if hit wall so player can change direction
            if (lastPosition == rb2d.position)
            {
                change = Vector3.zero;
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

                if (isTouchingOil == true && lastChange == Vector3.zero && change != Vector3.zero)
                {
                    audioSource.PlayOneShot(slip);
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

        wasTouchingOil = isTouchingOil;
    }


    void UpdateAnimationAndMove() {
		if (isAlive == true){
			if (change!=Vector3.zero) {
                // set old position
                lastPosition = rb2d.position;

                // get new position to attempt to move to
                Vector2 newPos = transform.position + change * speed * Time.deltaTime;
                
                // check if trying to go diagonal
                if (isTouchingOil && change.x != 0 && change.y != 0)
                {
                    BoxCollider2D bc2d = GetComponentInParent<BoxCollider2D>();

                    Vector2 offset = new Vector2(bc2d.offset.x, bc2d.offset.y);
                    offset.x *= (Input.GetAxis("Horizontal") < 0) ? -1 : 1;
                    Vector3 scale = bc2d.size;
                    ContactFilter2D filter = new ContactFilter2D();
                    Collider2D[] results = new Collider2D[64];

                    // thanks to Ryan Garvan for helping fix this bit c:
                    Physics2D.OverlapBox(newPos + offset, scale, 0.0f, filter, results);

                    foreach (var col in results)
                    {
                        if (col == null)
                        {
                            break;
                        }

                        if (col.gameObject.name == "TilemapWalls")
                        {
                            change = Vector3.zero;
                            newPos = transform.position;
                        }
                    }

                    rb2d.MovePosition(newPos);

                    anim.SetBool("Walk", false);
                }
                else
                {
                    rb2d.MovePosition(newPos);
                    //MoveCharacter();
                    //anim.SetFloat("moveX", change.x);
                    //anim.SetFloat("moveY", change.y);

                    anim.SetBool("Walk", true);
                }
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


    public void SetTouchingOil(bool state)
    {
        isTouchingOil = state;
    }


	IEnumerator ChangeColor(){
		// color values are R, G, B, and alpha, each 0-255 divided by 100
		rend.material.color = new Color(2.0f, 1.0f, 0.0f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		rend.material.color = Color.white;
	}
}
