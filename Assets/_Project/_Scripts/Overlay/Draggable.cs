using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable :
    MonoBehaviour, IPointerDownHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler {

  [SerializeField] private Canvas canvas;
  [SerializeField] private GameObject draggingZone;
  private CanvasGroup cardCanvasGroup;
  [SerializeField] public GameObject towerState;
  [SerializeField] public GameObject cardState;
  [SerializeField] private Overlay overlay;
  [SerializeField] private Hand hand;
  [SerializeField] private DiscardZone discard;
  [SerializeField] private Grid grid;

  public void Awake() {
    cardCanvasGroup = GetComponent<CanvasGroup>();
    towerState.SetActive(false);
  }

  public void OnBeginDrag(PointerEventData eventData) {
    cardCanvasGroup.blocksRaycasts = false;
    cardCanvasGroup.alpha = 0.6f;
    this.transform.SetParent(draggingZone.transform);
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
