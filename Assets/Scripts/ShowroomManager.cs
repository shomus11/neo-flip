using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShowroomManager : MonoBehaviour
{
    [Header("spawn Component")]
    [SerializeField] GameObject Prefabs;
    [SerializeField] Vector2 columnxRow;
    [Header("ingame Component")]
    [SerializeField] List<Sprite> backgroundList;
    [SerializeField] List<GameObject> boxPanel;
    [SerializeField] SpriteRenderer spriteRendererTarget;
    [SerializeField] GameObject textparty;

    float baseAnimationDuration = .25f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DoAnimationSequence();
        //DoAnimationSequence();
        //InvokeRepeating("ChangeBackground", 3f, 4f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeBackground()
    {

        StartCoroutine(ChangeBackgroundCoroutine());
    }
    IEnumerator ChangeBackgroundCoroutine()
    {

        //DoAnimationSequence();
        //yield return new WaitForSeconds(3f);

        //RotateSpriteMask();

        //yield return new WaitForSeconds(.5f);

        //spriteRendererTarget.sprite = backgroundList[Random.Range(0, backgroundList.Count)];
        //spriteRendererTarget.DOFade(1, .5f).From(0);
        //yield return new WaitForSeconds(.5f);
        yield return new WaitForSeconds(4f);
    }
    public void DoAnimationSequence()
    {
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < boxPanel.Count; i++)
        {

            boxPanel[i].transform.DOLocalMoveY(boxPanel[i].transform.position.y, baseAnimationDuration * 6f).From(boxPanel[i].transform.position.y + multiply).SetEase(Ease.Linear);
        }

        spriteRendererTarget.DOFade(1, 0);
        float totalAnimationDuration = 3;
        //totalAnimationDuration += baseAnimationDuration;
        sequence.Insert(totalAnimationDuration, spriteRendererTarget.DOFade(0, baseAnimationDuration * 3f).From(1));
        totalAnimationDuration += baseAnimationDuration * 3f;
        

        totalAnimationDuration = RotateSpriteMaskDuration(totalAnimationDuration, sequence);
        sequence.InsertCallback(totalAnimationDuration, () =>
        {
            spriteRendererTarget.sprite = backgroundList[Random.Range(0, backgroundList.Count)];
        });

        //totalAnimationDuration += baseAnimationDuration / .025f;
        sequence.Insert(totalAnimationDuration, spriteRendererTarget.DOFade(1, baseAnimationDuration * 3f).From(0));
        totalAnimationDuration += baseAnimationDuration * 8f;
        sequence.Insert(totalAnimationDuration, spriteRendererTarget.DOFade(0, baseAnimationDuration * 3f).From(1));
        totalAnimationDuration += baseAnimationDuration * 3f;
        sequence.Insert(totalAnimationDuration, Camera.main.DOFieldOfView(Camera.main.fieldOfView+9, baseAnimationDuration * 4f).From(Camera.main.fieldOfView));
        totalAnimationDuration += baseAnimationDuration * 4f;
        
        totalAnimationDuration = MoveSpriteMaskDuration(totalAnimationDuration, sequence);
        
        totalAnimationDuration += baseAnimationDuration * 4f;

        sequence.InsertCallback(totalAnimationDuration, () =>
        {
            textparty.SetActive(true);
        });
        sequence.Insert(totalAnimationDuration, textparty.transform.DOScale(Vector3.one, baseAnimationDuration * 2).From(Vector3.zero).SetEase(Ease.OutBack));
        totalAnimationDuration += baseAnimationDuration * 2f;
        sequence.Insert(totalAnimationDuration, textparty.transform.DOScale(Vector3.one * 1.5f, baseAnimationDuration * 2).From(Vector3.one).SetEase(Ease.OutBack).SetLoops(6,LoopType.Yoyo));
        sequence.Insert(totalAnimationDuration, textparty.transform.DOMoveZ(textparty.transform.position.z-2f, baseAnimationDuration * 2).From(textparty.transform.position.z).SetEase(Ease.OutBack).SetLoops(6, LoopType.Yoyo));
        totalAnimationDuration += baseAnimationDuration * 12f;
        sequence.Insert(totalAnimationDuration, textparty.transform.DOScale(Vector3.zero, baseAnimationDuration * 2).From(Vector3.one).SetEase(Ease.OutBack));
        totalAnimationDuration += baseAnimationDuration * 2f;
        sequence.InsertCallback(totalAnimationDuration, () =>
        {
            textparty.SetActive(false);
        });
        totalAnimationDuration += baseAnimationDuration * 4f;
        
        totalAnimationDuration = MoveOutDuration(totalAnimationDuration, sequence);

        totalAnimationDuration += baseAnimationDuration * 12f;

        totalAnimationDuration = MoveInDuration(totalAnimationDuration, sequence);
        
        totalAnimationDuration += baseAnimationDuration * 4f;
        
        sequence.Insert(totalAnimationDuration, spriteRendererTarget.DOFade(1, baseAnimationDuration * 3f).From(0));

        Debug.Log($"{totalAnimationDuration}");


    }
    float RotateSpriteMaskDuration(float totalAnimationDuration, Sequence sequence)
    {
        for (int i = 0; i < boxPanel.Count; i++)
        {
            float random = Random.Range(0f, 1000);
            float delay = Random.Range(0f, baseAnimationDuration);
            float multiply = 0;
            if (random < 500)
            {
                multiply = 360f;
            }
            else
            {
                multiply = -360f;
            }
            sequence.Insert(totalAnimationDuration + delay, boxPanel[i].transform.DOLocalRotate(Vector3.right * multiply, baseAnimationDuration * 3f, RotateMode.FastBeyond360).From(Vector3.zero).SetEase(Ease.Linear));
        }
        totalAnimationDuration += baseAnimationDuration * 4f;


        return totalAnimationDuration;
    }
    float MoveSpriteMaskDuration(float totalAnimationDuration, Sequence sequence)
    {
        for (int i = 0; i < boxPanel.Count; i++)
        {
            float random = Random.Range(0f, 1000);
            float delay = Random.Range(0f, baseAnimationDuration);
            float multiply = 0;
            if (random < 500)
            {
                multiply = Random.Range(.10f, .31f);
            }
            else
            {
                multiply = Random.Range(-.10f, -.31f);
            }
            sequence.Insert(totalAnimationDuration + delay, boxPanel[i].transform.DOLocalMoveZ(
                boxPanel[i].transform.localPosition.z + multiply, baseAnimationDuration)
                .From(transform.localPosition.z)
                .SetEase(Ease.Linear).SetLoops(34, LoopType.Yoyo)
                ).SetId("moves");
        }
        totalAnimationDuration += baseAnimationDuration * 12f;
        return totalAnimationDuration;
    }

    float MoveOutDuration(float totalAnimationDuration, Sequence sequence)
    {
        int rows = 18;
        int columns = 10;
        for (int i = 0; i < boxPanel.Count; i++)
        {

            int row = i / rows;
            int col = i % columns;

            float multiply = (row % 2 == 0) ? -25f : 25f;
            sequence.Insert(totalAnimationDuration, boxPanel[i].transform.DOLocalMoveX(boxPanel[i].transform.position.x + multiply, baseAnimationDuration * 6f).From(boxPanel[i].transform.position.x).SetEase(Ease.Linear));
        }
        totalAnimationDuration += baseAnimationDuration * 4f;


        return totalAnimationDuration;
    }
    float MoveInDuration(float totalAnimationDuration, Sequence sequence)
    {
        int rows = 18;
        int columns = 10;
        for (int i = 0; i < boxPanel.Count; i++)
        {

            int row = i / rows;
            int col = i % columns;

            float multiply = (col % 2 == 0) ? -25f : 25f;
            sequence.Insert(totalAnimationDuration, boxPanel[i].transform.DOLocalMoveY(boxPanel[i].transform.position.y, baseAnimationDuration * 6f).From(boxPanel[i].transform.position.y+multiply).SetEase(Ease.Linear));
        }
        totalAnimationDuration += baseAnimationDuration * 4f;


        return totalAnimationDuration;
    }


    public void RotateSpriteMask()
    {
        for (int i = 0; i < boxPanel.Count; i++)
        {
            boxPanel[i].transform.DOLocalRotate(Vector3.right * 360, 1f, RotateMode.FastBeyond360).From(Vector3.zero).SetEase(Ease.Linear);
        }
    }

    public void SpawnObject()
    {
        float xPos = 0;
        float yPos = 0;
        for (int i = 0; i < columnxRow.y; i++)
        {
            for (int y = 0; y < columnxRow.x; y++)
            {
                GameObject go = Instantiate(Prefabs);
                go.transform.position = new Vector3(xPos, yPos, 0);
                go.transform.parent = gameObject.transform;
                xPos += 1.1f;
            }
            yPos += 1.1f;
            xPos = 0;
        }
    }
}
