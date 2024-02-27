using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAimer : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private float transitionWeaponDuration = 0.3f;

    [SerializeField] private Camera mainCamera;
    
    [Header("Rig Components")]
    [SerializeField] private Rig aimLayer;
    [SerializeField] private TwoBoneIKConstraint LeftHandK;
    [SerializeField] private TwoBoneIKConstraint RightHandK;

    [Header("Components")]
    [SerializeField] private Weapon weapon;
    private PlayerInputHandler inputHandler;
    

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    private void OnEnable()
    {
        inputHandler.onAim.AddListener(OnAim);
        inputHandler.onReleaseAim.AddListener(OnReleaseAim);
        inputHandler.onShoot.AddListener(OnShoot);
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        float cameraRotation = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, cameraRotation, 0), turnSpeed * Time.fixedDeltaTime);
    }

    private void OnAim()
    {
        aimLayer.weight += Time.deltaTime / transitionWeaponDuration;
        DOTween.To(() => LeftHandK.weight, x => LeftHandK.weight = x, 1, 1f);
        DOTween.To(() => RightHandK.weight, x => RightHandK.weight = x, 1, 1f);
    }

    private void OnReleaseAim()
    {
        aimLayer.weight -= Time.deltaTime / transitionWeaponDuration;
        DOTween.To(() => LeftHandK.weight, x => LeftHandK.weight = x, 0, 1f);
        DOTween.To(() => RightHandK.weight, x => RightHandK.weight = x, 0, 1f);
    }
    
    private void OnShoot()
    {

    }

    private void OnDisable()
    {
        inputHandler.onAim.RemoveListener(OnAim);
        inputHandler.onReleaseAim.RemoveListener(OnReleaseAim);
        inputHandler.onShoot.RemoveListener(OnShoot);
    }

}
