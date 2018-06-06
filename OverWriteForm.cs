using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace asmCycleCount {
	public partial class OverWriteForm : Form {
		public bool remember { get; set; }

		public OverWriteForm(string file) {
			InitializeComponent();

			// filename and warning icon
			label1.Text = label1.Text.Replace("*", Path.GetFileName(file));
			WarningIco.Image = SystemIcons.Warning.ToBitmap();

			// play warning sound
			SystemSounds.Asterisk.Play();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e) {
			if (sender is CheckBox)
				remember = ((CheckBox) sender).Checked;
		}

		//yes
		private void button2_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.Yes;
			Close();
		}

		//no
		private void button1_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.No;
			Close();
		}
	}
}
