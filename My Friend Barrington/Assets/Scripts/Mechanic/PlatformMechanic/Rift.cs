using FMODUnity;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Rift : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject tpTransform;

    private Rift otherRift;
    private Player player;
    private CinemachineFollow playerCam;

    [Header("Teleport Settings")]
    [SerializeField] private float tpCooldown = 1f;
    [SerializeField] private bool hideSpawn = false;
    [SerializeField] private float triggerDistance = 2f;

    public bool isTp;

    [Header("Audio (FMOD)")]
    [SerializeField] private EventReference riftExitEvent;

    private Coroutine followCoroutine;

    private void Start()
    {
        otherRift = tpTransform.GetComponent<Rift>();
        player = gameObject.findPlayer();
        playerCam = GameObject.Find(GeneralGameTags.PlayerCamera).GetComponent<CinemachineFollow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.isPlayer()) return;
        if (isTp) return;

        // Force the fullscreen/audio rift effect off before teleporting
        if (RiftEffectManager.Instance != null)
            RiftEffectManager.Instance.ForceOff();

        // Disable camera damping briefly for teleport
        playerCam.TrackerSettings.PositionDamping = Vector3.zero;

        // Teleport player
        player.transform.position = tpTransform.transform.position;

        // Play exit sound
        RuntimeManager.PlayOneShotAttached(riftExitEvent, gameObject);

        // Prevent immediate re-teleport loops on both rifts
        isTp = true;
        if (otherRift != null)
            otherRift.isTp = true;

        // Start cooldown on both rifts
        StartCoroutine(StartCooldown());

        if (otherRift != null)
            otherRift.StartCoroutine(otherRift.StartCooldown());

        // Start camera restore once
        if (followCoroutine != null)
            StopCoroutine(followCoroutine);

        followCoroutine = StartCoroutine(StartFollowing());
    }

    private IEnumerator StartCooldown()
    {
        if (hideSpawn)
            gameObject.SetActive(false);

        yield return new WaitForSeconds(tpCooldown);
        isTp = false;
    }

    private IEnumerator StartFollowing()
    {
        yield return new WaitForSeconds(0.5f);
        playerCam.TrackerSettings.PositionDamping = Vector3.one;
        followCoroutine = null;
    }

    private void OnDisable()
    {
        // If this rift gets disabled while active, make sure effect is not left hanging
        if (RiftEffectManager.Instance != null)
            RiftEffectManager.Instance.ForceOff();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }
}