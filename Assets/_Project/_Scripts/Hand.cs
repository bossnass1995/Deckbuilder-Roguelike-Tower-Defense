using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hand :
    MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

  private bool drawerOpen = false;

  public void OnPointerEnter(PointerEventData eventData) {
    if (!drawerOpen) {
      drawerOpen = true;
      transform.localPosition = new Vector3(transform.localPosition.x, -184f, transform.localPosition.z);
    }
  }

  public void OnPointerExit(PointerEventData eventData) {
    if (drawerOpen) {
      drawerOpen = false;
      transform.localPosition = new Vector3(transform.localPosition.x, -300f, transform.localPosition.z);
    }
  }

}
