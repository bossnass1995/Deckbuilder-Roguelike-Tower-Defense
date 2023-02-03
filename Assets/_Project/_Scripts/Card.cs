using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

  [SerializeField] private GameObject towerState;
  [SerializeField] private GameObject cardState;

  public Color backgroundColor;
  public void Awake() {
    towerState.transform.GetComponent<Image>().color = backgroundColor;
    cardState.transform.GetComponent<Image>().color = backgroundColor;
  }
}
