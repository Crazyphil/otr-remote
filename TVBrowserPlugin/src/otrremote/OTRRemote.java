package otrremote;

import devplugin.ActionMenu;
import devplugin.ContextMenuAction;
import devplugin.Date;
import devplugin.Plugin;
import devplugin.PluginInfo;
import devplugin.Program;
import devplugin.ProgramReceiveTarget;
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
import java.util.TimeZone;
import javax.swing.AbstractAction;
import javax.swing.Action;
import javax.swing.Icon;
import javax.swing.JOptionPane;

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
		String author = "Crazysoft, www.crazysoft.net.ms";

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
	
	public void loadSettings(Properties settings) {
		if (settings == null) {
			settings = new Properties();
		}
		if (settings.getProperty("programPath") == null && System.getProperty("os.name").toLowerCase().startsWith("win")) {
			try {
				Regor reg = new Regor();
				Key key = reg.openKey(Regor.HKEY_LOCAL_MACHINE, "SOFTWARE\\Crazysoft\\AppUpdate\\InstalledProducts\\Crazysoft.OTR_Remote", Regor.KEY_QUERY_VALUE);
				if (key != null) {
					String path = reg.readValueAsString(key, "InstallationPath");
					if (path != null) {
						settings.setProperty("programPath", path);
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
	
	public SettingsTab getSettingsTab() {
		return new OTRRemoteSettingsTab();
	}

	public ActionMenu getContextMenuActions(Program program) {
		Date progDate = program.getDate();
		DateTime startDate = new DateTime(progDate.getYear(), progDate.getMonth(), progDate.getDayOfMonth());
		DateTime now = new DateTime();

		TimeZone thisZone = TimeZone.getDefault();
		int offset = thisZone.getRawOffset();
		if (thisZone.useDaylightTime()) {
			offset += thisZone.getDSTSavings();
		}
		offset /= 1000;

		startDate.advanceSecs(offset);
		now.advanceSecs(offset);

		if (now.daysBetween(startDate) > 0) {
			return null;
		}
		
		if (now.daysBetween(startDate) == 0) {
			DateTime endTime = (DateTime)startDate.clone();
			endTime.advanceMins(program.getStartTime());
			endTime.advanceMins(program.getLength());
			endTime.advanceSecs(-offset);

			if ((now.getDay() == endTime.getDay()) && (now.getHour() >= endTime.getHour())) {
				if ((now.getHour() == endTime.getHour()) && (now.getMin() > endTime.getMin())) {
					return null;
				}
				if ((program != Plugin.getPluginManager().getExampleProgram()) && (now.getHour() > endTime.getHour())) {
					return null;
				}
			}
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
					OTRRemote.this.modifyRecording(OTRRemote.this.curProgram, Boolean.FALSE);
				}
			};
			recordItem.putValue("Name", recordItemText);
			submenu[0] = recordItem;

			AbstractAction deleteItem = new AbstractAction() {
				private static final long serialVersionUID = -308102096298513374L;

				public void actionPerformed(ActionEvent e) {
					OTRRemote.this.modifyRecording(OTRRemote.this.curProgram, Boolean.TRUE);
				}
			};
			deleteItem.putValue("Name", deleteItemText);
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
		return new ActionMenu(action);
	}

	public void actionPerformed(ActionEvent e) {
		modifyRecording(this.curProgram, Boolean.FALSE);
	}

	protected String getMarkIconName() {
		return "otrremote/video.png";
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
	
	public void handleTvBrowserStartFinished() {
		Iterator<Program> it = this.markedPrograms.iterator();
		while (it.hasNext()) {
			Program program = it.next();
			program.mark(this);
			program.validateMarking();
		}
	}

	public boolean canReceiveProgramsWithTarget() {
		return true;
	}

	public boolean receivePrograms(Program[] programs, ProgramReceiveTarget target) {
		if ((target == null) || (target.getTargetId() == null)) {
			return false;
		}

		if (target.getTargetId() == "OTRRemote") {
			for (Program program : programs) {
				modifyRecording(program, Boolean.valueOf(false));
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

	private void modifyRecording(Program program, Boolean delete) {
		try {
			String path = properties.getProperty("programPath");

			File otrExe;
			if (path == null) {
				String message = "The path to the OTR Remote application is not set. Please configure the plugin before using it.";

				if (this.lang.equals("de")) {
					message = "Der Pfad zur OTR-Remote-Anwendung ist nicht gesetzt. Bitte konfigurieren Sie das Plugin vor dem Benutzen.";
				}

				throw new IOException(message);
			}
			
			File otrCheck;
			if (path.startsWith("mono")) {
				String[] pathParts = path.split(" ", 2);
				if (pathParts.length > 1) {
					otrCheck = new File(pathParts[1]);
				} else {
					otrCheck = new File("");
				}
			} else {
				otrCheck = new File(path);
			}
			
			if (otrCheck.getName().isEmpty() || !otrCheck.exists() || !otrCheck.isFile() || otrCheck.getParentFile().listFiles(new ApplicationFilenameFilter()).length == 0) {
				String message = "The path to the OTR Remote application is not valid. Please correct it in the plugin configuration.";

				if (this.lang.equals("de")) {
					message = "Der Pfad zur OTR-Remote-Anwendung ist ungültig. Bitte korrigieren Sie ihn in der Plugin-Konfiguration.";
				}

				throw new FileNotFoundException(message);
			}
			otrExe = new File(path);

			DateTime startTime = new DateTime(program.getDate().getYear(), program.getDate().getMonth(),
					program.getDate().getDayOfMonth(), program.getHours(), program.getMinutes(), 0);
			DateTime endTime = (DateTime)startTime.clone();
			endTime.advanceMins(program.getLength());
			
			StringBuffer arguments = new StringBuffer();
			if (delete) {
				arguments.append("-r ");
			} else {
				arguments.append("-a ");
			}
			arguments.append(String.format("-s=\"%s\"", program.getChannel()));
			arguments.append(String.format(" -sd=%F", startTime.makeLocalCalendar()));
			arguments.append(String.format("-st=%T", startTime.makeLocalCalendar()));
			arguments.append(String.format(" -t=\"%s\"", program.getTitle()));

			String command;
			if (path.startsWith("mono")) {
				if (path.split(" ", 2)[1].contains(" ")) {
					command = String.format("mono \"%s\" %s", path.split(" ", 2)[1], arguments.toString());
				} else {
					command = String.format("%s %s", path, arguments.toString());
				}
			} else {
				command = String.format("\"%s\" %s", otrExe.getAbsolutePath(), arguments.toString());
			}

			waitForMarking(program, Runtime.getRuntime().exec(command), delete);
		} catch (Exception excp) {
			String message = "Error while processing the recording job: ";
			String title = "Recording error";

			if (this.lang.equals("de")) {
				message = "Fehler beim Bearbeiten des Aufnahmeauftrags: ";
				title = "Aufnahmefehler";
			}

			JOptionPane.showMessageDialog(getParentFrame(), message + excp.getLocalizedMessage(), title, 0);
		}
	}

	private void waitForMarking(Program program, Process process, Boolean unmark) throws InterruptedException {
		if (process.waitFor() == 0) {
			if (!unmark) {
				this.markedPrograms.add(program);
				program.mark(this);
				program.validateMarking();
			} else {
				unmarkProgram(program);
			}
		}
	}

	private void unmarkProgram(Program program) {
		this.markedPrograms.remove(program);
		program.unmark(this);
		program.validateMarking();
	}
	
	private Version getTVBrowserVersion() {
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
	
	protected static class ApplicationFilenameFilter implements java.io.FilenameFilter {
		@Override
		public boolean accept(File dir, String name) {
			if (name.equals("TVBrowser.dll")) {
				return true;
			}
			return false;
		}
	}
}