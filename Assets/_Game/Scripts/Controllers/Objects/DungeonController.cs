using System;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    [SerializeField]
    private Animator DungeonAnimator;

    public static Action OnPlayerEntered;


    private void OnEnable()
    {
        CauldronController.OnCauldronInteraction += OpenDungeon;
    }

    private void OnDisable()
    {
        CauldronController.OnCauldronInteraction -= OpenDungeon;
    }

    private void OpenDungeon()
    {
        DungeonAnimator.Play("open_dungeon_entrance");
        Debug.Log("The Dungeon is Openned.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            DungeonAnimator.Play("close_dungeon_entrance");
            OnPlayerEntered?.Invoke();
            Debug.Log("The Dungeon is Closed.");
        }
    }
}
