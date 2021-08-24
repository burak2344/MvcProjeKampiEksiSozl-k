using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface ITalentCardService
	{
		List<TalentCard> GetList();
		void TalentCardAddBL(TalentCard talentCard);
		TalentCard GetByID(int id);
		void TalentCardDelete(TalentCard talentCard);
		void TalentCardUpdate(TalentCard talentCard);
	}
}
