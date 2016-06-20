using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

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

	//RELOAD
	public Text text_reload;
	private float text_reloadAnim;
	public CanvasGroup groupReload;

	// Kills
	public Text text_kills;
	private int numKills;

	// Time
	public Text text_time;
	private float startTime;

	protected void Awake()
	{
		text_scorePopupGroup.alpha = 0f;
		groupReload.alpha = 0f;
		text_reloadAnim = -1f;
		numKills = 0;
		startTime = Time.time;
		text_kills.text = "0 KILLS";

		playerCurrency = Dependency.Get<Currency>();
	}

	protected void OnEnable()
	{
		GlobalEvents.AddListener<CurrencyReceivedEvent>(OnCurrencyReceivedEvent);
		GlobalEvents.AddListener<EntityDiedEvent>(OnEntityDiedEvent);
	}

	protected void OnDisable()
	{
		GlobalEvents.RemoveListener<CurrencyReceivedEvent>(OnCurrencyReceivedEvent);
		GlobalEvents.RemoveListener<EntityDiedEvent>(OnEntityDiedEvent);
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

			playerEntity.Events.AddListener<ReloadEvent> (onReloadEvent);
			playerMagazine = playerEntity.GetMagazine();
			playerStockPile = playerEntity.GetStockPile();

			if(playerMagazine == null || playerStockPile == null)
			{
				Debug.LogError("No magazine or stockpile found");
				return;
			}
		}

		float currentTime = Time.time;
		int minutes = Mathf.FloorToInt((currentTime - startTime) / 60f);
		int seconds = Mathf.FloorToInt((currentTime - startTime) % 60f);
		text_time.text = "";

		text_time.text += minutes < 10 ? "0" + minutes : minutes.ToString();
		text_time.text += ":";
		text_time.text += seconds < 10 ? "0" + seconds : seconds.ToString();

		text_ammoCurrent.text = "" + playerMagazine.Remaining;
		text_ammoLeft.text = "" + playerStockPile.Remaining;
		text_credits.text = playerCurrency.Amount + " CR";

		if(text_reloadAnim > 0f)
		{
			text_reloadAnim -= Time.deltaTime;
		}

		if((playerMagazine.Remaining < 11) != text_reload.enabled && text_reloadAnim <= 0f)
		{
			StartCoroutine (SwitchReloadText((playerMagazine.Remaining < 11)));
		}
	}

	private void onReloadEvent(ReloadEvent _event)
	{
		if (!text_reload.enabled) 
		{
			text_reloadAnim = 2f;
		} 
		else
		{
			StartCoroutine (SwitchReloadText(false, 2f));
		}
	}

	private IEnumerator SwitchReloadText(bool _newState, float delay = 0.25f)
	{
		if(_newState) { text_reload.enabled = true; }
		text_reloadAnim = delay;
		HOTweenHelper.Fade (groupReload, _newState ? 0f : 1f, _newState ? 1f : 0f, 0.25f);
		yield return new WaitForSeconds (0.2f);
		if(!_newState) { text_reload.enabled = false; }
	}

	private IEnumerator ShowScorePopup()
	{
		if(text_scorePopupScaleTween != null && !text_scorePopupScaleTween.IsComplete())
		{
			text_scorePopupScaleTween.Kill ();
		}

		if(text_scorePopupFadeTween != null && !text_scorePopupFadeTween.IsComplete())
		{
			text_scorePopupFadeTween.Kill ();
		}

		text_scorePopupGroup.alpha = 1f;
		text_scorePopup.rectTransform.localScale = new Vector3 (1.8f, 1.8f, 1.8f);
		text_scorePopupScaleTween = HOTweenHelper.Scale(text_scorePopup.rectTransform, new Vector3(1f, 1f, 1f), 0.2f, 0f, Ease.OutCubic);

		yield return new WaitForSeconds (1f);

		text_scorePopupFadeTween = HOTweenHelper.Fade (text_scorePopupGroup, 1f, 0f, 0.5f, 0f);
	}

	private void OnCurrencyReceivedEvent(CurrencyReceivedEvent evt)
	{
		text_scorePopup.text = "+" + evt.Amount;

		StopCoroutine("ShowScorePopup");
		StartCoroutine("ShowScorePopup");
	}

	private void OnEntityDiedEvent(EntityDiedEvent evt)
	{
		if(evt.Entity.HasTag("Enemy"))
		{
			numKills++;

			string suffix = numKills == 1 ? "KILL" : "KILLS";			
			text_kills.text = numKills + " " + suffix;
		}
	}
}
