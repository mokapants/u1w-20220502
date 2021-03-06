using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using InGame.Core;
using InGame.Field.Panel;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace InGame.Ball
{
    public class Ball : MonoBehaviour
    {
        private GameManager gameManager;
        private BallManager ballManager;
        [SerializeField] private Rigidbody ballRigidbody;
        [SerializeField] private float panelCoolTime;
        private bool canHitPanel;

        private CancellationTokenSource canHitPanelCooldownCTS;

        // SE
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip hitWallClip;
        [SerializeField] private AudioClip hit1ptClip;
        [SerializeField] private AudioClip hitU1WClip;
        [SerializeField] private AudioClip hitBumperClip;

        [Inject]
        public void Constructor(
            GameManager gameManager,
            BallManager ballManager
        )
        {
            this.gameManager = gameManager;
            this.ballManager = ballManager;
        }

        private void Awake()
        {
            SetStatus(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Untagged"))
            {
                OnCollisionEnterWall();
            }

            if (collision.gameObject.CompareTag("Goal"))
            {
                OnCollisionEnterGoal();
            }

            if (collision.gameObject.CompareTag("JackPot"))
            {
                OnCollisionEnterJackPot();
            }

            if (collision.gameObject.CompareTag("Bumper"))
            {
                OnCollisionEnterBumper(collision.transform.position);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Panel") && canHitPanel)
            {
                OnTriggerEnterPanel(other.gameObject);
            }
        }

        /// <summary>
        /// ボールの状態を設定
        /// </summary>
        public void SetStatus(bool isActive)
        {
            ballRigidbody.isKinematic = !isActive;
            ballRigidbody.useGravity = isActive;
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.position = isActive ? ballRigidbody.position : new Vector3(-1000, -1000, 0);

            // アクティブになってすぐはパネルに触れられない
            if (isActive)
            {
                // canHitPanelCooldownCTS?.Cancel();
                canHitPanelCooldownCTS = new CancellationTokenSource();
                CanHitPanelCooldown(canHitPanelCooldownCTS.Token);
            }
        }

        /// <summary>
        /// パネルに触れられないようにクールタイムを設ける
        /// </summary>
        private async UniTask CanHitPanelCooldown(CancellationToken token)
        {
            canHitPanel = false;
            await UniTask.Delay(TimeSpan.FromSeconds(panelCoolTime), DelayType.Realtime, PlayerLoopTiming.Update, token);
            canHitPanel = true;
        }

        /// <summary>
        /// ボールの位置を設定
        /// </summary>
        public void SetPosition(Vector3 position)
        {
            ballRigidbody.position = position;
        }

        /// <summary>
        /// ボールに力を加える
        /// </summary>
        public void AddForce(Vector3 direction, float power, bool isOverrideVelocity = false)
        {
            if (isOverrideVelocity)
            {
                ballRigidbody.velocity = Vector3.zero;
            }

            var force = direction.normalized * power;
            ballRigidbody.AddForce(force, ForceMode.Impulse);
        }

        /// <summary>
        /// 倍率のパネルに衝突したときに呼ばれる
        /// </summary>
        private void OnTriggerEnterPanel(GameObject panelObject)
        {
            var panel = panelObject.GetComponent<Panel>();
            for (var i = 0; i < panel.Multiply - 1; i++)
            {
                ballManager.SetBall(panel.BallGeneratePoint);
            }
        }

        /// <summary>
        /// 壁と衝突したときに呼ばれる
        /// </summary>
        private void OnCollisionEnterWall()
        {
            if (hitWallClip != null)
            {
                audioSource.PlayOneShot(hitWallClip);
            }
        }

        /// <summary>
        /// ゴールと衝突したときに呼ばれる
        /// </summary>
        private void OnCollisionEnterGoal()
        {
            if (hit1ptClip != null)
            {
                audioSource.PlayOneShot(hit1ptClip);
            }

            gameManager.AddScore();
            ballManager.EnqueueBall(this);
        }

        /// <summary>
        /// ジャックポットのポケットに衝突したときに呼ばれる
        /// </summary>
        private void OnCollisionEnterJackPot()
        {
            if (hitU1WClip != null)
            {
                audioSource.PlayOneShot(hitU1WClip);
            }

            gameManager.AddScore(5);
            gameManager.AddJackPotPoint(3);
            ballManager.EnqueueBall(this);
        }

        /// <summary>
        /// バンパーに衝突したときに呼ばれる
        /// </summary>
        private void OnCollisionEnterBumper(Vector3 bumperPosition)
        {
            if (hitBumperClip != null)
            {
                audioSource.PlayOneShot(hitBumperClip, 0.7f);
            }

            var direction = ballRigidbody.position - bumperPosition;
            var power = Random.Range(15f, 30f);
            AddForce(direction, power, true);
        }
    }
}