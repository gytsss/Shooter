using UnityEngine;

//TODO: Documentation - Add summary
public class CameraEffects : MonoBehaviour
{
    [SerializeField]
    AnimationCurve camCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1.0f),
                                                                    new Keyframe(1.0f, 0), new Keyframe(1.5f, -1.0f),
                                                                  new Keyframe(2.0f, 0));

    [SerializeField] private float breakX = 2;
    [SerializeField] private float breakY = 1;
    [SerializeField] private float multiplierX = 0.02f;
    [SerializeField] private float multiplierY = 0.04f;

    private Camera cam;
    private PlayerMovement playerMovement;
    private Vector3 cameraInitPos;
    private float curveLastFrame;

    private float pitchingX;
    private float pitchingY;

    void Start()
    {
        cam = Camera.main;
        playerMovement = GetComponent<PlayerMovement>();
        cameraInitPos = cam.transform.localPosition;
        curveLastFrame = camCurve[camCurve.length - 1].time;
    }

    void LateUpdate()
    {

        pitchingX += Time.deltaTime / breakX;
        pitchingY += Time.deltaTime / breakY;

        if (pitchingX > curveLastFrame)
            pitchingX -= curveLastFrame;

        if (pitchingY > curveLastFrame)
            pitchingY -= curveLastFrame;

        float posX = camCurve.Evaluate(pitchingX) * multiplierX;
        float posY = camCurve.Evaluate(pitchingY) * multiplierY;

        Vector3 pitchingFrameMovement = new Vector3(posX, posY, 0);

        if (playerMovement.isMoving() && playerMovement.isJumping())
        {
            cam.transform.localPosition = cameraInitPos + pitchingFrameMovement;
        }
        else
        {
            cam.transform.localPosition = cameraInitPos;
        }
    }
}


