using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class WriterManager : IWriterService
	{
		IWriterDal _writerDal;

		public WriterManager(IWriterDal writerDal)
		{
			_writerDal = writerDal;
		}

		private void CheckIfWriterExists(Writer writer)
		{
			if (_writerDal.Get(x => x.WriterMail == writer.WriterMail) != null)
			{
				throw new Exception("Bu kullanıcı daha önce kayıt olmuştur.");
			}
		}

		public Writer GetByID(int id)
		{
			return _writerDal.Get(x => x.WriterId == id);
		}

		public List<Writer> GetList()
		{
			return _writerDal.List();
		}

		public Writer GetWriter(string mail, string password)
		{
			return _writerDal.Get(x => x.WriterMail == mail && x.WriterPassword == password);
		}

		public void UpdatePasswordWriterPanel(Writer writer)
		{
			WriterPasswordUpdate(writer);
			_writerDal.Update(writer);
		}

		public void UpdateWriterPanel(Writer writer)
		{
			WriterUpdate2(writer);
			_writerDal.Update(writer);
		}

		public void WriterAdd(Writer writer)
		{
			CheckIfWriterExists(writer);
			_writerDal.Insert(writer);
		}

		public void WriterDelete(Writer writer)
		{
			_writerDal.Delete(writer);	
		}

		private void WriterUpdate2(Writer writer)
		{
			SHA1 sha1 = new SHA1CryptoServiceProvider();
			string password = writer.WriterPassword;
			string result = Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(password)));
			if (writer.WriterPassword == null)
			{
				writer.WriterPassword = result;
			}

			writer.WriterStatus = true;
			writer.WriterRole = "C";

		}

		private void WriterPasswordUpdate(Writer writer)
		{
			if (writer.WriterPassword != null)
			{
				SHA1 sha1 = new SHA1CryptoServiceProvider();
				string password = writer.WriterPassword;
				string result = Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(password)));
				writer.WriterPassword = result;
			}
			var writerInfo = GetByID(writer.WriterId);

			writer.WriterName = writerInfo.WriterName;
			writer.WriterSurnam = writerInfo.WriterSurnam;
			writer.WriterImage = writerInfo.WriterImage;
			writer.WriterMail = writerInfo.WriterMail;
			writer.WriterAbout = writerInfo.WriterAbout;
			writer.WriterTitle = writerInfo.WriterTitle;
			writer.WriterStatus = true;
			writer.WriterRole = "C";
		}

		public void WriterUpdate(Writer writer)
		{
			_writerDal.Update(writer);
		}
	}
}
