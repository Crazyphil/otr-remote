package otrremote;

import javax.swing.JPanel;
import java.awt.BorderLayout;

import javax.swing.JFileChooser;
import javax.swing.JLabel;
import javax.swing.JTextField;
import javax.swing.JButton;

import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import java.io.File;
import java.util.Locale;

import javax.swing.ImageIcon;

public class OTRRemoteSettingsPanel extends JPanel {
	private static final long serialVersionUID = 540261327946331447L;
	private JTextField txtPath;
	private JLabel lblNote, lblPath, lblError, lblOk;
	private JButton btnBrowse;

	/**
	 * Create the panel.
	 */
	public OTRRemoteSettingsPanel() {
		setLayout(new BorderLayout(10, 10));
		
		lblNote = new JLabel("<html>Please note that the OTR Remote plugin for TV-Browser needs an existing installation of OTR Remote.<br/>\r\nIf you don't have it, you can download it at our <a href=\"http://www.crazysoft.net.ms/\">homepage</a>.</html>");
		add(lblNote, BorderLayout.NORTH);
		
		JPanel pnlContent = new JPanel();
		add(pnlContent, BorderLayout.CENTER);
		pnlContent.setLayout(new BorderLayout(5, 5));
		
		JPanel pnlApp = new JPanel();
		pnlContent.add(pnlApp, BorderLayout.NORTH);
		pnlApp.setLayout(new BorderLayout(5, 5));
		
		lblPath = new JLabel("OTR Remote application:");
		pnlApp.add(lblPath, BorderLayout.WEST);
		
		txtPath = new JTextField();
		txtPath.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				File file = new File(txtPath.getText());
				if (file.getName().isEmpty() || !file.exists() || !file.isFile() || file.getParentFile().listFiles(new OTRRemoteSettingsPanel.ApplicationFilenameFilter()).length == 0) {
					OTRRemoteSettingsPanel.this.lblError.setVisible(true);
					OTRRemoteSettingsPanel.this.lblOk.setVisible(false);
				} else {
					OTRRemoteSettingsPanel.this.lblError.setVisible(false);
					OTRRemoteSettingsPanel.this.lblOk.setVisible(true);
				}
			}
		});
		lblPath.setLabelFor(txtPath);
		pnlApp.add(txtPath, BorderLayout.CENTER);
		txtPath.setColumns(10);
		
		btnBrowse = new JButton("Browse...");
		btnBrowse.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				JFileChooser chooser = new JFileChooser(txtPath.getText());
				chooser.setAcceptAllFileFilterUsed(false);
				chooser.setFileFilter(new OTRRemoteFileFilter());
				if (chooser.showOpenDialog(OTRRemoteSettingsPanel.this) == JFileChooser.APPROVE_OPTION) {
					txtPath.setText(chooser.getSelectedFile().getAbsolutePath());
				}
			}
		});
		pnlApp.add(btnBrowse, BorderLayout.EAST);
		
		lblError = new JLabel("<html>The configured OTR Remote application path is not valid. You will not be able to use the plugin before fixing this!</html>");
		lblError.setIcon(new ImageIcon(OTRRemoteSettingsPanel.class.getResource("/otrremote/error.png")));
		pnlApp.add(lblError, BorderLayout.SOUTH);
		
		lblOk = new JLabel("<html>The configured OTR Remote application path is valid.</html>");
		lblOk.setIcon(new ImageIcon(OTRRemoteSettingsPanel.class.getResource("/otrremote/ok.png")));
		lblOk.setVisible(false);
		pnlApp.add(lblOk, BorderLayout.SOUTH);
		
		localize();
	}
	
	public void setProgramPath(String path) {
		txtPath.setText(path);
	}
	
	public String getProgramPath() {
		return txtPath.getText();
	}
	
	private void localize() {
		if (Locale.getDefault().getLanguage().equals("de")) {
			lblNote.setText("<html>Bitte beachten Sie, dass das OTR-Remote-Plugin für TV-Browser eine existierende Installation von OTR Remote benötigt.<br/>\r\nWenn Sie diese nicht haben, können Sie sie auf unserer <a href=\"http://www.crazysoft.net.ms/\">Homepage</a> herunterladen.</html>");
			lblPath.setText("OTR-Remote-Anwendung:");
			btnBrowse.setText("Durchsuchen...");
			lblError.setText("<html>Der konfigurierte Anwendungspfad zu OTR Remote ist ungültig. Sie werden das Plugin erst benutzen können, wenn Sie ihn korrigieren!</html>");
			lblOk.setText("<html>Der konfigurierte Anwendungspfad zu OTR Remote ist gültig.</html>");
		}
	}

	private class OTRRemoteFileFilter extends javax.swing.filechooser.FileFilter {
		@Override
		public String getDescription() {
			return "OTR Remote Application";
		}
		
		@Override
		public boolean accept(File pathname) {
			if (pathname.getName().equals("OTRRemote.exe") || pathname.getName().equals("OTRRemote.sh")) {
				return true;
			}
			return false;
		}
	}
	
	private class ApplicationFilenameFilter implements java.io.FilenameFilter {
		@Override
		public boolean accept(File dir, String name) {
			if (name.equals("TVBrowser.dll")) {
				return true;
			}
			return false;
		}
	}
}
