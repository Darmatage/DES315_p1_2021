using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace RyanHeath
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovementPattern : MonoBehaviour
    {
        private enum MovementOption
        {
            Idle,
            Left,
            Up,
            Down,
            Right,
            Attack
        }

        [SerializeField] private UnityEvent onAttack;
        [SerializeField] private List<MovementOption> movementPattern;
        [Tooltip("Movement speed of the object this is attached to")]
        [SerializeField] private float speed = 1f;
        [Tooltip("Amount of time spent on each segment of the movement pattern")]
        [SerializeField] private float segmentTime = .5f;
        private Rigidbody2D rb;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Assert.IsNotNull(onAttack);
            StartCoroutine(nameof(MoveInPattern));
        }

        private IEnumerator MoveInPattern()
        {
            foreach (MovementOption movementOption in movementPattern)
            {
                switch (movementOption)
                {
                    case MovementOption.Idle:
                        rb.velocity = Vector2.zero;
                        break;
                    case MovementOption.Left:
                        rb.velocity = Vector2.left * speed;
                        break;
                    case MovementOption.Up:
                        rb.velocity = Vector2.up * speed;
                        break;
                    case MovementOption.Down:
                        rb.velocity = Vector2.down * speed;
                        break;
                    case MovementOption.Right:
                        rb.velocity = Vector2.right * speed;
                        break;
                    case MovementOption.Attack:
                        rb.velocity = Vector2.zero;
                        onAttack.Invoke();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                yield return new WaitForSeconds(segmentTime);
            }
            StartCoroutine(nameof(MoveInPattern));
        }
    }
}
