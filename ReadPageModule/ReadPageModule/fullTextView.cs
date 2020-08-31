using ConfigureManagerModule;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ReadPageModule
{
	public class fullTextView : Form
	{
		public string htmlString;

		private int m_a = 10;

		private string m_b = "FFFFFF";

		private ConfigurationManager m_c;

		private IContainer m_d;

		private WebBrowser m_e;

		private ToolStrip m_f;

		private ToolStripButton m_g;

		private ToolStripButton m_h;

		private ToolStripButton m_i;

		private ToolStripButton m_j;

		private ToolStripButton k;

		private ToolStripSeparator l;

		private ToolStripButton m;

		private ToolStripButton n;

		private ToolStripButton o;

		public fullTextView(ConfigurationManager configMng)
		{
			this.m_c = configMng;
			a();
			this.m_a = configMng.saveFullTextSize;
			this.m_b = configMng.saveFullTextColor;
		}

		private void j(object A_0, EventArgs A_1)
		{
			if (!htmlString.Contains("<body>"))
			{
				htmlString = htmlString.Replace("\n\r", "</br>");
				htmlString = "<html><head></head><body>" + htmlString + "</body></html>";
			}
			b();
		}

		private void b()
		{
			string str = "<style>";
			str += "body {";
			switch (this.m_b)
			{
			case "FFFFFF":
				str += "background-color:#FFFFFF;\n";
				str += "color:#000000;\n";
				break;
			case "000000":
				str += "background-color:#000000;\n";
				str += "color:#FFFFFF;\n";
				break;
			case "c0c0c0":
				str += "background-color:#c0c0c0;\n";
				str += "color:#0c0c0c;\n";
				break;
			case "f5eabd":
				str += "background-color:#f5eabd;\n";
				str += "color:#8b4513;\n";
				break;
			case "00c1c1":
				str += "background-color:#00c1c1;\n";
				str += "color:#0000ff;\n";
				break;
			}
			str = str + "background-color:#" + this.m_b + ";\n";
			str = str + "zoom: " + Convert.ToString(this.m_a * 10) + "%;\n";
			str += "margin:20px;\n";
			str += "}";
			str += "</style>";
			htmlString = htmlString.Replace("</head>", str + "</head>");
			this.m_e.DocumentText = htmlString;
			this.m_c.saveFullTextColor = this.m_b;
			this.m_c.saveFullTextSize = this.m_a;
		}

		private void i(object A_0, EventArgs A_1)
		{
			this.m_b = "FFFFFF";
			b();
		}

		private void h(object A_0, EventArgs A_1)
		{
			this.m_b = "000000";
			b();
		}

		private void g(object A_0, EventArgs A_1)
		{
			this.m_b = "c0c0c0";
			b();
		}

		private void f(object A_0, EventArgs A_1)
		{
			this.m_b = "f5eabd";
			b();
		}

		private void e(object A_0, EventArgs A_1)
		{
			this.m_b = "00c1c1";
			b();
		}

		private void d(object A_0, EventArgs A_1)
		{
			this.m_a--;
			if (this.m_a <= 0)
			{
				this.m_a = 1;
			}
			b();
		}

		private void c(object A_0, EventArgs A_1)
		{
			this.m_a++;
			b();
		}

		private void b(object A_0, EventArgs A_1)
		{
			this.m_a = 10;
			this.m_b = "FFFFFF";
			b();
		}

		private void a(object A_0, EventArgs A_1)
		{
			this.m_e.Width = base.Width - 20;
			this.m_e.Height = base.Height - this.m_f.Height;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.m_d != null)
			{
				this.m_d.Dispose();
			}
			base.Dispose(disposing);
		}

		private void a()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(fullTextView));
			this.m_e = new WebBrowser();
			this.m_f = new ToolStrip();
			this.m_g = new ToolStripButton();
			this.m_h = new ToolStripButton();
			this.m_i = new ToolStripButton();
			this.m_j = new ToolStripButton();
			k = new ToolStripButton();
			l = new ToolStripSeparator();
			m = new ToolStripButton();
			n = new ToolStripButton();
			o = new ToolStripButton();
			this.m_f.SuspendLayout();
			SuspendLayout();
			this.m_e.IsWebBrowserContextMenuEnabled = false;
			this.m_e.Location = new Point(0, 28);
			this.m_e.MinimumSize = new Size(20, 20);
			this.m_e.Name = "webBrowser1";
			this.m_e.Size = new Size(863, 565);
			this.m_e.TabIndex = 0;
			this.m_e.WebBrowserShortcutsEnabled = false;
			this.m_f.Items.AddRange(new ToolStripItem[9]
			{
				this.m_g,
				this.m_h,
				this.m_i,
				this.m_j,
				k,
				l,
				m,
				n,
				o
			});
			this.m_f.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.m_f.Location = new Point(0, 0);
			this.m_f.Name = "toolStrip1";
			this.m_f.Size = new Size(863, 25);
			this.m_f.TabIndex = 1;
			this.m_f.Text = "toolStrip1";
			this.m_g.BackColor = Color.White;
			this.m_g.DisplayStyle = ToolStripItemDisplayStyle.None;
			this.m_g.ForeColor = Color.White;
			this.m_g.Image = (Image)componentResourceManager.GetObject("color1.Image");
			this.m_g.ImageTransparentColor = Color.Magenta;
			this.m_g.Name = "color1";
			this.m_g.Size = new Size(23, 22);
			this.m_g.Text = "toolStripButton1";
			this.m_g.ToolTipText = "背景色";
			this.m_g.Click += new EventHandler(i);
			this.m_h.BackColor = Color.Black;
			this.m_h.DisplayStyle = ToolStripItemDisplayStyle.None;
			this.m_h.ForeColor = Color.Black;
			this.m_h.Image = (Image)componentResourceManager.GetObject("color2.Image");
			this.m_h.ImageTransparentColor = Color.Magenta;
			this.m_h.Name = "color2";
			this.m_h.Size = new Size(23, 22);
			this.m_h.Text = "toolStripButton2";
			this.m_h.ToolTipText = "背景色";
			this.m_h.Click += new EventHandler(h);
			this.m_i.BackColor = Color.Silver;
			this.m_i.DisplayStyle = ToolStripItemDisplayStyle.None;
			this.m_i.ForeColor = Color.Silver;
			this.m_i.Image = (Image)componentResourceManager.GetObject("color3.Image");
			this.m_i.ImageTransparentColor = Color.Magenta;
			this.m_i.Name = "color3";
			this.m_i.Size = new Size(23, 22);
			this.m_i.Text = "toolStripButton3";
			this.m_i.ToolTipText = "背景色";
			this.m_i.Click += new EventHandler(g);
			this.m_j.BackColor = Color.FromArgb(245, 234, 189);
			this.m_j.DisplayStyle = ToolStripItemDisplayStyle.None;
			this.m_j.ForeColor = Color.FromArgb(245, 234, 189);
			this.m_j.Image = (Image)componentResourceManager.GetObject("color4.Image");
			this.m_j.ImageTransparentColor = Color.Magenta;
			this.m_j.Name = "color4";
			this.m_j.Size = new Size(23, 22);
			this.m_j.Text = "toolStripButton4";
			this.m_j.ToolTipText = "背景色";
			this.m_j.Click += new EventHandler(f);
			k.BackColor = Color.FromArgb(0, 193, 193);
			k.DisplayStyle = ToolStripItemDisplayStyle.None;
			k.ForeColor = Color.FromArgb(0, 193, 193);
			k.Image = (Image)componentResourceManager.GetObject("color5.Image");
			k.ImageTransparentColor = Color.Magenta;
			k.Name = "color5";
			k.Size = new Size(23, 22);
			k.Text = "toolStripButton5";
			k.ToolTipText = "背景色";
			k.Click += new EventHandler(e);
			l.Name = "toolStripSeparator1";
			l.Size = new Size(6, 25);
			m.DisplayStyle = ToolStripItemDisplayStyle.Image;
			m.Image = (Image)componentResourceManager.GetObject("textZoomOut.Image");
			m.ImageTransparentColor = Color.Magenta;
			m.Name = "textZoomOut";
			m.Size = new Size(23, 22);
			m.Text = "a";
			m.ToolTipText = "縮小字型";
			m.Click += new EventHandler(d);
			n.DisplayStyle = ToolStripItemDisplayStyle.Image;
			n.Image = (Image)componentResourceManager.GetObject("textZoomIn.Image");
			n.ImageTransparentColor = Color.Magenta;
			n.Name = "textZoomIn";
			n.Size = new Size(23, 22);
			n.Text = "A";
			n.ToolTipText = "放大字型";
			n.Click += new EventHandler(c);
			o.DisplayStyle = ToolStripItemDisplayStyle.Image;
			o.Image = (Image)componentResourceManager.GetObject("defaultView.Image");
			o.ImageTransparentColor = Color.Magenta;
			o.Name = "defaultView";
			o.Size = new Size(23, 22);
			o.Text = "初始狀態";
			o.Click += new EventHandler(b);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(863, 599);
			base.Controls.Add(this.m_f);
			base.Controls.Add(this.m_e);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "fullTextView";
			Text = "觀看全文";
			base.Shown += new EventHandler(j);
			base.Resize += new EventHandler(a);
			this.m_f.ResumeLayout(false);
			this.m_f.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
