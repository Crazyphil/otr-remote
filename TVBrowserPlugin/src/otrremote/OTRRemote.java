package otrremote;

import devplugin.ActionMenu;
import devplugin.ContextMenuAction;
import devplugin.Date;
import devplugin.Plugin;
import devplugin.PluginInfo;
import devplugin.PluginTreeNode;
import devplugin.Program;
import devplugin.ProgramFieldType;
import devplugin.ProgramReceiveTarget;
import devplugin.ProgressMonitor;
import devplugin.SettingsTab;
import devplugin.Version;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.Locale;
import java.util.Properties;
import javax.swing.AbstractAction;
import javax.swing.Action;
import javax.swing.Icon;
import javax.swing.JOptionPane;

import org.joda.time.DateTime;

import tvbrowser.ui.mainframe.MainFrame;

import at.jta.Key;
import at.jta.RegistryErrorException;
import at.jta.Regor;

public class OTRRemote extends Plugin implements ActionListener {
	private static OTRRemote instance;
	
	private Program curProgram;
	private String lang = Locale.getDefault().getLanguage();
	private Version tvBrowserVersion;
	private Properties properties;

	private ArrayList<Program> markedPrograms = new ArrayList<Program>();

	public static OTRRemote getInstance() {
		return instance;
	}
	
	public OTRRemote() {
		instance = this;
	}
	
	public PluginInfo getInfo() {
		String name = "Crazysoft OTR Remote";
		String desc = "Adds a recording job to OnlineTvRecorder.com";
		String author = "Crazysoft, www.crazysoft-software.tk";

		if (this.lang.equals("de")) {
			desc = "Fügt einen Aufnahmeauftrag zu OnlineTvRecorder.com hinzu";
		}

		return new PluginInfo(this.getClass(), name, desc, author);
	}
	
	public static Version getVersion() {
		return new Version(2, 4, true);
	}
	
	public Icon getIcon() {
		return createImageIcon("otrremote/video.png");
	}

	protected String getMarkIconName() {
		return "otrremote/video.png";
	}
	
	public void loadSettings(Properties settings) {
		if (settings == null) {
			settings = new Properties();
		}
		if (settings.getProperty("programPath") == null && util.misc.OperatingSystem.isWindows()) {
			try {
				Regor reg = new Regor();
				Key key = reg.openKey(Regor.HKEY_LOCAL_MACHINE, "SOFTWARE\\Crazysoft\\AppUpdate\\InstalledProducts\\Crazysoft.OTR_Remote", Regor.KEY_QUERY_VALUE);
				if (key != null) {
					String path = reg.readValueAsString(key, "InstallationPath");
					if (path != null) {
						settings.setProperty("programPath", String.format("%s%sOTRRemote.exe", path, File.separator));
					}
				}
			} catch (RegistryErrorException e) {
			}
			
		}
		this.properties = settings;
	}
	
	public Properties storeSettings() {
		return this.properties;
	}

	public void readData(ObjectInputStream in) throws IOException, ClassNotFoundException {
		int size = in.readInt();

		for (int i = 0; i < size; i++) {
			Date programDate;
			if (this.getTVBrowserVersion().getMajor() >= 3) {
				programDate = Date.readData(in);
			} else {
				programDate = new Date(in);
			}
			
			String progId = (String)in.readObject();
			Program program = getPluginManager().getProgram(programDate, progId);

			if (program != null) {
				this.markedPrograms.add(program);
			}
		}
	}

	public void writeData(ObjectOutputStream out) throws IOException {
		out.writeInt(this.markedPrograms.size());

		for (int i = 0; i < this.markedPrograms.size(); i++) {
			Program p = (Program)this.markedPrograms.get(i);
			
			if (this.getTVBrowserVersion().getMajor() >= 3) {
				p.getDate().writeData(new DataOutputStream(out));
			} else {
				p.getDate().writeData(out);
			}
			out.writeObject(p.getID());
		}
	}
	
