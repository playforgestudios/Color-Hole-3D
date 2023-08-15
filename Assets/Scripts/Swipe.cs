using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private bool hasSwiped = false;
    public GameObject swipePanel;

    void Update()
    {
        if (!hasSwiped && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = touch.deltaPosition;

                // Check if the swipe is in a specific direction (e.g., horizontal swipe)
                if (Mathf.Abs(touchDeltaPosition.x) > Mathf.Abs(touchDeltaPosition.y))
                {
                    // Horizontal swipe detected
                    hasSwiped = true;
                    Debug.Log("Swipe detected!");
                }
            }
        }
    }
}
