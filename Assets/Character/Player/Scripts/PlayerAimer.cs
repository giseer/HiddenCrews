using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAimer : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 15f;

    private Camera mainCamera;

    [Header("Shoot Settings")] 
    [SerializeField] private GameObject shootImpact;
    private Transform raycastOrigin;
    private Transform raycastDestination;

    [Header("Weapons")] 
    [SerializeField] private Weapon[] weaponsOwned;
    private Weapon activeWeapon;

    [Header("Events")]
    [HideInInspector] public UnityEvent<Weapon> onWeaponChange;
    [HideInInspector] public UnityEvent onNoWeapon;
    
    [Header("Components")]
    private PlayerInputHandler inputHandler;
    private RiggingAnimationer riggingAnimationer;
    

    private void Awake()
    {
        mainCamera = Camera.main;
        inputHandler = GetComponentInChildren<PlayerInputHandler>();
        riggingAnimationer = GetComponentInChildren<RiggingAnimationer>();
        Cursor.lockState = CursorLockMode.Locked;
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

        ActiveWeapon();
    }

    private void ActiveWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DesactiveAllWeapons();
            
            if(activeWeapon == weaponsOwned[0])
            {
                activeWeapon = null;
                return;    
            }
            
            activeWeapon = weaponsOwned[0];
            activeWeapon.gameObject.SetActive(true);
            onWeaponChange.Invoke(activeWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DesactiveAllWeapons();
            
            if(activeWeapon == weaponsOwned[1])
            {
                activeWeapon = null;
                return;
            }
            
            activeWeapon = weaponsOwned[1];
            activeWeapon.gameObject.SetActive(true);
            onWeaponChange.Invoke(activeWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DesactiveAllWeapons();
            
            if(activeWeapon == weaponsOwned[2])
            {
                activeWeapon = null;
                return;
            }
            
            activeWeapon = weaponsOwned[2];
            activeWeapon.gameObject.SetActive(true);
            onWeaponChange.Invoke(activeWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DesactiveAllWeapons();
            
            if(activeWeapon == weaponsOwned[3])
            {
                activeWeapon = null;
                return;
            }
            
            activeWeapon = weaponsOwned[3];
            activeWeapon.gameObject.SetActive(true);
            onWeaponChange.Invoke(activeWeapon);
        }
    }

    private void DesactiveAllWeapons()
    {
        foreach (Weapon weapon in weaponsOwned)
        {
            weapon.gameObject.SetActive(false);
        }
        onNoWeapon.Invoke();
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
        if(activeWeapon != null && activeWeapon.muzzleFlash)
        {
            activeWeapon.muzzleFlash.Emit(1);
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, activeWeapon.range))
            {
                Debug.Log("Estoy Instanciando Impactos");
                GameObject ImpactInstanciated = Instantiate(shootImpact, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(ImpactInstanciated, 2f);
            }
        }
    }

    private void OnDisable()
    {
        inputHandler.onAim.RemoveListener(OnAim);
        inputHandler.onReleaseAim.RemoveListener(OnReleaseAim);
        inputHandler.onShoot.RemoveListener(OnShoot);
    }

}
