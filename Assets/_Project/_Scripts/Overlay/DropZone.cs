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
  public bool cancels;
  public bool overrideDropEvent;
  public bool overridePlacement;

  private Image dropZoneBackground;

  private void Awake() {
    dropZoneBackground = this.transform.GetComponent<Image>();
    if (dropZoneBackground == null) {
      dropZoneBackground = this.transform.GetChild(0).GetComponent<Image>();
    }
  }

  public void OnDrop(PointerEventData eventData) {
    dropZoneBackground.color = startingColor;
    if (overrideDropEvent) return;
    if (eventData.pointerDrag != null) {
      if (cancels) {
        ResetSize();
        eventData.pointerDrag.GetComponent<Draggable>()?.Cancel();
        return;
      }
      var draggable = eventData.pointerDrag.GetComponent<RectTransform>();
      draggable.SetParent(this.transform);
      if (overridePlacement) {
        draggable.transform.position = this.transform.position;
      }
    }
  }

  public void OnPointerEnter(PointerEventData eventData) {
    // If the pointer is dragging something, highlight the drop zone
    if (overrideDropEvent) return;
    if (eventData.pointerDrag != null) {
      if (cancels) transform.localScale += new Vector3(0.25f, 0.25f, 0);
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
    if (cancels) ResetSize();
    dropZoneBackground.color = startingColor;
  }

  private void ResetSize() {
    transform.localScale = new Vector3(1f, 1f, 1f);
  }
}
