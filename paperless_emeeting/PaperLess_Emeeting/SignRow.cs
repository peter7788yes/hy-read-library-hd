using PaperLess_Emeeting.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace PaperLess_Emeeting
{
	public class SignRow : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass5
		{
			private sealed class _003C_003Ec__DisplayClass7
			{
				public _003C_003Ec__DisplayClass5 CS_0024_003C_003E8__locals6;

				public BitmapImage bitmapImage;

				public void _003CGetHttpImage_003Eb__3()
				{
					if (CS_0024_003C_003E8__locals6._003C_003E4__this.sb != null)
					{
						CS_0024_003C_003E8__locals6._003C_003E4__this.sb.Stop();
					}
					CS_0024_003C_003E8__locals6._003C_003E4__this.imgSignedPic.Source = bitmapImage;
				}
			}

			public string imgUrl;

			public SignRow _003C_003E4__this;

			public void _003CGetHttpImage_003Eb__2()
			{
				try
				{
					_003C_003Ec__DisplayClass7 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass7();
					_003C_003Ec__DisplayClass.CS_0024_003C_003E8__locals6 = this;
					WebClient webClient = new WebClient();
					byte[] buffer = webClient.DownloadData(imgUrl);
					_003C_003Ec__DisplayClass.bitmapImage = new BitmapImage();
					using (MemoryStream streamSource = new MemoryStream(buffer))
					{
						_003C_003Ec__DisplayClass.bitmapImage.BeginInit();
						_003C_003Ec__DisplayClass.bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
						_003C_003Ec__DisplayClass.bitmapImage.StreamSource = streamSource;
						_003C_003Ec__DisplayClass.bitmapImage.EndInit();
						_003C_003Ec__DisplayClass.bitmapImage.Freeze();
					}
					_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003C_003Ec__DisplayClass._003CGetHttpImage_003Eb__3));
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
			}
		}

		internal TextBlock txtIndex;

		internal TextBlock txtDept;

		internal TextBlock txtTitle;

		internal TextBlock txtUserName;

		internal TextBlock txtAgent;

		internal TextBlock txtPicLoading;

		internal TextBlock txtUnSigned;

		internal Image imgSignedPic;

		private bool _contentLoaded;

		[CompilerGenerated]
		private SigninDataUser _003CsigninDataUser_003Ek__BackingField;

		[CompilerGenerated]
		private Storyboard _003Csb_003Ek__BackingField;

		public SigninDataUser signinDataUser
		{
			[CompilerGenerated]
			get
			{
				return _003CsigninDataUser_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CsigninDataUser_003Ek__BackingField = value;
			}
		}

		public Storyboard sb
		{
			[CompilerGenerated]
			get
			{
				return _003Csb_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003Csb_003Ek__BackingField = value;
			}
		}

		public SignRow(SigninDataUser signinDataUser)
		{
			InitializeComponent();
			sb = (Storyboard)TryFindResource("sb");
			this.signinDataUser = signinDataUser;
			base.Loaded += new RoutedEventHandler(SignRow_Loaded);
		}

		private void SignRow_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CSignRow_Loaded_003Eb__0));
		}

		private void InitSelectDB()
		{
		}

		private void InitEvent()
		{
		}

		private void InitUI()
		{
			string signListEmptyDash = Settings.Default.SignListEmptyDash;
			if (signinDataUser.Rank != null)
			{
				txtIndex.Text = (signinDataUser.Rank.Equals("") ? signListEmptyDash : signinDataUser.Rank);
			}
			else
			{
				txtIndex.Text = signListEmptyDash;
			}
			txtUserName.Text = signinDataUser.Name;
			txtDept.Text = signinDataUser.Dept;
			txtTitle.Text = signinDataUser.Title;
			bool.Parse(signinDataUser.IsSigned.Equals("") ? "" : signinDataUser.IsSigned);
			string text = signinDataUser.Attend.Trim();
			string text2 = signinDataUser.AgentName.Trim();
			signinDataUser.SignedPic.Trim();
			if (text.Equals("0") || text.Equals("2"))
			{
				if (!text2.Equals(""))
				{
					txtAgent.Text = string.Format("(指派代表，由{0}出席)", signinDataUser.AgentName);
				}
				else
				{
					txtAgent.Text = "(請假)";
				}
			}
			else
			{
				txtAgent.Visibility = Visibility.Collapsed;
			}
			GetHttpImage();
		}

		private void GetHttpImage()
		{
			Action action = null;
			_003C_003Ec__DisplayClass5 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass5();
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			_003C_003Ec__DisplayClass.imgUrl = signinDataUser.SignedPic.Trim();
			if (!_003C_003Ec__DisplayClass.imgUrl.Equals(""))
			{
				txtUserName.FontWeight = FontWeights.Bold;
				if (sb != null)
				{
					sb.Begin();
				}
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003C_003Ec__DisplayClass._003CGetHttpImage_003Eb__2);
				}
				factory.StartNew(action);
			}
			else
			{
				txtUnSigned.Visibility = Visibility.Visible;
			}
		}

		private void imgSignedPic_SizeChanged(object sender, SizeChangedEventArgs e)
		{
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/signrow.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				txtIndex = (TextBlock)target;
				break;
			case 2:
				txtDept = (TextBlock)target;
				break;
			case 3:
				txtTitle = (TextBlock)target;
				break;
			case 4:
				txtUserName = (TextBlock)target;
				break;
			case 5:
				txtAgent = (TextBlock)target;
				break;
			case 6:
				txtPicLoading = (TextBlock)target;
				break;
			case 7:
				txtUnSigned = (TextBlock)target;
				break;
			case 8:
				imgSignedPic = (Image)target;
				imgSignedPic.SizeChanged += new SizeChangedEventHandler(imgSignedPic_SizeChanged);
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CSignRow_Loaded_003Eb__0()
		{
			base.Dispatcher.BeginInvoke(new Action(_003CSignRow_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CSignRow_Loaded_003Eb__1()
		{
			InitUI();
		}
	}
}
