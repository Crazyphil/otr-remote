package otrremote;

import java.util.Properties;

import javax.swing.Icon;
import javax.swing.JPanel;

import devplugin.SettingsTab;

public class OTRRemoteSettingsTab implements SettingsTab {

	private OTRRemoteSettingsPanel settingsPanel;
	
	@Override
	public JPanel createSettingsPanel() {
		settingsPanel = new OTRRemoteSettingsPanel();
		
		Properties props = OTRRemote.getInstance().storeSettings();
		if (props != null && props.getProperty("programPath") != null) {
			settingsPanel.setProgramPath(props.getProperty("programPath"));
		}
		return settingsPanel;
	}

	@Override
	public void saveSettings() {
		Properties props = OTRRemote.getInstance().storeSettings();
		if (props == null) {
			props = new Properties();
		}
		
		props.setProperty("programPath", settingsPanel.getProgramPath());
		OTRRemote.getInstance().loadSettings(props);
	}

	@Override
	public Icon getIcon() {
		return OTRRemote.getInstance().getIcon();
	}

	@Override
	public String getTitle() {
		return "OTR Remote";
	}

}
