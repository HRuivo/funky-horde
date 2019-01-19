using UnityEngine;

public class Crawler : Enemy
{
    // only let to overlap the player by just a little
    private const float DESTINATION_THRESHOLD = 0.4f;

    protected override void Update()
    {
        if (!IsAlive)
            return;

        Vector3 currentPos = transform.position;
        Vector3 playerPos = GameManager.Instance.Player.transform.position;

        if ((_characterController.collisionFlags & CollisionFlags.Below) != 0)
        {
            _amountToMove.y = -1f;
        }
        else
        {
            _amountToMove.y -= gravity * Time.deltaTime;
        }

        if (Mathf.Abs(currentPos.x - playerPos.x) >= DESTINATION_THRESHOLD)
        {
            _amountToMove.x = currentPos.x > playerPos.x ? -1f : 1f;

            _characterController.Move(_amountToMove * runSpeed * Time.deltaTime);
        }

        base.Update();
    }
}
