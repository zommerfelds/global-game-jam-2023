using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer renderer = transform.GetComponent<SpriteRenderer>();

        objectWidth = renderer.bounds.size.x / 2;
        objectHeight = renderer.bounds.size.y / 2;
        screenBounds = Camera.main.ScreenToWorldPoint(
            new Vector3(
                Screen.width - objectWidth,
                Screen.height - objectHeight - 100,
                Camera.main.transform.position.z
            )
        );
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        //viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1, screenBounds.x);
        //viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1, screenBounds.y);
        //transform.position = viewPos;

        if (Mathf.Abs(viewPos.x) > screenBounds.x)
        {
            gameObject.GetComponent<DropletController>().OnHitWall();
        }
    }
}
