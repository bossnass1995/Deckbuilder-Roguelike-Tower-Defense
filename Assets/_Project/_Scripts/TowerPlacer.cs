using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour {
  [SerializeField] private GameObject towerPrefab;

  void Awake() {
    CommunicationBridge.INSTANCE.ListenToTowerEvent(tower => {
      Debug.Log("Creating Tower");
      var uiPosition = new Vector3(tower.posX, tower.posY, 1f);
      var position = Camera.main.ScreenToWorldPoint(uiPosition);
      Debug.Log("UI Position " + uiPosition + ", World Position " + position);
      Instantiate(tower.tower, position, Quaternion.identity, this.gameObject.transform.parent);
    });
  }
}
