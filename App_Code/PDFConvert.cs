using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

namespace ConvertPDF
{
    /// <summary>
    /// Create by : TaGoH
    /// URL of the last version: http://www.codeproject.com/KB/cs/GhostScriptUseWithCSharp.aspx
    /// Description:
    /// Class to convert a pdf to an image using GhostScript DLL
    /// A big Credit for this code go to:Rangel Avulso
    /// I mainly create a better interface and refactor it to made it ready to use!
    /// </summary>
    /// <see cref="http://www.codeproject.com/KB/cs/GhostScriptUseWithCSharp.aspx"/>
    /// <seealso cref="http://www.hrangel.com.br/index.php/2006/12/04/converter-pdf-para-imagem-jpeg-em-c/"/>
    public class PDFConvert
    {
        #region GhostScript Import

        /// <summary>Create a new instance of Ghostscript. This instance is passed to most other gsapi functions. The caller_handle will be provided to callback functions.
        ///  At this stage, Ghostscript supports only one instance. </summary>
        /// <param name="pinstance"></param>
        /// <param name="caller_handle"></param>
        /// <returns></returns>
        [DllImport(@"C:\Projects\DLLs\gsdll64.dll", EntryPoint = "gsapi_new_instance")]
        private static extern int gsapi_new_instance (out IntPtr pinstance, IntPtr caller_handle);

        /// <summary>This is the important function that will perform the conversion</summary>
        /// <param name="instance"></param>
        /// <param name="argc"></param>
        /// <param name="argv"></param>
        /// <returns></returns>
        [DllImport(@"C:\Projects\DLLs\gsdll64.dll", EntryPoint = "gsapi_init_with_args")]
        private static extern int gsapi_init_with_args (IntPtr instance, int argc, IntPtr argv);
        /// <summary>
        /// Exit the interpreter. This must be called on shutdown if gsapi_init_with_args() has been called, and just before gsapi_delete_instance(). 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        [DllImport(@"C:\Projects\DLLs\gsdll64.dll", EntryPoint = "gsapi_exit")]
        private static extern int gsapi_exit (IntPtr instance);

        /// <summary>
        /// Destroy an instance of Ghostscript. Before you call this, Ghostscript must have finished. If Ghostscript has been initialised, you must call gsapi_exit before gsapi_delete_instance. 
        /// </summary>
        /// <param name="instance"></param>
        [DllImport(@"C:\Projects\DLLs\gsdll64.dll", EntryPoint = "gsapi_delete_instance")]
        private static extern void gsapi_delete_instance (IntPtr instance);

        #endregion
        #region Const
        const int e_Quit=-101;
        const int e_NeedInput = -106;
        #endregion
        #region Variables
        private string _sDeviceFormat;
        private string _sParametersUsed;

        private int _iWidth;
        private int _iHeight;
        private int _iResolutionX;
        private int _iResolutionY;
        private int _iJPEGQuality;
        /// <summary>The first page to convert in image</summary>
        private int _iFirstPageToConvert = -1;
        /// <summary>The last page to conver in an image</summary>
        private int _iLastPageToConvert = -1;
        private Boolean _bFitPage;
        private Boolean _bThrowOnlyException = false;        
        private IntPtr _objHandle;
        #endregion
        #region Proprieties
        /// <summary>
        /// What format to use to convert
        /// is suggested to use png256 instead of jpeg for document!
        /// they are smaller and better suited!
        /// </summary>
        /// <see cref="http://pages.cs.wisc.edu/~ghost/doc/cvs/Devices.htm"/>
        public string OutputFormat
        {
            get { return _sDeviceFormat; }
            set { _sDeviceFormat = value; }
        }

        public string ParametersUsed
        {
            get { return _sParametersUsed; }
            set { _sParametersUsed = value; }
        }

        public int Width
        {
            get { return _iWidth; }
            set { _iWidth = value; }
        }

        public int Height
        {
            get { return _iHeight; }
            set { _iHeight = value; }
        }

        public int ResolutionX
        {
            get { return _iResolutionX; }
            set { _iResolutionX = value; }
        }

        public int ResolutionY
        {
            get { return _iResolutionY; }
            set { _iResolutionY = value; }
        }

