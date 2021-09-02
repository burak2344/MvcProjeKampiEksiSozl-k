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
	public class InternManager : IInternService
	{
		IInternDal _ınternDal;

		public InternManager(IInternDal ınternDal)
		{
			_ınternDal = ınternDal;
		}
		public void Add(Intern ıntern)
		{
			_ınternDal.Insert(ıntern);
		}

		public List<Intern> GetAll()
		{
			return _ınternDal.List();
		}
	}
}
