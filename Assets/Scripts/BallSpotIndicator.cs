using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpotIndicator : MonoBehaviour
{
    public BoxCollider bowlingRegion;
    public float speed = 20;

    Coroutine positionIndicator;
    IController controller;

    IEnumerator PositionIndicator()
    {
        controller = new GameController();

        while (gameObject.activeSelf)
        {
            float xPos = transform.position.x - controller.HorizontalAxis() * speed * Time.deltaTime;
            float yPos = transform.position.y;
            float zPos = transform.position.z - controller.VerticalAxis() * speed * Time.deltaTime;

            xPos = Mathf.Clamp(xPos, bowlingRegion.bounds.min.x, bowlingRegion.bounds.max.x);
            zPos = Mathf.Clamp(zPos, bowlingRegion.bounds.min.z, bowlingRegion.bounds.max.z);

            transform.position = new Vector3(xPos, yPos, zPos);
            yield return null;
        }
    }

    public void ToggleIndicator(bool isActive)
    {
        gameObject.SetActive(isActive);

        if (isActive)
        {
            positionIndicator = StartCoroutine(PositionIndicator());
            return;
        }

        StopCoroutine(positionIndicator);
        EventManager.Instance.TriggerEvent(new SetPositionEvent(transform.position));
    }
}
