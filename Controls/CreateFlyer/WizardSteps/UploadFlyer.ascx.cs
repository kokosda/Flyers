using System;
using System.Drawing.Imaging;
using System.Linq;

namespace FlyerMe.Controls.CreateFlyer.WizardSteps
{
    public partial class UploadFlyer : CreateFlyerWizardControlBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            InitFields(Profile);
            SetInputs();
            form.Action = FormAction;
        }

        public override void SetSessionData(Boolean isInitial)
        {
            InitFields(Profile);

            if (!isInitial)
            {
                SetDeliveryDate();
                Flyer.EmailSubject = Request[GetKey("emailsubject")];
                Flyer.Link = Request[GetKey("link")];
                SetFile();
                Flyer.Markup = Helper.GetFlyerMarkup(Flyer.Layout);
                Flyer.UploadFlyerStepCompleted = true;
            }
        }

        #region private

        private void SetInputs()
        {
            inputDeliveryDate.Value = Flyer.DeliveryDate.HasValue ? Flyer.DeliveryDate.Value.FormatDate() : DateTime.Now.Date.FormatDate();
            inputEmailSubject.Value = Flyer.EmailSubject;
            inputLink.Value = Flyer.Link;
            inputFile.Attributes.Add("value", Flyer.Photos[0]);
            inputNext.Disabled = (!Flyer.DeliveryDate.HasValue) || String.IsNullOrEmpty(Flyer.EmailSubject) || String.IsNullOrEmpty(Flyer.Photos[0]);
        }

        private void SetDeliveryDate()
        {
            DateTime deliveryDate;

            if (DateTime.TryParse(Request[GetKey("deliverydate")], out deliveryDate))
            {
                Flyer.DeliveryDate = deliveryDate;
            }
            else
            {
                throw new Exception("Date format is incorrect.");
            }
        }

        private String GetKey(String keyFriendlyName)
        {
            var result = Request.Form.AllKeys.FirstOrDefault(k => k.IndexOf(keyFriendlyName, StringComparison.OrdinalIgnoreCase) >= 0);

            if (String.IsNullOrEmpty(result))
            {
                throw new Exception(keyFriendlyName + " is not supplied.");
            }

            return result;
        }

        private void SetFile()
        {
            var orderMediaDirectories = Helper.CreateOrderMediaDirectoriesIfNeeded(Flyer.OrderId.Value);

            if (Request.Files.Count == 0 || Request.Files[0].ContentLength == 0)
            {
                if (String.IsNullOrEmpty(Flyer.Photos[0]))
                {
                    Helper.SetErrorResponse(System.Net.HttpStatusCode.BadRequest, "File is requried.");
                }
                else
                {
                    return;
                }
            }

            var key = Request.Files.Keys[0];
            var random = new Random();
            var bytes = new Byte[16];
            var file = Request.Files[key];
            var imageName = "Photo1";

            random.NextBytes(bytes);

            var fileName = Helper.GetHexademicalString(bytes) + "_" + imageName;

            Helper.HandleFlyersPhoto(fileName, orderMediaDirectories[0], true, false, file.InputStream, imageName, 1);
        }

        #endregion
    }
}
