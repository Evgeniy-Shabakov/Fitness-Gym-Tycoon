using UnityEngine;

public class TrashOnMouseClick : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        Destroy(gameObject);
    }
}
