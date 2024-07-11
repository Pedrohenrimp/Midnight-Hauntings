using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private GameObject phaseComponent;

    [SerializeField]
    private TextMeshProUGUI timeLabel;

    [SerializeField]
    private Slider timeSlider;

    [SerializeField]
    private int totalSeconds = 180;

    [SerializeField]
    private TextMeshProUGUI collectibleItemLabel;

    [SerializeField]
    private GameObject collectibleComponent;

    [SerializeField]
    [Range(1, 10)]
    private int totalCollectibles = 10;

    private int collectedItems = 0;

    private int remainingTime;

    private string stringFormat = "{0:0}:{1:00}";

    public static Action OnTimeEnded;

    private void OnEnable()
    {
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
        SetupPhaseComponent();

        collectibleItemLabel.text = $"{collectedItems}/{totalCollectibles}";
        remainingTime = totalSeconds;
        SetupTime();

        PlayerController.OnPlayerDied += OnPlayerDiedCallback;
        CollectibleControler.OnCollected += OnItemCollectedCallback;
    }

    private void OnDisable()
    {
        pauseButton.onClick.RemoveListener(OnPauseButtonClicked);

        PlayerController.OnPlayerDied -= OnPlayerDiedCallback;
        CollectibleControler.OnCollected += OnItemCollectedCallback;
    }

    public void OnPauseButtonClicked()
    {
    }

    private async void SetupPhaseComponent()
    {
        phaseComponent.transform.localScale = Vector3.zero;
        await Task.Delay(500);
        phaseComponent?.SetActive(true);
        phaseComponent?.transform.DOScale(Vector3.one, 0.3f);
        await Task.Delay(400);
        phaseComponent?.transform.DOShakeScale(0.3f,0.1f,1);
        await Task.Delay(3500);
        phaseComponent?.transform.DOShakeScale(0.3f, 0.1f, 1);
        await Task.Delay(200);
        phaseComponent.transform.DOScale(Vector3.zero, 0.3f);
        await Task.Delay(2000);
        phaseComponent?.SetActive(false);
    }

    private void OnPlayerDiedCallback()
    {

    }

    private void OnItemCollectedCallback()
    {
        collectedItems++;
        collectibleItemLabel.text = $"{collectedItems}/{totalCollectibles}";
        collectibleComponent.transform.DOShakeScale(0.3f, 0.2f, 1);
    }

    private void SetupTime()
    {
        var minutes = Math.Floor(remainingTime / 60d);
        var seconds = Math.Floor(remainingTime % 60d);

        timeLabel.text = string.Format(stringFormat, minutes, seconds);
        timeSlider.value = 1;


        Invoke(nameof(DecreaseTime), 3);
    }

    private void DecreaseTime()
    {
        remainingTime--;
        if(remainingTime > 0)
        {

            var minutes = Math.Floor(remainingTime / 60d);
            var seconds = Math.Floor(remainingTime % 60d);
            
            timeLabel.text = string.Format(stringFormat, minutes, seconds);
            SetupTextColor();

            SetupSliderColor();
            timeSlider.DOValue((float)remainingTime / totalSeconds, 1);


        }
        else
        {
            timeLabel.text = string.Format(stringFormat, 0, 0);
            timeSlider.image.enabled = false;
            OnTimeEnded?.Invoke();
            Debug.Log("Time Ended");
            return;
        }

        Invoke(nameof(DecreaseTime), 1);
    }

    private void SetupSliderColor()
    {
        if(remainingTime == totalSeconds)
        {
            timeSlider.image.color = Color.green;
        }
        else if(remainingTime == totalSeconds/2)
        {
            timeSlider.image.DOColor(Color.white, 1);
        }
        else if(remainingTime == 15)
        {
            timeSlider.image.DOColor(new Color(1, 0.3f, 0.3f), 1);
        }
    }

    private async void SetupTextColor()
    {
        if (remainingTime <= 55)
        {
            timeLabel.DOColor(new Color(1f, 0.5f, 0.5f), 0.3f);
            await Task.Delay(500);
            timeLabel.DOColor(Color.white, 0.5f);
        }
    }
}
