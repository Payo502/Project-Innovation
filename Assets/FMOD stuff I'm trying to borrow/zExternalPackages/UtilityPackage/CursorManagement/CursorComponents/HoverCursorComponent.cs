﻿using SerializableDictionaryPackage.SerializableDictionary;
using UnityEngine;
using UnityEngine.UI;
using UtilityPackage.CursorManagement.CursorUtility;
using UtilityPackage.CursorManagement.CursorUtility.Singletons;
using UtilityPackage.CursorManagement.Structs;

namespace UtilityPackage.CursorManagement.CursorComponents
{
    public class HoverCursorComponent : AbstractCursorComponent
    {
        [SerializeField] [Tooltip("The CursorData to use if no other data is specified")]
        private CursorData defaultHoverDatum;

        [SerializeField] [Tooltip("Specify a CursorData for a specific tag")]
        private SerializableDictionary<string, CursorData> tagData;

        private CursorData? cursorDataToSet;

        private bool pointerIsHoveringOverSelectable;

        public override bool IsAdditiveEffect => false;

        private void LateUpdate()
        {
            if (IsPointerOverSelectable(out var hoveredSelectableObject))
            {
                var newCursorData = GetCursorData(hoveredSelectableObject);

                if (!newCursorData.Equals(cursorDataToSet)) // Prevent updating to the cursorData that is already set
                {
                    cursorDataToSet = newCursorData;
                    ShouldUpdateCursor = true;
                }
            }
        }

        protected override void OnDeactivate()
        {
            cursorDataToSet = null;
        }

        public override bool AreConditionsMet()
        {
            return pointerIsHoveringOverSelectable;
        }

        public override CursorData GetCursorData()
        {
            ShouldUpdateCursor = false;
            return cursorDataToSet!.Value;
        }

        private bool IsPointerOverSelectable(out GameObject hoveredSelectableObject)
        {
            pointerIsHoveringOverSelectable = false;
            hoveredSelectableObject = null;

            if (CursorUtil.Instance.TryGetHoveredGameObject(out var hoveredGameObject))
                if (hoveredGameObject.GetComponent<Selectable>() != null)
                {
                    pointerIsHoveringOverSelectable = true;
                    hoveredSelectableObject = hoveredGameObject;
                }

            return pointerIsHoveringOverSelectable;
        }

        private CursorData GetCursorData(GameObject hoveredObject)
        {
            if (hoveredObject.TryGetComponent(out CursorTextureComponent cursorTexture))
                return cursorTexture.CursorData;

            if (tagData.TryGetValue(hoveredObject.tag, out var cursorData)) return cursorData;

            return defaultHoverDatum;
        }
    }
}