using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAimer : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private float transitionWeaponDuration = 0.3f;

    [SerializeField] private Camera mainCamera;
    
    [Header("Animation Riggings")]
    [SerializeField] private Rig aimLayer;

    [SerializeField] private TwoBoneIKConstraint rightHandK;
    [SerializeField] private TwoBoneIKConstraint leftHandK;

    [Header("Shoot Settings")] 
    [SerializeField] private ParticleSystem shootParticleSystem;
    [SerializeField] private GameObject shootImpact;
    private Transform raycastOrigin;
    private Transform raycastDestination;
    
    
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
        
        DOTween.To(()=> leftHandK.weight, x=> leftHandK.weight = x, 1f, 0.5f);
    }

    private void OnReleaseAim()
    {
        aimLayer.weight -= Time.deltaTime / transitionWeaponDuration;
        
         DOTween.To(()=> leftHandK.weight, x=> leftHandK.weight = x, 0f, 0.5f);
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
