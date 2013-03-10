package otrremote;

import javax.swing.JPanel;
import java.awt.BorderLayout;

import javax.swing.JEditorPane;
import javax.swing.JFileChooser;
import javax.swing.JLabel;
import javax.swing.JTextField;
import javax.swing.JButton;

import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.io.File;
import java.util.Locale;

import javax.swing.ImageIcon;
import javax.swing.border.EmptyBorder;
import javax.swing.event.DocumentEvent;
import javax.swing.event.DocumentListener;
import javax.swing.event.HyperlinkEvent;
import javax.swing.event.HyperlinkListener;

import util.ui.UiUtilities;

public class OTRRemoteSettingsPanel extends JPanel {
	private static final long serialVersionUID = 540261327946331447L;
	private JPanel pnlApp;
	private JTextField txtPath;
	private JLabel lblPath, lblError, lblOk;
	private JButton btnBrowse;
	private JEditorPane lblNote;

	/**
	 * Create the panel.
	 */
	public OTRRemoteSettingsPanel() {
		setBorder(new EmptyBorder(10, 10, 10, 10));
		setLayout(new BorderLayout(10, 10));
		
		JPanel pnlContent = new JPanel();
		add(pnlContent, BorderLayout.CENTER);
		pnlContent.setLayout(new BorderLayout(5, 5));
		
		pnlApp = new JPanel();
		pnlContent.add(pnlApp, BorderLayout.NORTH);
		pnlApp.setLayout(new BorderLayout(5, 5));
		
		lblNote = UiUtilities.createHtmlHelpTextArea("<html>Please note that the OTR Remote plugin for TV-Browser needs an existing installation of OTR Remote.<br/>If you don't have it, you can download it at our <a href=\"#link\">homepage</a>.</html>", new HyperlinkListener() {
			@Override
			public void hyperlinkUpdate(HyperlinkEvent e) {
				if (e.getEventType() == HyperlinkEvent.EventType.ACTIVATED) {
					util.browserlauncher.Launch.openURL("http://www.crazysoft.net.ms/");
				}
			}
		});
		lblNote.setBorder(new EmptyBorder(1, 1, 1, 1));
		add(lblNote, BorderLayout.NORTH);
		
		lblPath = new JLabel("OTR Remote application:");
		pnlApp.add(lblPath, BorderLayout.WEST);
		
		txtPath = new JTextField();
		txtPath.getDocument().addDocumentListener(new DocumentListener() {
			@Override
			public void removeUpdate(DocumentEvent e) {
				changedUpdate(e);
			}
			
			@Override
			public void insertUpdate(DocumentEvent e) {
				changedUpdate(e);
			}
			
			@Override
			public void changedUpdate(DocumentEvent e) {
				OTRRemoteSettingsPanel.this.updatePathHint();
			}
		});
		txtPath.addPropertyChangeListener("text", new PropertyChangeListener() {
			@Override
			public void propertyChange(PropertyChangeEvent e) {

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
		
		lblOk = new JLabel("<html>The configured OTR Remote application path is valid.</html>");
		lblOk.setIcon(new ImageIcon(OTRRemoteSettingsPanel.class.getResource("/otrremote/ok.png")));
		
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
			UiUtilities.updateHtmlHelpTextArea(lblNote, "<html>Bitte beachten Sie, dass das OTR-Remote-Plugin für TV-Browser eine existierende Installation von OTR Remote benötigt.<br/>Wenn Sie diese nicht haben, können Sie sie auf unserer <a href=\"#link\">Homepage</a> herunterladen.</html>");
			lblPath.setText("OTR-Remote-Anwendung:");
			btnBrowse.setText("Durchsuchen...");
			lblError.setText("<html>Der konfigurierte Anwendungspfad zu OTR Remote ist ungültig. Sie werden das Plugin erst benutzen können, wenn Sie ihn korrigieren!</html>");
			lblOk.setText("<html>Der konfigurierte Anwendungspfad zu OTR Remote ist gültig.</html>");
		}
	}
	
	private void updatePathHint() {
		File file = new File(txtPath.getText());
		if (file.getName().isEmpty() || !file.exists() || !file.isFile() || file.getAbsoluteFile().getParentFile().listFiles(new OTRRemote.ApplicationFilenameFilter()).length == 0) {
			pnlApp.remove(lblOk);
			pnlApp.add(lblError, BorderLayout.SOUTH);
			
		} else {
			pnlApp.remove(lblError);
			pnlApp.add(lblOk, BorderLayout.SOUTH);
		}
		validate();
	}

	private class OTRRemoteFileFilter extends javax.swing.filechooser.FileFilter {
		@Override
		public String getDescription() {
			if (Locale.getDefault().getLanguage().equals("de")) {
				return "OTR-Remote-Anwendung";
			}
			return "OTR Remote Application";
		}
		
		@Override
		public boolean accept(File pathname) {
			if (pathname.isDirectory() || pathname.getName().equals("OTRRemote.exe") || pathname.getName().equals("OTRRemote.sh")) {
				return true;
			}
			return false;
		}
	}
}
