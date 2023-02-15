using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DropZone :
    MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

  public Color startingColor;
  public Color hoverColor;

  protected Image dropZoneBackground;

  private void Awake() {
    if (dropZoneBackground == null) {
      dropZoneBackground = this.transform.GetComponent<Image>();
    }
  }

  public void OnDrop(PointerEventData eventData) {
    dropZoneBackground.color = startingColor;
    if (eventData.pointerDrag != null) {
      OnDropHandler(eventData);
    }
  }

  public void OnPointerEnter(PointerEventData eventData) {
    // If the pointer is dragging something, highlight the drop zone
    if (eventData.pointerDrag != null) {
      OnPointerEnterHandler(eventData);
      dropZoneBackground.color = hoverColor;
      var card = eventData.pointerDrag;
      if (card != null) {
        card.GetComponent<Card>()?.ChangeDraggableState(PlacesTroop());
      }
    }
  }

  public void OnPointerExit(PointerEventData eventData) {
    if (eventData.pointerDrag != null) {
      OnPointerExitHandler(eventData);
    }
    dropZoneBackground.color = startingColor;
  }

  public abstract void OnDropHandler(PointerEventData eventData);
  public abstract void OnPointerEnterHandler(PointerEventData eventData);
  public abstract void OnPointerExitHandler(PointerEventData eventData);
  public virtual bool PlacesTroop() { return false; }
}
