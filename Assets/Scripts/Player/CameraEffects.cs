using UnityEngine;

/// <summary>
/// Responsible for manipulating the camera's position based on an animation curve and the player's movement
/// </summary>
public class CameraEffects : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] AnimationCurve camCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1.0f),
        new Keyframe(1.0f, 0), new Keyframe(1.5f, -1.0f),
        new Keyframe(2.0f, 0));

    [SerializeField] private float breakX = 2;
    [SerializeField] private float breakY = 1;
    [SerializeField] private float multiplierX = 0.02f;
    [SerializeField] private float multiplierY = 0.04f;

    #endregion

    #region PRIVATE_FIELDS

    private Camera cam;
    private PlayerMovement playerMovement;
    private Vector3 cameraInitPos;
    private float curveLastFrame;

    private float pitchingX;
    private float pitchingY;

    #endregion

    #region UNITY_CALLS

    /// <summary>
    /// Initializes the references and variables used by the CameraEffects component.
    /// </summary>
    private void Start()
    {
        cam = Camera.main;
        playerMovement = GetComponent<PlayerMovement>();
        cameraInitPos = cam.transform.localPosition;
        curveLastFrame = camCurve[camCurve.length - 1].time;
    }

    /// <summary>
    /// Updates the camera position based on the animation curve and player movement.
    /// </summary>
    private void LateUpdate()
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

    #endregion
}