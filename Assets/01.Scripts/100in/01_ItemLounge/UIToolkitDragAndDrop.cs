#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UIElements;

//public class UIToolkitDragAndDrop : PointerManipulator
//{

//    // The Label in the window that shows the stored asset, if any.
//    Label dropLabel;
//    // The stored asset object, if any.
//    Object droppedObject = null;
//    // The path of the stored asset, or the empty string if there isn't one.
//    string assetPath = string.Empty;

//    public UIToolkitDragAndDrop(VisualElement root)
//    {
//        target = root.Q<VisualElement>("drop-area");
//        dropLabel = root.Q<Label>(className: "drop-area__label");
//    }

//    protected override void RegisterCallbacksOnTarget()
//    {
//        target.RegisterCallback<PointerDownEvent>(OnPointerDown);
//        target.RegisterCallback<DragEnterEvent>(OnDragEnter);
//        target.RegisterCallback<DragLeaveEvent>(OnDragLeave);
//        target.RegisterCallback<DragUpdatedEvent>(OnDragUpdate);
//        target.RegisterCallback<DragPerformEvent>(OnDragPerform);
//    }
//    protected override void UnregisterCallbacksFromTarget()
//    {
//        target.UnregisterCallback<PointerDownEvent>(OnPointerDown);
//        target.UnregisterCallback<DragEnterEvent>(OnDragEnter);
//        target.UnregisterCallback<DragLeaveEvent>(OnDragLeave);
//        target.UnregisterCallback<DragUpdatedEvent>(OnDragUpdate);
//        target.UnregisterCallback<DragPerformEvent>(OnDragPerform);
//    }
//    void OnPointerDown(PointerDownEvent _)
//    {
//        // Only do something if the window currently has a reference to an asset object.
//        if (droppedObject != null)
//        {
//#if UNITY_EDITOR
//            // Clear existing data in DragAndDrop class.
//            DragAndDrop.PrepareStartDrag();

//            // Store reference to object and path to object in DragAndDrop static fields.
//            DragAndDrop.objectReferences = new[] { droppedObject };
//            if (assetPath != string.Empty)
//            {
//                DragAndDrop.paths = new[] { assetPath };
//            }
//            else
//            {
//                DragAndDrop.paths = new string[] { };
//            }

//            // Start a drag.
//            DragAndDrop.StartDrag(string.Empty);
//#endif
//        }
//    }

//    // This method runs if a user brings the pointer over the target while a drag is in progress.
//    void OnDragEnter(DragEnterEvent evt)
//    {
//#if UNITY_EDITOR
//        // Get the name of the object the user is dragging.
//        var draggedName = string.Empty;
//        if (DragAndDrop.paths.Length > 0)
//        {
//            assetPath = DragAndDrop.paths[0];
//            var splitPath = assetPath.Split('/');
//            draggedName = splitPath[splitPath.Length - 1];
//        }
//        else if (DragAndDrop.objectReferences.Length > 0)
//        {
//            draggedName = DragAndDrop.objectReferences[0].name;
//        }
//#endif
//        // Change the appearance of the drop area if the user drags something over the drop area and holds it
//        // there.
//        dropLabel.text = $"Dropping '{draggedName}'...";
//        target.AddToClassList("drop-area--dropping");
//    }

//    // This method runs if a user makes the pointer leave the bounds of the target while a drag is in progress.
//    void OnDragLeave(DragLeaveEvent evt)
//    {
//        assetPath = string.Empty;
//        droppedObject = null;
//        dropLabel.text = "Drag an asset here...";
//        target.RemoveFromClassList("drop-area--dropping");
//    }

//    // This method runs every frame while a drag is in progress.
//    void OnDragUpdate(DragUpdatedEvent evt)
//    {
//#if UNITY_EDITOR
//        DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
//#endif
//    }

//    // This method runs when a user drops a dragged object onto the target.
//    void OnDragPerform(DragPerformEvent evt)
//    {
//#if UNITY_EDITOR
//        // Set droppedObject and draggedName fields to refer to dragged object.
//        droppedObject = DragAndDrop.objectReferences[0];
//        string draggedName;
//        if (assetPath != string.Empty)
//        {
//            var splitPath = assetPath.Split('/');
//            draggedName = splitPath[splitPath.Length - 1];
//        }
//        else
//        {
//            draggedName = droppedObject.name;
//        }
//#endif
//        // Visually update target to indicate that it now stores an asset.
//        dropLabel.text =  $"Containing '{draggedName}'...\n\n" +
//                    $"(You can also drag from here)";
//        target.RemoveFromClassList("drop-area--dropping");
//    }
//}


   

