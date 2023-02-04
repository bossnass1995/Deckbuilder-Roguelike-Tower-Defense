using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hand :
    MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

  private Animator anim;

  void Awake() {
    anim = gameObject.GetComponent<Animator>();
  }

  public void Hide() {
    if (anim?.GetBool("Hidden") == false) {
      anim?.SetTrigger("Hide");
      anim?.SetBool("Hidden", true);
    }
  }

  public void Show() {
    if (anim?.GetBool("Hidden") == true) {
      anim?.SetTrigger("Show");
      anim?.SetBool("Hidden", false);
      anim?.SetBool("Opened", false);
    }
  }

  public void OnPointerEnter(PointerEventData eventData) {
    if (anim?.GetBool("Opened") == false) {
      anim?.SetTrigger("Open");
      anim?.SetBool("Opened", true);
    }
  }

  public void OnPointerExit(PointerEventData eventData) {
    if (anim?.GetBool("Opened") == true && !RectTransformUtility.RectangleContainsScreenPoint(gameObject.GetComponent<RectTransform>(), eventData.position, null)) {
      anim?.SetTrigger("Close");
      anim?.SetBool("Opened", false);
    }
  }

}
