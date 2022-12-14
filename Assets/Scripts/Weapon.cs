using System.Threading.Tasks;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Gun")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform muzzle;
    [SerializeField] private AmmoClip clip;

    [Header("Config")]
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float reloadTime = 1f;
    public bool canFire { get; private set; }
    public WeaponType weaponType;
    public enum WeaponType { 
        primary, sidearm
    }


    private void Start() => canFire = true;

    public virtual async void Fire() {
        if (clip.ammo == 0) {
            clip.Reload();
            return;
        }
        if (!canFire || !GameManager.Input.Player.Aim.IsPressed()) return;

        var bullet = ObjectPool.Get(ObjectPool.BulletPool);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, muzzle.rotation.eulerAngles.y, muzzle.rotation.eulerAngles.z));
        bullet.SetActive(true);

        clip.ammo -= 1;
        canFire = false;
        await Wait(fireRate);
        canFire = true;
    }

    public virtual async void Reload() {
        await Wait(reloadTime);
    }

    protected virtual async Task Wait(float time) {
        await Task.Delay(Mathf.CeilToInt(1000 * time));
    }

    [System.Serializable]
    public struct AmmoClip {
        public int ammo;
        public int clip;
        public int quantity;

        public void Reload() {
            var r = (clip - ammo) > quantity ? (clip - ammo) : quantity;
            quantity -= r;
            ammo += r;
        }
    }
}
