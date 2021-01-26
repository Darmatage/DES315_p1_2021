using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLogic : MonoBehaviour
{
    public GameObject turret;
	public GameObject switchOn;
	public GameObject switchOff;
    //public Vector2 direction;
    private Transform playerTransform;
    public bool triggered = false;
    
	public GameObject spikePrefab;
	public float speed = 10f;
    public float projectileLife = 3f;
	private int spikeCount = 0;
	public int spikeLimit = 2;
    private Vector2 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
		switchOn.SetActive(false);
		switchOff.SetActive(true);
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPos = new Vector2(turret.transform.position.x, turret.transform.position.y);
    }

    // Update is called once per frame
    void Update(){
        if(triggered == true){
			while (spikeCount <= spikeLimit){
				StartCoroutine(makeSpike());
                spikeCount += 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
		if (triggered == false){
			triggered = true; 
			switchOn.SetActive(true); 
			switchOff.SetActive(false); 
			//StartCoroutine(makeSpike());
		}
		else if (triggered == true){
			triggered = false; 
			spikeCount = 0;
			switchOn.SetActive(false);
			switchOff.SetActive(true);
		}
    }

	IEnumerator makeSpike(){
		yield return new WaitForSeconds(0.5f);
		GameObject newSpike;
		newSpike = Instantiate(spikePrefab, spawnPos, Quaternion.identity);
		//newSpike.transform.position = Vector2.MoveTowards(turret.transform.position, targetPos, speed * Time.deltaTime);
		newSpike.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed, ForceMode2D.Impulse);
		yield return new WaitForSeconds(projectileLife);
		Destroy(newSpike);
		spikeCount = 0;
	}

}
