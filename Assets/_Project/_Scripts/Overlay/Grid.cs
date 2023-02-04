using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
  public GameObject gridSlotPrefab;
  public int size;

  private GameObject gridView;
  private float slotSize;
  private float gridStartX;
  private float gridStartY;

  void Start() {
    this.gridView = this.gameObject;
    var gridRect = (RectTransform)this.gridView.transform;
    if (!gridRect) {
      Debug.Log("Unable to find Rect Transform for Grid");
      return;
    }
    var gridWidth = gridRect.rect.width;
    var gridHeight = gridRect.rect.height;

    if (gridWidth != gridHeight) {
      Debug.Log("Grid is not a square");
      return;
    }
    var gridPosX = gridRect.position.x;
    var gridPosY = gridRect.position.y;

    Debug.Log("Grid is " + gridWidth + "x" + gridHeight + " at " + gridPosX + ", " + gridPosY);

    float slotSize = gridWidth / size;

    this.gridSlotPrefab.transform.localScale = Vector3.one * slotSize;

    this.gridStartX = gridRect.rect.xMin;
    this.gridStartY = gridRect.rect.yMin;

    for (int i = 0; i < size; i++) {
      for (int j = 0; j < size; j++) {
        var xPos = (i * slotSize) + (slotSize / 2) - (gridWidth / 2) + gridPosX;
        var yPos = (j * slotSize) + (slotSize / 2) - (gridHeight / 2) + gridPosY;
        var newSlot = Instantiate(gridSlotPrefab, new Vector3(xPos, yPos, 0f), Quaternion.identity);
        newSlot.transform.SetParent(gridView.transform);
      }
    }
  }
}
