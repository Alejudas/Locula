using UnityEngine;
using DG.Tweening;
public class ShootArrow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform destiny;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float travelTime = 0.5f;
    [SerializeField] private float cooldown = 15f;

    private bool isShooting;

    void Start()
    {
        arrow.transform.position = spawnPoint.position;
        StartCoroutine(ShootLoop());
    }

    System.Collections.IEnumerator ShootLoop()
    {
        while (true)
        {
            // activar y disparar
            arrow.SetActive(true);
            isShooting = true;

            yield return arrow.transform
                .DOMove(destiny.position, travelTime)
                .WaitForCompletion();

            // llegó se apaga
            arrow.SetActive(false);

            // vuelve al spawn apagada
            arrow.transform.position = spawnPoint.position;

            isShooting = false;

            // cooldown
            yield return new WaitForSeconds(cooldown);
        }
    }
}