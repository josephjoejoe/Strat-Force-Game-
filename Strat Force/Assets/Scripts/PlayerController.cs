using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GridBehavior gridBehavior;
    public float moveSpeed = 2f;
    private List<GameObject> currentPath;
    private bool isMoving = false;

    public GameObject bullet;
    public Transform bulletPos;


    public float raylength = 5;
    public bool isEnemy;

    private const string interactableTag = "Enemy";


    [SerializeField] private float playerHeightOffset = 1f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            HandleMouseClick();
        }

        // If pathfinding is triggered and finished, start movement
        if (!isMoving && gridBehavior.path.Count > 0)
        {
            StartMovingToPath();
        }

        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        if(Physics.Raycast(transform.position, forward, out hit, raylength))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                Shoot();
                isEnemy = true;
            }
            else
            {
                isEnemy = false;
            }
        }


    }
       
    

    void HandleMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject clickedTile = hit.collider.gameObject;
            GridStat tileStat = clickedTile.GetComponent<GridStat>();

            if (tileStat != null)
            {
                // Set start and end positions for pathfinding
                gridBehavior.startX = GetCurrentTileX();
                gridBehavior.startY = GetCurrentTileY();

                gridBehavior.endX = tileStat.x;
                gridBehavior.endY = tileStat.y;

                gridBehavior.findDistance = true;  // Triggers Update() to calculate path
            }
        }
    }

    void StartMovingToPath()
    {
        if (gridBehavior.path.Count == 0)
        {
            Debug.LogWarning("No path found.");
            return;
        }

        currentPath = new List<GameObject>(gridBehavior.path);
        currentPath.Reverse(); // Start from current position to destination
        StartCoroutine(MoveAlongPath());
    }

    IEnumerator MoveAlongPath()
    {
        isMoving = true;

        foreach (GameObject tile in currentPath)
        {
            Vector3 tilePos = tile.transform.position;
            Vector3 targetPos = new Vector3(tilePos.x, tilePos.y + playerHeightOffset, tilePos.z);

            // Calculate direction to rotate
            Vector3 direction = (targetPos - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1f); // Instant rotate (or change 1f to 0.1f for smooth)
            }

            // Move toward the tile
            while (Vector3.Distance(transform.position, targetPos) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = targetPos;
            yield return new WaitForSeconds(0.05f);
        }

        gridBehavior.path.Clear(); // Reset path
        isMoving = false;
    }

    

    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    int GetCurrentTileX()
    {
        return Mathf.RoundToInt((transform.position.x - gridBehavior.leftBottomLocation.x) / gridBehavior.scale);
    }

    int GetCurrentTileY()
    {
        return Mathf.RoundToInt((transform.position.z - gridBehavior.leftBottomLocation.z) / gridBehavior.scale);
    }
}
