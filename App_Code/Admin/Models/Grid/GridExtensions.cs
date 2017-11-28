using System;
using System.Collections.Generic;

namespace FlyerMe.Admin.Models
{
    public static class GridExtensions
    {
        static GridExtensions()
        {
            editActionAttributes = new Dictionary<String, String> { { "class", "edit" } };
            deleteActionAttributes = new Dictionary<String, String> { { "class", "del" } };
        }

        public static Dictionary<String, String> GetActionTypeAttributes(this ActionTypes at)
        {
            if (at == ActionTypes.Edit)
            {
                return editActionAttributes;
            }
            else if (at == ActionTypes.Delete)
            {
                return deleteActionAttributes;
            }
            else
            {
                return null;
            }
        }

        public static ActionTypes Parse(String toActionType)
        {
            ActionTypes result;

            if (!Enum.TryParse(toActionType, out result))
            {
                result = ActionTypes.None;
            }

            return result;
        }

        #region private

        private static readonly Dictionary<String, String> editActionAttributes;
        private static readonly Dictionary<String, String> deleteActionAttributes;

        #endregion
    }
}