using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiscardZone : DropZone {

  public override void OnDropHandler(PointerEventData eventData) {
    var draggable = eventData.pointerDrag.GetComponent<RectTransform>();
    draggable.SetParent(this.transform);
    draggable.position = this.transform.position;
  }

  public override void OnPointerEnterHandler(PointerEventData eventData) {

  }

  public override void OnPointerExitHandler(PointerEventData eventData) {

  }
}
