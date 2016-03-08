using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGameUI : MonoBehaviour 
{

	//SCORE POPUP
	public Text text_scorePopup;
	public CanvasGroup text_scorePopupGroup;

	void Awake ()
	{
		text_scorePopupGroup.alpha = 0f;
	}

	void OnEnable()
	{
		GlobalEvents.AddListener<EntityDiedEvent> (onEntityDied);
	}

	void Update () 
	{
		
	}

	private IEnumerator showScorePopup()
	{
		text_scorePopupGroup.alpha = 1f;

		yield return new WaitForSeconds (3f);

		text_scorePopupGroup.alpha = 0f;
	}

	private void onEntityDied(EntityDiedEvent e)
	{
		text_scorePopup.text = "+100";
		StopCoroutine ("showScorePopup");
		StartCoroutine ("showScorePopup");
	}
}
