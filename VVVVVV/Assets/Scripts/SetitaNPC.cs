using System.Collections;
using UnityEngine;

public class SetitaNPC : MonoBehaviour
{
    public float Speed, MinPos, MaxPos;
    private Vector3 direction;
    private bool stopped = false;

    void Start()
    {
        direction = Vector3.right;
    }

    void Update()
    {
        if (!stopped)
        {
            float newXpos = transform.position.x + direction.x * Speed * Time.deltaTime;
            if (newXpos <= MinPos || newXpos >= MaxPos)
            {
                direction = -direction;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                StartCoroutine(LittleStop());
            }
            transform.position += direction * Speed * Time.deltaTime;
        }
    }

    private IEnumerator LittleStop()
    {
        stopped = true;
        yield return new WaitForSeconds(3);
        stopped = false;
    }
}
