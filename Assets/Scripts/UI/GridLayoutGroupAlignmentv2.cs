using UnityEngine;
using UnityEngine.UI;

public class GridLayoutGroupAlignmentv2 : MonoBehaviour
{
    public RectTransform rtCanvas;

    public float percentScreenWidth;
    public float percentScreenHeight;
    public int countColumn;
    public int countRow;
    
    private GridLayoutGroup glg;
    
    void Start()
    {
        glg = GetComponent<GridLayoutGroup>();
        
        float x = (rtCanvas.rect.width * percentScreenWidth/100  - glg.spacing.x * (countColumn + 1)) / countColumn;
        float y = (rtCanvas.rect.height * percentScreenHeight/100  - glg.spacing.y * (countRow + 1)) / countRow;
        
        if (x < y) glg.cellSize = new Vector2(x, x);
        else glg.cellSize = new Vector2(y, y);
    }
}
