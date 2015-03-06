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

namespace ToBase64 {
	public partial class Form1 : Form {
		public Form1(string filename) {
			InitializeComponent();
			OpenFile(filename);
		}

		private void OpenFile(string file) {
			if (string.IsNullOrEmpty(file) || !File.Exists(file)) {
				textBox1.Text = "";
				Text = "Drag a file onto this window";
				return;
			}
			var bytes = File.ReadAllBytes(file);
			var base64 = Convert.ToBase64String(bytes);
			Text = String.Format("{0}, {1}Kb -> {2}Kb", Path.GetFileName(file), bytes.Length / 1000, base64.Length / 1000);
			Clipboard.SetText(base64);
			textBox1.Text = base64.Length < 2000 ? base64 : "Too long to be displayed, copied to clipboard";
		}

		private void Form1_DragDrop(object sender, DragEventArgs e) {
			var file = e.Data.GetData(DataFormats.FileDrop) as string[];
			if (file != null && file.Length == 1 && File.Exists(file[0]))
				OpenFile(file[0]);
		}

		private void Form1_DragEnter(object sender, DragEventArgs e) {
			var file = e.Data.GetData(DataFormats.FileDrop) as string[];
			if (file != null && file.Length == 1 && File.Exists(file[0]))
				e.Effect = DragDropEffects.Move;
		}
	}
}
