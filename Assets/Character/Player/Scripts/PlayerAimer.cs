using UnityEngine;

public class PlayerAimer : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 15f;

    [SerializeField] private Camera mainCamera;

    [Header("Shoot Settings")] 
    [SerializeField] private ParticleSystem shootParticleSystem;
    [SerializeField] private GameObject shootImpact;
    private Transform raycastOrigin;
    private Transform raycastDestination;
    
    [Header("Components")]
    [SerializeField] private Weapon weapon;
    private PlayerInputHandler inputHandler;
    private RiggingAnimationer riggingAnimationer;
    

    private void Awake()
    {
        inputHandler = GetComponentInChildren<PlayerInputHandler>();
        riggingAnimationer = GetComponentInChildren<RiggingAnimationer>();
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
        riggingAnimationer.PerformAim();
    }

    private void OnReleaseAim()
    {
       riggingAnimationer.PerformReleaseAim();
    }

    private Ray ray;
    RaycastHit hitInfo; 
    
    private void OnShoot()
    {
        shootParticleSystem.Emit(1);
        
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo,  weapon.range))
        {
            Debug.Log("Estoy Instanciando Impactos");
            GameObject ImpactInstanciated = Instantiate(shootImpact, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(ImpactInstanciated, 2f);
        }
    }

    private void OnDisable()
    {
        inputHandler.onAim.RemoveListener(OnAim);
        inputHandler.onReleaseAim.RemoveListener(OnReleaseAim);
        inputHandler.onShoot.RemoveListener(OnShoot);
    }

}
