using UnityEngine;

public class GameLayers : MonoBehaviour {
    [SerializeField] private LayerMask gridLayer;
    
    public static GameLayers instance { get; set; }

    public LayerMask GridLayer => gridLayer;

    private void Awake() {
        instance = this;
    }
}