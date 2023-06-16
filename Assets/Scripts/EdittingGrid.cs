using System.Collections.Generic;
using UnityEngine;

public class EdittingGrid : MonoBehaviour {

    [SerializeField] private int initialWidth;
    [SerializeField] private int initialDepth;
    [SerializeField] private int initialHeight;
    [SerializeField] private GameObject emptyBlockPrefab;
    
    private Dictionary<int, Dictionary<int, Dictionary<int, GameObject>>> grid;
    
    private void Start() {
        grid = new Dictionary<int, Dictionary<int, Dictionary<int, GameObject>>>();

        for (int x = 0; x < initialWidth; x++) {
            grid[x] = new Dictionary<int, Dictionary<int, GameObject>>();
            for (int y = 0; y < initialHeight; y++) {
                grid[x][y] = new Dictionary<int, GameObject>();
                for (int z = 0; z < initialDepth; z++) {
                    grid[x][y][z] = Instantiate(emptyBlockPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
                }
            }
        }
    }

    // Update is called once per frame
    private void Update() {
    }
}