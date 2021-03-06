﻿using System;
using MonoDevelop.Components;
using MonoDevelop.Projects;

namespace MonoDevelop.Ide.GettingStarted
{
	public abstract class GettingStartedProviderBase : IGettingStartedProvider
	{
		public abstract bool SupportsProject (Project project);

		public abstract Control GetGettingStartedWidget (Project project);

		public virtual void ShowGettingStarted (Project project)
		{
			GettingStartedViewContent view;
			foreach (var doc in IdeApp.Workbench.Documents) {
				view = doc.PrimaryView.GetContent<GettingStartedViewContent> ();
				if (view != null && view.Project == project) {
					view.WorkbenchWindow.SelectWindow ();
					return;
				}
			}

			var provider = project.GetGettingStartedProvider ();
			if (provider != null) {
				var vc = new GettingStartedViewContent (project, provider);
				IdeApp.Workbench.OpenDocument (vc, true);
			}
		}
	}
}

