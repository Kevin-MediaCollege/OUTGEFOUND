using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Holoville.HOTween;

public class FlightCamera : MonoBehaviour 
{
	public Camera cam;
	public CanvasGroup ui;
	public Text textSettings;
	public GameObject pausedAction;

	//tween
	public GameObject slideStart;
	public GameObject slideLine;
	public GameObject slideEnd;
	private bool sliding;
	private float tweenLenght;
	private float tweenTime;
	private bool linearTween;
	private Tweener positionTween;
	private Tweener rotationTween;

	private Vector3 savedCamPosition;
	private Vector3 savedCamRotation;

	//keys
	private bool shift;
	private bool alt;

	void Awake () 
	{
		tweenLenght = 1f;
		linearTween = true;

		if(pausedAction != null)
		{
			pausedAction.SetActive(false);	
		}
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.RightShift) && pausedAction != null)
		{
			pausedAction.SetActive(!pausedAction.activeSelf);	
		}

		if(sliding)
		{
			if(Input.GetKeyDown(KeyCode.Space) || tweenTime > 1.5f)
			{
				sliding = false;
				ui.alpha = 1f;

				cam.transform.position = savedCamPosition;
				cam.transform.rotation = Quaternion.Euler(savedCamRotation);

				slideStart.SetActive(true);
				slideLine.SetActive(true);
				slideEnd.SetActive(true);

				rotationTween.Kill();
				positionTween.Kill();
			}
			else
			{
				if(tweenTime >= 0f && tweenTime <= 1f)
				{
					tweenTime += Time.deltaTime / tweenLenght;
				}
				else
				{
					tweenTime += Time.deltaTime;
				}
			}
		}
		else
		{
			//key input
			updateKeys();

			//camera rotation
			if(Input.GetKey(KeyCode.Mouse1))
			{
				float deltaX = Input.GetAxis("Mouse X") * 5f;
				float deltaY = Input.GetAxis("Mouse Y") * 5f;

				Vector3 rotationCurrent = cam.transform.rotation.eulerAngles;
				rotationCurrent.x -= deltaY;
				rotationCurrent.y += deltaX;
				cam.transform.rotation = Quaternion.Euler(rotationCurrent);
			}

			//movement speed
			float speed = shift ? 0.3f : alt ? 0.01f : 0.05f;

			//camera position
			if(Input.GetKey(KeyCode.W))
			{
				cam.transform.position = cam.transform.position + (cam.transform.forward * speed);
			}
			if(Input.GetKey(KeyCode.S))
			{
				cam.transform.position = cam.transform.position - (cam.transform.forward * speed);
			}
			if(Input.GetKey(KeyCode.A))
			{
				cam.transform.position = cam.transform.position - (cam.transform.right * speed);
			}
			if(Input.GetKey(KeyCode.D))
			{
				cam.transform.position = cam.transform.position + (cam.transform.right * speed);
			}

			//Movie keys
			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				slideStart.transform.position = cam.transform.position;
				slideStart.transform.rotation = cam.transform.rotation;
				updateSlideObjects();
			}			
			if(Input.GetKeyDown(KeyCode.Alpha2))
			{
				slideEnd.transform.position = cam.transform.position;
				slideEnd.transform.rotation = cam.transform.rotation;
				updateSlideObjects();
			}
			if(Input.GetKey(KeyCode.Alpha3))
			{
				tweenLenght -= (shift ? 4f : alt ? 0.05f : 0.5f) * Time.deltaTime;
			}			
			if(Input.GetKey(KeyCode.Alpha4))
			{
				tweenLenght += (shift ? 4f : alt ? 0.05f : 0.5f) * Time.deltaTime;
			}			
			if(Input.GetKeyDown(KeyCode.Alpha5))
			{
				linearTween = !linearTween;
			}
			if(Input.GetKeyDown(KeyCode.Space))
			{
				sliding = true;
				ui.alpha = 0f;

				savedCamPosition = cam.transform.position;
				savedCamRotation = cam.transform.rotation.eulerAngles;

				tweenTime = -0.5f;
				cam.transform.position = slideStart.transform.position;
				cam.transform.rotation = slideStart.transform.rotation;

				positionTween = HOTweenHelper.TransformPosition(cam.transform, slideEnd.transform.position, tweenLenght, 0.5f, linearTween ? Holoville.HOTween.EaseType.Linear : Holoville.HOTween.EaseType.EaseInOutCubic);
				rotationTween = HOTweenHelper.Rotate(cam.transform, slideEnd.transform.rotation, tweenLenght, 0.5f, linearTween ? Holoville.HOTween.EaseType.Linear : Holoville.HOTween.EaseType.EaseInOutCubic);

				slideStart.SetActive(false);
				slideLine.SetActive(false);
				slideEnd.SetActive(false);
			}

			textSettings.text = "Current Settings:\nTween Lenght in seconds = " + (Mathf.FloorToInt(tweenLenght * 100f) / 100f + "\nTransition type = " + (linearTween ? "Linear" : "InOutCubic"));
		}
	}

	private void updateKeys()
	{
		shift = Input.GetKeyDown(KeyCode.LeftShift) ? true : Input.GetKeyUp(KeyCode.LeftShift) ? false : shift;
		alt = Input.GetKeyDown(KeyCode.LeftAlt) ? true : Input.GetKeyUp(KeyCode.LeftAlt) ? false : alt;
	}

	private void updateSlideObjects()
	{
		slideLine.transform.position = slideStart.transform.position - ((slideStart.transform.position - slideEnd.transform.position) * 0.5f);
		slideLine.transform.LookAt(slideEnd.transform.position);
		slideLine.transform.localScale = new Vector3(0.1f, 0.1f, Vector3.Distance(slideStart.transform.position, slideEnd.transform.position));
	}
}
