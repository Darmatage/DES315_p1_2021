using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructableWalls_AS : MonoBehaviour
{
    //public Tilemap destructableTilemap;
    //public GameObject explosion;
    //List<Transform> enemies;
    // Start is called before the first frame update
    //private void Start()
    //{

    //    enemies = new List<Transform>();
    //    destructableTilemap = GetComponent<Tilemap>();

    //    GameObject[] enemyObj = GameObject.FindGameObjectsWithTag("ExplodeEnemy");

    //    if (enemyObj.Length > 0)
    //    {
    //        foreach(GameObject obj in enemyObj)
    //        {
    //            if(obj.GetComponent<Transform>() != null)
    //            {
    //                enemies.Add(obj.GetComponent<Transform>());
    //            }
    //        }
    //    }

    //    Debug.Log("Num of enemies: " + enemies.Count.ToString());
    //}

    private void FixedUpdate()
    {
        //foreach (Tilemap tilemap in grid.GetComponentsInChildren<Tilemap>())
        //{
        //    TilemapCollider2D collider = tilemap.GetComponent<TilemapCollider2D>();

        //    if (collider != null)
        //    {
        //        for (int row = 0; row < rowCount; ++row)
        //        {
        //            for (int col = 0; col < colCount; ++col)
        //            {
        //                if (tilemap.HasTile(new Vector3Int(col + colMin, row + rowMin, 0)))
        //                {
        //                    nodeGrid[row][col].isObstacle = true;
        //                    ++i;
        //                }
        //            }
        //        }
        //    }
        //}

       // destructableTilemap.cellBounds.
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //        if (collision.gameObject.CompareTag("ExplodeEnemy"))
    //        {
    //            Vector3 hitPosition = Vector3.zero;

    //            List<ContactPoint2D> contacts = new List<ContactPoint2D>();
    //            collision.GetContacts(contacts);

    //            foreach (ContactPoint2D hit in contacts)
    //            {
    //                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
    //                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
    //                hitPosition.z = -1f;
    //                //destructableTilemap.Get
    //                Debug.DrawRay(hitPosition, hit.normal, Color.red);
    //                Instantiate(explosion, hitPosition, Quaternion.identity);

    //                destructableTilemap.SetTile(destructableTilemap.WorldToCell(hitPosition), null);
    //                //Debug.Log("loop");

    //            }
    //        }

    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("ExplodeEnemy"))
    //    {
    //        Vector3 hitPosition = Vector3.zero;

    //        List<ContactPoint2D> contacts = new List<ContactPoint2D>();
    //        collision.GetContacts(contacts);

    //        foreach (ContactPoint2D hit in contacts)
    //        {
    //            hitPosition.x = hit.point.x/* - 0.01f * hit.normal.x*/;
    //            hitPosition.y = hit.point.y/* - 0.01f * hit.normal.y*/;
    //            hitPosition.z = -1f;
    //            //destructableTilemap.Get
    //            Debug.DrawRay(hitPosition, hit.normal, Color.red);
    //            Instantiate(explosion, hitPosition, Quaternion.identity);

    //            destructableTilemap.SetTile(destructableTilemap.WorldToCell(hitPosition), null);
    //            //Debug.Log("loop");

    //        }

    //    }
    //}

}
