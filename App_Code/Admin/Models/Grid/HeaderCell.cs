
namespace FlyerMe.Admin.Models
{
    public sealed class HeaderCell : CellBase
    {
        public HeaderCell(ActionTypes? at = null) : base(null)
        {
            if (at.HasValue)
            {
                ActionType = at.Value;
                Attributes = at.Value.GetActionTypeAttributes();
            }
        }

        public ActionTypes ActionType { get; set; }
    }
}