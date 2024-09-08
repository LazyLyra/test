using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{

    [SerializeField] Transform PlayerTransform;
    [SerializeField] bool _IsFacingRight;
    private PlayerController player;
    [SerializeField] float flipRotationTime = 0.5f;

    private Coroutine turn;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        _IsFacingRight = player.IsFacingRight;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }

    public void CallTurn()
    {
        turn = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < flipRotationTime)
        {
            elapsedTime += Time.deltaTime;
            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elapsedTime / flipRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }

    private float DetermineEndRotation()
    {
        _IsFacingRight = !_IsFacingRight;

        if (_IsFacingRight )
        {
            return 180f;
        }
        else
        {
            return 0f;
        }
    }
}


