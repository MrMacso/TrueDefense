using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(60f, 0f,0f);
    }
}
