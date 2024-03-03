using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public enum Alignment
    {
        Horizontal,
        Vertical
    }
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FlexWidth, // group type must be free
        FlexHeight, // group type must be free
        Free
    }

    public enum GroupType
    {
        Free,
        FixedRows,
        FixedColumns,
        FixedBoth
    }
    public TextAnchor ChildAlignment;
    public bool ChangePivot;
    public Alignment alignment;
    public FitType fitType;
    public GroupType groupType;

    public int columns;
    public int rows;
    public RectOffset Padding = new RectOffset();
    public Vector2 spacing;
    public float cellWidth;
    public float cellHeight;

    private bool AutoWidth;
    private bool AutoHeight;

    public bool FitWidth;
    public bool FitHeight;
    public bool ForceSquare;
    public bool MiddleLast;

    public override void CalculateLayoutInputVertical()
    {
        ValidateData();
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        childAlignment = ChildAlignment;
        base.CalculateLayoutInputHorizontal();

        float sqrRt;
        int childCount = rectChildren.Count;
        if (childCount == 0)
        {
            return;
        }
        if (fitType == FitType.FlexWidth || fitType == FitType.FlexHeight)
        {
            groupType = GroupType.Free;
        }
        switch (groupType)
        {
            case GroupType.FixedRows:
                columns = Mathf.CeilToInt(childCount / (float)rows);
                break;
            case GroupType.FixedColumns:
                rows = Mathf.CeilToInt(childCount / (float)columns);
                break;
            case GroupType.Free:
                sqrRt = Mathf.Sqrt(childCount);
                rows = Mathf.CeilToInt(sqrRt);
                columns = Mathf.CeilToInt(sqrRt);
                rows = Mathf.CeilToInt(childCount / (float)columns);
                columns = Mathf.CeilToInt(childCount / (float)rows);
                break;
            default:
                // both
                break;
        }
        columns = columns <= 0 ? 1 : columns;
        rows = rows <= 0 ? 1 : rows;

        switch (fitType)
        {
            case FitType.Uniform:
                FitWidth = FitHeight = true;
                AutoWidth = AutoHeight = true;
                ForceSquare = false;
                break;
            case FitType.Width:
                FitWidth = true;
                AutoWidth = true;
                AutoHeight = false;
                break;
            case FitType.Height:
                FitHeight = true;
                AutoWidth = false;
                AutoHeight = true;
                break;
            case FitType.FlexWidth:
                AutoWidth = false;
                FitWidth = false;
                columns = Mathf.FloorToInt((width - Padding.left - Padding.right + spacing.x) / (float)(this.cellWidth + spacing.x));
                columns = columns <= 1 ? 1 : columns;
                rows = Mathf.CeilToInt(childCount / (float)columns);
                break;
            case FitType.FlexHeight:
                AutoHeight = false;
                FitHeight = false;
                rows = Mathf.FloorToInt((height - Padding.top - Padding.bottom + spacing.y) / (float)(this.cellHeight + spacing.y));
                rows = rows <= 1 ? 1 : rows;
                columns = Mathf.CeilToInt(childCount / (float)rows);

                break;
            default:
                ForceSquare = false;
                AutoWidth = false;
                AutoHeight = false;
                break;
        }

        float cellWidth = (width - Padding.left - Padding.right - (columns - 1) * spacing.x) / columns;
        float cellHeight = (height - Padding.top - Padding.bottom - (rows - 1) * spacing.y) / rows;


        if (AutoWidth)
        {
            this.cellWidth = cellWidth;
            if (ForceSquare)
            {
                this.cellHeight = this.cellWidth;
            }
        }
        if (AutoHeight)
        {
            this.cellHeight = cellHeight;
            if (ForceSquare)
            {
                this.cellWidth = this.cellHeight;
            }
        }


        int rowCount;
        int columnCount;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            var item = rectChildren[i];
            float xPos = 0;
            float yPos = 0;
            float xLastItemOffset = 0;
            int numberInlast = 0;

            switch (alignment)
            {
                case Alignment.Horizontal:
                    rowCount = i / columns;
                    columnCount = i % columns;
                    if (MiddleLast && rowCount == (childCount / columns))
                    {
                        numberInlast = childCount - ((rows - 1) * columns);
                        xLastItemOffset = (columns - numberInlast) / 2f * (this.cellWidth + spacing.x);
                    }

                    xPos = (this.cellWidth * columnCount) + (spacing.x * columnCount) + Padding.left + xLastItemOffset;
                    yPos = (this.cellHeight * rowCount) + (spacing.y * rowCount) + Padding.top;
                    break;
                case Alignment.Vertical:
                    columnCount = i / rows;
                    rowCount = i % rows;
                    if (MiddleLast && columnCount == (childCount / rows))
                    {
                        numberInlast = childCount - ((columns - 1) * rows);
                        xLastItemOffset = (rows - numberInlast) / 2f * (this.cellHeight + spacing.y);
                    }

                    xPos = (this.cellWidth * columnCount) + (spacing.x * columnCount) + Padding.left;
                    yPos = (this.cellHeight * rowCount) + (spacing.y * rowCount) + Padding.top + xLastItemOffset;
                    break;
            }


            switch (m_ChildAlignment)
            {
                case TextAnchor.UpperLeft:
                default:
                    //No need to change xPos;
                    //No need to change yPos;
                    break;
                case TextAnchor.UpperCenter:
                    xPos += (0.5f * (width + (spacing.x + Padding.left + Padding.left) - (columns * (this.cellWidth + spacing.x + Padding.left)))); //Center xPos
                                                                                                                                                    //No need to change yPos;
                    break;
                case TextAnchor.UpperRight:
                    xPos = -xPos + width - this.cellWidth; //Flip xPos to go bottom-up
                                                           //No need to change yPos;
                    break;
                case TextAnchor.MiddleLeft:
                    //No need to change xPos;
                    yPos += (0.5f * (height + (spacing.y + Padding.top + Padding.top) - (rows * (this.cellHeight + spacing.y + Padding.top)))); //Center yPos
                    break;
                case TextAnchor.MiddleCenter:
                    xPos += (0.5f * (width + (spacing.x + Padding.left + Padding.left) - (columns * (this.cellWidth + spacing.x + Padding.left)))); //Center xPos
                    yPos += (0.5f * (height + (spacing.y + Padding.top + Padding.top) - (rows * (this.cellHeight + spacing.y + Padding.top)))); //Center yPos
                    break;
                case TextAnchor.MiddleRight:
                    xPos = -xPos + width - this.cellWidth; //Flip xPos to go bottom-up
                    yPos += (0.5f * (height + (spacing.y + Padding.top + Padding.top) - (rows * (this.cellHeight + spacing.y + Padding.top)))); //Center yPos
                    break;
                case TextAnchor.LowerLeft:
                    //No need to change xPos;
                    yPos = -yPos + height - this.cellHeight; //Flip yPos to go Right to Left
                    break;
                case TextAnchor.LowerCenter:
                    xPos += (0.5f * (width + (spacing.x + Padding.left + Padding.left) - (columns * (this.cellWidth + spacing.x + Padding.left)))); //Center xPos
                    yPos = -yPos + height - this.cellHeight; //Flip yPos to go Right to Left
                    break;
                case TextAnchor.LowerRight:
                    xPos = -xPos + width - this.cellWidth; //Flip xPos to go bottom-up
                    yPos = -yPos + height - this.cellHeight; //Flip yPos to go Right to Left
                    break;
            }


            SetChildAlongAxis(item, 0, xPos, this.cellWidth);
            SetChildAlongAxis(item, 1, yPos, this.cellHeight);
        }

        float maxX = FitWidth ? this.cellWidth * columns + spacing.x * (columns - 1) + Padding.left + Padding.right : width;
        float maxY = FitHeight ? this.cellHeight * rows + spacing.y * (rows - 1) + Padding.top + Padding.bottom : height;

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxX);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxY);
    }

    public void ValidateData()
    {
        if (ChangePivot)
        {
            switch (ChildAlignment)
            {
                case TextAnchor.UpperLeft:
                    rectTransform.pivot = new Vector2(0, 1);
                    break;
                case TextAnchor.UpperCenter:
                    rectTransform.pivot = new Vector2(0.5f, 1);
                    break;
                case TextAnchor.UpperRight:
                    rectTransform.pivot = new Vector2(1f, 1);
                    break;
                case TextAnchor.MiddleLeft:
                    rectTransform.pivot = new Vector2(0, 0.5f);
                    break;
                case TextAnchor.MiddleCenter:
                    rectTransform.pivot = new Vector2(0.5f, 0.5f);
                    break;
                case TextAnchor.MiddleRight:
                    rectTransform.pivot = new Vector2(1, 0.5f);
                    break;
                case TextAnchor.LowerLeft:
                    rectTransform.pivot = new Vector2(0f, 0);
                    break;
                case TextAnchor.LowerCenter:
                    rectTransform.pivot = new Vector2(0.5f, 0);
                    break;
                case TextAnchor.LowerRight:
                    rectTransform.pivot = new Vector2(1f, 0);
                    break;
            }
        }

        if (Padding == null)
        {
            return;
        }

        if (Padding.left < 0)
        {
            Padding.left = 0;
        }
        else if (Padding.left > rectTransform.rect.width / 2f)
        {
            Padding.left = Mathf.FloorToInt(rectTransform.rect.width / 2f);
        }
        if (Padding.right < 0)
        {
            Padding.right = 0;
        }
        else if (Padding.right > rectTransform.rect.width / 2f)
        {
            Padding.right = Mathf.FloorToInt(rectTransform.rect.width / 2f);
        }
        if (Padding.top < 0)
        {
            Padding.top = 0;
        }
        else if (Padding.top > rectTransform.rect.height / 2f)
        {
            Padding.top = Mathf.FloorToInt(rectTransform.rect.height / 2f);
        }
        if (Padding.bottom < 0)
        {
            Padding.bottom = 0;
        }
        else if (Padding.bottom > rectTransform.rect.height / 2f)
        {
            Padding.bottom = Mathf.FloorToInt(rectTransform.rect.height / 2f);
        }

    }

    public override void SetLayoutHorizontal() { }

    public override void SetLayoutVertical() { }
}
