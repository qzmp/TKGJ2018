using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    public const string HIGHSCORE_KEY = "highscore";

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

	// Audio
	private AudioSource[] audioSource;
	//private AudioSource audioSource;
	//private AudioSource audioSource2;
	public AudioClip point;
	public AudioClip miss;
	public AudioClip wind;
	public AudioClip strong_wind;
	public AudioClip birds;

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
    

	// Flagi koloru wiatru
	private bool Red;
	private bool Green;
	private bool Blue;

    // Kolor wiatru
    public Color color;

    private Renderer renderer;
    private Animator anim;

    public UIController uiController;
    public GameObject GameController;
    private float verticalSpeedOnPause;
    private float horizontalSpeedOnPause;

    void Start () 
	{
		// Audio
		//audioSource = GetComponent<AudioSource> ();
		//audioSource2 = GetComponent<AudioSource> ();
		audioSource = GetComponents<AudioSource>();

		audioSource [0].clip = null;
		audioSource [0].loop = false;
		audioSource [1].clip = null;
		audioSource [1].loop = false;
		audioSource [2].clip = wind;
		audioSource [2].loop = true;
		audioSource [2].Play ();
		audioSource [3].clip = birds;
		audioSource [3].loop = true;
		audioSource [3].Play ();

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

        Vector3 playerPosition = Camera.main.WorldToScreenPoint(GetComponent<Transform>().position);

        //Maybe delete clamping later
		float mousePositionY = Mathf.Clamp(Input.mousePosition.y, 0, Camera.main.pixelHeight);
		float scaledSpeedY = (mousePositionY- playerPosition.y) / Camera.main.pixelHeight * verticalSpeed;

		Vector3 movement = new Vector3 (horizontalSpeed, scaledSpeedY, 0.0f);
		GetComponent<Rigidbody>().velocity = movement;

        CalculateColors();

        RotatePlayerDisplay();
    }

    void CalculateColors()
    {
        Red = false;
        Green = false;
        Blue = false;
        if (Input.anyKey)
        {
            if (Input.GetKey("q"))
            {
                Red = true;
            }
            if (Input.GetKey("w"))
            {
                Green = true;
            }
            if (Input.GetKey("e"))
            {
                Blue = true;
            }
        }
        SetWindColor();
    }

    void RotatePlayerDisplay()
    {
        GetComponentInChildren<Transform>().Rotate(new Vector3(1, 0, 0), rotateSpeed);
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
                    color = new Color32(245, 223, 123, 255);
            }
            else if (Blue)
                color = new Color32(199, 82, 118, 255);
            else
                color = new Color32(197, 94, 94, 255);
		} 
		else if (Green) 
		{
			if (Blue)
				color = new Color32(105, 201, 167, 255);
			else
				color = new Color32(74, 172, 107, 255);
		} 
		else if (Blue)
			color = new Color32(65, 108, 139, 255);
		else
			color = Color.black;

        updateViewedColor();
	}

    void updateViewedColor()
    {
        foreach(TrailRenderer r in transform.GetChild(0).GetComponentsInChildren<TrailRenderer>())
        {
            r.material.SetColor("_Color", this.color);
        }
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
		audioSource[1].clip = point;
		audioSource[1].Play ();

        uiController.UpdateScore(score);
    }

    private void updateHP()
    {
		audioSource[1].clip = miss;
		audioSource[1].Play ();

        uiController.RemoveHP();
        if(hp == 0)
        {
            gameOver();
        }
    }

    private void gameOver()
    {
        uiController.ActivateGameOver();
        maybeSaveHighScore();
        verticalSpeed = 0;
        horizontalSpeed = 0;
    }

    private void maybeSaveHighScore()
    {
        var highscoreFromPrefs = PlayerPrefs.GetInt(HIGHSCORE_KEY);
        if (highscoreFromPrefs < score)
        {
            PlayerPrefs.SetInt(HIGHSCORE_KEY, score);
            PlayerPrefs.Save();
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

    public void pauseGame()
    {
		audioSource [0].Pause ();
		audioSource [2].Pause ();

        verticalSpeedOnPause = verticalSpeed;
        horizontalSpeedOnPause = horizontalSpeed;

        horizontalSpeed = 0;
        verticalSpeed = 0;

        GameController.SetActive(false);
    }

    public void resumeGame()
    {
		audioSource [0].Play ();
		audioSource [2].Play ();

        verticalSpeed = verticalSpeedOnPause;
        horizontalSpeed = horizontalSpeedOnPause;

        GameController.SetActive(true);
    }
}
