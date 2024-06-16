using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStage : MonoBehaviour
{
    [SerializeField] private Transform firstPosition;
    [SerializeField] private Transform secondPosition;
    [SerializeField] private Transform thirdPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ChangeGameState(GameState.Menu);
            UIManager.Instance.CloseAll();
            UIManager.Instance.Open<CanvasVictory>();
        }
    }
}
