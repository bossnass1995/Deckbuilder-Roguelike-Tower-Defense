using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDTO {
  public GameObject tower;
  public float posX;
  public float posY;
  public TowerDTO(GameObject tower, float posX, float posY) {
    this.tower = tower;
    this.posX = posX;
    this.posY = posY;
  }
}