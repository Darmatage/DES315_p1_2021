using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleJW : MonoBehaviour
{
	public string type = "INVALID"; // should be changed

	static InventoryHandlerJW inventoryHandler;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (inventoryHandler == null)
		{
			inventoryHandler = GameObject.Find("JWInventoryHandler").GetComponent<InventoryHandlerJW>();
		}
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
	    if (collision.CompareTag("Player"))
		{
			if (inventoryHandler.Store(this))
			{
				Destroy(gameObject);
			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
        if (collision.other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }            
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.otherCollider.CompareTag("Player"))
		{
            Destroy(gameObject);
		}
	}

	protected virtual void Store()
	{

	}
}
