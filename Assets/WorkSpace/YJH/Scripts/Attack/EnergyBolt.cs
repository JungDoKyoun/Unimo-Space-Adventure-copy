using UnityEngine;

using ZL.Unity;

using ZL.Unity.Unimo;
using Photon.Pun;
public class EnergyBolt : MonoBehaviour, IAttackType
{
    [SerializeField]
    
    private float speed = 5f;

    [SerializeField]

    private float maxRange = 5f;

    [SerializeField]

    private int energyCost = 1;

    [SerializeField]

    private float damage = 1f;

    [SerializeField]

    private bool canPierce = false;

    [SerializeField]

    private Transform firePos;

    [SerializeField]

    private GameObject hitEffect;

    [SerializeField]

    //사용 안함
    private AudioClip hitSound;

    [SerializeField]

    //사용 안함
    private AudioSource hitSoundSource;

    [SerializeField]

    private Rigidbody selfBody;

    [SerializeField]

    private Collider selfCollider;

    [SerializeField]

    private LayerMask damageableLayerMask = 0;

    public float Speed { get => speed; set => speed = value; }

    public float MaxRange { get => maxRange; set => maxRange = value; }

    public int EnergyCost { get => energyCost; set => energyCost = value; }

    public float Damage { get => damage; set => damage = value; }

    public bool CanPierce { get => canPierce; set => canPierce = value; }

    public Transform FirePos { get => firePos; set => firePos = value; }

    public GameObject HitEffect { get => hitEffect; set => hitEffect = value; }

    public AudioClip HitSound { get => hitSound; set => hitSound = value; }

    
    public void Shoot(Vector3 fireDirection)
    {
        Vector3 tempVelocity = new Vector3(fireDirection.x, 0, fireDirection.z);

        //fireDirection.normalized * speed;

        firePos = transform;

        selfBody.velocity = tempVelocity.normalized * speed;

        Destroy(gameObject, maxRange / speed);
    }

    //private void OnDestroy()
    //{
    //    var temp = Instantiate(hitEffect, transform.position, Quaternion.identity);
    //
    //    Destroy(temp, 2f);
    //
    //    //PlayHit();
    //}

    //public void OnHit()
    //{
    //    Destroy(gameObject);
    //
    //    //hit 이펙트 활성화
    //}

    //public void PlayHit()
    //{
    //    if (hitSoundSource != null)
    //    {
    //        hitSoundSource.clip = hitSound;
    //
    //        hitSoundSource.Play();
    //
    //        hitEffect.SetActive(true);
    //    }
    //
    //    //Debug.Log("playhit!");
    //}

    private void OnTriggerEnter(Collider other)
    {
        //selfCollider.enabled = false;

        //Debug.Log("trigger");

        //OnHit();

        if (damageableLayerMask.Contains(other.gameObject.layer) == false)
        {
            return;
        }

        if (other.gameObject.TryGetComponent<IDamageable>(out var damageable) == true)
        {
            damageable.TakeDamage(damage, other.ClosestPoint(transform.position));

            if (PhotonNetwork.IsConnected == false)
            {
                var hitEffectTemp = Instantiate(this.hitEffect, transform.position, Quaternion.identity);
                Destroy(hitEffectTemp, 2f);
            }
            else
            {
                var hitEffectTemp = PhotonNetwork.Instantiate(this.hitEffect.name, transform.position, Quaternion.identity);
                PhotonNetwork.Destroy(hitEffectTemp);
            }



                Destroy(gameObject);
        }
    }
}