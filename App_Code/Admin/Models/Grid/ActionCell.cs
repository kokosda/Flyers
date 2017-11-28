using System;

namespace FlyerMe.Admin.Models
{
    public sealed class ActionCell : CellBase
    {
        public ActionCell(RowBase row, ActionTypes? at = null) : base(row)
        {
            if (at.HasValue)
            {
                ActionType = at.Value;
                Attributes = at.Value.GetActionTypeAttributes();

                if (at == ActionTypes.Edit)
                {
                    Text = "Edit";
                }
            }
        }

        public ActionTypes ActionType { get; set; }
    }
}