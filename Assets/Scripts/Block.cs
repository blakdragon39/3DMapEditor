using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField] private Material unselectedMaterial;
    [SerializeField] private Material selectedMaterial;
    
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    private MeshRenderer meshRenderer;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update() {
    }

    public void SetSelected(bool selected) {
        Material[] materials = meshRenderer.materials;
        materials[0] = selected ? selectedMaterial : unselectedMaterial;
        meshRenderer.materials = materials;
    }
}