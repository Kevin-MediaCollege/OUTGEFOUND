using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Holoville.HOTween;

public class InGameUI : MonoBehaviour 
{
	private Entity playerEntity;
	private Magazine playerMagazine;
	private AmmoStockPile playerStockPile;
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

	void Awake ()
	{
		text_scorePopupGroup.alpha = 0f;
	}

	void OnEnable()
	{
		GlobalEvents.AddListener<EntityDiedEvent> (onEntityDied);
		playerEntity = EntityUtils.GetEntityWithTag ("Player");
		playerMagazine = playerEntity.GetMagazine ();
		playerStockPile = playerEntity.GetStockPile ();
		playerCurrency = Dependency.Get<Currency> ();
	}

	void OnDisable()
	{
		GlobalEvents.RemoveListener<EntityDiedEvent> (onEntityDied);
	}

	void Update () 
	{
		text_ammoCurrent.text = "" + playerMagazine.Remaining;
		text_ammoLeft.text = "" + playerStockPile.Current;
		text_credits.text = playerCurrency.Amount + " CR";
	}

	private IEnumerator showScorePopup()
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

	private void onEntityDied(EntityDiedEvent e)
	{
		if(e.DamageInfo.Hit.Source == playerEntity)
		{
			text_scorePopup.text = e.DamageInfo.Hit.Tag == "Head" ? "+250" : "+100";
			StopCoroutine ("showScorePopup");
			StartCoroutine ("showScorePopup");
		}
	}
}
