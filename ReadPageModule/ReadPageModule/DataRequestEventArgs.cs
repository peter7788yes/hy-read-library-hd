using System;

namespace ReadPageModule
{
	public class DataRequestEventArgs : EventArgs
	{
		public string bookId;

		public string userId;

		public string filename;

		public DataRequestEventArgs(string bookId, string userId, string filename)
		{
			this.bookId = bookId;
			this.userId = userId;
			this.filename = filename;
		}
	}
}
