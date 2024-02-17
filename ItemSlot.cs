using Godot;
using System;

/// <summary>
/// The slot that holds the <see cref="DraggableItem"/>
/// 
/// <para>This examples uses <see cref="CanvasItem.SelfModulate"/> instead of <see cref="CanvasItem.Modulate"/> to avoid recoloring the <see cref="DraggableItem"/> as well</para>
/// </summary>
public partial class ItemSlot : TextureRect
{
    /// <summary>
    /// This is technically not needed but in case you want to check if it has hovered while dragged
    /// <para>Example: To change color when its hovered then resetting the color when its not hovered</para>
    /// </summary>
    private bool _Hovered;

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        var draggableItem = data.As<DraggableItem>();

        //Check if the data is our DraggableItem and if the CurrentAttachetSlot is not the same to avoid reparenting to the same parent
        if (draggableItem != null && draggableItem.CurrentAttachedSlot != this)
        {
            _Hovered = true;
            SelfModulate = Colors.Green;
            return true;
        }

        return false;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        var draggableItem = data.As<DraggableItem>();

        //Change the parent and position
        draggableItem.CurrentAttachedSlot = this;
        draggableItem.Reparent(this, false);

        SelfModulate = new Color(0, 0, 0, 0.5f);
        _Hovered = false;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        MouseExited += () =>
        {
            //If mouse exit this item after being hovered while dragging, then reset the color
            if (_Hovered)
            {
                SelfModulate = new Color(0, 0, 0, 0.5f);
                _Hovered = false;
            }
        };
	}
}
