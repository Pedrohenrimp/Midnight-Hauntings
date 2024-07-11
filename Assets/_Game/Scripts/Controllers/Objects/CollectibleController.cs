using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectibleControler : MonoBehaviour
{
    public static Action OnCollected;

    private bool gateIsOpenned = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            OnCollected?.Invoke();
            Debug.Log("Item Collected");
            Destroy(gameObject);
        }
    }
}
