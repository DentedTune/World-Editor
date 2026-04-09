using DG.Tweening;
using UnityEngine;

public class BetterTileBehavior : MonoBehaviour
{
    public Transform HoveringFurniture;
    public Transform PlacedFurniture;

    public LocalRoomState state;

    public Object2D furniture;
    public int xValue;
    public int yValue;

    public float DownSquash; //Move down before hop
    public float FlatSquash;
    public float UpHeight;
    public float UpStretch;
    public float SquashDuration;
    public float UpDuration;
    public float LandDuration;

    private void Start()
    {
        state = GameObject.FindAnyObjectByType<LocalRoomState>();
    }

    private void OnMouseEnter()
    {
        HoveringFurniture.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        HoveringFurniture.gameObject.SetActive(false);
    }

    private void OnMouseUpAsButton()
    {
        if (!PlacedFurniture.gameObject.activeSelf)
        {
            state.SetPlaceEdit(furniture);
            PlacedFurniture.gameObject.SetActive(true);
            PlacingBounce(PlacedFurniture);
        }
        else
        {
            state.SetDeleteEdit(furniture.positionX, furniture.positionY);
            PlacedFurniture.gameObject.SetActive(false);
        }
    }

    private void PlacingBounce(Transform bouncyObject)
    {
        Tween squashDown = bouncyObject.DOLocalMove(new Vector3(0, DownSquash), SquashDuration).SetEase(Ease.OutQuad);
        Tween jumpUp = bouncyObject.DOLocalMove(new Vector3(0, UpHeight), UpDuration).SetEase(Ease.InOutQuad);
        Tween fallDown = bouncyObject.DOLocalMove(new Vector3(0, 0), LandDuration).SetEase(Ease.OutBounce);
        Tween squashFlat = bouncyObject.DOScale(new Vector3(FlatSquash, 1 / FlatSquash), SquashDuration).SetEase(Ease.OutQuad);
        Tween jumpStretch = bouncyObject.DOScale(new Vector3(1 / UpStretch, UpStretch), UpDuration).SetEase(Ease.InOutQuad);
        Tween fallResize = bouncyObject.DOScale(new Vector3(1, 1), LandDuration).SetEase(Ease.OutBounce);

        Sequence bounce = DOTween.Sequence();
        bounce.Insert(0, squashDown);
        bounce.Insert(SquashDuration, jumpUp);
        bounce.Insert(SquashDuration + UpDuration, fallDown);
        bounce.Insert(0, squashFlat);
        bounce.Insert(SquashDuration, jumpStretch);
        bounce.Insert(SquashDuration + UpDuration, fallResize);
    }
}
