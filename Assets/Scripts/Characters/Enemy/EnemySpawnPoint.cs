using System;
using UnityEditor;
using UnityEngine;

namespace Characters.Enemy
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        public EnemyType EnemyType;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 1f);
            Gizmos.color = Color.white;
        }
    }
}