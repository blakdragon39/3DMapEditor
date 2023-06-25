using System.Collections.Generic;
using UnityEngine;

public class EditingGrid : MonoBehaviour {

    [SerializeField] private int initialWidth;
    [SerializeField] private int initialDepth;
    [SerializeField] private int initialHeight;
    [SerializeField] private GameObject emptyBlockPrefab;
    [SerializeField] private new Camera camera;
    
    private Dictionary<int, Dictionary<int, Dictionary<int, GameObject>>> grid;

    private Block selectedBlock;
    
    private void Awake() {
        grid = new Dictionary<int, Dictionary<int, Dictionary<int, GameObject>>>();

        for (int x = 0; x < initialWidth; x++) {
            grid[x] = new Dictionary<int, Dictionary<int, GameObject>>();
            for (int y = 0; y < initialHeight; y++) {
                grid[x][y] = new Dictionary<int, GameObject>();
                for (int z = 0; z < initialDepth; z++) {
                    var blockObject = Instantiate(emptyBlockPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
                    var block = blockObject.GetComponent<Block>();
                    block.X = x;
                    block.Y = y;
                    block.Z = z;
                    grid[x][y][z] = blockObject;
                }
            }
        }
    }

    private void Update() {
    }
    
    public void SelectBlock() {
        Debug.Log($"select block at ...");
        var mousePos = InputUtils.GetMousePos();
        Debug.Log($"select block at {mousePos}");
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
}