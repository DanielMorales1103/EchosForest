using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int count = 3;
    [SerializeField] float radius = 2f;
    [SerializeField] float dropHeight = 2f;
    [SerializeField] float yOffset = 0.1f;

    public void Spawn()
    {
        if (!enemyPrefab) return;
        for (int i = 0; i < count; i++)
        {
            Vector2 c = count == 1 ? Vector2.zero : Random.insideUnitCircle.normalized * radius;
            Vector3 pos = transform.position + new Vector3(c.x, dropHeight, c.y);
            if (Physics.Raycast(pos, Vector3.down, out var hit, dropHeight + 10f))
                pos = hit.point + Vector3.up * yOffset;
            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }
}
