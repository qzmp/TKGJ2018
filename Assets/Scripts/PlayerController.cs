using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    public int hp = 3;
    public int score;

	// Predkosc ruchu
	public float verticalSpeed = 5;
    public float horizontalSpeed = 5;
    public float rotateSpeed = 3;

	// Referencja do kamery
	public GameObject cameraObject;

	// Flagi koloru wiatru
	private bool Red;
	private bool Green;
	private bool Blue;

    // Kolor wiatru
    public Color color;

    private Renderer renderer;
    private Animator anim;

	// Use this for initialization
	void Start () 
	{
		// Ustawienie koloru wiatru na czarny (brak koloru)
		Red = false;
		Green = false;
		Blue = false;
        color = Color.black;
        renderer = GetComponentInChildren<Renderer>();
        anim = GetComponentInChildren<Animator>();

        score = 0;
	    updateViewedColor();
    }

	
	// Update is called once per frame
	void Update () {

        // Score update
	    if (hp > 0)
	    {
	        //score++;
	    }

	    // Przemieszczanie gracza wzgledem pozycji myszki
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(GetComponent<Transform>().position);
        Debug.Log("target: " + playerPosition.y + ", mouse is: " + Input.mousePosition);
		float moveVertical = 0;
        if ((Input.mousePosition.y > playerPosition.y + 1 * verticalSpeed) && (playerPosition.y < Camera.main.pixelHeight))
            moveVertical = 1;
        else if ((Input.mousePosition.y < playerPosition.y - 1 * verticalSpeed) && (playerPosition.y > 0))
            moveVertical = -1;
        float scaledSpeed = Mathf.Abs(Mathf.Abs(playerPosition.y - Input.mousePosition.y) / (Camera.main.pixelHeight / 2)) * verticalSpeed;
        Vector3 movement = new Vector3 (horizontalSpeed, moveVertical * scaledSpeed, 0.0f);
		GetComponent<Rigidbody>().velocity = movement;

        if (Input.GetKeyDown("r"))
        {
            Red = !Red;
            SetWindColor();
        }
        if (Input.GetKeyDown("g"))
        {
            Green = !Green;
            SetWindColor();
        }
        if (Input.GetKeyDown("b"))
        {
            Blue = !Blue;
            SetWindColor();
        }
        rotatePlayer();
    }

    void rotatePlayer()
    {
        GetComponentInChildren<Transform>().Rotate(new Vector3(1, 0, 0), rotateSpeed);
    }

	void FixedUpdate ()
	{
		// Zmiana flag koloru po przycisnieciu klawiszy RGB


		// Ruch przy pomocy strzalek
		//if (Input.GetKeyDown ("up")) 
			//rb.AddForce (new Vector3 (0, 5, 0));
			//transform.position = new Vector3(0, transform.position.y + 0.05f * speed, 0);

		//float moveVertical = Input.GetAxis ("Vertical");
		//Vector3 movement = new Vector3 (0.0f, moveVertical, 0.0f);
		//GetComponent<Rigidbody>().velocity = movement * speed;



	}

	// Ustalanie koloru wiatru na podstawie flag
	private void SetWindColor ()
	{
		if (Red) 
		{
			if (Green) 
			{
				if (Blue)
					color = Color.white;
				else
					color = Color.yellow;
			} 
			else if (Blue)
				color = Color.magenta;
			else
				color = Color.red;
		} 
		else if (Green) 
		{
			if (Blue)
				color = Color.cyan;
			else
				color = Color.green;
		} 
		else if (Blue)
			color = Color.blue;
		else
			color = Color.black;
        updateViewedColor();
	}

    void updateViewedColor()
    {
        renderer.material.SetColor("_Color", this.color);
        anim.SetTrigger("swirl");
    }


    public bool IsRedEnabled()
    {
        return Red;
    }

    public bool IsGreenEnabled()
    {
        return Green;
    }

    public bool IsBlueEnabled()
    {
        return Blue;
    }
}