        public Boolean FitPage
        {
            get { return _bFitPage; }
            set { _bFitPage = value; }
        }
        /// <summary>Quality of compression of JPG</summary>
        public int JPEGQuality
        {
            get { return _iJPEGQuality; }
            set { _iJPEGQuality = value; }
        }
        /// <summary>The first page to convert in image</summary>
        public int FirstPageToConvert
        {
            get { return _iFirstPageToConvert; }
            set { _iFirstPageToConvert = value; }
        }
        /// <summary>The last page to conver in an image</summary>
        public int LastPageToConvert
        {
            get { return _iLastPageToConvert; }
            set { _iLastPageToConvert = value; }
        }
        /// <summary>Set to True if u want the program to never display Messagebox
        /// but otherwise throw exception</summary>
        public Boolean ThrowOnlyException
        {
            get { return _bThrowOnlyException; }
            set { _bThrowOnlyException = value; }
        }
        #endregion
        #region Init
        public PDFConvert(IntPtr objHandle)
        {
            _objHandle = objHandle;
        }

        public PDFConvert()
        {
            _objHandle = IntPtr.Zero;
        }
        #endregion

        /// <summary>Convert a single file!</summary>
        /// <param name="inputFile">The file PDf to convert</param>
        /// <param name="outputFile">The image file that will be created</param>
        /// <remarks>You must pass all the parameter for the conversion
        /// as Proprieties of this class</remarks>
        /// <returns>True if the conversion succed!</returns>
        public bool Convert(string inputFile, string outputFile)
        {
            return Convert(inputFile, outputFile, _bThrowOnlyException);
        }

