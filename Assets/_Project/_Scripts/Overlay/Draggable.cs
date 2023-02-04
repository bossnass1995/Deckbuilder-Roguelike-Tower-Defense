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
  [SerializeField] private GameObject towerState;
  [SerializeField] private GameObject cardState;
  [SerializeField] private Overlay overlay;

  public void Awake() {
    cardCanvasGroup = GetComponent<CanvasGroup>();
    towerState.SetActive(false);
  }

  public void OnBeginDrag(PointerEventData eventData) {
    Debug.Log("OnBeginDrag");
    cardCanvasGroup.blocksRaycasts = false;
    cardCanvasGroup.alpha = 0.6f;
    this.transform.SetParent(draggingZone.transform);
    overlay.HideOverlay();
  }

  public void OnDrag(PointerEventData eventData) {
    this.transform.position = eventData.position;
  }

  public void OnEndDrag(PointerEventData eventData) {
    Debug.Log("OnEndDrag");
    cardCanvasGroup.blocksRaycasts = true;
    cardCanvasGroup.alpha = 1f;
    overlay.ShowOverlay();
  }

  public void OnPointerDown(PointerEventData eventData) {
    Debug.Log("OnPointerDown");
  }

  public void PlaceTroopState() {
    cardState.SetActive(false);
    towerState.SetActive(true);
  }

  public void MoveCardState() {
    cardState.SetActive(true);
    towerState.SetActive(false);
  }

  public void ChangeDraggableState(bool placeTroop = false) {
    if (placeTroop) {
      PlaceTroopState();
    } else {
      MoveCardState();
    }
  }
}
