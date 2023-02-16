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
    var card = eventData.pointerDrag.GetComponent<Card>();
    if (slotTaken) {
      card?.Cancel();
      return;
    }
    card.transform.position = this.transform.position;
    Debug.Log(card.transform.position);
    Debug.Log(card.transform.localPosition);
    var worldPosition = card.transform.localToWorldMatrix.GetPosition();
    Debug.Log(worldPosition);
    CommunicationBridge.INSTANCE.CreateTower(new TowerDTO(card.towerState, card.transform.position.x, card.transform.position.y));
    slotTaken = true;
    card?.Discard();
  }

  public override void OnPointerEnterHandler(PointerEventData eventData) {
    
  }

  public override void OnPointerExitHandler(PointerEventData eventData) {
    
  }

  public override bool PlacesTroop() { return true; }
}