	public SettingsTab getSettingsTab() {
		return new OTRRemoteSettingsTab();
	}

	public ActionMenu getContextMenuActions(Program program) {
		boolean isEnabled = true;
		
		DateTime now = DateTime.now();
		DateTime endTime = getProgramDateTime(program);
		if (program.getLength() > 0) {
			endTime = endTime.plusMinutes(program.getLength());
		}

		if (!program.equals(Plugin.getPluginManager().getExampleProgram()) && now.isAfter(endTime)) {
			isEnabled = false;
		}

		this.curProgram = program;
		Icon menuIcon = createImageIcon("otrremote/video.png");
		if (this.markedPrograms.contains(program)) {
			String menuTitle = "OTR Remote";
			Action[] submenu = new Action[3];

			String recordItemText = "Re-Record";
			String deleteItemText = "Delete Recording";
			String unmarkItemText = "Remove mark";

			if (this.lang.equals("de")) {
				recordItemText = "Neu aufnehmen";
				deleteItemText = "Aufnahme löschen";
				unmarkItemText = "Markierung entfernen";
			}

			AbstractAction recordItem = new AbstractAction() {
				private static final long serialVersionUID = 8579287847683162753L;

				public void actionPerformed(ActionEvent e) {
					OTRRemote.this.modifyRecording(OTRRemote.this.curProgram, false);
				}
			};
			recordItem.putValue("Name", recordItemText);
			recordItem.setEnabled(isEnabled);
			submenu[0] = recordItem;

			AbstractAction deleteItem = new AbstractAction() {
				private static final long serialVersionUID = -308102096298513374L;

				public void actionPerformed(ActionEvent e) {
					OTRRemote.this.modifyRecording(OTRRemote.this.curProgram, true);
				}
			};
			deleteItem.putValue("Name", deleteItemText);
			deleteItem.setEnabled(isEnabled);
			submenu[1] = deleteItem;

			AbstractAction unmarkItem = new AbstractAction() {
				private static final long serialVersionUID = -8799920264022400671L;

				public void actionPerformed(ActionEvent e) {
					OTRRemote.this.unmarkProgram(OTRRemote.this.curProgram);
				}
			};
			unmarkItem.putValue("Name", unmarkItemText);
			submenu[2] = unmarkItem;
			
			if (this.getTVBrowserVersion().getMajor() >= 3) {
				return new ActionMenu(menuTitle, menuIcon, submenu);
			}
			else {
				return new ActionMenu(new ContextMenuAction(menuTitle, menuIcon), submenu);
			}
		}

		ContextMenuAction action = new ContextMenuAction();
		action.setText("Record via OTR");
		if (this.lang.equals("de")) {
			action.setText("Mit OTR aufnehmen");
		}
		action.setActionListener(this);
		action.setSmallIcon(menuIcon);
		action.setEnabled(isEnabled);
		return new ActionMenu(action);
	}
	
	public void handleTvBrowserStartFinished() {
		Iterator<Program> it = this.markedPrograms.iterator();
		while (it.hasNext()) {
			Program program = it.next();
			program.mark(this);
			program.validateMarking();
		}
	}
	
	public void handleTvDataUpdateFinished() {
		Program[] programs = markedPrograms.toArray(new Program[0]);
		for (Program program : programs) {
			if (program.getProgramState() == Program.WAS_DELETED_STATE) {
				markedPrograms.remove(program);
			} else if (program.getProgramState() == Program.WAS_UPDATED_STATE) {
				markedPrograms.remove(program);
				markedPrograms.add(getPluginManager().getProgram(program.getDate(), program.getID()));
			}
		}
	}

	public void actionPerformed(ActionEvent e) {
		modifyRecording(this.curProgram, false);
	}

	public boolean canReceiveProgramsWithTarget() {
		return true;
	}

