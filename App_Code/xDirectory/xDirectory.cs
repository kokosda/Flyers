using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace FlyerMe
{

    /// <summary>
    /// xDirectory - Copy Files and SubFolders.
    /// </summary>
    [Serializable]
    public class xDirectory
    {


        #region Private Members
        /// <summary>
        /// The Source Directory.
        /// </summary>
        private DirectoryInfo _Source = null;

        /// <summary>
        /// The Destination Directory.
        /// </summary>
        private DirectoryInfo _Destination = null;

        /// <summary>
        /// The File Filters for "GetFiles".
        /// </summary>
        private List<string> _FileFilters = new List<string>();

        /// <summary>
        /// The Folder Filter for "GetDirectories".
        /// </summary>
        private string _FolderFilter = null;
        
        /// <summary>
        /// The Overwrite Setting: Whether or not to overwrite on copy.
        /// </summary>
        private bool _Overwrite = false;

        /// <summary>
        /// The Status of the xDirectory "StartCopy" Object.
        /// </summary>
        private static xDirectoryStatus _CopierStatus = xDirectoryStatus.Stopped;

        /// <summary>
        /// The "Cancel" Setting. Set to true to Cancel the xDirectory.StartCopy Method.
        /// </summary>
        private static bool _CancelCopy = false;

        #endregion



        #region Constructor

        /// <summary>
        /// The Default Constructor for the xDirectory Object
        /// </summary>
        public xDirectory()
        { }

        #endregion



        #region Event Related Methods

        /// <summary>
        /// The 'Item Indexed' Event
        /// </summary>
        public event ItemIndexedEventHandler ItemIndexed;

        /// <summary>
        /// The 'On Item Indexed' Event Handler: Called when an Item (File or Folder) Is Indexed
        /// </summary>
        /// <param name="e">The 'Item Indexed' Event Arguments.</param>
        protected virtual void OnItemIndexed(ItemIndexedEventArgs e)
        {
            ItemIndexedEventHandler Handler = ItemIndexed;
            if (Handler != null)
            {
                foreach (ItemIndexedEventHandler Caster in Handler.GetInvocationList())
                {
                    ISynchronizeInvoke SyncInvoke = Caster.Target as ISynchronizeInvoke;
                    try
                    {
                        if (SyncInvoke != null && SyncInvoke.InvokeRequired)
                            SyncInvoke.Invoke(Handler, new object[] { this, e });
                        else
                            Caster(this, e);
                    }
                    catch
                    { }
                }
            }
        }

        /// <summary>
        /// The 'Index Complete' Event
        /// </summary>
        public event IndexCompleteEventHandler IndexComplete;

        /// <summary>
        /// The 'On Index Complete' Event Handler: Called when Indexing of the Source Folder is Complete.
        /// </summary>
        /// <param name="e">The 'Index Complete' Event Arguments.</param>
        protected virtual void OnIndexComplete(IndexCompleteEventArgs e)
        {
            IndexCompleteEventHandler Handler = IndexComplete;
            if (Handler != null)
            {
                foreach (IndexCompleteEventHandler Caster in Handler.GetInvocationList())
                {
                    ISynchronizeInvoke SyncInvoke = Caster.Target as ISynchronizeInvoke;
                    try
                    {
                        if (SyncInvoke != null && SyncInvoke.InvokeRequired)
                            SyncInvoke.Invoke(Handler, new object[] { this, e });
                        else
                            Caster(this, e);
                    }
                    catch
                    { }
                }
            }
        }

        /// <summary>
        /// The 'Item Copied' Event
        /// </summary>
        public event ItemCopiedEventHandler ItemCopied;

        /// <summary>
        /// The 'On Item Copied' Event Handler: Called when an Item (File or Folder) Is Copied from the Source to Destination
        /// </summary>
        /// <param name="e">The 'Item Copied' Event Arguments.</param>
        protected virtual void OnItemCopied(ItemCopiedEventArgs e)
        {
            ItemCopiedEventHandler Handler = ItemCopied;
            if (Handler != null)
            {
                foreach (ItemCopiedEventHandler Caster in Handler.GetInvocationList())
                {
                    ISynchronizeInvoke SyncInvoke = Caster.Target as ISynchronizeInvoke;
                    try
                    {
                        if (SyncInvoke != null && SyncInvoke.InvokeRequired)
                            SyncInvoke.Invoke(Handler, new object[] { this, e });
                        else
                            Caster(this, e);
                    }
                    catch
                    { }
                }
            }
        }

        /// <summary>
        /// The 'Copy Complete' Event
        /// </summary>
        public event CopyCompleteEventHandler CopyComplete;

        /// <summary>
        /// The 'On Copy Complete' Event Handler: Called when Copying of the Source Folder to Destination Folder is Complete.
        /// </summary>
        /// <param name="e">The 'Item Copied' Event Arguments.</param>
        protected virtual void OnCopyComplete(CopyCompleteEventArgs e)
        {
            CopyCompleteEventHandler Handler = CopyComplete;
            if (Handler != null)
            {
                foreach (CopyCompleteEventHandler Caster in Handler.GetInvocationList())
                {
                    ISynchronizeInvoke SyncInvoke = Caster.Target as ISynchronizeInvoke;
                    try
                    {
                        if (SyncInvoke != null && SyncInvoke.InvokeRequired)
                            SyncInvoke.Invoke(Handler, new object[] { this, e });
                        else
                            Caster(this, e);
                    }
                    catch
                    { }
                }
            }
        }

        /// <summary>
        /// The 'Copy Error' Event
        /// </summary>
        public event CopyErrorEventHandler CopyError;

        /// <summary>
        /// The 'On Copy Error' Event Handler: Called when an Attempted Copy of a file Fails.
        /// </summary>
        /// <param name="e">The 'Copy Error' Event Arguments.</param>
        protected virtual void OnCopyError(CopyErrorEventArgs e)
        {
            CopyErrorEventHandler Handler = CopyError;
            if (Handler != null)
            {
                foreach (CopyErrorEventHandler Caster in Handler.GetInvocationList())
                {
                    ISynchronizeInvoke SyncInvoke = Caster.Target as ISynchronizeInvoke;
                    try
                    {
                        if (SyncInvoke != null && SyncInvoke.InvokeRequired)
                            SyncInvoke.Invoke(Handler, new object[] { this, e });
                        else
                            Caster(this, e);
                    }
                    catch
                    { }
                }
            }
        }

        #endregion



        #region Public Properties

        /// <summary>
        /// The Source Directory.
        /// </summary>
        public DirectoryInfo Source
        {
            get { return _Source; }
            set
            {
                if (_CopierStatus != xDirectoryStatus.Stopped)
                    throw new Exception("Attempt to Set Property 'Source' While AsyncCopier Status != Stopped");
                _Source = value; 
            }
        }

        /// <summary>
        /// The Destination Directory.
        /// </summary>
        public DirectoryInfo Destination
        {
            get { return _Destination; }
            set
            {
                if (_CopierStatus != xDirectoryStatus.Stopped)
                    throw new Exception("Attempt to Set Property 'Destinatiton' While AsyncCopier Status != Stopped");
                _Destination = value;
            }
        }

        /// <summary>
        /// The File Filters for "GetFiles".
        /// </summary>
        public List<string> FileFilters
        {
            get { return _FileFilters; }
            set
            {
                if (_CopierStatus != xDirectoryStatus.Stopped)
                    throw new Exception("Attempt to Set Property 'FileFilter' While AsyncCopier Status != Stopped");
                _FileFilters = value;
            }
        }

        /// <summary>
        /// The Folder Filter for "GetDirectories".
        /// </summary>
        public string FolderFilter
        {
            get { return _FolderFilter; }
            set
            {
                if (_CopierStatus != xDirectoryStatus.Stopped)
                    throw new Exception("Attempt to Set Property 'FolderFilter' While AsyncCopier Status != Stopped");
                _FolderFilter = value;
            }
        }

        /// <summary>
        /// The Overwrite Setting: Whether or not to overwrite on copy.
        /// </summary>
        public bool Overwrite
        {
            get { return _Overwrite; }
            set
            {
                if (_CopierStatus != xDirectoryStatus.Stopped)
                    throw new Exception("Attempt to Set Property 'Overwrite' While AsyncCopier Status != Stopped");
                _Overwrite = value;
            }
        }

        /// <summary>
        /// The Status of the xDirectory.StartCopy Method
        /// </summary>
        public xDirectoryStatus Status
        {
            get { return _CopierStatus; }
        }

        #endregion



        #region Public Methods

        /// <summary>
        /// The 'Start Copy' Method: Async Call to Start the Copy Process.
        /// </summary>
        public void StartCopy()
        {
            try
            {
                if (_CopierStatus != xDirectoryStatus.Stopped)
                    throw new Exception("Attempt to call StartCopy Failed - Status is not 'Stopped'");

                _CancelCopy = false;

                // Error Checking
                ///////////////////////////////////////////////////////

                if (_Source == null)
                    throw new ArgumentException("Source Directory: NULL");

                if (_Destination == null)
                    throw new ArgumentException("Destination Directory: NULL");

                if (!_Source.Exists)
                    throw new IOException("Source Directory: Does Not Exist");

                if (string.IsNullOrEmpty(_FolderFilter))
                    _FolderFilter = "*";

                if (List<string>.ReferenceEquals(_FileFilters, null))
                    _FileFilters = new List<string>();

                if (_FileFilters.Count == 0)
                    _FileFilters.Add("*");


                // This can be changed to suit the way the programmer wishes the thread to be created/used.
                ///////////////////////////////////////////////////////

                ThreadPool.QueueUserWorkItem(new WaitCallback(DoWork));
            }
            catch
            { throw; }
        }


        // DirectoryInfo Source/Directory Method Versions
        ///////////////////////////////////////////////////////

        /// <summary>
        /// The 'Start Copy' Method: Async Call to Start the Copy Process.
        /// </summary>
        /// <param name="Source">The Source Directory.</param>
        /// <param name="Destination">The Destination Directory.</param>
        public void StartCopy(DirectoryInfo Source, DirectoryInfo Destination)
        {
            try
            {
                _Source = Source;
                _Destination = Destination;

                StartCopy();
            }
            catch
            { throw; }
        }

        /// <summary>
        /// The 'Start Copy' Method: Async Call to Start the Copy Process.
        /// </summary>
        /// <param name="Source">The Source Directory.</param>
        /// <param name="Destination">The Destination Directory.</param>
        /// <param name="Overwrite">Whether or not to force Overwrite on Destination.</param>
        public void StartCopy(DirectoryInfo Source, DirectoryInfo Destination, bool Overwrite)
        {
            try
            {
                _Source = Source;
                _Destination = Destination;
                _Overwrite = Overwrite;

                StartCopy();
            }
            catch
            { throw; }
        }

        /// <summary>
        /// The 'Start Copy' Method: Async Call to Start the Copy Process.
        /// </summary>
        /// <param name="Source">The Source Directory.</param>
        /// <param name="Destination">The Destination Directory.</param>
        /// <param name="FileFilters">The File Filters.</param>
        public void StartCopy(DirectoryInfo Source, DirectoryInfo Destination, List<string> FileFilters)
        {
            try
            {
                _Source = Source;
                _Destination = Destination;
                _FileFilters = FileFilters;

                StartCopy();
            }
            catch
            { throw; }
        }

        /// <summary>
        /// The 'Start Copy' Method: Async Call to Start the Copy Process.
        /// </summary>
        /// <param name="Source">The Source Directory.</param>
        /// <param name="Destination">The Destination Directory.</param>
        /// <param name="FileFilters">The File Filters.</param>
        /// <param name="Overwrite">Whether or not to force Overwrite on Destination.</param>
        public void StartCopy(DirectoryInfo Source, DirectoryInfo Destination, List<string> FileFilters, bool Overwrite)
        {
            try
            {
                _Source = Source;
                _Destination = Destination;
                _FileFilters = FileFilters;
                _Overwrite = Overwrite;

                StartCopy();
            }
            catch
            { throw; }
        }

        /// <summary>
        /// The 'Start Copy' Method: Async Call to Start the Copy Process.
        /// </summary>
        /// <param name="Source">The Source Directory.</param>
        /// <param name="Destination">The Destination Directory.</param>
        /// <param name="FileFilters">The File Filters.</param>
        /// <param name="FolderFilter">The Folder Filter.</param>
        public void StartCopy(DirectoryInfo Source, DirectoryInfo Destination, List<string> FileFilters, string FolderFilter)
        {
            try
            {
                _Source = Source;
                _Destination = Destination;
                _FileFilters = FileFilters;
                _FolderFilter = FolderFilter;

                StartCopy();
            }
            catch
            { throw; }
        }

        /// <summary>
        /// The 'Start Copy' Method: Async Call to Start the Copy Process.
        /// </summary>
        /// <param name="Source">The Source Directory.</param>
        /// <param name="Destination">The Destination Directory.</param>
        /// <param name="FileFilter">The File Filters.</param>
        /// <param name="FolderFilter">The Folder Filter.</param>
        /// <param name="Overwrite">Whether or not to force Overwrite on Destination.</param>
        public void StartCopy(DirectoryInfo Source, DirectoryInfo Destination, List<string> FileFilters, string FolderFilter, bool Overwrite)
        {
            try
            {
                _Source = Source;
                _Destination = Destination;
                _FileFilters = FileFilters;
                _FolderFilter = FolderFilter;
                _Overwrite = Overwrite;

                StartCopy();
            }
            catch
            { throw; }
        }


        // String Source/Directory Method Versions
        ///////////////////////////////////////////////////////

        /// <summary>
        /// The 'Start Copy' Method: Async Call to Start the Copy Process.
        /// </summary>
        /// <param name="Source">The Source Directory.</param>
        /// <param name="Destination">The Destination Directory.</param>
        public void StartCopy(string Source, string Destination)
        {
            try
            {
                _Source = new DirectoryInfo(Source);
                _Destination = new DirectoryInfo(Destination);

                StartCopy();
            }
            catch
            { throw; }
        }

        /// <summary>
        /// The 'Start Copy' Method: Async Call to Start the Copy Process.
        /// </summary>
        /// <param name="Source">The Source Directory.</param>
        /// <param name="Destination">The Destination Directory.</param>
        /// <param name="Overwrite">Whether or not to force Overwrite on Destination.</param>
        public void StartCopy(string Source, string Destination, bool Overwrite)
        {
            try
            {
                _Source = new DirectoryInfo(Source);
                _Destination = new DirectoryInfo(Destination);
                _Overwrite = Overwrite;

                StartCopy();
            }
            catch
            { throw; }
        }

        /// <summary>
        /// The 'Start Copy' Method: Async Call to Start the Copy Process.
        /// </summary>
        /// <param name="Source">The Source Directory.</param>
        /// <param name="Destination">The Destination Directory.</param>
        /// <param name="FileFilter">The File Filter.</param>
        public void StartCopy(string Source, string Destination, List<string> FileFilters)
        {
            try
            {
                _Source = new DirectoryInfo(Source);
                _Destination = new DirectoryInfo(Destination);
                _FileFilters = FileFilters;

                StartCopy();
            }
            catch
            { throw; }
        }

        /// <summary>
        /// The 'Start Copy' Method: Async Call to Start the Copy Process.
        /// </summary>
        /// <param name="Source">The Source Directory.</param>
        /// <param name="Destination">The Destination Directory.</param>
        /// <param name="FileFilter">The File Filter.</param>
        /// <param name="Overwrite">Whether or not to force Overwrite on Destination.</param>
        public void StartCopy(string Source, string Destination, List<string> FileFilters, bool Overwrite)
        {
            try
            {
                _Source = new DirectoryInfo(Source);
                _Destination = new DirectoryInfo(Destination);
                _FileFilters = FileFilters;
                _Overwrite = Overwrite;

                StartCopy();
            }
            catch
            { throw; }
        }

        /// <summary>
        /// The 'Start Copy' Method: Async Call to Start the Copy Process.
        /// </summary>
        /// <param name="Source">The Source Directory.</param>
        /// <param name="Destination">The Destination Directory.</param>
        /// <param name="FileFilter">The File Filter.</param>
        /// <param name="FolderFilter">The Folder Filter.</param>
        public void StartCopy(string Source, string Destination, List<string> FileFilters, string FolderFilter)
        {
            try
            {
                _Source = new DirectoryInfo(Source);
                _Destination = new DirectoryInfo(Destination);
                _FileFilters = FileFilters;
                _FolderFilter = FolderFilter;

                StartCopy();
            }
            catch
            { throw; }
        }

        /// <summary>
        /// The 'Start Copy' Method: Async Call to Start the Copy Process.
        /// </summary>
        /// <param name="Source">The Source Directory.</param>
        /// <param name="Destination">The Destination Directory.</param>
        /// <param name="FileFilter">The File Filter.</param>
        /// <param name="FolderFilter">The Folder Filter.</param>
        /// <param name="Overwrite">Whether or not to force Overwrite on Destination.</param>
        public void StartCopy(string Source, string Destination, List<string> FileFilters, string FolderFilter, bool Overwrite)
        {
            try
            {
                _Source = new DirectoryInfo(Source);
                _Destination = new DirectoryInfo(Destination);
                _FileFilters = FileFilters;
                _FolderFilter = FolderFilter;
                _Overwrite = Overwrite;

                StartCopy();
            }
            catch
            { throw; }
        }

        /// <summary>
        /// The 'Cancel Copy' Method: To Cancel the Working Thread of the xDirectory.StartCopy Method.
        /// </summary>
        public void CancelCopy()
        {
            _CancelCopy = true;
        }

        #endregion



        #region Private Methods

        /// <summary>
        /// The Main Work Method of the xDirectory Class: Handled in a Separate Thread.
        /// </summary>
        /// <param name="StateInfo">Undefined</param>
        private void DoWork(object StateInfo)
        {
            _CopierStatus = xDirectoryStatus.Started;

            int iterator = 0;
            List<DirectoryInfo> FolderSourceList = new List<DirectoryInfo>();
            List<FileInfo> FileSourceList = new List<FileInfo>();
            DirectoryInfo FolderPath;
            FileInfo FilePath;

            try
            {
                // Part 1: Indexing
                ///////////////////////////////////////////////////////
                
                _CopierStatus = xDirectoryStatus.Indexing;
                
                FolderSourceList.Add(_Source);

                while (iterator < FolderSourceList.Count)
                {
                    if (_CancelCopy) return;

                    foreach (DirectoryInfo di in FolderSourceList[iterator].GetDirectories(_FolderFilter))
                    {
                        if (_CancelCopy) return;

                        FolderSourceList.Add(di);

                        OnItemIndexed(new ItemIndexedEventArgs(
                            di.FullName,
                            0,
                            FolderSourceList.Count,
                            true));
                    }

                    foreach (string FileFilter in _FileFilters)
                    {
                        foreach (FileInfo fi in FolderSourceList[iterator].GetFiles(FileFilter))
                        {
                            if (_CancelCopy) return;

                            FileSourceList.Add(fi);

                            OnItemIndexed(new ItemIndexedEventArgs(
                                fi.FullName,
                                fi.Length,
                                FileSourceList.Count,
                                false));
                        }
                    }

                    iterator++;
                }

                OnIndexComplete(new IndexCompleteEventArgs(
                    FolderSourceList.Count, 
                    FileSourceList.Count));



                // Part 2: Destination Folder Creation
                ///////////////////////////////////////////////////////

                _CopierStatus = xDirectoryStatus.CopyingFolders;

                for (iterator = 0; iterator < FolderSourceList.Count; iterator++)
                {
                    if (_CancelCopy) return;

                    
                    if (FolderSourceList[iterator].Exists)
                    {
                        FolderPath = new DirectoryInfo(
                            _Destination.FullName +
                            Path.DirectorySeparatorChar +
                            FolderSourceList[iterator].FullName.Remove(0, _Source.FullName.Length));

                        try
                        {

                            if (!FolderPath.Exists) FolderPath.Create(); // Prevent IOException

                            OnItemCopied(new ItemCopiedEventArgs(
                                    FolderSourceList[iterator].FullName,
                                    FolderPath.FullName,
                                    0,
                                    iterator,
                                    FolderSourceList.Count,
                                    true));
                        }
                        catch (Exception iError)
                        {
                            OnCopyError(new CopyErrorEventArgs(
                                    FolderSourceList[iterator].FullName,
                                    FolderPath.FullName,
                                    iError));
                        }
                    }
                    
                }



                // Part 3: Source to Destination File Copy
                ///////////////////////////////////////////////////////

                _CopierStatus = xDirectoryStatus.CopyingFiles;

                for (iterator = 0; iterator < FileSourceList.Count; iterator++)
                {
                        if (_CancelCopy) return;

                        if (FileSourceList[iterator].Exists)
                        {
                            FilePath = new FileInfo(
                                _Destination.FullName +
                                Path.DirectorySeparatorChar +
                                FileSourceList[iterator].FullName.Remove(0, _Source.FullName.Length));

                            try
                            {
                                if (_Overwrite)
                                    FileSourceList[iterator].CopyTo(FilePath.FullName, true); 
                                else
                                {
                                    if (!FilePath.Exists)
                                        FileSourceList[iterator].CopyTo(FilePath.FullName, true);
                                }

                                OnItemCopied(new ItemCopiedEventArgs(
                                        FileSourceList[iterator].FullName,
                                        FilePath.FullName,
                                        FileSourceList[iterator].Length,
                                        iterator,
                                        FileSourceList.Count,
                                        false));

                            }
                            catch (Exception iError)
                            {
                                OnCopyError(new CopyErrorEventArgs(
                                        FileSourceList[iterator].FullName,
                                        FilePath.FullName,
                                        iError));
                            }
                        }
                    
                }

            }
            catch
            { throw; }
            finally
            {
                _CopierStatus = xDirectoryStatus.Stopped;
                OnCopyComplete(new CopyCompleteEventArgs(_CancelCopy));
            }
        }

        #endregion
    }


}
