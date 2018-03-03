using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    private int _hp = 3;
    public int hp {
        get { return _hp; }
        set { _hp = value;
            updateHP();
        }
    }

    private int _score = 0;

    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            updateScore();
        }
    }

	// Predkosc ruchu
	public float verticalSpeed = 5;
    public float horizontalSpeed = 5;
    public float rotateSpeed = 3;
    public float horizontalSpeedIncrease = 0.1f;
    public float horizontalToVerticalSpeedIncreaseFactor = 2;
    public float maxHorizontalSpeed = 20;
    public float maxVerticalSpeed = 20;

    private float verticalSpeedIncrease;

    public float getVerticalSpeedIncrease()
    {
        return verticalSpeedIncrease;
    }



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

    public UIController uiController;

    void Start () 
	{
		// Ustawienie koloru wiatru na czarny (brak koloru)
		Red = false;
		Green = false;
		Blue = false;
        color = Color.black;
        renderer = GetComponentInChildren<Renderer>();
        anim = GetComponentInChildren<Animator>();

	    verticalSpeedIncrease = horizontalSpeedIncrease / horizontalToVerticalSpeedIncreaseFactor;

	    updateViewedColor();
    }

	
	// Update is called once per frame
	void Update () {


	    // Przemieszczanie gracza wzgledem pozycji myszki
		//--------
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(GetComponent<Transform>().position);

		float mousePositionX = Input.mousePosition.x;
		if (mousePositionX < 0.0f)
			mousePositionX = 0;
		else if (mousePositionX > Camera.main.pixelWidth)
			mousePositionX = Camera.main.pixelWidth;

		float mousePositionY = Input.mousePosition.y;
		if (mousePositionY < 0.0f)
			mousePositionY = 0;
		else if (mousePositionY > Camera.main.pixelHeight)
			mousePositionY = Camera.main.pixelHeight;

        Debug.Log("target: " + playerPosition.y + ", mouse is: " + Input.mousePosition);

		float moveVertical = 0;
        if ((mousePositionY > playerPosition.y + 1 * verticalSpeed) && (playerPosition.y < Camera.main.pixelHeight))
            moveVertical = 1;
        else if ((mousePositionY < playerPosition.y - 1 * verticalSpeed) && (playerPosition.y > 0))
            moveVertical = -1;

		float moveHorizontal = 0;
		if ((mousePositionX > playerPosition.x + 1 * verticalSpeed) && (playerPosition.x < Camera.main.pixelWidth))
			moveHorizontal = 1;
		else if ((mousePositionX < playerPosition.x - 1 * verticalSpeed) && (playerPosition.x > 0))
			moveHorizontal = -1;
		
		//float scaledSpeedY = Mathf.Abs(Mathf.Abs(playerPosition.y - mousePositionY) / (Camera.main.pixelHeight / 2)) * verticalSpeed;
		//float scaledSpeedX = Mathf.Abs(Mathf.Abs(playerPosition.x - mousePositionX) / (Camera.main.pixelWidth / 2)) * verticalSpeed;

		//float scaledSpeedY = Mathf.Sqrt((Mathf.Abs(playerPosition.y - mousePositionY))/(Camera.main.pixelHeight/50) * verticalSpeed);
		//float scaledSpeedX = Mathf.Sqrt((Mathf.Abs(playerPosition.x - mousePositionX))/(Camera.main.pixelWidth/50) * horizontalSpeed);

		float scaledSpeedY = Mathf.Sqrt((Mathf.Abs(playerPosition.y - mousePositionY))/20 * verticalSpeed);
		float scaledSpeedX = Mathf.Sqrt((Mathf.Abs(playerPosition.x - mousePositionX))/20 * horizontalSpeed);


		Vector3 movement = new Vector3 (horizontalSpeed + (moveHorizontal * scaledSpeedX), moveVertical * scaledSpeedY, 0.0f);
		GetComponent<Rigidbody>().velocity = movement;
		//--------

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

    private void updateScore()
    {
        uiController.UpdateScore(score);
    }

    private void updateHP()
    {
        uiController.RemoveHP();
        if(hp == 0)
        {
            uiController.ActivateGameOver();
            verticalSpeed = 0;
            horizontalSpeed = 0;
        }
    }

    public void updateSpeed()
    {
        if (horizontalSpeed < maxHorizontalSpeed)
        {
            horizontalSpeed = horizontalSpeed + score * horizontalSpeedIncrease;
            verticalSpeed = verticalSpeed + score * verticalSpeedIncrease;
        }
 
    }
}
