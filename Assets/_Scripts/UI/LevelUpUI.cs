using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    private void OnEnable()
    {
        snail.ManageGame.TogglePause();

    }
    private void OnDisable()
    {
        snail.ManageGame.TogglePause();
    }
}
