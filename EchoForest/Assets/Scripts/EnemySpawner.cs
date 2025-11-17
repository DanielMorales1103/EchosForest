using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int count = 3;
    [SerializeField] float radius = 2f;
    [SerializeField] float dropHeight = 2f;
    [SerializeField] float yOffset = 0.1f;

    [SerializeField] float spawnDelay = 7f;
    [SerializeField] float perEnemyDelay = 2f;

    public void Spawn()
    {
        if (!enemyPrefab) return;
        StartCoroutine(SpawnDelayed());
    }

    private IEnumerator SpawnDelayed()
    {
        yield return new WaitForSeconds(spawnDelay);

        for (int i = 0; i < count; i++)
        {
            Vector2 c = count == 1 ? Vector2.zero : Random.insideUnitCircle.normalized * radius;
            Vector3 pos = transform.position + new Vector3(c.x, dropHeight, c.y);
            if (Physics.Raycast(pos, Vector3.down, out var hit, dropHeight + 10f))
                pos = hit.point + Vector3.up * yOffset;
            Instantiate(enemyPrefab, pos, Quaternion.identity);

            if (i < count - 1)
                yield return new WaitForSeconds(perEnemyDelay);
        }
    }
}
