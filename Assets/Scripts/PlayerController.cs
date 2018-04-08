using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {

    public const string HIGHSCORE_KEY = "highscore";

    public ColorsManager colorsManager;

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

	// #Audio
	/*
	private AudioSource[] audioSource;
	public AudioClip point;
	public AudioClip miss;
	public AudioClip wind;
	public AudioClip strong_wind;
	public AudioClip birds;
	*/

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
    
    public ControlButtonScript redButton;
    public ControlButtonScript greenButton;
    public ControlButtonScript blueButton;
    
    private Rect movementTouchArea;
    private Touch? movementTouch = null;
    private bool isMovementHeld = false;

    void Start () 
	{
		//#Audio
		/*
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
		*/
		//---

		// Ustawienie koloru wiatru na czarny (brak koloru)
		Red = false;
		Green = false;
		Blue = false;
        color = Color.black;
        renderer = GetComponentInChildren<Renderer>();
        anim = GetComponentInChildren<Animator>();

        movementTouchArea = new Rect(0, 0, Screen.width * 0.8f, Screen.height);

	    verticalSpeedIncrease = horizontalSpeedIncrease / horizontalToVerticalSpeedIncreaseFactor;

	    updateViewedColor();
    }
    void OnGUI()
    {
        //GUI.Box(movementTouchArea, "");
    }


    // Update is called once per frame
    void Update () {

        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began && movementTouchArea.Contains(Input.GetTouch(i).position) && !isMovementHeld)
            {
                movementTouch = Input.GetTouch(i);
                isMovementHeld = true;
            }
            if(Input.GetTouch(i).phase == TouchPhase.Moved && movementTouch != null && Input.GetTouch(i).fingerId == movementTouch.Value.fingerId)
            {
                movementTouch = Input.GetTouch(i);
            }
            if(Input.GetTouch(i).phase == TouchPhase.Ended && Input.GetTouch(i).fingerId == movementTouch.Value.fingerId)
            {
                isMovementHeld = false;
            }
        }
        
        if(movementTouch != null)
        {
            Vector3 playerPosition = Camera.main.WorldToScreenPoint(GetComponent<Transform>().position);

            //Maybe delete clamping later
            float mousePositionY = Mathf.Clamp(movementTouch.Value.position.y, 0, Camera.main.pixelHeight);
            float scaledSpeedY = (mousePositionY - playerPosition.y) / Camera.main.pixelHeight * verticalSpeed;

            Vector3 velocity = new Vector3(horizontalSpeed, scaledSpeedY, 0.0f);
            GetComponent<Rigidbody>().velocity = velocity;
        }
        else
        {
            Vector3 velocity = new Vector3(horizontalSpeed, 0f, 0f);
            GetComponent<Rigidbody>().velocity = velocity;
        }
        

        CalculateColors();

        RotatePlayerDisplay();
    }



    void CalculateColors()
    {
        Red = false;
        Green = false;
        Blue = false;
        if (Input.GetKey("q") || redButton.buttonPressed)
        {
            Red = true;
        }
        if (Input.GetKey("w") || greenButton.buttonPressed)
        {
            Green = true;
        }
        if (Input.GetKey("e") || blueButton.buttonPressed)
        {
            Blue = true;
        }
        SetWindColor();
    }

    void RotatePlayerDisplay()
    {
        GetComponentInChildren<Transform>().Rotate(new Vector3(1, 0, 0), rotateSpeed);
    }
    
	private void SetWindColor ()
	{

        this.color = colorsManager.GetColor(Red, Green, Blue);
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
		//#Audio
		//audioSource[1].clip = point;
		//audioSource[1].Play ();
		//---

        uiController.UpdateScore(score);
    }

    private void updateHP()
    {
		//#Audio
		//audioSource[1].clip = miss;
		//audioSource[1].Play ();
		//---

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
            horizontalSpeed += horizontalSpeedIncrease;
            verticalSpeed += verticalSpeedIncrease;
        }
 
    }

    public void pauseGame()
    {
		//#Audio
		//audioSource [0].Pause ();
		//audioSource [2].Pause ();
		//---

        verticalSpeedOnPause = verticalSpeed;
        horizontalSpeedOnPause = horizontalSpeed;

        horizontalSpeed = 0;
        verticalSpeed = 0;

        GameController.SetActive(false);
    }

    public void resumeGame()
    {
		//#Audio
		//audioSource [0].Play ();
		//audioSource [2].Play ();
		//---

        verticalSpeed = verticalSpeedOnPause;
        horizontalSpeed = horizontalSpeedOnPause;

        GameController.SetActive(true);
    }
    
}
