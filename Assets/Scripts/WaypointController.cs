using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    private float health = 1.0f;
    private float eggDamage = 0.25f;
    private float alphaValue = 1.0f;
    private float numOfEggCollisions = 0;

    private float moveRange = 15.0f;

    private SpriteRenderer spriteRenderer = null;

    private Bounds worldBound;

    private bool hideWaypoint = false;

    private bool isShaking = false;
    private float shakeDuration = 0f;
    private Vector2 shakeMagnitude = Vector2.zero;
    private float elapsedShakeTime = 0f;
    private Vector3 originalShakePosition;
    private bool startingShake = true;

    public MinimapController minimapController;

    public bool minimapActive = false;

    public Camera minimapCamera;

    // Start is called before the first frame update
    void Start()
    {
        // Get SpriteRenderer in order to manipulate color
        spriteRenderer = GetComponent<SpriteRenderer>();

        worldBound = Camera.main.GetComponent<CameraSupport>().GetWorldBounds();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            hideWaypoint = !hideWaypoint;
        }

        if (hideWaypoint)
        {
            // Hide Waypoint
            Color currentColor = spriteRenderer.color;
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.0f);
        } else
        {
            // Change alpha to reflect remaining health.
            Color currentColor = spriteRenderer.color;
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, alphaValue);
        }

        
        
        if (isShaking)
        {
            
            if (elapsedShakeTime < shakeDuration)
            {
                if (!minimapActive) {
                    
                    minimapController.ToggleMinimap(true);
                    minimapActive = !minimapActive;
                }

                Vector3 newCAmPosition = new Vector3(transform.position.x, transform.position.y, minimapCamera.transform.position.z);
                minimapCamera.transform.position = newCAmPosition;
                
                
                if (startingShake)
                {
                    originalShakePosition = transform.position;

                    // Calculate shake offset
                    float offsetX = Random.Range(-shakeMagnitude.x, shakeMagnitude.x);
                    float offsetY = Random.Range(-shakeMagnitude.y, shakeMagnitude.y);

                    // Apply shake offset
                    transform.position += new Vector3(offsetX, offsetY, 0f);

                    
                    startingShake = false;
                } else
                {
                    transform.position = originalShakePosition;
                    startingShake = true;
                }

                // Update elapsed shake time
                elapsedShakeTime += Time.deltaTime;

            }
            else
            {
                Vector3 newCAmPosition = new Vector3(300, 300, minimapCamera.transform.position.z);
                minimapCamera.transform.position = newCAmPosition;
                minimapActive = !minimapActive;
                isShaking = false; // End shaking
                
                toggleMinimapOff();
            }
        }

    }

    IEnumerator toggleMinimapOff()
    {
        yield return new WaitForSeconds(0.5f);

        minimapController.ToggleMinimap(false);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        // Else if an egg hits the plane then decrement health and alpha.
        if (other.name == "Egg(Clone)" && !hideWaypoint)
        {
            health = health - eggDamage;
            numOfEggCollisions++;
            alphaValue = alphaValue - eggDamage;

            // If four or more eggs hit the plane then destroy and move.
            if (numOfEggCollisions >= 4)
            {
                float minX = worldBound.min.x * 0.9f;
                float maxX = worldBound.max.x * 0.9f;
                float minY = worldBound.min.y * 0.9f;
                float maxY = worldBound.max.y * 0.9f;
                Vector3 initialPosition = transform.position;

                float randomX = Random.Range(Mathf.Max(minX, initialPosition.x - moveRange), Mathf.Min(maxX, initialPosition.x + moveRange));
                float randomY = Random.Range(Mathf.Max(minY, initialPosition.y - moveRange), Mathf.Min(maxY, initialPosition.y + moveRange));

                Vector3 targetPosition = new Vector3(randomX, randomY, 0f);

                // Move the object to the target position
                transform.position = targetPosition;

                health = 1.0f;
                alphaValue = 1.0f;
                numOfEggCollisions = 0;

                // Reset shaking variables
                isShaking = false;
                shakeDuration = 0f;
                shakeMagnitude = Vector2.zero;
                elapsedShakeTime = 0f;
                originalShakePosition = transform.position;

                Vector3 newCAmPosition = new Vector3(300, 300, minimapCamera.transform.position.z);
                minimapCamera.transform.position = newCAmPosition;
                minimapActive = !minimapActive;
                toggleMinimapOff();

            } else
            {
                // Apply shaking effects based on collision count
                switch (numOfEggCollisions)
                {
                    case 1:
                        StartShake(1f, new Vector2(1f, 1f));
                        break;
                    case 2:
                        StartShake(2f, new Vector2(2f, 2f));
                        break;
                    case 3:
                        StartShake(3f, new Vector2(3f, 3f));
                        break;
                }
            }

        } 
    }

    private void StartShake(float duration, Vector2 magnitude)
    {
        isShaking = true;
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        elapsedShakeTime = 0f;
    }
}
