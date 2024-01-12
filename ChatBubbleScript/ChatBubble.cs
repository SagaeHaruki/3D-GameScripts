using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    private SpriteRenderer backGround;
    private TextMeshPro textMesh;

    private void Awake()
    {
        backGround = transform.Find("Background").GetComponent<SpriteRenderer>();
        textMesh = transform.Find("ChatText").GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        Setup("HELLOOOOOOOOOOOOOOOO");
    }

    private void Setup(string text)
    {
        textMesh.SetText(text);
        textMesh.ForceMeshUpdate();
        Vector2 textSize = textMesh.GetRenderedValues(false);
        Vector2 padding = new Vector2(7f, 2f);
        Vector2 offset = new Vector2(-2f, 0f);
        backGround.size = textSize + padding;

        backGround.transform.localPosition = new Vector3(backGround.size.x / 2f, 0f);
    }
}
