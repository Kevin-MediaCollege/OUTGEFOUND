using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Holoville.HOTween;

public class InGameUI : MonoBehaviour 
{
	private Entity playerEntity;
	private Magazine playerMagazine;
	private Stockpile playerStockPile;
	private Currency playerCurrency;

	//CREDITS
	public Text text_credits;

	//SCORE POPUP
	public Text text_scorePopup;
	public CanvasGroup text_scorePopupGroup;
	private Tweener text_scorePopupScaleTween;
	private Tweener text_scorePopupFadeTween;

	//AMMO
	public Text text_ammoCurrent;
	public Text text_ammoLeft;

	protected void Awake()
	{
		text_scorePopupGroup.alpha = 0f;

		playerCurrency = Dependency.Get<Currency>();
	}

	protected void OnEnable()
	{
		GlobalEvents.AddListener<CurrencyReceivedEvent>(OnCurrencyReceivedEvent);
	}

	protected void OnDisable()
	{
		GlobalEvents.RemoveListener<CurrencyReceivedEvent>(OnCurrencyReceivedEvent);
	}

	protected void Update() 
	{
		if(playerEntity == null || playerMagazine == null || playerStockPile == null)
		{
			playerEntity = EntityUtils.GetEntityWithTag("Player");

			if(playerEntity == null)
			{
				Debug.LogError("No player found");
				return;
			}

			playerMagazine = playerEntity.GetMagazine();
			playerStockPile = playerEntity.GetStockPile();

			if(playerMagazine == null || playerStockPile == null)
			{
				Debug.LogError("No magazine or stockpile found");
				return;
			}
		}

		text_ammoCurrent.text = "" + playerMagazine.Remaining;
		text_ammoLeft.text = "" + playerStockPile.Remaining;
		text_credits.text = playerCurrency.Amount + " CR";
	}

	private IEnumerator ShowScorePopup()
	{
		if(text_scorePopupScaleTween != null && !text_scorePopupScaleTween.isComplete)
		{
			text_scorePopupScaleTween.Kill ();
		}

		if(text_scorePopupFadeTween != null && !text_scorePopupFadeTween.isComplete)
		{
			text_scorePopupFadeTween.Kill ();
		}

		text_scorePopupGroup.alpha = 1f;
		text_scorePopup.rectTransform.localScale = new Vector3 (1.8f, 1.8f, 1.8f);
		text_scorePopupScaleTween = HOTweenHelper.Scale(text_scorePopup.rectTransform, new Vector3(1f, 1f, 1f), 0.2f, 0f, Holoville.HOTween.EaseType.EaseOutCubic);

		yield return new WaitForSeconds (1f);

		text_scorePopupFadeTween = HOTweenHelper.Fade (text_scorePopupGroup, 1f, 0f, 0.5f, 0f);
	}

	private void OnCurrencyReceivedEvent(CurrencyReceivedEvent evt)
	{
		text_scorePopup.text = "+" + evt.Amount;

		StopCoroutine("ShowScorePopup");
		StartCoroutine("ShowScorePopup");
	}
}
