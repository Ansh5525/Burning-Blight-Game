using System.Collections;
using UnityEngine;

public class CharSelectManager : MonoBehaviour
{
    [SerializeField] public CharSelection selection1;
    [SerializeField] public CharSelection selection2;
    [SerializeField] public CharSelection selection3;
    [SerializeField] public CharSelection selection4;
    [SerializeField] public CharSelection selection5;
    [SerializeField] public CharSelection selection6;
    [SerializeField] public CharSelection selection7;
    [SerializeField] public CharSelection selection8;
    [SerializeField] public CharSelection selection9;
    [SerializeField] public CharSelection selection10;
    [SerializeField] public CharSelection selection11;
    [SerializeField] public CharSelection selection12;
    [SerializeField] public CharSelection selection13;
    [SerializeField] public CharSelection selection14;
    [SerializeField] public CharSelection selection15;
    [SerializeField] public CharSelection selection16;

    [SerializeField] private Vector2Int p1Pos = new Vector2Int(0, 0);
    [SerializeField] private Vector2Int p2Pos = new Vector2Int(0, 7);

    private CharSelection[,] grid = new CharSelection[2, 8];

    private Vector2Int up = new Vector2Int(-1, 0);
    private Vector2Int down = new Vector2Int(1, 0);
    private Vector2Int left = new Vector2Int(0, -1);
    private Vector2Int right = new Vector2Int(0, 1);

    [SerializeField] private SpriteRenderer vsArtP1;
    [SerializeField] private SpriteRenderer vsArtP2;

    [SerializeField] private float slideDuration = 0.3f;

    private Coroutine vsArtRoutineP1;
    private Coroutine vsArtRoutineP2;

    private Vector3 vsArtP1EndPos;
    private Vector3 vsArtP2EndPos;

    private void OnEnable()
    {
        SetGrid();

        SetCharAvailability();

        // Setup all icons
        for (int row = 0; row < 2; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                if (grid[row, col] != null)
                {
                    grid[row, col].UpdateVisual();
                }
            }
        }

        UpdateMarkerVisuals(true);
        UpdateMarkerVisuals(false);
    }

    void Awake()
    {
        vsArtP1EndPos = vsArtP1.transform.localPosition;
        vsArtP2EndPos = vsArtP2.transform.localPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) MoveP1(up);
        if (Input.GetKeyDown(KeyCode.S)) MoveP1(down);
        if (Input.GetKeyDown(KeyCode.A)) MoveP1(left);
        if (Input.GetKeyDown(KeyCode.D)) MoveP1(right);

        if (Input.GetKeyDown(KeyCode.I)) MoveP2(up);
        if (Input.GetKeyDown(KeyCode.K)) MoveP2(down);
        if (Input.GetKeyDown(KeyCode.J)) MoveP2(left);
        if (Input.GetKeyDown(KeyCode.L)) MoveP2(right);
    }


    void SetGrid()
    {
        grid[0, 0] = selection1;
        grid[0, 1] = selection2;
        grid[0, 2] = selection3;
        grid[0, 3] = selection4;
        grid[0, 4] = selection5;
        grid[0, 5] = selection6;
        grid[0, 6] = selection7;
        grid[0, 7] = selection8;

        grid[1, 0] = selection9;
        grid[1, 1] = selection10;
        grid[1, 2] = selection11;
        grid[1, 3] = selection12;
        grid[1, 4] = selection13;
        grid[1, 5] = selection14;
        grid[1, 6] = selection15;
        grid[1, 7] = selection16;
    }

    void SetCharAvailability()
    {
        selection1.available = true;
        selection2.available = false;
        selection3.available = false;
        selection4.available = false;
        selection5.available = false;
        selection6.available = false;
        selection7.available = false;
        selection8.available = true;
        selection9.available = false;
        selection10.available = false;
        selection11.available = false;
        selection12.available = false;
        selection13.available = false;
        selection14.available = false;
        selection15.available = false;
        selection16.available = false;
    }

    void DisplayVSArtP1(CharData data)
    {
        if (vsArtP1 == null || data == null) return;

        vsArtP1.sprite = data.vsArt;
        vsArtP1.flipX = false;

        if (vsArtRoutineP1 != null)
            StopCoroutine(vsArtRoutineP1);

        Vector3 startOffset = vsArtP1EndPos + new Vector3(-1f, 0f, 0f); // Slide from left
        vsArtRoutineP1 = StartCoroutine(SlideVSArt(vsArtP1, startOffset, vsArtP1EndPos));
    }  // -0.671521336

    void DisplayVSArtP2(CharData data)
    {
        if (vsArtP2 == null || data == null) return;

        vsArtP2.sprite = data.vsArt;
        vsArtP2.flipX = true;

        if (vsArtRoutineP2 != null)
            StopCoroutine(vsArtRoutineP2);

        Vector3 startOffset = vsArtP2EndPos + new Vector3(1f, 0f, 0f); // Slide from right
        vsArtRoutineP2 = StartCoroutine(SlideVSArt(vsArtP2, startOffset, vsArtP2EndPos));
    }


    IEnumerator SlideVSArt(SpriteRenderer target, Vector3 startPos, Vector3 endPos)
    {
        target.transform.localPosition = startPos;
        target.enabled = true;

        float time = 0f;
        while (time < slideDuration)
        {
            float t = time / slideDuration;
            target.transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            time += Time.deltaTime;
            yield return null;
        }
        target.transform.localPosition = endPos;
    }


    void MoveP1(Vector2Int dir)
    {
        TryMove(ref p1Pos, dir);
        UpdateMarkerVisuals(true); // Only update P1 VS art
    }

    void MoveP2(Vector2Int dir)
    {
        TryMove(ref p2Pos, dir);
        UpdateMarkerVisuals(false); // Only update P2 VS art
    }

    void TryMove(ref Vector2Int playerPos, Vector2Int dir)
    {
        Vector2Int newPos = playerPos;

        while (true)
        {
            newPos += dir;
            if (newPos.x < 0 || newPos.x > 1 || newPos.y < 0 || newPos.y > 7)
                break;

            if (grid[newPos.x, newPos.y] != null && grid[newPos.x, newPos.y].available)
            {
                playerPos = newPos;
                break;
            }
        }
    }

    void UpdateMarkerVisuals(bool isP1OnlyUpdate)
    {
        for (int row = 0; row < 2; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                var icon = grid[row, col];
                if (icon == null) continue;

                bool isP1Here = (p1Pos.x == row && p1Pos.y == col);
                bool isP2Here = (p2Pos.x == row && p2Pos.y == col);

                icon.SetMarkers(isP1Here, isP2Here);

                if (isP1Here && isP1OnlyUpdate)
                    DisplayVSArtP1(icon.charData);

                if (isP2Here && !isP1OnlyUpdate)
                    DisplayVSArtP2(icon.charData);
            }
        }
    }
}