using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public void PlayButton()
    {
        GameManager.Instance.ChangeGameState(GameState.Playing);
        Close(0);
        UIManager.Instance.Open<CanvasGameplay>();
    }
    public void SettingsButton()
    {
        GameManager.Instance.ChangeGameState(GameState.Menu);
        UIManager.Instance.Open<CanvasSettings>().SetState(this);
    }
}
