using UnityEngine;

public class Heart : MonoBehaviour
{

    public Sprite OnHeart;
    public Sprite OffHeart;

public SpriteRenderer HeartRenderer;
    public int LiveNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.Lives >= LiveNumber){
            HeartRenderer.sprite = OnHeart;
        } else {
            HeartRenderer.sprite = OffHeart;
        }
        
    }
}
