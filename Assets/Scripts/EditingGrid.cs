using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditingGrid : MonoBehaviour {

    [SerializeField] private int initialWidth;
    [SerializeField] private int initialDepth;
    [SerializeField] private int initialHeight;
    [SerializeField] private GameObject emptyBlockPrefab;
    [SerializeField] private new Camera camera;
    
    private Dictionary<int, Dictionary<int, Dictionary<int, GameObject>>> grid;

    private Block selectedBlock;
    private int curWidth;
    private int curDepth;
    private int curHeight;
    
    private void Awake() {
        grid = new Dictionary<int, Dictionary<int, Dictionary<int, GameObject>>>();
        curWidth = initialWidth;
        curDepth = initialDepth;
        curHeight = initialHeight;

        for (int x = 0; x < initialWidth; x++) {
            grid[x] = new Dictionary<int, Dictionary<int, GameObject>>();
            for (int y = 0; y < initialHeight; y++) {
                grid[x][y] = new Dictionary<int, GameObject>();
                for (int z = 0; z < initialDepth; z++) {
                    var blockObject = Instantiate(emptyBlockPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
                    var block = blockObject.GetComponent<Block>();
                    
                    blockObject.GetComponent<BoxCollider>().enabled = y == 0;
                    
                    block.X = x;
                    block.Y = y;
                    block.Z = z;
                    
                    grid[x][y][z] = blockObject;
                }
            }
        }
    }

    public void SelectBlock() {
        var mousePos = InputUtils.GetMousePos();
        var clickRay = camera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(clickRay, out var hitData)) {
            if (selectedBlock != null) {
                selectedBlock.SetSelected(false);
            }

            var newBlock = hitData.transform.gameObject.GetComponent<Block>();
            newBlock.SetSelected(true);

            selectedBlock = newBlock;
        }
    }

    public void MoveSelectionUp(InputAction.CallbackContext context) {
        if (context.started) {
            ChangeSelection(0, 1, 0);   
        }
    }
    
    public void MoveSelectionDown(InputAction.CallbackContext context) {
        if (context.started) {
            ChangeSelection(0, -1, 0);
        }
    }

    private void ChangeSelection(int x, int y, int z) {
        int newX = selectedBlock.X + x;
        int newY = selectedBlock.Y + y;
        int newZ = selectedBlock.Z + z;

        if (newX >= curWidth || newX < 0) return;
        if (newY >= curHeight || newY < 0) return;
        if (newX >= curDepth || newZ < 0) return;
        
        selectedBlock.SetSelected(false);

        var newBlock = grid[newX][newY][newZ].GetComponent<Block>();
        newBlock.SetSelected(true);

        selectedBlock = newBlock;
    }
}