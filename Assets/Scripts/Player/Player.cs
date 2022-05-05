// using System.Collections;
// using Abilities;
// using Events;
// using UnityEngine;
//
// namespace Player
// {
//     public class Player : MonoBehaviour
//     {
//         [SerializeField] private PlayerController _controller;
//         [SerializeField] private PlayerStats _stats;
//         [SerializeField] private Ability _ultimate;
//         [SerializeField] private Ability _dash;
//         
//         
//         public void Start()
//         {
//             _controller = GetComponentInParent<PlayerController>();
//             _stats = GetComponentInParent<PlayerStats>();
//             _ultimate = GetComponentInParent<Ability>();//first and second help?
//             _dash = GetComponentInParent<Ability>();
//         }
//         
//         
//         public void Update()
//         {
//             Vector2 movementDirection = _controller.GetMovementDirection();
//
//             _controller.ProcessMouseClicks();
//             _controller.ProcessKeyboardClicks();
//
//             Vector3 newPosition = _controller.CalculateNewPosition(movementDirection);
//
//             transform.position += Time.deltaTime * newPosition;
//         }
//
//     }
// }