        /// <summary>Convert a single file!</summary>
        /// <param name="inputFile">The file PDf to convert</param>
        /// <param name="outputFile">The image file that will be created</param>
        /// <param name="throwException">if the function should throw an exception
        /// or display a message box</param>
        /// <remarks>You must pass all the parameter for the conversion
        /// as Proprieties of this class</remarks>
        /// <returns>True if the conversion succed!</returns>
        public bool Convert(string inputFile, string outputFile,bool throwException)
        {
            #region Variables
            int intReturn, intCounter, intElementCount;
            //The pointer to the current istance of the dll
            IntPtr intGSInstanceHandle;
            object[] aAnsiArgs;
            IntPtr[] aPtrArgs;
            GCHandle[] aGCHandle;
            IntPtr callerHandle, intptrArgs;
            GCHandle gchandleArgs;
            #endregion
            //Generate the list of the parameters i need to pass to the dll
            string[] sArgs = GetGeneratedArgs(inputFile, outputFile);
            #region Convert Unicode strings to null terminated ANSI byte arrays
            // Convert the Unicode strings to null terminated ANSI byte arrays
            // then get pointers to the byte arrays.
            intElementCount = sArgs.Length;
            aAnsiArgs = new object[intElementCount];
            aPtrArgs = new IntPtr[intElementCount];
            aGCHandle = new GCHandle[intElementCount];
            //Convert the parameters
            for (intCounter = 0; intCounter < intElementCount; intCounter++)
            {
                aAnsiArgs[intCounter] = StringToAnsiZ(sArgs[intCounter]);
                aGCHandle[intCounter] = GCHandle.Alloc(aAnsiArgs[intCounter], GCHandleType.Pinned);
                aPtrArgs[intCounter] = aGCHandle[intCounter].AddrOfPinnedObject();
            }
            gchandleArgs = GCHandle.Alloc(aPtrArgs, GCHandleType.Pinned);
            intptrArgs = gchandleArgs.AddrOfPinnedObject();
            #endregion
            #region Create a new istance of the library!
            intReturn = -1;
            try
            {
                intReturn = gsapi_new_instance(out intGSInstanceHandle, _objHandle);
                //Be sure that we create an istance!
                if (intReturn < 0)
                {
                    for (intCounter = 0; intCounter < intReturn; intCounter++)
                        aGCHandle[intCounter].Free();
                    gchandleArgs.Free();
                    if (throwException)
                        throw new ApplicationException("I can't create a new istance of Ghostscript please verify no other istance are running!");
                    else
                    {
                        return false;
                    }
                }
            }
            catch (DllNotFoundException)
            {//in this case the dll we r using isn't the dll we expect
                for (intCounter = 0; intCounter < intReturn; intCounter++)
                    aGCHandle[intCounter].Free();
                gchandleArgs.Free();
                if (throwException)
                    throw new ApplicationException("The gs32dll.dll in the program directory doesn't "+
                        "expose the methods i need!\nplease download the version 8.63 from the original website!");
                else
                {
                    //System.Windows.Forms.MessageBox.Show("The gs32dll.dll in the program directory"+
                    //    " doesn't expose the methods i need!\nPlease download the version 8.63 "+
                    //    "from the original website!");
                    return false;
                }
            }
            callerHandle = IntPtr.Zero;//remove unwanter handler
            #endregion
            intReturn = -1;//if nothing change it is an error!
            //Ok now is time to call the interesting method
            try { intReturn = gsapi_init_with_args(intGSInstanceHandle, intElementCount, intptrArgs); }
            catch (Exception ex)
            {
                if (throwException)
                    throw new ApplicationException(ex.Message, ex);
                else
                    return false;
            }
            finally//No matter what happen i MUST close the istance!
            {   //free all the memory
                for (intCounter = 0; intCounter < intReturn; intCounter++)
                {
                    aGCHandle[intCounter].Free();
                }
                gchandleArgs.Free();
                gsapi_exit(intGSInstanceHandle);//Close the istance
                gsapi_delete_instance(intGSInstanceHandle);//delete it
            }
            //Conversion was successfull if return code was 0 or e_Quit
            return (intReturn == 0) | (intReturn == e_Quit);//e_Quit = -101
        }
        #region Accessory Functions
        /// <summary>This function create the list of parameters to pass to the dll</summary>
        /// <param name="inputFile">the file to convert</param>
        /// <param name="outputFile">where to write the image</param>
        /// <returns>the list of the arguments</returns>
        private string[] GetGeneratedArgs(string inputFile, string outputFile)
        {
            // Count how many extra args are need - HRangel - 11/29/2006, 3:13:43 PM
            ArrayList lstExtraArgs = new ArrayList();

            if ( _sDeviceFormat=="jpeg" && _iJPEGQuality > 0 && _iJPEGQuality < 101)
                lstExtraArgs.Add("-dJPEGQ=" + _iJPEGQuality);

            if (_iWidth > 0 && _iHeight > 0)
                lstExtraArgs.Add("-g" + _iWidth + "x" + _iHeight);

            if (_bFitPage)
                lstExtraArgs.Add("-dPDFFitPage");

            _iResolutionX = 120;
            if (_iResolutionX > 0)
            {
                if (_iResolutionY > 0)
                    lstExtraArgs.Add("-r" + _iResolutionX + "x" + _iResolutionY);
                else
                    lstExtraArgs.Add("-r" + _iResolutionX);
            }

            if (_iFirstPageToConvert > 0)
                lstExtraArgs.Add("-dFirstPage=" + _iFirstPageToConvert);

            if (_iLastPageToConvert > 0)
                lstExtraArgs.Add("-dLastPage=" + _iLastPageToConvert);
            int iFixedCount = 7;
            int iExtraArgsCount = lstExtraArgs.Count;
            string[] args = new string[iFixedCount + lstExtraArgs.Count];
            args[0]="pdf2img";//this parameter have little real use
            args[1]="-dNOPAUSE";//I don't want interruptions
            args[2]="-dBATCH";//stop after
            args[3]="-dSAFER";
            args[4]="-sDEVICE="+_sDeviceFormat;//what kind of export format i should provide
            //For a complete list watch here:
            //http://pages.cs.wisc.edu/~ghost/doc/cvs/Devices.htm
            //Fill the remaining parameters
            for (int i=0; i < iExtraArgsCount; i++)
            {
                args[5+i] = (string) lstExtraArgs[i];
            }
            //Fill outputfile and inputfile
            args[5 + iExtraArgsCount] = string.Format("-sOutputFile={0}",outputFile);
            args[6 + iExtraArgsCount] = string.Format("{0}",inputFile);
            //Ok now save them to be shown 4 debug use
            _sParametersUsed = "";
            foreach (string arg in args)
                _sParametersUsed += " " + arg;
            return args;
        }

        /// <summary>
        /// Convert a Unicode string to a null terminated Ansi string for Ghostscript.
        /// The result is stored in a byte array
        /// </summary>
        /// <param name="str">The parameter i want to convert</param>
        /// <returns>the byte array that contain the string</returns>
        private byte[] StringToAnsiZ(string str)
        {
            // Later you will need to convert
            // this byte array to a pointer with
            // GCHandle.Alloc(XXXX, GCHandleType.Pinned)
            // and GSHandle.AddrOfPinnedObject()
            int intElementCount,intCounter;
            byte[] aAnsi;
            byte bChar;

            intElementCount = str.Length;
            aAnsi = new byte[intElementCount + 1];
            for (intCounter = 0; intCounter < intElementCount; intCounter++)
            {
                bChar = (byte)str[intCounter];
                aAnsi[intCounter] = bChar;
            }

            aAnsi[intElementCount] = 0;
            return aAnsi;
        }
        #endregion
    }

}
