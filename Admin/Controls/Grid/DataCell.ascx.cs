using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Controls.Grid
{
    public partial class DataCellControl : CellControlBase
    {
        public DataInputTypes DataInputType { get; set; }

        public String Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;

                if (!Html)
                {
                    gridInputCheckbox.Value = this.value;
                    gridInputText.Value = this.value;
                    gridInputHidden.Value = this.value;
                    gridSubmit.Text = this.value;
                    
                    if (DataInputType == DataInputTypes.Select)
                    {
                        if (gridDdl.Items.Count > 0)
                        {
                            var gridDdlItem = gridDdl.Items.FindByValue(this.value);

                            if (gridDdlItem != null)
                            {
                                gridDdl.SelectedIndex = gridDdl.Items.IndexOf(gridDdlItem);
                            }
                        }
                        else
                        {
                            gridDdl.Items.Add(new System.Web.UI.WebControls.ListItem(this.value, this.value));
                        }
                    }
                    else if (DataInputType == DataInputTypes.Checkbox)
                    {
                        gridInputCheckbox.Checked = IsChecked;
                    }
                }

                ltlText.Text = this.value;
            }
        }

        public String PrevValue { get; set; }

        public Boolean Disabled
        {
            get
            {
                return disabled;
            }
            set
            {
                disabled = value;
                gridInputCheckbox.Disabled = disabled;
                gridInputText.Disabled = disabled;

                if (value)
                {
                    gridDdl.Attributes.Add("disabled", String.Empty);
                }
            }
        }

        public Boolean DisplayNoneInputs
        {
            get
            {
                return displayNoneInputs;
            }
            set
            {
                displayNoneInputs = value;

                if (displayNoneInputs)
                {
                    gridInputCheckbox.Style.Add("display", "none");
                    gridInputText.Style.Add("display", "none");
                    gridDdl.Style.Add("display", "none");
                }
                else
                {
                    gridInputCheckbox.Style.Remove("display");
                    gridInputText.Style.Remove("display");
                    gridDdl.Style.Remove("display");
                }
            }
        }

        public Boolean TextIsInvisible
        {
            get
            {
                return textIsInvisible;
            }
            set
            {
                textIsInvisible = value;
                ltlText.Visible = !textIsInvisible;
            }
        }

        public Boolean Html { get; set; }

        public Boolean DisplayAsDisabledInput { get; set; }
        
        protected Boolean IsChecked
        {
            get
            {
                return String.Compare(Value, Boolean.TrueString, true) == 0;
            }
        }

        public override HtmlTableCell TdHtmlTableCell
        {
            get
            {
                return tdCell;
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                RecoverInputState();
            }

            if (DisplayAsDisabledInput)
            {
                ltlText.Visible = false;
            }
        }

        protected void gridSubmit_Command(Object sender, CommandEventArgs e)
        {
            var rowIndex = (CellsList.Parent as RepeaterItem).ItemIndex;
            var args = new RowCommandedEventArgs(rowIndex, CellsList, sender, e);

            CellsList.Body.GridControl.OnRowCommanded(args);
        }

        public void SetDisplayAsDisabledInputMode()
        {
            Disabled = true;
            DisplayNoneInputs = false;
            TextIsInvisible = true;
        }

        public void SetDefaultDisplayMode()
        {
            if (DisplayAsDisabledInput)
            {
                SetDisplayAsDisabledInputMode();
            }
            else
            {
                SetDisplayAsTextMode();
            }
        }

        public void SetDisplayAsTextMode()
        {
            DisplayNoneInputs = true;
            TextIsInvisible = false;

            if (DataInputType == DataInputTypes.Hidden)
            {
                TextIsInvisible = true;
            }
        }

        public void SetDisplayAsInputMode()
        {
            DisplayNoneInputs = false;
            TextIsInvisible = true;
            Disabled = false;
        }

        public void BindDataCell(DataCell dc)
        {
            if (dc.Html.HasText())
            {
                Html = true;
                Value = dc.Html;
            }
            else
            {
                DataInputType = dc.InputType;
                Value = dc.Text;
                DisplayAsDisabledInput = dc.DisplayAsDisabledInput;
                SetDefaultDisplayMode();

                if (dc.TextIsInvisible.HasValue)
                {
                    TextIsInvisible = dc.TextIsInvisible.Value;
                }
            }

            if (dc.Attributes != null)
            {
                AddTdHtmlTableCellAttributes(dc.Attributes);
            }

            if (dc.HideCell)
            {
                TdHtmlTableCell.Style.Add("display", "none");
            }

            PersistInputState();
        }

        public void UpdatePrevValue()
        {
            PrevValue = Value;
            PersistInputState();
        }

        #region private

        private String value;
        private Boolean disabled;
        private Boolean displayNoneInputs;
        private Boolean textIsInvisible;

        private class DataCellControlStateModel
        {
            public DataInputTypes DataInputType { get; set; }

            public String Value { get; set; }

            public Boolean Disabled { get; set; }

            public Boolean DisplayNoneInputs { get; set; }

            public Boolean TextIsInvisible { get; set; }

            public Boolean DisplayAsDisabledInput { get; set; }

            public Boolean Html { get; set; }
        }

        private void PersistInputState()
        {
            var inputStateModel = new DataCellControlStateModel
                                        {
                                            DataInputType = DataInputType,
                                            Value = Value,
                                            Disabled = Disabled,
                                            DisplayNoneInputs = DisplayNoneInputs,
                                            TextIsInvisible = TextIsInvisible,
                                            DisplayAsDisabledInput = DisplayAsDisabledInput,
                                            Html = Html
                                        };

            hfInputState.Value = Helper.SerializeToJsonFromObject(inputStateModel);
        }

        private void RecoverInputState()
        {
            var inputStateModel = Helper.DeserializeJsonToObject<DataCellControlStateModel>(hfInputState.Value);

            if (inputStateModel != null)
            {
                Html = inputStateModel.Html;
                DataInputType = inputStateModel.DataInputType;
                PrevValue = inputStateModel.Value;
                Disabled = inputStateModel.Disabled;
                DisplayNoneInputs = inputStateModel.DisplayNoneInputs;
                TextIsInvisible = inputStateModel.TextIsInvisible;
                DisplayAsDisabledInput = inputStateModel.DisplayAsDisabledInput;

                if (DataInputType == DataInputTypes.Checkbox)
                {
                    Value = gridInputCheckbox.Checked.ToString();
                }
                else if (DataInputType == DataInputTypes.Text)
                {
                    Value = gridInputText.Value;
                }
                else if (DataInputType == DataInputTypes.Hidden)
                {
                    Value = gridInputHidden.Value;
                }
                else if (DataInputType == DataInputTypes.Select)
                {
                    Value = gridDdl.SelectedValue;
                }
            }
        }

        #endregion
    }
}