using UnityEngine;
using DG.Tweening;
public class ShootArrow : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform destiny;
    [SerializeField] private Vector3 spawnPoint;

    private void Start()
    {

        spawnPoint = arrowPrefab.transform.position;
    }
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.F))
        //{
        Shoot();
        //}
    }
    public void Shoot()
    {

        arrowPrefab.transform.DOMove(destiny.position, 0.5f);

        if (Vector3.Distance(arrowPrefab.transform.position, destiny.position) < 0.1f)
        {
            arrowPrefab.SetActive(false);
        }

    }
}
