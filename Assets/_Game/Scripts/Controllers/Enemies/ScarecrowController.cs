using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowController : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 10f)]
    private float rotationTimeInterval = 4f;

    [SerializeField]
    [Range(0f, 10f)]
    private float transitionDuration = 1.5f;

    [SerializeField]
    private List<int> rotationValues = new List<int>() {45, -45, 90, -90 };

    private void OnEnable()
    {
        InvokeRepeating(nameof(RotateRandom), Mathf.Max(0, rotationTimeInterval - transitionDuration), rotationTimeInterval);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(RotateRandom));
    }

    private void RotateRandom()
    {
        var i = Random.Range(0, rotationValues.Count);
        var rotation = rotationValues[i];

        transform.DORotate(new Vector3(0, transform.rotation.eulerAngles.y + rotation, 0), transitionDuration);
    }
}
