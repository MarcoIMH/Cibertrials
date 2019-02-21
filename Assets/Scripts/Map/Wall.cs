using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public Sprite[] sprites;
    public int health = 2;
    SpriteRenderer spriteRend;

	
	void Start ()
    {
        spriteRend = GetComponent<SpriteRenderer>();
	}
	
	/// <summary>
    /// Decrementa la vida del muro y si es menor de 0 se destuye
    /// A medida que va bajando la vida cambia el sprite del muro
    /// </summary>
    /// <param name="damage">daño que causamos al muro</param>
    public void DamageWall(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
            GameObject.Destroy(this.gameObject);
        }
        spriteRend.sprite = sprites[health];   
    }
}
