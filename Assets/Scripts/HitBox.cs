using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] HitBoxType hitBoxType;

    MoveController moveController;

    #region Unity Cycle
    private void Awake() { moveController = GetComponentInParent<MoveController>(); }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        moveController.TriggerEnter(hitBoxType, collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        moveController.TriggerExit(hitBoxType, collision);
    }
    #endregion
}
