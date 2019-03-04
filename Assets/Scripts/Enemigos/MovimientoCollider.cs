using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCollider : MonoBehaviour {

    SpriteRenderer spriteEnemigo;
    Collider2D colliderVision;

    float colliderVisionPosX;
    float colliderVisionNegX;

    // Use this for initialization
    void Start ()
    {
        colliderVision = GetComponent<Collider2D>();
        spriteEnemigo = GetComponent<SpriteRenderer>();
        colliderVisionPosX = colliderVision.offset.x;
        colliderVisionNegX = -colliderVision.offset.x;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(spriteEnemigo.flipX)
        {
            colliderVision.offset = new Vector2(colliderVisionNegX, colliderVision.offset.y);
        }
        else
        {
            colliderVision.offset = new Vector2( colliderVisionPosX, colliderVision.offset.y);
        }
	}
}
