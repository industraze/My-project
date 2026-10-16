using UnityEngine;

public class Spinner : MonoBehaviour
{

     void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }
}
