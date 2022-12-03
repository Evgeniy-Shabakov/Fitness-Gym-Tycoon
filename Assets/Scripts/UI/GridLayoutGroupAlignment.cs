using UnityEngine;
using UnityEngine.UI;

public class GridLayoutGroupAlignment : MonoBehaviour
{
    private GridLayoutGroup glg;
    public RectTransform rtCanvas;
    
    void Start()
    {
        glg = GetComponent<GridLayoutGroup>();
        float y = (rtCanvas.rect.height * 0.3f  - glg.spacing.y * 4) / 3f;
        glg.cellSize = new Vector2(y, y);
    }
}
