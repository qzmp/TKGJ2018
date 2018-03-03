using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mozliwe kolory wiatru
public enum COLORS
{
	BLACK,
	RED,
	GREEN,
	BLUE,
	CYAN,
	MAGENTA,
	YELLOW,
	WHITE
}

public class PlayerController : MonoBehaviour {

	// Predkosc ruchu
	public float speed;

	// Referencja do kamery
	public GameObject cameraObject;

	// Flagi koloru wiatru
	private bool Red;
	private bool Green;
	private bool Blue;

	// Kolor wiatru
	public COLORS color;

	// Use this for initialization
	void Start () 
	{
		// Ustawienie koloru wiatru na czarny (brak koloru)
		Red = false;
		Green = false;
		Blue = false;
		color = COLORS.BLACK;
	}
	
	// Update is called once per frame
	void Update () {

		// Przemieszczanie gracza wzgledem pozycji myszki
		Vector3 playerPosition = cameraObject.GetComponent<Camera>().WorldToScreenPoint(GetComponent<Transform>().position);
		Debug.Log("target: " + playerPosition.y + ", mouse is: " + Input.mousePosition);
		float moveVertical = 0;
		if (Input.mousePosition.y > playerPosition.y)
			moveVertical = 1;
		else if (Input.mousePosition.y < playerPosition.y)
			moveVertical = -1;
		Vector3 movement = new Vector3 (0.0f, moveVertical, 0.0f);
		GetComponent<Rigidbody>().velocity = movement * speed;

	}

	void FixedUpdate ()
	{
		// Zmiana flag koloru po przycisnieciu klawiszy RGB
		if (Input.GetKeyDown ("r")) 
		{
			Red = !Red;
			SetWindColor ();
		}
		if (Input.GetKeyDown ("g"))
		{
			Green = !Green;
			SetWindColor ();
		}
		if (Input.GetKeyDown ("b"))
		{
			Blue = !Blue;
			SetWindColor ();
		}

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
					color = COLORS.WHITE;
				else
					color = COLORS.YELLOW;
			} 
			else if (Blue)
				color = COLORS.MAGENTA;
			else
				color = COLORS.RED;
		} 
		else if (Green) 
		{
			if (Blue)
				color = COLORS.CYAN;
			else
				color = COLORS.GREEN;
		} 
		else if (Blue)
			color = COLORS.BLUE;
		else
			color = COLORS.BLACK;
	}

	// Metoda zwracajaca kolor wiatru
	public COLORS GetColor ()
	{
		return color;
	}


}
