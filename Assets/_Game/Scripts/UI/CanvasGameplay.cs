using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGameplay : UICanvas
{
    public FloatingJoystick joyStick;
    public void SettingsButton()
    {
        GameManager.Instance.ChangeGameState(GameState.Pausing);
        UIManager.Instance.Open<CanvasSettings>().SetState(this);
    }
}
