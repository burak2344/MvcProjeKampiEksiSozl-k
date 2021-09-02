using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class ContentManager : IContentService
	{
		IContentDal _contentDal;

		public ContentManager(IContentDal contentDal)
		{
			_contentDal = contentDal;
		}

		public void ContentAddBL(Content content)
		{
			_contentDal.Insert(content);
		}

		public void ContentDelete(Content content)
		{
			_contentDal.Delete(content);
		}

		public void ContentUpdate(Content content)
		{
			_contentDal.Update(content);
		}

		public List<Content> GetAll(string parameter)
		{
			if (parameter == null)
			{
				return _contentDal.List();
			}
			return _contentDal.List(x => x.ContentValue.Contains(parameter));
		}

		public List<Content> GetAllByWriter(int id)
		{
			return _contentDal.List(x => x.WriterId == id);

		}

		public Content GetByID(int id)
		{
			return _contentDal.Get(x => x.ContentId == id);
		}

		public List<Content> GetList()
		{
			return _contentDal.List();
		}

		public List<Content> GetListByHeadingID(int id)
		{
			return _contentDal.List(x => x.HeadingId == id);
		}
	}
}
