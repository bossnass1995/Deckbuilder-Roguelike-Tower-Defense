using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CancelZone : DropZone {
  public override void OnDropHandler(PointerEventData eventData) {
    ResetSize();
    eventData.pointerDrag.GetComponent<Draggable>()?.Cancel();
  }

  public override void OnPointerEnterHandler(PointerEventData eventData) {
    transform.localScale += new Vector3(0.25f, 0.25f, 0);
  }

  public override void OnPointerExitHandler(PointerEventData eventData) {
    ResetSize();
  }

  public override bool PlacesTroop() { return true; }

  private void ResetSize() {
    transform.localScale = new Vector3(1f, 1f, 1f);
  }
}
