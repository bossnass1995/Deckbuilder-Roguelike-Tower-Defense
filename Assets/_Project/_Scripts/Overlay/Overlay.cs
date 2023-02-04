using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour {
  [SerializeField] private Hand hand;
  private Animator anim;
  
  [SerializeField] private GameObject cancelZone;

  void Awake() {
    anim = gameObject.GetComponent<Animator>();
  }

  public void HideOverlay() {
    if (anim?.GetBool("Hidden") == false) {
      anim?.SetTrigger("Hide");
      anim?.SetBool("Hidden", true);
      hand.Hide();
      cancelZone.SetActive(true);
    }
  }

  public void ShowOverlay() {
    if (anim?.GetBool("Hidden") == true) {
      anim?.SetTrigger("Show");
      anim?.SetBool("Hidden", false);
      hand.Show();
      cancelZone.SetActive(false);
    }
  }
}