	public boolean receivePrograms(Program[] programs, ProgramReceiveTarget target) {
		if (target == null || target.getTargetId() == null) {
			return false;
		}

		if (target.getTargetId().equals("OTRRemote")) {
			for (Program program : programs) {
				modifyRecording(program, false);
			}
			return true;
		}
		return false;
	}

	public ProgramReceiveTarget[] getProgramReceiveTargets() {
		ArrayList<ProgramReceiveTarget> targets = new ArrayList<ProgramReceiveTarget>();

		String title = "Record";
		if (this.lang.equals("de")) {
			title = "Aufnehmen";
		}
		
		targets.add(new ProgramReceiveTarget(this, title, "OTRRemote"));

		return targets.toArray(new ProgramReceiveTarget[0]);
	}
	
	public boolean canUseProgramTree() {
		return true;
	}

	public void createMyTree() {
		PluginTreeNode rootNode = getRootNode();
		rootNode.removeAllActions();
		rootNode.removeAllChildren();

		for (Program program : markedPrograms) {
			PluginTreeNode programNode = rootNode.addProgram(program);
			programNode.addActionMenu(getContextMenuActions(program));
		}

		rootNode.update();
	}
	
	public Version getTVBrowserVersion() {
		if (this.tvBrowserVersion == null) {
	        try {
				Method m = getPluginManager().getClass().getMethod("getTVBrowserVersion", new Class[0]);
				this.tvBrowserVersion = (Version)m.invoke(getPluginManager(), new Object[0]);
			} catch (Exception e) {
				this.tvBrowserVersion = new Version(2, 7, 6);
			}
		}
		return this.tvBrowserVersion;
	}

	private void modifyRecording(Program program, boolean delete) {
		try {
			String path = properties.getProperty("programPath");

			if (path == null) {
				String message = "The path to the OTR Remote application is not set. Please configure the plugin before using it.";

				if (this.lang.equals("de")) {
					message = "Der Pfad zur OTR-Remote-Anwendung ist nicht gesetzt. Bitte konfigurieren Sie das Plugin vor dem Benutzen.";
				}

				throw new IOException(message);
			}
			
			File otrExe;
			if (path.startsWith("mono")) {
				String[] pathParts = path.split(" ", 2);
				if (pathParts.length > 1) {
					if (pathParts[1].startsWith("\"") && pathParts[1].endsWith("\"")) {
						pathParts[1] = pathParts[1].substring(1, pathParts[1].length() - 1);
						path = String.format("%s %s", pathParts[0], pathParts[1]);
					}
					otrExe = new File(pathParts[1]);
				} else {
					otrExe = new File("");
				}
			} else {
				if (path.startsWith("\"") && path.endsWith("\"")) {
					path = path.substring(1, path.length() - 1);
				}
				otrExe = new File(path);
			}
			
			if (otrExe.getName().isEmpty() || !otrExe.exists() || !otrExe.isFile() || otrExe.getAbsoluteFile().getParentFile().listFiles(new ApplicationFilenameFilter()).length == 0) {
				String message = "The path to the OTR Remote application is not valid. Please correct it in the plugin configuration.";

				if (this.lang.equals("de")) {
					message = "Der Pfad zur OTR-Remote-Anwendung ist ungültig. Bitte korrigieren Sie ihn in der Plugin-Konfiguration.";
				}

				throw new FileNotFoundException(message);
			}

			DateTime startTime = getProgramDateTime(program);
			DateTime endTime = startTime;
			if (program.getLength() > 0) {
				endTime = endTime.plusMinutes(program.getLength());
			}
			
			ArrayList<String> arguments = new ArrayList<String>();
			if (path.startsWith("mono")) {
				arguments.add("mono");
				arguments.add(path.split(" ", 2)[1]);
			} else {
				arguments.add(path);
			}
			
			if (delete) {
				arguments.add("-r");
			} else {
				arguments.add("-a");
			}
			arguments.add(String.format("-s=%s", program.getChannel()));
			arguments.add(String.format("-sd=%tF", startTime.toGregorianCalendar()));
			arguments.add(String.format("-st=%tR", startTime.toGregorianCalendar()));
			if (program.getLength() >= 0) {
				arguments.add(String.format("-et=%tR", endTime.toGregorianCalendar()));
			}
			arguments.add(String.format("-t=%s", program.getTitle()));
			if (program.getTextField(ProgramFieldType.GENRE_TYPE) != null) {
				arguments.add(String.format("-g=%s", program.getTextField(ProgramFieldType.GENRE_TYPE)));
			}

			ProcessBuilder pb = new ProcessBuilder(arguments);
			pb.directory(otrExe.getAbsoluteFile().getParentFile());
			Thread thread = new Thread(new OTRRemoteRunner(program, pb.start(), delete));
			thread.start();
		} catch (Exception excp) {
			String message = "Error while processing the recording job: ";
			String title = "Recording error";

			if (this.lang.equals("de")) {
				message = "Fehler beim Bearbeiten des Aufnahmeauftrags: ";
				title = "Aufnahmefehler";
			}

			JOptionPane.showMessageDialog(getParentFrame(), message + excp.getLocalizedMessage(), title, JOptionPane.ERROR_MESSAGE);
		}
	}
	
