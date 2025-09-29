using UnityEngine;

public class GridStat : MonoBehaviour
{
    public int visited = -1;
    public int x = 0;
    public int y = 0;

    private Renderer rend;
    private Color originalColor;
    public Color hoverColor = Color.cyan;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            originalColor = rend.material.color;
        }
    }

    public void Highlight()
    {
        if (rend != null)
        {
            rend.material.color = hoverColor;
        }
    }

    public void Unhighlight()
    {
        if (rend != null)
        {
            rend.material.color = originalColor;
        }
    }
}

