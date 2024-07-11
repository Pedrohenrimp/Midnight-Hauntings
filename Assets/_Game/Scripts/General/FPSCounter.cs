using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI FPSLabel;

    [SerializeField]
    private TextMeshProUGUI MinLabel;

    [SerializeField]
    private TextMeshProUGUI MaxLabel;

    private int min = 1000000;
    private int max = 0;
    private void OnEnable()
    {
        if(FPSLabel != null)
            InvokeRepeating(nameof(SetFPS), 1, 1);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(SetFPS));
    }

    private void SetFPS()
    {
        if (FPSLabel != null)
        {
            var fps = (int)(1f / Time.deltaTime);
            FPSLabel.text = $"FPS: {fps}";

            min = Mathf.Min(min, fps);
            max = Mathf.Max(max, fps);

            if(MinLabel != null)
                MinLabel.text = $"Min: {min}";

            if (MaxLabel != null)
                MaxLabel.text = $"Max: {max}";


        }
    }
}