	private void markProgram(Program program) {
		this.markedPrograms.add(program);
		program.mark(this);
		program.validateMarking();
	}

	private void unmarkProgram(Program program) {
		this.markedPrograms.remove(program);
		program.unmark(this);
		program.validateMarking();
	}
	
	private DateTime getProgramDateTime(Program program) {
		Date progDate = program.getDate();
		return new DateTime(progDate.getYear(), progDate.getMonth(), progDate.getDayOfMonth(), program.getHours(), program.getMinutes());
	}
	
	protected static class ApplicationFilenameFilter implements java.io.FilenameFilter {
		@Override
		public boolean accept(File dir, String name) {
			if (name.equals("TVBrowser.dll")) {
				return true;
			}
			return false;
		}
	}
	
	private class OTRRemoteRunner implements Runnable {
		Program program;
		Process process;
		boolean unmark;
		
		public OTRRemoteRunner(Program program, Process process, boolean unmark) {
			this.program = program;
			this.process = process;
			this.unmark = unmark;
		}
		
		@Override
		public void run() {
			ProgressMonitor monitor = MainFrame.getInstance().getStatusBar().createProgressMonitor();
			monitor.setMaximum(2);
			monitor.setValue(1);
			MainFrame.getInstance().getStatusBar().getProgressBar().setVisible(true);
			if (lang.equals("de")) {
				monitor.setMessage("Gewählte Aktion wird an OTR Remote gesendet...");
			} else {
				monitor.setMessage("Sending action to OTR Remote...");
			}
			
			try {
				if (process.waitFor() == 0) {
					if (!unmark) {
						markProgram(program);
					} else {
						unmarkProgram(program);
					}
					
					monitor.setValue(2);
					if (lang.equals("de")) {
						monitor.setMessage("OTR Remote hat die Aktion erfolgreich durchgeführt.");
					} else {
						monitor.setMessage("OTR Remote has executed the action successfully.");
					}
				} else {
					monitor.setValue(2);
					if (lang.equals("de")) {
						monitor.setMessage("OTR Remote hat die Aktion nicht durchgeführt.");
					} else {
						monitor.setMessage("OTR Remote did not execute the action.");
					}
				}
			} catch (InterruptedException e) {
				monitor.setValue(2);
				if (lang.equals("de")) {
					monitor.setMessage("OTR Remote konnte nicht erfolgreich ausgeführt werden!");
				} else {
					monitor.setMessage("OTR Remote could not be executed successfully!");
				}
			}
			
			try {
				Thread.sleep(5000);
				MainFrame.getInstance().getStatusBar().getProgressBar().setVisible(false);
				monitor.setMessage("");
			} catch (InterruptedException e) {
			}
		}
	}

}