using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card :
    MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler {

  private CanvasGroup cardCanvasGroup;
  [SerializeField] public GameObject towerState;
  [SerializeField] public GameObject cardState;
  public Overlay overlay {get;set;}
  public Hand hand {get;set;}
  public DiscardZone discard {get;set;}
  public Grid grid {get;set;}

  public void Awake() {
    cardCanvasGroup = GetComponent<CanvasGroup>();
    towerState.SetActive(false);
  }

  public void OnPointerEnter(PointerEventData eventData) {
    transform.localScale += new Vector3(0.25F, 0.25f, 0);
  }
  
  public void OnPointerExit(PointerEventData eventData) {
    transform.localScale = new Vector3(1f, 1f, 1f);
  }

  public void OnBeginDrag(PointerEventData eventData) {
    cardCanvasGroup.blocksRaycasts = false;
    cardCanvasGroup.alpha = 0.6f;
    this.transform.SetParent(overlay.transform);
    overlay.HideOverlay();
    grid.ShowGrid();
  }

  public void OnDrag(PointerEventData eventData) {
    this.transform.position = eventData.position;
  }

  public void OnEndDrag(PointerEventData eventData) {
    cardCanvasGroup.blocksRaycasts = true;
    cardCanvasGroup.alpha = 1f;
    overlay.ShowOverlay();
    grid.HideGrid();
  }

  public void OnPointerDown(PointerEventData eventData) {
    // Debug.Log("OnPointerDown");
  }

  public void PlaceTroopState() {
    cardState.SetActive(false);
    towerState.SetActive(true);
  }

  public void MoveCardState() {
    cardState.SetActive(true);
    towerState.SetActive(false);
  }

  public void Cancel() {
    ChangeDraggableState();
    transform.SetParent(hand.transform);
  }

  public void Discard() {
    ChangeDraggableState();
    transform.SetParent(discard.transform);
    transform.position = discard.transform.position;
  }

  public void ChangeDraggableState(bool placeTroop = false) {
    if (placeTroop) {
      PlaceTroopState();
    } else {
      MoveCardState();
    }
  }
}
