using UnityEngine;

/// <summary>
/// Display the current FPS
/// </summary>
public class FPSDisplay : MonoBehaviour
{
	private const float REFRESH_RATE = 0.5f;

	private float timeCounter;
	private float lastFrameRate;

	private int frameCounter;

	protected void Update()
	{
		if(timeCounter < REFRESH_RATE)
		{
			timeCounter += Time.deltaTime;
			frameCounter++;
		}
		else
		{
			lastFrameRate = frameCounter / timeCounter;
			frameCounter = 0;
			timeCounter = 0;
		}
	}

	protected void OnGUI()
	{
		Vector2 size = new Vector2(60, 20);
		Vector2 position = new Vector2(Screen.width - size.x, Screen.height - size.y);

		GUI.Label(new Rect(position, size), new GUIContent("FPS: " + (int)lastFrameRate));
	}
}
