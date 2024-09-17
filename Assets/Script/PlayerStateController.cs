using UnityEngine;
using UnityEngine.UI; // UI���g�����߂ɕK�v

public class PlayerStateController : MonoBehaviour
{
    // Enum�ŏ�Ԃ��Ǘ�
    public enum PlayerState { Human, Ghost }
    public PlayerState currentState;

    // ��Ԑ؂�ւ��Ɏg���L�[
    public KeyCode switchKey = KeyCode.Space;

    // �ǂ̃R���C�_�[ (�l��Ԃł̂ݕ����I�ɉe����^����)
    public Collider2D wallCollider;

    // �X�v���C�g�����_���[ (�O����ς��邽�߂Ɏg�p)
    private SpriteRenderer spriteRenderer;

    // �J�����̔w�i�F�ύX�p
    private Camera mainCamera;

    // �l��ԂƗH���Ԃ̔w�i�F
    public Color humanBackgroundColor = Color.black; // �l��Ԃł͍�
    public Color ghostBackgroundColor = Color.white; // �H���Ԃł͔�

    // ��Ԃɉ����������ڂ̃X�v���C�g��ݒ�
    public Sprite humanSprite;
    public Sprite ghostSprite;

    // ��Ԑ؂�ւ��Ɋւ��鐧��
    public int maxSwitchCount = 5; // �ő�؂�ւ���
    private int currentSwitchCount; // ���݂̐؂�ւ���

    public float switchCooldown = 1f; // �N�[���^�C���i�b�j
    private bool canSwitch = true; // �؂�ւ��\���ǂ����̃t���O

    // UI �e�L�X�g
    public Text switchCountText; // �c��̎g�p�񐔂�\������e�L�X�g

    void Start()
    {
        // �ŏ��͐l��Ԃɐݒ�
        currentState = PlayerState.Human;

        // SpriteRenderer���擾
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �J�����̎Q�Ƃ��擾
        mainCamera = Camera.main;

        // �l��Ԃɉ����������ݒ�
        SetHumanState();

        // �؂�ւ��񐔂�������
        currentSwitchCount = 0;

        // �c��̎g�p�񐔂�UI�ɕ\��
        UpdateSwitchCountText();
    }

    void Update()
    {
        // �N�[���^�C�������A�؂�ւ��񐔂��ő�񐔂ɒB���Ă��邩�ǂ������m�F
        if (canSwitch && currentSwitchCount < maxSwitchCount && Input.GetKeyDown(switchKey))
        {
            if (currentState == PlayerState.Human)
            {
                SetGhostState();
            }
            else
            {
                SetHumanState();
            }

            // �؂�ւ��񐔂𑝉�
            currentSwitchCount++;

            // �c��̎g�p�񐔂�UI�ɍX�V
            UpdateSwitchCountText();

            // �N�[���^�C�����J�n
            StartCoroutine(SwitchCooldown());
        }
    }

    // �N�[���^�C�������i1�b�Ԑ؂�ւ��𖳌����j
    private System.Collections.IEnumerator SwitchCooldown()
    {
        canSwitch = false; // �؂�ւ��s��
        yield return new WaitForSeconds(switchCooldown); // �N�[���^�C���ҋ@
        canSwitch = true; // �؂�ւ��\
    }

    // �l��Ԃɐ؂�ւ�
    void SetHumanState()
    {
        currentState = PlayerState.Human;
        wallCollider.enabled = true; // �ǂɂԂ���悤�ɂ���
        spriteRenderer.sprite = humanSprite; // �l��Ԃ̌����ڂɕύX
        mainCamera.backgroundColor = humanBackgroundColor; // �w�i�F�����ɕύX
    }

    // �H���Ԃɐ؂�ւ�
    void SetGhostState()
    {
        currentState = PlayerState.Ghost;
        wallCollider.enabled = false; // �ǂ����蔲������悤�ɂ���
        spriteRenderer.sprite = ghostSprite; // �H���Ԃ̌����ڂɕύX
        mainCamera.backgroundColor = ghostBackgroundColor; // �w�i�F�𔒂ɕύX
    }

    // �c��̎g�p�񐔂�UI�ɕ\��
    void UpdateSwitchCountText()
    {
        // �c��̉񐔂�\��
        int switchesLeft = maxSwitchCount - currentSwitchCount;
        switchCountText.text = "Switches Left: " + switchesLeft;
    }
}
