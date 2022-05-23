using System;
using System.Collections.Generic;
using BuildBall.Models;
using BuildBall.Player;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BuildBall.Networking
{
    public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField]
        private NetworkObject CharacterPrefab;

        [SerializeField]
        private NetworkObject BallPrefab;

        private NetworkRunner _runner;

        private readonly Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new();
        private readonly Dictionary<PlayerRef, NetworkObject> _spawnedBalls = new();

        private bool _shootPressed;
        private bool _standardAbilityPressed;
        private bool _ultimateAbilityPressed;

        private void Awake()
        {
            _runner = GetComponent<NetworkRunner>();
            _runner.ProvideInput = true;
        }

        private void Start()
        {
            // Automatically join the default room when running outside of the Unity Editor
            // Use the Network Debug Component on the NetworkManager object to test in the editor.
            if (Application.platform != RuntimePlatform.WindowsEditor)
            {
                JoinOrHostGame();
            }
        }

        public void Update()
        {
            // Sample for one-time key pressed here, to make sure quick inputs are not missed.
            _shootPressed |= Input.GetMouseButton(0);
            _standardAbilityPressed |= Input.GetKey(KeyCode.LeftShift);
            _ultimateAbilityPressed |= Input.GetKey(KeyCode.R);
        }

        private async void JoinOrHostGame()
        {
            var startGameArgs = new StartGameArgs
            {
                GameMode = GameMode.AutoHostOrClient,
                SessionName = "BuildBall_TestRoom",
                Scene = SceneManager.GetActiveScene().buildIndex,
                SceneObjectProvider = gameObject.GetComponent<NetworkSceneManagerDefault>()
            };

            await _runner.StartGame(startGameArgs);
        }


        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            // TODO: Spawn positions based on team affiliation
            var spawnPosition = Vector3.left + Vector3.right * 6 * _spawnedCharacters.Values.Count;

            void OnBeforePlayerSpawned(NetworkRunner r, NetworkObject o)
            {
                var team = _spawnedCharacters.Count % 2 == 0 ? TeamAffiliationEnum.TeamA : TeamAffiliationEnum.TeamB;
                o.GetComponent<PlayerStats>().Init(team);
            }

            void OnBeforeBallSpawned(NetworkRunner r, NetworkObject o)
            {
                o.GetComponent<BallMovementBehaviour>().SetStationary();
            }

            var networkPlayerObject = runner.Spawn(CharacterPrefab, spawnPosition, Quaternion.identity, player, OnBeforePlayerSpawned);
            var networkBallObject = runner.Spawn(BallPrefab, spawnPosition + 2*Vector3.right, Quaternion.identity, null, OnBeforeBallSpawned);

            // Keep track of the player avatars so we can remove it when they disconnect
            _spawnedCharacters.Add(player, networkPlayerObject);
            _spawnedBalls.Add(player, networkBallObject);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            // Find and remove the players avatar
            if (_spawnedCharacters.TryGetValue(player, out var playerObject))
            {
                runner.Despawn(playerObject);
                _spawnedCharacters.Remove(player);
            }

            if (_spawnedBalls.TryGetValue(player, out var ballObject))
            {
                runner.Despawn(ballObject);
                _spawnedBalls.Remove(player);
            }
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            var data = new NetworkInputData();

            data.MoveDirection = GetMovementDirection();

            data.IsShootPressed = _shootPressed;
            data.IsStandardAbilityPressed = _standardAbilityPressed;
            data.IsUltimateAbilityPressed = _ultimateAbilityPressed;

            _shootPressed = false;
            _standardAbilityPressed = false;
            _ultimateAbilityPressed = false;

            input.Set(data);
        }

        private Vector2 GetMovementDirection()
        {
            var direction = Vector2.zero;

            if (Input.GetKey(KeyCode.W)) direction += Vector2.up;
            if (Input.GetKey(KeyCode.S)) direction += Vector2.down;
            if (Input.GetKey(KeyCode.A)) direction += Vector2.left;
            if (Input.GetKey(KeyCode.D)) direction += Vector2.right;

            return direction.normalized;
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
        // ReSharper disable once Unity.IncorrectMethodSignature
        public void OnConnectedToServer(NetworkRunner runner) { }
        public void OnDisconnectedFromServer(NetworkRunner runner) { }
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
        public void OnSceneLoadDone(NetworkRunner runner) { }
        public void OnSceneLoadStart(NetworkRunner runner) { }
    }
}
