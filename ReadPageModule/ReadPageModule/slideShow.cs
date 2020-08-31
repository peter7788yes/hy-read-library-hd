using CACodec;
using ConfigureManagerModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ReadPageModule
{
	public class slideShow : Form
	{
		public string hsdFile;

		private string m_a;

		private Bitmap m_b;

		private Bitmap m_c;

		private int m_d;

		private int m_e = 1;

		private int m_f = 3;

		private int m_g = 80;

		private int m_h = 100;

		private int i;

		private int j;

		private int k;

		private List<Image> l = new List<Image>();

		private CACodecTools m = new CACodecTools();

		private ConfigurationManager n;

		private byte[] o;

		private IContainer p;

		private PictureBox q;

		private ToolStrip r;

		private ToolStripButton s;

		private ToolStripLabel t;

		private Timer u;

		private SplitContainer v;

		private Panel w;

		private ToolStripButton x;

		private ToolStripComboBox y;

		private ToolStripSeparator z;

		private ToolStripButton aa;

		public slideShow(ConfigurationManager configMng, byte[] aesKey)
		{
			n = configMng;
			o = aesKey;
			a();
			Rectangle bounds = Screen.PrimaryScreen.Bounds;
			base.Width = (int)((double)bounds.Width * 0.8);
			base.Height = (int)((double)bounds.Height * 0.8);
			this.m_f = configMng.saveSlideShowTime;
			y.SelectedIndex = this.m_f - 1;
		}

		private void h(object A_0, EventArgs A_1)
		{
			f();
			b();
			d();
			if (l.Count > 0)
			{
				e();
				u.Start();
			}
			else
			{
				MessageBox.Show("讀取幻燈片圖檔失敗");
				Close();
			}
		}

		private void f()
		{
			this.m_a = hsdFile.Substring(0, hsdFile.IndexOf("slides")) + "images\\";
			Stream stream = m.fileAESDecode(hsdFile, o, false);
			XmlDocument xmlDocument = new XmlDocument();
			string text = "";
			l = new List<Image>();
			if (stream.Length > 0)
			{
				using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
				{
					text = streamReader.ReadToEnd();
				}
				text = text.Replace("xmlns=\"http://www.hyweb.com.tw/schemas/hsd\" version=\"1.0\"", "");
				try
				{
					xmlDocument.LoadXml(text);
					XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/slide/itemref");
					w.Controls.Clear();
					foreach (XmlNode item2 in xmlNodeList)
					{
						string str = a(item2, "idref");
						Image item = (Bitmap)Image.FromStream(m.fileAESDecode(this.m_a + str + ".jpg", false));
						l.Add(item);
					}
				}
				catch
				{
				}
			}
		}

		private void g(object A_0, EventArgs A_1)
		{
			k++;
			if (k / 2 >= this.m_f)
			{
				this.m_d += this.m_e;
				if (this.m_d < 0)
				{
					this.m_d = l.Count - 1;
				}
				else if (this.m_d == l.Count)
				{
					this.m_d = 0;
				}
				e();
				k = 0;
			}
		}

		private void e()
		{
			if (this.m_d < this.i)
			{
				this.i = this.m_d - j / 2;
				this.i = ((this.i >= 0) ? this.i : 0);
				d();
			}
			else if (l.Count > j)
			{
				if (this.i + j > l.Count)
				{
					c();
				}
				else if (this.m_d > j / 2)
				{
					this.i = this.m_d - j / 2;
					this.i = ((this.i >= 0) ? this.i : 0);
					d();
				}
				else
				{
					c();
				}
			}
			else
			{
				c();
			}
			Image image = l[this.m_d];
			this.m_b = (Bitmap)image;
			q.Height = v.Panel1.Height;
			q.Width = (int)((float)q.Height / (float)image.Height * (float)image.Width);
			q.Location = new Point((v.Panel1.Width - q.Width) / 2, 0);
			this.m_c = new Bitmap(this.m_b, q.Width, q.Height);
			int width = q.Width;
			int height = q.Height;
			Graphics graphics = q.CreateGraphics();
			for (int i = 1; i <= width; i += q.Width / 100)
			{
				Bitmap image2 = this.m_c.Clone(new Rectangle(0, 0, i, height), PixelFormat.Format24bppRgb);
				graphics.DrawImage(image2, 0, 0);
			}
			q.Image = this.m_c;
		}

		private void d()
		{
			w.Controls.Clear();
			int num = 0;
			for (int i = this.i; i < l.Count; i++)
			{
				PictureBox pictureBox = new PictureBox();
				pictureBox.Size = new Size(this.m_g, this.m_h);
				pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
				pictureBox.Padding = new Padding(2, 2, 2, 2);
				if (i == this.m_d)
				{
					pictureBox.BackColor = Color.Blue;
				}
				pictureBox.Left = num;
				pictureBox.Tag = i;
				pictureBox.Image = l[i];
				w.Controls.Add(pictureBox);
				num += pictureBox.Width;
				pictureBox.MouseClick += new MouseEventHandler(a);
			}
		}

		private void c()
		{
			int num = i;
			foreach (PictureBox control in w.Controls)
			{
				if (num == this.m_d)
				{
					control.BackColor = Color.Blue;
				}
				else
				{
					control.BackColor = Color.White;
				}
				num++;
			}
		}

		private void f(object A_0, EventArgs A_1)
		{
			k = 0;
			this.m_d = (int)((PictureBox)A_0).Tag;
			e();
		}

		private static string a(XmlNode A_0, string A_1)
		{
			string result = null;
			try
			{
				for (int i = 0; i < A_0.Attributes.Count; i++)
				{
					if (A_0.Attributes[i].Name.ToString().Equals(A_1))
					{
						result = A_0.Attributes[i].Value.ToString();
						return result;
					}
				}
				return result;
			}
			catch
			{
				return result;
			}
		}

		private void a(object A_0, FormClosingEventArgs A_1)
		{
			u.Stop();
		}

		private void e(object A_0, EventArgs A_1)
		{
			b();
		}

		private void b()
		{
			v.SplitterDistance = Convert.ToInt32((double)base.Height * 0.7);
			q.Width = (int)((double)v.Panel1.Width - (double)q.Width * 0.2);
			q.Height = v.Panel1.Height;
			q.Location = new Point((v.Panel1.Width - q.Width) / 2, 0);
			w.Width = (int)((double)v.Panel2.Width * 0.6);
			w.Height = (int)((double)v.Panel2.Height * 0.95);
			int num = (v.Panel1.Width - w.Width) / 2;
			w.Location = new Point(num, 5);
			q.Refresh();
			this.m_h = w.Height;
			this.m_g = (int)((double)this.m_h * 0.8);
			int num2 = l.Count * this.m_g;
			j = w.Width / this.m_g;
			if (num2 <= w.Width)
			{
				i = 0;
				w.Width = num2;
				num = (v.Panel1.Width - w.Width) / 2;
				w.Location = new Point(num, 5);
			}
			d();
		}

		private void d(object A_0, EventArgs A_1)
		{
			this.m_f = Convert.ToInt16(y.Text);
		}

		private void c(object A_0, EventArgs A_1)
		{
			u.Start();
			s.Visible = false;
			x.Visible = true;
		}

		private void b(object A_0, EventArgs A_1)
		{
			u.Stop();
			s.Visible = true;
			x.Visible = false;
		}

		private void a(object A_0, EventArgs A_1)
		{
			this.m_e *= -1;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && p != null)
			{
				p.Dispose();
			}
			base.Dispose(disposing);
		}

		private void a()
		{
			p = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(slideShow));
			q = new PictureBox();
			r = new ToolStrip();
			s = new ToolStripButton();
			x = new ToolStripButton();
			aa = new ToolStripButton();
			z = new ToolStripSeparator();
			t = new ToolStripLabel();
			y = new ToolStripComboBox();
			u = new Timer(p);
			v = new SplitContainer();
			w = new Panel();
			((ISupportInitialize)q).BeginInit();
			r.SuspendLayout();
			v.Panel1.SuspendLayout();
			v.Panel2.SuspendLayout();
			v.SuspendLayout();
			SuspendLayout();
			q.BackgroundImageLayout = ImageLayout.Zoom;
			q.Location = new Point(138, 3);
			q.Name = "pic_SlideBox";
			q.Size = new Size(536, 384);
			q.SizeMode = PictureBoxSizeMode.CenterImage;
			q.TabIndex = 0;
			q.TabStop = false;
			r.BackColor = Color.Black;
			r.ImageScalingSize = new Size(24, 24);
			r.Items.AddRange(new ToolStripItem[6]
			{
				s,
				x,
				aa,
				z,
				t,
				y
			});
			r.Location = new Point(0, 0);
			r.Name = "toolStrip1";
			r.Size = new Size(784, 31);
			r.TabIndex = 1;
			r.Text = "toolStrip1";
			s.DisplayStyle = ToolStripItemDisplayStyle.Image;
			s.Image = (Image)componentResourceManager.GetObject("btn_Play.Image");
			s.ImageTransparentColor = Color.Magenta;
			s.Name = "btn_Play";
			s.Size = new Size(28, 28);
			s.Text = "||";
			s.ToolTipText = "播放";
			s.Visible = false;
			s.Click += new EventHandler(c);
			x.DisplayStyle = ToolStripItemDisplayStyle.Image;
			x.Image = (Image)componentResourceManager.GetObject("btn_stop.Image");
			x.ImageTransparentColor = Color.Magenta;
			x.Name = "btn_stop";
			x.Size = new Size(28, 28);
			x.ToolTipText = "停止播放";
			x.Click += new EventHandler(b);
			aa.DisplayStyle = ToolStripItemDisplayStyle.Text;
			aa.Font = new Font("Microsoft JhengHei UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 136);
			aa.ForeColor = Color.White;
			aa.Image = (Image)componentResourceManager.GetObject("btn_playDirection.Image");
			aa.ImageTransparentColor = Color.Magenta;
			aa.Name = "btn_playDirection";
			aa.Size = new Size(65, 28);
			aa.Text = "順<>逆";
			aa.Click += new EventHandler(a);
			z.Name = "toolStripSeparator1";
			z.Size = new Size(6, 31);
			t.Font = new Font("Microsoft JhengHei UI", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 136);
			t.ForeColor = Color.White;
			t.Name = "lab_slideShowTime";
			t.Size = new Size(69, 28);
			t.Text = "播放速度";
			y.AutoSize = false;
			y.BackColor = Color.Black;
			y.DropDownStyle = ComboBoxStyle.DropDownList;
			y.DropDownWidth = 30;
			y.FlatStyle = FlatStyle.Flat;
			y.Font = new Font("Microsoft JhengHei UI", 12f, FontStyle.Bold, GraphicsUnit.Point, 136);
			y.ForeColor = Color.White;
			y.Items.AddRange(new object[12]
			{
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"15",
				"20"
			});
			y.Name = "show_speedcombo";
			y.Size = new Size(50, 28);
			y.ToolTipText = "播放速度";
			y.SelectedIndexChanged += new EventHandler(d);
			u.Interval = 500;
			u.Tick += new EventHandler(g);
			v.Dock = DockStyle.Fill;
			v.Location = new Point(0, 31);
			v.Name = "splitContainer1";
			v.Orientation = Orientation.Horizontal;
			v.Panel1.Controls.Add(q);
			v.Panel2.Controls.Add(w);
			v.Size = new Size(784, 530);
			v.SplitterDistance = 390;
			v.TabIndex = 2;
			w.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			w.BackColor = SystemColors.Control;
			w.Location = new Point(138, 14);
			w.Name = "smallPic_panel";
			w.Size = new Size(536, 111);
			w.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(784, 561);
			base.Controls.Add(v);
			base.Controls.Add(r);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MinimizeBox = false;
			base.Name = "slideShow";
			base.StartPosition = FormStartPosition.CenterScreen;
			Text = "幻燈片展示";
			base.FormClosing += new FormClosingEventHandler(a);
			base.Shown += new EventHandler(h);
			base.Resize += new EventHandler(e);
			((ISupportInitialize)q).EndInit();
			r.ResumeLayout(false);
			r.PerformLayout();
			v.Panel1.ResumeLayout(false);
			v.Panel2.ResumeLayout(false);
			v.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		[CompilerGenerated]
		private void a(object A_0, MouseEventArgs A_1)
		{
			f(A_0, A_1);
		}
	}
}
