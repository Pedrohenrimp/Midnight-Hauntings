using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CauldronController : MonoBehaviour
{
    [SerializeField]
    private Animator CaudronAnimator;

    [SerializeField]
    private GameObject EffectsComponent;

    [SerializeField]
    private GameObject SecondEffectsComponent;

    public static Action OnConllectedAllItems;
    public static Action OnCauldronInteraction;

    private bool isActive;

    private bool canInteract = true;

    private bool isInside;

    private void OnEnable()
    {
        OnConllectedAllItems += ActivateCauldron;
    }

    private void OnDisable()
    {
        OnConllectedAllItems -= ActivateCauldron;
    }

    private void ActivateCauldron()
    {
        isActive = true;
        CaudronAnimator.Play("bubble_cauldron");
        EffectsComponent.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && isActive)
        {
            isInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && isActive)
        {
            isInside = false;
        }
    }

    private void Update()
    {
        if(isActive && canInteract && isInside && Input.GetKeyDown(KeyCode.Space))
        {
            canInteract = false;
            OnCauldronInteraction?.Invoke();
            Debug.Log("Cauldron Interaction");
            SecondEffectsComponent.SetActive(true);
            EffectsComponent.SetActive(false);
        }
    }
}
