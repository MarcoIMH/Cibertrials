using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int alturaSalto;
    public string axisHorizontal, axisVertical;

    public float velocidadX, fuerzaDeSalto, maxVelocidadX;
    public float deltaX, deltaY;
    Rigidbody2D rb;
    bool jump;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = (2 * maxVelocidadX/2 * maxVelocidadX/2) / (alturaSalto * alturaSalto);
        jump = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.5f)
        {
            deltaY = 1;
            jump = true;
            
        }
        if (Mathf.Abs(rb.velocity.x) < maxVelocidadX)
        {
            deltaX = Input.GetAxis("Horizontal");
        }
    }
    private void FixedUpdate()
    {
        if (jump)
        {
            rb.velocity = new Vector2(deltaX * velocidadX, deltaY * fuerzaDeSalto);
            jump = false;
        }
        
       else 
        //mov horizontal y salto
        rb.velocity = new Vector2(deltaX * velocidadX, rb.velocity.y);
    }
}
