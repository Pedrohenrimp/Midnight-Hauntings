using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class VampireController : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 10f)]
    private float SleepTime = 4f;

    [SerializeField]
    [Range(0f, 10f)]
    private float AwakeTime = 2f;

    [SerializeField]
    [Range(0, 50)]
    private int LightIntensity = 15;

    [SerializeField]
    private Animator VampireAnimator;

    [SerializeField]
    private Animator CoffinAnimator;

    [SerializeField]
    private Light Light;

    [SerializeField]
    private GameObject Collider;


    private void OnEnable()
    {
        InvokeRepeating(nameof(DoAwake), SleepTime, SleepTime + AwakeTime);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(DoAwake));
    }

    private async void DoAwake()
    {
        CoffinAnimator?.Play("open_coffin");
        await Task.Delay(300);
        VampireAnimator?.Play("Waking");
        Light.gameObject.SetActive(true);
        Light.intensity = 0;
        Light.DOIntensity(LightIntensity, 0.5f);
        Collider.SetActive(true);
        await Task.Delay((int)(AwakeTime*1000));
        VampireAnimator?.Play("Laying");
        await Task.Delay(350);
        VampireAnimator?.Play("Laying Sleeping");
        CoffinAnimator?.Play("close_coffin");
        Light.DOIntensity(0, 0.5f);
        Collider.SetActive(false);
        await Task.Delay(500);
        Light.gameObject.SetActive(false);
    }
}
