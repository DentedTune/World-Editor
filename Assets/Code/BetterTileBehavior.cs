using DG.Tweening;
using UnityEngine;

public class BetterTileBehavior : MonoBehaviour
{
    public Transform HoveringFurniture;
    public Transform PlacedFurniture;


    public Transform HoveringChairDown;
    public Transform HoveringChairUp;
    public Transform HoveringChairLeft;
    public Transform HoveringChairRight;
    public Transform HoveringTable;
    public Transform HoveringVase;

    public Transform PlacedChairDown;
    public Transform PlacedChairUp;
    public Transform PlacedChairLeft;
    public Transform PlacedChairRight;
    public Transform PlacedTable;
    public Transform PlacedVase;

    public LocalRoomState state;
    public PlacingSettings placeSettings;

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
        placeSettings = GameObject.FindAnyObjectByType<PlacingSettings>();
        PlaceExistingFurniture();
    }

    private void OnMouseEnter()
    {
        SetHoveringFurnitureSprite();
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
            furniture.objectType = placeSettings.furnitureLook.objectType;
            furniture.direction = placeSettings.furnitureLook.direction;
            state.SetPlaceEdit(furniture);
            PlaceFurniture(furniture);
        }
        else
        {
            state.SetDeleteEdit(furniture.positionX, furniture.positionY);
            RemoveFurniture();
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

    private void PlaceExistingFurniture()
    {
        for (int i = 0; i < state.LastSavedState.Count; i++)
        {
            if (state.LastSavedState[i].positionX == furniture.positionX && state.LastSavedState[i].positionY == furniture.positionY)
            {
                furniture = state.LastSavedState[i];
                PlaceFurniture(state.LastSavedState[i]);
            }
        }
    }

    private void PlaceFurniture(Object2D furnitureToMatch)
    {
        SetActualFurnitureSprite(furnitureToMatch);
        PlacedFurniture.gameObject.SetActive(true);
        PlacingBounce(PlacedFurniture);
    }

    private void RemoveFurniture()
    {
        PlacedFurniture.gameObject.SetActive(false);
    }

    private void SetHoveringFurnitureSprite()
    {
        HoveringChairDown.gameObject.SetActive(false);
        HoveringChairUp.gameObject.SetActive(false);
        HoveringChairLeft.gameObject.SetActive(false);
        HoveringChairRight.gameObject.SetActive(false);
        HoveringTable.gameObject.SetActive(false);
        HoveringVase.gameObject.SetActive(false);

        switch (placeSettings.furnitureLook.objectType)
        {
            case "chair":
                switch (placeSettings.furnitureLook.direction)
                {
                    case 0:
                        HoveringChairDown.gameObject.SetActive(true);
                        break;
                    case 1:
                        HoveringChairLeft.gameObject.SetActive(true);
                        break;
                    case 2:
                        HoveringChairUp.gameObject.SetActive(true);
                        break;
                    case 3:
                        HoveringChairRight.gameObject.SetActive(true);
                        break;
                    default:
                        HoveringChairDown.gameObject.SetActive(true);
                        Debug.Log($"Incorrect direction value: {placeSettings.furnitureLook.direction}");
                        break;
                }
                break;

            case "table":
                HoveringTable.gameObject.SetActive(true);
                break;
            case "vase":
                HoveringVase.gameObject.SetActive(true);
                break;

            default:
                Debug.Log($"Unknown object type: {placeSettings.furnitureLook.objectType}, Placing a chair instead");
                switch (placeSettings.furnitureLook.direction)
                {
                    case 0:
                        HoveringChairDown.gameObject.SetActive(true);
                        break;
                    case 1:
                        HoveringChairLeft.gameObject.SetActive(true);
                        break;
                    case 2:
                        HoveringChairUp.gameObject.SetActive(true);
                        break;
                    case 3:
                        HoveringChairRight.gameObject.SetActive(true);
                        break;
                    default:
                        HoveringChairDown.gameObject.SetActive(true);
                        Debug.Log($"Incorrect direction value: {placeSettings.furnitureLook.direction}");
                        break;
                }
                break;
        }
    }

    private void SetActualFurnitureSprite(Object2D furnitureToMatch)
    {
        PlacedChairDown.gameObject.SetActive(false);
        PlacedChairUp.gameObject.SetActive(false);
        PlacedChairLeft.gameObject.SetActive(false);
        PlacedChairRight.gameObject.SetActive(false);
        PlacedTable.gameObject.SetActive(false);
        PlacedVase.gameObject.SetActive(false);

        switch (furnitureToMatch.objectType)
        {
            case "chair":
                switch (furnitureToMatch.direction)
                {
                    case 0:
                        PlacedChairDown.gameObject.SetActive(true);
                        break;
                    case 1:
                        PlacedChairLeft.gameObject.SetActive(true);
                        break;
                    case 2:
                        PlacedChairUp.gameObject.SetActive(true);
                        break;
                    case 3:
                        PlacedChairRight.gameObject.SetActive(true);
                        break;
                    default:
                        PlacedChairDown.gameObject.SetActive(true);
                        Debug.Log($"Incorrect direction value: {furnitureToMatch.direction}");
                        break;
                }
                break;

            case "table":
                PlacedTable.gameObject.SetActive(true);
                break;
            case "vase":
                PlacedVase.gameObject.SetActive(true);
                break;

            default:
                Debug.Log($"Unknown object type: {furnitureToMatch.objectType}, Placing a chair instead");
                switch (furnitureToMatch.direction)
                {
                    case 0:
                        PlacedChairDown.gameObject.SetActive(true);
                        break;
                    case 1:
                        PlacedChairLeft.gameObject.SetActive(true);
                        break;
                    case 2:
                        PlacedChairUp.gameObject.SetActive(true);
                        break;
                    case 3:
                        PlacedChairRight.gameObject.SetActive(true);
                        break;
                    default:
                        PlacedChairDown.gameObject.SetActive(true);
                        Debug.Log($"Incorrect direction value: {furnitureToMatch.direction}");
                        break;
                }
                break;
        }
    }
}
