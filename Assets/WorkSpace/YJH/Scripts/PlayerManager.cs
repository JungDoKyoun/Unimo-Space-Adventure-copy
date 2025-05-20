using UnityEngine;

using ZL.Unity.Unimo;

public partial class PlayerManager 
{
    private void OnEnable()
    {
        MonsterManager.Instance.Target = transform;
    }

    private void OnDisable()
    {
        if (MonsterManager.Instance != null)
        {
            MonsterManager.Instance.Target = null;
        }
    }

    private void Start()
    {
        ActionStart();

        MoveStart();
        StatusStart();
    }

    private void Update()
    {
        ActionUpdate();

        MoveUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Energy"))
        {
            GetEnergy(1);

            Destroy(other.gameObject);
        }
    }
}