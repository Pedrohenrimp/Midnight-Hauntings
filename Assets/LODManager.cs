using System;
using UnityEngine;

public class LODManager : MonoBehaviour
{
    [SerializeField]
    private LODGroup LODGroupComponent;

    [SerializeField]
    [Range(30, 240)]
    public static int MinFPS = 120;

    [SerializeField]
    [Range(0, 1)]
    private float WarningLimit = 0.20f;

    [SerializeField]
    [Range(0f, 10f)]
    private float UpdateTimeInSeconds = 1f;

    private int fpsLimit;

    private LOD[] lods;

    public static bool UseDinamicLOD = true;

    public static Action<bool> OnUseDinamicLODValueChanged;

    private void OnEnable()
    {
        SetupLOD(UseDinamicLOD);

        OnUseDinamicLODValueChanged += SetupLOD;
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(UpdateLOD));

        OnUseDinamicLODValueChanged -= SetupLOD;
    }

    public void SetupLOD(bool useDinamicLOD)
    {
        UseDinamicLOD = useDinamicLOD;
        if (useDinamicLOD)
        {
            fpsLimit = (int)(MinFPS * (1 + WarningLimit));

            if (LODGroupComponent == null)
                LODGroupComponent = GetComponent<LODGroup>();

            lods = LODGroupComponent.GetLODs();
            InvokeRepeating(nameof(UpdateLOD), UpdateTimeInSeconds, UpdateTimeInSeconds);
        }
        else
        {
            CancelInvoke(nameof(UpdateLOD));
        }
    }

    private void UpdateLOD()
    {
        var currentLODIndex = GetCurrentLODIndex();
        if(currentLODIndex != -1)
        {
            var currentFPS = GetFPS();
            if (currentFPS < fpsLimit)
            {
                if (currentLODIndex < LODGroupComponent.lodCount - 1)
                    LODGroupComponent.ForceLOD(currentLODIndex + 1);
            }
            else if(currentFPS >= fpsLimit)
            {
                if(currentLODIndex != 0)
                    LODGroupComponent.ForceLOD(-1);
            }
        }
    }

    private int GetCurrentLODIndex()
    {
        for (int i = 0; i < lods.Length; i++)
        {
            if (lods[i].renderers.Length > 0 && lods[i].renderers[0].isVisible)
                return i;
        }
        return -1;
    }

    private int GetFPS()
    {
        return (int)(1f / Time.deltaTime);
    }
}
