using FlyerMe.Admin.Controls;
using System;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Users
{
    public partial class WebsiteUserNote : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            SetInputs();
        }

        protected void save_Command(Object sender, CommandEventArgs e)
        {
            var userId = Request["userid"];

            if (userId.HasNoText())
            {
                message.MessageText = "Provide User ID to save user note.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else
            {
                try
                {
                    var result = new clsAdmin().SaveUserNote(userId, textareaNote.Value);

                    if (result > 0)
                    {
                        message.MessageText = "Note has been saved successfully!";
                        message.MessageClass = MessageClassesEnum.Ok;
                    }
                    else
                    {
                        message.MessageText = "Note has not been saved! Possible reason: User ID (" + userId + ") was not found.";
                        message.MessageClass = MessageClassesEnum.System;
                    }
                }
                catch (Exception ex)
                {
                    message.MessageText = ex.Message;
                    message.MessageClass = MessageClassesEnum.Error;
                }
            }

            message.RedirectToShowMessage();
        }

        #region private

        private void SetInputs()
        {
            if (!IsPostBack)
            {
                if (Request["userid"].HasText())
                {
                    var dt = new clsAdmin().GetUserNote(Request["userid"]);

                    if (dt.Rows.Count > 0)
                    {
                        textareaNote.Value = dt.Rows[0]["Note"] as String;
                    }
                }
                else
                {
                    message.RedirectToShowMessage("Provide User ID to view user note.", MessageClassesEnum.System);
                }
            }
        }

        #endregion
    }
}
