using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card :
    MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

  [SerializeField] private GameObject towerState;
  [SerializeField] private GameObject cardState;
  private CanvasGroup cardCanvasGroup;
  public Color backgroundColor;
  
  public void Awake() {
    cardCanvasGroup = GetComponent<CanvasGroup>();
    towerState.transform.GetComponent<Image>().color = backgroundColor;
    cardState.transform.GetComponent<Image>().color = backgroundColor;
  }

  public void OnPointerEnter(PointerEventData eventData) {
    transform.localScale += new Vector3(0.25F, 0.25f, 0);
    Hand hand = transform.parent.GetComponent<Hand>();
    if (hand != null) {
      hand.OnPointerEnter(eventData);
    }
  }
  
  public void OnPointerExit(PointerEventData eventData) {
    transform.localScale = new Vector3(1f, 1f, 1f);
  }
}
