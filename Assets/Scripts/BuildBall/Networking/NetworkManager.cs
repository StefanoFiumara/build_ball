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
        private readonly Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new();

        private NetworkRunner _runner;

        [SerializeField]
        private NetworkObject CharacterPrefab;

        private void Awake()
        {
            _runner = GetComponent<NetworkRunner>();
            _runner.ProvideInput = true;
        }

        private void Start()
        {
            #if UNITY_STANDALONE
            JoinOrHostGame();
            #endif

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
            var spawnPosition = Vector3.right * 3 * _spawnedCharacters.Values.Count;
            Debug.Log($"Player Id: {player.PlayerId}, Raw Encoded: {player.RawEncoded}");
            void OnBeforeSpawned(NetworkRunner r, NetworkObject o)
            {
                o.GetComponent<PlayerStats>().Init(TeamAffiliationEnum.TeamA);
            }

            var networkPlayerObject = runner.Spawn(CharacterPrefab, spawnPosition, Quaternion.identity, player, OnBeforeSpawned);

            // Keep track of the player avatars so we can remove it when they disconnect
            _spawnedCharacters.Add(player, networkPlayerObject);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            // Find and remove the players avatar
            if (_spawnedCharacters.TryGetValue(player, out var networkObject))
            {
                runner.Despawn(networkObject);
                _spawnedCharacters.Remove(player);
            }
        }

        private bool _shootPressed;
        private bool _standardAbilityPressed;
        private bool _ultimateAbilityPressed;

        public void Update()
        {
            // Sample for one-time key pressed here, to make sure quick inputs are not missed.
            _shootPressed |= Input.GetMouseButton(0);
            _standardAbilityPressed |= Input.GetKey(KeyCode.LeftShift);
            _ultimateAbilityPressed |= Input.GetKey(KeyCode.R);
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            var data = new NetworkInputData();

            data.MoveDirection = GetMovementDirection();
            if (_shootPressed)
            {
                data.IsShootPressed = true;
            }

            if (_standardAbilityPressed)
            {
                data.IsStandardAbilityPressed = true;

            }

            if (_ultimateAbilityPressed)
            {
                data.IsUltimateAbilityPressed = true;

            }

            _shootPressed = false;
            _standardAbilityPressed = false;
            _ultimateAbilityPressed = false;

            input.Set(data);
        }


        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
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

        private Vector2 GetMovementDirection()
        {
            var direction = Vector2.zero;

            if (Input.GetKey(KeyCode.W)) direction += Vector2.up;
            if (Input.GetKey(KeyCode.S)) direction += Vector2.down;
            if (Input.GetKey(KeyCode.A)) direction += Vector2.left;
            if (Input.GetKey(KeyCode.D)) direction += Vector2.right;

            return direction.normalized;
        }
    }
}
