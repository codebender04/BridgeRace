using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGameplay : UICanvas
{
    public void SettingsButton()
    {
        GameManager.Instance.ChangeGameState(GameState.Menu);
        UIManager.Instance.Open<CanvasSettings>().SetState(this);
    }
}
