using Godot;
using System;

/// <summary>
/// The item you want to drag and drop to <see cref="ItemSlot"/>
/// </summary>
public partial class DraggableItem : TextureRect
{
    public ItemSlot CurrentAttachedSlot { get; set; }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        Input.SetDefaultCursorShape(Input.CursorShape.Drag);

        //Set the mouse drag by creating a copy of this
        //Do not pass the current item as is because the item will be removed automatically by Godot on drag ended
        SetDragPreview(Duplicate() as Control);

        //OPTIONAL: Since we are creating a duplicate, disable the visibility on this until dragging has completed
        Visible = false;

        return this;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        //Get the current parent and set the default mouse cursor to drag when hovering into this item
        CurrentAttachedSlot = GetParent<ItemSlot>();
        MouseDefaultCursorShape = CursorShape.Drag;
    }

    // On finished drag, reset the mouse cursor shape and set it to visible again
    public override void _Notification(int what)
    {
        if (what == NotificationDragEnd)
        {
            Input.SetDefaultCursorShape();
            Visible = true;
        }
    }
}
