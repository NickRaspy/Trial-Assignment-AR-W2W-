using UnityEngine;

namespace TA_W2W
{
    public class LookAtPlayer : MonoBehaviour
    {
        public Transform player;

        void Update()
        {
            if (player != null)
            {
                Vector3 direction = player.position - transform.position;

                direction.y = 0;

                Quaternion targetRotation = Quaternion.LookRotation(-direction);

                transform.rotation = targetRotation;
            }
        }
    }
}
