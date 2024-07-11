using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GateController : MonoBehaviour
{
    [SerializeField]
    private Animator GateAnimator;

    private bool gateIsOpenned = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            if(gateIsOpenned)
            {
                GateAnimator.Play("close_gate");
                gateIsOpenned = false;
            }
        }
    }
}
