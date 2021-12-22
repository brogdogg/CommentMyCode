//------------------------------------------------------------------------------
// <copyright file="CommentMyCodePackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using MB.VS.Extension.CommentMyCode.UserOptions;
using Task = System.Threading.Tasks.Task;
using System.Threading;

namespace MB.VS.Extension.CommentMyCode
{
  /// <summary>
  /// This is the class that implements the package exposed by this assembly.
  /// </summary>
  /// <remarks>
  /// <para>
  /// The minimum requirement for a class to be considered a valid package for Visual Studio
  /// is to implement the IVsPackage interface and register itself with the shell.
  /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
  /// to do it: it derives from the Package class that provides the implementation of the
  /// IVsPackage interface and uses the registration attributes defined in the framework to
  /// register itself and its components with the shell. These attributes tell the pkgdef creation
  /// utility what data to put into .pkgdef file.
  /// </para>
  /// <para>
  /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
  /// </para>
  /// </remarks>
  [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
  [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
  [Guid(CommentMyCodePackage.PackageGuidString)]
  [ProvideMenuResource("Menus.ctmenu", 1)]
  [ProvideProfile(typeof(MainOptionPage), "CommentMyCode", "Comment Templates", 106, 107, isToolsOptionPage:true, DescriptionResourceID = 108)]
  [ProvideOptionPage(typeof(MainOptionPage), "CommentMyCode", "Main", 0, 0, true)]
  public sealed class CommentMyCodePackage : AsyncPackage
  {
    /// <summary>
    /// CommentMyCodePackage GUID string.
    /// </summary>
    public const string PackageGuidString = "b5566e51-743a-4617-99f2-b35a09c10bec";

    /// <summary>
    /// Initializes a new instance of the <see cref="CommentMyCodePackage"/> class.
    /// </summary>
    public CommentMyCodePackage()
    {
      // Inside this method you can place any initialization code that does not
      // require any Visual Studio service because at this point the package
      // object is created but not sited yet inside Visual Studio environment.
      // The place to do all the other initialization is the Initialize method.
    }

    #region Package Members

    /// <summary>
    /// Initialization of the package; this method is called right after the
    /// package is sited, so this is the place where you can put all the
    /// initialization code that rely on services provided by VisualStudio.
    /// </summary>
    protected override async Task InitializeAsync(
      CancellationToken cancellationToken,
      IProgress<ServiceProgressData> progress)
    {
      await base.InitializeAsync(cancellationToken, progress);

      // When initialized asynchronously, we *may* be on a background thread at
      // this point.   Do any initialization that requires the UI thread after
      // switching to the UI thread.   Otherwise, remove the switch to the UI
      // thread if you don't need it.
      await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
      CommentMyCode.Initialize(this);
    }

    #endregion
  }
}
