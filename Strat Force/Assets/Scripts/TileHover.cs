using UnityEngine;

public class TileHover : MonoBehaviour
{
    private GridStat currentHoveredTile = null;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GridStat tile = hit.collider.GetComponent<GridStat>();

            if (tile != null)
            {
                if (currentHoveredTile != tile)
                {
                    if (currentHoveredTile != null)
                        currentHoveredTile.Unhighlight();

                    tile.Highlight();
                    currentHoveredTile = tile;
                }
            }
            else
            {
                ClearCurrentTile();
            }
        }
        else
        {
            ClearCurrentTile();
        }
    }

    void ClearCurrentTile()
    {
        if (currentHoveredTile != null)
        {
            currentHoveredTile.Unhighlight();
            currentHoveredTile = null;
        }
    }
}

