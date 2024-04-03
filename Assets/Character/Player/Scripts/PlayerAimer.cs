using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAimer : MonoBehaviour
{
    [Header("Input Actions")] 
    [SerializeField] private InputActionReference weapon1;
    [SerializeField] private InputActionReference weapon2;
    [SerializeField] private InputActionReference weapon3;
    [SerializeField] private InputActionReference weapon4;
    [SerializeField] private InputActionReference weaponScroll;
    
    
    [Header("Aim Settings")]
    [SerializeField] private float turnSpeed = 15f;

    private Camera mainCamera;
    
    [Header("Shoot Settings")] 
    [SerializeField] private GameObject shootImpact;
    private Transform raycastOrigin;
    private Transform raycastDestination;

    [Header("Weapons Settings")] 
    [SerializeField] private Weapon[] weaponsOwned;
    [HideInInspector] public Weapon activeWeapon;
    private int desiredWeaponIndex = -1;

    [Header("Sensitivity Settings")] 
    [SerializeField] private float normalSensitivity;

    [SerializeField] private float aimSensitivity;
    
    [Header("Events")]
    [HideInInspector] public UnityEvent onWeaponChange;
    [HideInInspector] public UnityEvent onNoWeapon;
    
    [Header("Components")]
    private PlayerInputHandler inputHandler;
    private RiggingAnimationer riggingAnimationer;
    [SerializeField] private CameraHandler cameraHandler;
    

    private void Awake()
    {
        mainCamera = Camera.main;
        inputHandler = GetComponentInChildren<PlayerInputHandler>();
        riggingAnimationer = GetComponentInChildren<RiggingAnimationer>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        weapon1.action.Enable();
        weapon2.action.Enable();
        weapon3.action.Enable();
        weapon4.action.Enable();
        weaponScroll.action.Enable();
        
        inputHandler.onAim.AddListener(OnAim);
        inputHandler.onReleaseAim.AddListener(OnReleaseAim);
        inputHandler.onShoot.AddListener(OnShoot);
    }

    private void Start()
    {
        mainCamera = Camera.main;
        cameraHandler.SetSensitivity(normalSensitivity);
    }

    private void LateUpdate()
    {
        float cameraRotation = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, cameraRotation, 0), turnSpeed * Time.fixedDeltaTime);

        if (CanChangeWeapon())
        {
            ActiveWeapon();
        }
    }

    // Esto lo ha de tener el Weapon Manager
    
    private bool CanChangeWeapon()
    {
        return weapon1.action.WasPerformedThisFrame() || weapon2.action.WasPerformedThisFrame() || weapon3.action.WasPerformedThisFrame() || weapon4.action.WasPerformedThisFrame();
    }

    private void ActiveWeapon()
    {
        if (weapon1.action.WasPerformedThisFrame())
        {
            desiredWeaponIndex = 0;
        }
        if (weapon2.action.WasPerformedThisFrame())
        {
            desiredWeaponIndex = 1;
        }
        if (weapon3.action.WasPerformedThisFrame())
        {
            desiredWeaponIndex = 2;
        }
        if (weapon4.action.WasPerformedThisFrame())
        {
            desiredWeaponIndex = 3;
        }
        
        DesactiveAllWeapons();
        ChangeWeapon(desiredWeaponIndex);
        ActiveCurrentWeapon();
    }
    
    private void DesactiveAllWeapons()
    {
        foreach (Weapon weapon in weaponsOwned)
        {
            weapon.gameObject.SetActive(false);
        }
        onNoWeapon.Invoke();
    }
    
    private void ChangeWeapon(int index)
    {
        if(activeWeapon == weaponsOwned[index])
        {
            activeWeapon = null;
            desiredWeaponIndex = -1;
            return;    
        }
        
        activeWeapon = weaponsOwned[index];
    }
    
    private void ActiveCurrentWeapon()
    {
        if (desiredWeaponIndex == -1)
        {
            return;
        }

        activeWeapon.gameObject.SetActive(true);
        onWeaponChange.Invoke();
    }

    
    // Esto si que es del aimer
    
    private void OnAim()
    {
        //riggingAnimationer.PerformAim();
        cameraHandler.SetSensitivity(aimSensitivity);
    }

    private void OnReleaseAim()
    {
       //riggingAnimationer.PerformReleaseAim();
       cameraHandler.SetSensitivity(normalSensitivity); 
    }

    private Ray ray;
    RaycastHit hitInfo; 
    
    
    //Esto tambien ha de estar en el WeaponManager
    private void OnShoot()
    {
        if(activeWeapon != null && activeWeapon.muzzleFlash)
        {
            activeWeapon.muzzleFlash.Emit(1);
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, activeWeapon.range))
            {
                if (hitInfo.transform.gameObject.tag.Equals("Rival"))
                {
                    hitInfo.transform.GetComponentInChildren<EnemyHealther>().TakeDamage();
                }
                
                GameObject ImpactInstanciated = Instantiate(shootImpact, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(ImpactInstanciated, 2f);
            }
        }
    }

    private void OnDisable()
    {
        weapon1.action.Disable();
        weapon2.action.Disable();
        weapon3.action.Disable();
        weapon4.action.Disable();
        weaponScroll.action.Disable();
        
        inputHandler.onAim.RemoveListener(OnAim);
        inputHandler.onReleaseAim.RemoveListener(OnReleaseAim);
        inputHandler.onShoot.RemoveListener(OnShoot);
    }

}
