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
	public class TalentCardManager : ITalentCardService
	{
		ITalentCardDal _talentCardDal;

		public TalentCardManager(ITalentCardDal talentCardDal)
		{
			_talentCardDal = talentCardDal;
		}

		public TalentCard GetByID(int id)
		{
			return _talentCardDal.Get(x => x.CardId == id);
		}

		public List<TalentCard> GetList()
		{
			return _talentCardDal.List();
		}

		public void TalentCardAddBL(TalentCard talentCard)
		{
			_talentCardDal.Insert(talentCard);
		}

		public void TalentCardDelete(TalentCard talentCard)
		{
			_talentCardDal.Delete(talentCard);
		}

		public void TalentCardUpdate(TalentCard talentCard)
		{
			_talentCardDal.Update(talentCard);
		}
	}
}
