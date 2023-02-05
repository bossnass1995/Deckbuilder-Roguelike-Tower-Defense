using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommunicationBridge : MonoBehaviour {

  public static CommunicationBridge INSTANCE;
  private UnityEvent<TowerDTO> createTowerEvent = new UnityEvent<TowerDTO>();

  void Awake() {
    if (INSTANCE == null) INSTANCE = this;
  }
  
  public void CreateTower(TowerDTO tower) {
    createTowerEvent.Invoke(tower);
  }

  public void ListenToTowerEvent(UnityAction<TowerDTO> call) {
    createTowerEvent.AddListener(call);
  }
}
