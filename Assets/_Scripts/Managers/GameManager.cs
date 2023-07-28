using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player _player;
    [SerializeField] GameObject _skillUI;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponentInChildren<Player>();
        _player.playerLeveledUp += OpenSelectSkillUI;
    }

    private void OpenSelectSkillUI()
    {
        _skillUI.SetActive(true);
    }
}
