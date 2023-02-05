using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour {
  private static int MENU_SCENE = 0;
  private static int GAME_SCENE = 1;
  private static int OVERLAY_SCENE = 2;

  public static ManagerScene INSTANCE {get;private set;}
  
  void Awake() {
    if (INSTANCE == null) INSTANCE = this;
  }

  public static void StartGame() {
    SceneManager.LoadScene(GAME_SCENE);
    SceneManager.LoadScene(OVERLAY_SCENE, LoadSceneMode.Additive);
  }
}
