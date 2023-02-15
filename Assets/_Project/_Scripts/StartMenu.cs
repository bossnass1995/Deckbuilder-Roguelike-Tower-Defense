using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {
  public void StartGame() {
    Debug.Log("Starting Game");
    ManagerScene.StartGame();
  }
}
