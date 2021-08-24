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
	public class TalentCardSkillManager : ITalentCardSkillService
	{
		ITalentCardSkillDal _talentCardSkillDal;

		public TalentCardSkillManager(ITalentCardSkillDal talentCardSkillDal)
		{
			_talentCardSkillDal = talentCardSkillDal;
		}

		public TalentCardSkill GetByID(int id)
		{
			throw new NotImplementedException();
		}

		public List<TalentCardSkill> GetList()
		{
			return _talentCardSkillDal.List();
		}

		public void TalentCardSkillAddBL(TalentCardSkill talentCardSkill)
		{
			_talentCardSkillDal.Insert(talentCardSkill);
		}

		public void TalentCardSkillDelete(TalentCardSkill talentCardSkill)
		{
			throw new NotImplementedException();
		}

		public void TalentCardSkillUpdate(TalentCardSkill talentCardSkill)
		{
			throw new NotImplementedException();
		}
	}
}
