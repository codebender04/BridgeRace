using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public void PlayButton()
    {
        Close(0);
        GameManager.Instance.ChangeGameState(GameState.Playing);
        UIManager.Instance.Open<CanvasGameplay>();
        LevelManager.Instance.LoadLevel();
    }
    public void SettingsButton()
    {
        GameManager.Instance.ChangeGameState(GameState.Pausing);
        UIManager.Instance.Open<CanvasSettings>().SetState(this);
    }
}
