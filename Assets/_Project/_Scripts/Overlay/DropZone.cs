using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone :
    MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

  public Color startingColor;
  public Color hoverColor;
  public bool placeTroop;
  public bool overrideDropEvent;
  public bool overridePlacement;

  private Image dropZoneBackground;

  private void Awake() {
    dropZoneBackground = this.transform.GetComponent<Image>();
  }

  public void OnDrop(PointerEventData eventData) {
    Debug.Log("OnDrop");
    if (overrideDropEvent) return;
    if (eventData.pointerDrag != null) {
      var draggable = eventData.pointerDrag.GetComponent<RectTransform>();
      draggable.SetParent(this.transform);
      dropZoneBackground.color = startingColor;
      if (overridePlacement) {
        draggable.transform.position = this.transform.position;
      }
    }
  }

  public void OnPointerEnter(PointerEventData eventData) {
    // If the pointer is dragging something, highlight the drop zone
    if (overrideDropEvent) return;
    if (eventData.pointerDrag != null) {
      Debug.Log("OnPointerEnter with " + eventData.pointerDrag.ToString());
      dropZoneBackground.color = hoverColor;
      var draggable = eventData.pointerDrag;
      if (draggable != null) {
        draggable.GetComponent<Draggable>()?.ChangeDraggableState(placeTroop);
      }
    }
  }

  public void OnPointerExit(PointerEventData eventData) {
    if (overrideDropEvent) return;
    dropZoneBackground.color = startingColor;
  }
}
