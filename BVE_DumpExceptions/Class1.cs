using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using Mackoy.Bvets;

namespace BVE_DumpExceptions;

public class Class1 : IInputDevice
{
	public event InputEventHandler? LeverMoved;
	public event InputEventHandler? KeyDown;
	public event InputEventHandler? KeyUp;

	static readonly string FilePath;

	static Class1()
	{
		FilePath = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
			$"BVE_DumpExceptions.{DateTime.Now:yyyyMMdd.HHmmss}.log"
		);

		AppDomain.CurrentDomain.FirstChanceException +=CurrentDomain_FirstChanceException;
	}

	private static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs eventArgs)
	{
		Exception e = eventArgs.Exception;

		File.WriteAllText(FilePath, $"[{DateTime.Now:HH:mm:ss.ffff}]: {e.GetType().Name}={e.Message} (InnerException: {e.InnerException})");
	}

	public void Configure(IWin32Window owner)
	{
		if (File.Exists(FilePath))
		{
			Process.Start(FilePath);
		}
		else
		{
			MessageBox.Show("No log are available in " + FilePath);
		}
	}

	public void Dispose()
	{
	}

	public void Load(string settingsPath)
	{
	}

	public void SetAxisRanges(int[][] ranges)
	{
	}

	public void Tick()
	{
	}
}
