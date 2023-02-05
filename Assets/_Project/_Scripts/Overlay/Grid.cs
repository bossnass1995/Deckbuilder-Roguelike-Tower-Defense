using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
  public GameObject gridSlotPrefab;

  public int columns;
  public int rows;

  private float gridStartX;
  private float gridStartY;

  void Awake() {
    HideGrid();
    var gridRect = (RectTransform) this.gameObject.transform;
    if (!gridRect) {
      Debug.Log("Unable to find Rect Transform for Grid");
      return;
    }
    var gridWidth = gridRect.rect.width;
    var gridHeight = gridRect.rect.height;
    var gridPosX = gridRect.position.x;
    var gridPosY = gridRect.position.y;

    Debug.Log("Grid is " + gridWidth + "x" + gridHeight + " at " + gridPosX + ", " + gridPosY);

    float colSize = gridWidth / columns;
    float rowSize = gridWidth / rows;

    this.gridSlotPrefab.transform.localScale = new Vector3(colSize, rowSize, 0);

    this.gridStartX = gridRect.rect.xMin;
    this.gridStartY = gridRect.rect.yMin;

    for (int i = 0; i < columns; i++) {
      for (int j = 0; j < rows; j++) {
        var xPos = (i * colSize) + (colSize / 2) - (gridWidth / 2) + gridPosX;
        var yPos = (j * rowSize) + (rowSize / 2) - (gridHeight / 2) + gridPosY;
        var newSlot = Instantiate(gridSlotPrefab, new Vector3(xPos, yPos, 0f), Quaternion.identity);
        newSlot.transform.SetParent(this.transform);
      }
    }
  }

  public void ShowGrid() {
    this.gameObject.SetActive(true);
  }

  public void HideGrid() {
    this.gameObject.SetActive(false);
  }
}
