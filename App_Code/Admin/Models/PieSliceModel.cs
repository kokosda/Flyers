using System;

namespace FlyerMe.Admin.Models
{
    public sealed class PieSliceModel
    {
        public PieSliceModel(String caption, Single dataValue)
        {
            Caption = caption;
            DataValue = dataValue;
        }

        public Single DataValue { get; set; }

        public String Caption { get; set; }

        public override String ToString()
        {
            return Caption + " (" + DataValue.ToString() + ")";
        }
    }
}