using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour {
  private List<GameObject> overlayElements;
  
  [SerializeField] private GameObject cancelZone;

  void Awake() {
    overlayElements = new List<GameObject>(GameObject.FindGameObjectsWithTag("Overlay"));
  }

  public void HideOverlay() {
    overlayElements.ForEach(x => x.SetActive(false));
    cancelZone.SetActive(true);
  }

  public void ShowOverlay() {
    overlayElements.ForEach(x => x.SetActive(true));
    cancelZone.SetActive(false);
  }
}
