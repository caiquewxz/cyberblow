using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CollectableAmmo : MonoBehaviour
{
    [SerializeField] Projectile bulletPrefab;
    [SerializeField] GameObject player;
    [SerializeField] private Material bulletMaterial;
    [SerializeField] private string acquiredText = "You acquired a new weapon!";

    private Text acquiredTextComponent;
    ShootComponent shootComponent;
    
    void Start()
    {
        if(player == null)
        {
            player = CharacterMovement.instance.gameObject;
        }

        shootComponent = player.GetComponent<ShootComponent>();
        
        acquiredTextComponent = GameObject.FindGameObjectWithTag("AcquiredAmmoText").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            shootComponent.bulletPrefab = bulletPrefab;
            shootComponent.playerRenderer.SetMaterials(new List<Material>(){ bulletMaterial });
            StartCoroutine(ShowCollectWeaponTextAndDestroy());
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    IEnumerator ShowCollectWeaponTextAndDestroy()
    {
        acquiredTextComponent.text = acquiredText;
        yield return new WaitForSeconds(3);
        acquiredTextComponent.text = "";
        Destroy(gameObject);
    }
}
