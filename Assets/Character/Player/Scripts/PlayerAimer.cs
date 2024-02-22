using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAimer : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private float transitionWeaponDuration = 0.3f;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Rig aimLayer;

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

    private void FixedUpdate()
    {
        float cameraRotation = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, cameraRotation, 0), turnSpeed * Time.fixedDeltaTime);
    }

    private void OnAim()
    {
        aimLayer.weight += Time.deltaTime / transitionWeaponDuration;
    }

    private void OnReleaseAim()
    {
        aimLayer.weight -= Time.deltaTime / transitionWeaponDuration;
    }
    
    private void OnShoot()
    {
        throw new System.NotImplementedException();
    }

    private void OnDisable()
    {
        inputHandler.onAim.RemoveListener(OnAim);
        inputHandler.onReleaseAim.RemoveListener(OnReleaseAim);
        inputHandler.onShoot.RemoveListener(OnShoot);
    }

}
