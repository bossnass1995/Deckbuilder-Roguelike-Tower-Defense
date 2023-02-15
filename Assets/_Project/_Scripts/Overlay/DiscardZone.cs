using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiscardZone : DropZone {

  public override void OnDropHandler(PointerEventData eventData) {
    var card = eventData.pointerDrag.GetComponent<RectTransform>();
    card.SetParent(this.transform);
    card.position = this.transform.position;
  }

  public override void OnPointerEnterHandler(PointerEventData eventData) {

  }

  public override void OnPointerExitHandler(PointerEventData eventData) {

  }
}
