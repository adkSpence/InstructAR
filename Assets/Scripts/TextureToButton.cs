using UnityEngine;
using UnityEngine.UI;

public class TextureToButton : MonoBehaviour
{
    public Texture2D sourceTexture;
    public Button targetButton;

    void Start()
    {
        Sprite sprite = Sprite.Create(
            sourceTexture,
            new Rect(0, 0, sourceTexture.width, sourceTexture.height),
            new Vector2(0.5f, 0.5f)
        );

        targetButton.GetComponent<Image>().sprite = sprite;
    }
}
