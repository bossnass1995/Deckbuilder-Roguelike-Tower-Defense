using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridSlot : DropZone {
  private bool slotTaken = false;

  void Awake() {
    dropZoneBackground = this.transform.GetChild(0).GetComponent<Image>();
  }

  public override void OnDropHandler(PointerEventData eventData) {
    var draggable = eventData.pointerDrag.GetComponent<Draggable>();
    if (slotTaken) {
      draggable?.Cancel();
      return;
    }
    draggable.transform.position = this.transform.position;
    Debug.Log(draggable.transform.position);
    Debug.Log(draggable.transform.localPosition);
    var worldPosition = draggable.transform.localToWorldMatrix.GetPosition();
    Debug.Log(worldPosition);
    CommunicationBridge.INSTANCE.CreateTower(new TowerDTO(draggable.towerState, draggable.transform.position.x, draggable.transform.position.y));
    slotTaken = true;
    draggable?.Discard();
  }

  public override void OnPointerEnterHandler(PointerEventData eventData) {
    
  }

  public override void OnPointerExitHandler(PointerEventData eventData) {
    
  }

  public override bool PlacesTroop() { return true; }
}
