using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CancelZone : 
    MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
  [SerializeField] private GameObject hand;

  private Overlay overlay;

  void Awake() {
    this.overlay = this.transform.parent.GetComponent<Overlay>();
  }

  public void Cancel(RectTransform draggable) {
    ResetSize();
    draggable.GetComponent<Draggable>()?.ChangeDraggableState(false);
    draggable.SetParent(hand.transform);
    if (this.overlay != null) {
      this.overlay.ShowOverlay();
    }
  }

  public void OnDrop(PointerEventData eventData) {
    if (eventData.pointerDrag != null) {
      var draggable = eventData.pointerDrag.GetComponent<RectTransform>();
      if (draggable != null) Cancel(draggable);
    }
  }

  public void OnPointerEnter(PointerEventData eventData) {
    this.transform.localScale += new Vector3(0.25f, 0.25f, 0);
  }

  public void OnPointerExit(PointerEventData eventData) {
    ResetSize();
  }

  private void ResetSize() {
    this.transform.localScale = new Vector3(1f, 1f, 1f);
  }
}
