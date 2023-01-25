using System;
using ChartDirector;

namespace CSharpChartExplorer
{
	public class zoomscrolldemo : DemoModule
	{
		//Name of demo module
		public string getName() { return "Zooming and Scrolling"; }

		//Number of charts produced in this demo module
		public int getNoOfCharts() 
		{ 
			//This demonstration open its own window instead of using the right pane for 
			//display, so we just load the form, then returns 0 to the main window.
			if ((currentFrm == null) || (currentFrm.IsDisposed))
				currentFrm = new FrmZoomScrollDemo();
			currentFrm.ShowDialog();
			return 0; 
		}

		//Main code for creating chart.
		public void createChart(WinChartViewer viewer, string img)
		{
			//do nothing, as right pane is not used
		}

		private static FrmZoomScrollDemo currentFrm;
	}
}
