using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The speed at which the background scrolls")]
    public float scrollSpeed;

    [Header("References")]
    public MeshRenderer meshRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed
        * GameManager.instance.CalculateGameSpeed()
        / 20
        * Time.deltaTime, 0);
    }
}
