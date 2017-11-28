using ConvertPDF;
using FlyerMe.Controls;
using System;
using System.IO;
using System.Web.UI.HtmlControls;

namespace FlyerMe
{
    public partial class PdfToJpg : PageBase
    {
        protected string RootURL;
        PDFConvert converter = new PDFConvert();

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("PDF To Image Converter | {0}", clsUtility.ProjectName);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;
            lblMessage.Text = "";

        }

        protected void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                string strImgPath=System.IO.Path.GetFileName(ConvertSingleImage(fileUpload));
                imgFile.Src =  RootURL + "pdf/" + strImgPath;
                aImageText.HRef = RootURL + "fullpreview.aspx?imgpath=" + strImgPath;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Cannot convert because of following error: " + ex.Message.ToString();
                imgFile.Visible = false;
                aImageText.Visible = false;
            }
        }

        private string ConvertSingleImage(HtmlInputFile filename)
        {
            try
            {
                //Setup the converter
                string strFileName = Path.GetFileName(filename.PostedFile.FileName);
                var workingDirectory = Server.MapPath("~/pdf/");
                filename.PostedFile.SaveAs(workingDirectory + strFileName);
                converter.FirstPageToConvert = Convert.ToInt32(txtPageNo.Text);
                converter.LastPageToConvert = Convert.ToInt32(txtPageNo.Text);
                converter.FitPage = false;
                //converter.JPEGQuality = (int)numQuality.Value;
                converter.JPEGQuality = 80;
                converter.OutputFormat = "jpeg";
                System.IO.FileInfo input = new FileInfo(workingDirectory + strFileName);
                string output = string.Format("{0}\\{1}{2}", input.Directory, input.Name, ".jpg");
                //If the output file exist alrady be sure to add a random name at the end until is unique!
                output = output.Replace(".pdf", "");
                while (File.Exists(output))
                {
                    output = output.Replace(".jpg", string.Format("{1}{0}", ".jpg", DateTime.Now.Ticks));
                }
                //txtArguments.Text = converter.ParametersUsed;
                if (converter.Convert(input.FullName, output) == true)
                {
                    //lblInfo.Text = string.Format("{0}:File converted!", DateTime.Now.ToShortTimeString());
                    //txtArguments.ForeColor = System.Drawing.Color.Black;
                    imgFile.Visible = true;
                    aImageText.Visible = true;
                    divMessage.Visible = true;
                }
                else
                {
                    divMessage.Visible = false;
                    imgFile.Visible = false;
                    aImageText.Visible = false;
                    lblMessage.Text = "Conversion failed.";
                    //lblInfo.Text = string.Format("{0}:File NOT converted! Check Args!", DateTime.Now.ToShortTimeString());
                    //txtArguments.ForeColor = System.Drawing.Color.Red;
                }
                return output;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Cannot convert because of following error: " + ex.Message.ToString();
                imgFile.Visible = false;
                aImageText.Visible = false;
                return "";
            }
        }
    }
}
