using UnityEngine;
using UnityEngine.UI;

public class GridLayoutGroupAlignmentv2 : MonoBehaviour
{
    public RectTransform rtCanvas;

    public float percentScreenWidth;
    public float percentScreenHeight;
    public int maxCountColumn;
    public int maxCountRow;
    
    private GridLayoutGroup glg;
    
    void Start()
    {
        glg = GetComponent<GridLayoutGroup>();
        
        float x = (rtCanvas.rect.width * percentScreenWidth  - glg.spacing.x * (maxCountColumn + 1)) / maxCountColumn;
        float y = (rtCanvas.rect.height * percentScreenHeight  - glg.spacing.y * (maxCountRow + 1)) / maxCountRow;
        
        if (x < y) glg.cellSize = new Vector2(x, x);
        else glg.cellSize = new Vector2(y, y);
    }
}
