using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe.Controls.CreateFlyer.WizardSteps
{
    public partial class Photo : CreateFlyerWizardControlBase
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
                var customerMediaDirectories = Helper.CreateCustomerMediaDirectoriesIfNeeded(Profile.UserName);
                var orderMediaDirectories = Helper.CreateOrderMediaDirectoriesIfNeeded(Flyer.OrderId.Value);
                HtmlInputControl inputFile;
                var filesKeys = Request.Files.Keys.Cast<String>().ToList();
                var random = new Random();
                var bytes = new Byte[16];

                foreach(Control control in form.Controls)
                {
                    inputFile = control as HtmlInputFile;
                    
                    if (inputFile == null)
                    {
                        var panelControl = control as Panel;

                        if (panelControl != null)
                        {
                            if (String.Compare(panelControl.ID, panelMyPhoto.ID, true) == 0)
                            {
                                inputFile = panelControl.FindControl("inputMyPhoto") as HtmlInputFile;
                            }
                            else if (String.Compare(panelControl.ID, panelMyLogo.ID, true) == 0)
                            {
                                inputFile = panelControl.FindControl("inputMyLogo") as HtmlInputFile;
                            }
                        }
                    }

                    if (inputFile != null)
                    {
                        var key = filesKeys.FirstOrDefault(fk => fk.IndexOf(inputFile.ID, StringComparison.OrdinalIgnoreCase) >= 0);

                        if (!String.IsNullOrEmpty(key))
                        {
                            filesKeys.Remove(key);

                            var file = Request.Files[key];

                            random.NextBytes(bytes);

                            var fileName = Helper.GetHexademicalString(bytes);
                            var canSaveFile = file != null && file.ContentLength > 0;
                            var imageName = inputFile.ID.Replace("input", String.Empty);
                            var canDeleteFile = Request.ParseCheckboxValue("delete" + imageName) ?? false;

                            if (String.Compare(inputFile.ID, inputMyPhoto.ID, true) == 0)
                            {
                                if (customerMediaDirectories.Count > 1)
                                {
                                    Helper.HandleCustomerFileImage(fileName, customerMediaDirectories[0], customerMediaDirectories[1], canSaveFile, canDeleteFile, file.InputStream, Profile);
                                }
                            }
                            else if (String.Compare(inputFile.ID, inputMyLogo.ID, true) == 0)
                            {
                                if (customerMediaDirectories.Count > 3)
                                {
                                    Helper.HandleCustomerFileImage(fileName, customerMediaDirectories[2], customerMediaDirectories[3], canSaveFile, canDeleteFile, file.InputStream, Profile);
                                }
                            }
                            else if (inputFile.ID.IndexOf("inputPhoto", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                var number = Int32.Parse(imageName.Replace("Photo", String.Empty));

                                if (orderMediaDirectories.Count > number - 1)
                                {
                                    fileName += "_" + imageName;

                                    Helper.HandleFlyersPhoto(fileName, orderMediaDirectories[number - 1], canSaveFile, canDeleteFile, file.InputStream, imageName, number);
                                }
                            }
                        }
                    }
                }

                Flyer.PhotoStepCompleted = true;
            }
        }

        #region private

        private void SetInputs()
        {
            if (!String.IsNullOrEmpty(Profile.ImageURL))
            {
                imageMyPhoto.ImageUrl = Profile.ImageURL;
                panelMyPhoto.CssClass += " change";
            }
            else
            {
                imageMyPhoto.Visible = false;
            }

            if (!(String.IsNullOrEmpty(Profile.LogoURL)))
            {
                imageMyLogo.ImageUrl = Profile.LogoURL;
                panelMyLogo.CssClass += " change";
            }
            else
            {
                imageMyLogo.Visible = false;
            }

            SetInputsOfFlyersPhotos();
        }

        private void SetInputsOfFlyersPhotos()
        {
            Image imagePhoto;
            Int32 number;

            for (var i = 0; i < Flyer.Photos.Length; i++)
            {
                number = i + 1;
                imagePhoto = form.FindControl("imagePhoto" + number.ToString()) as Image;

                if (imagePhoto != null)
                {
                    var photoFileName = Flyer.Photos[i];

                    if (!String.IsNullOrEmpty(photoFileName))
                    {
                        imagePhoto.ImageUrl = Helper.GetPhotoPath(Flyer.OrderId.Value, number, photoFileName);

                        continue;
                    }

                    imagePhoto.Visible = false;
                }
            }
        }

        #endregion
    }
}
