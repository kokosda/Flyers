using FlyerMe.Admin.Controls;
using System;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin
{
    public partial class AlterUsername : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Command(Object sender, CommandEventArgs e)
        {
            if (inputOldUsername.Value.HasNoText())
            {
                message.MessageText = "Old Username is required.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else if (inputNewUsername.Value.HasNoText())
            {
                message.MessageText = "New Username is required.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else if (inputConfirmUsername.Value.HasNoText())
            {
                message.MessageText = "Confirm Username is required.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else if (String.Compare(inputNewUsername.Value, inputConfirmUsername.Value, false) != 0)
            {
                message.MessageText = "New and Confirm Usernames must match.";
                message.MessageClass = MessageClassesEnum.System;
            }

            if (message.MessageText.HasNoText())
            {
                try
                {
                    using (var myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ConnectionString))
                    {
                        var updateString = @"UPDATE aspnet_Users SET username= @NewUserName, loweredusername= @NewUserName 
                                             where  username= @OldUserName 
                                             UPDATE aspnet_Membership set email= @NewUserName, LoweredEmail= @NewUserName Where email= @OldUserName 
                                             UPDATE fly_order SET customer_id= @NewUserName WHERE customer_id= @OldUserName";
                        using (var cmd = new SqlCommand(updateString, myConnection))
                        {
                            var param = new SqlParameter();
                            param.ParameterName = "@NewUserName";
                            param.Value = inputNewUsername.Value.ToLower();
                            cmd.Parameters.Add(param);

                            var oldparam = new SqlParameter();
                            oldparam.ParameterName = "@OldUserName";
                            oldparam.Value = inputOldUsername.Value;
                            cmd.Parameters.Add(oldparam);

                            myConnection.Open();
                            cmd.ExecuteNonQuery();
                        }

                        message.MessageText = inputNewUsername.Value + " has been altered successfully.";
                        message.MessageClass = MessageClassesEnum.Ok;
                    }
                }
                catch (Exception ex)
                {
                    message.MessageText = ex.Message;
                    message.MessageClass = MessageClassesEnum.Error;
                }
            }

            if (message.MessageClass == MessageClassesEnum.Ok)
            {
                message.RedirectToShowMessage();
            }
            else
            {
                message.ShowMessage();
            }
        }
    }
}
