using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface ITalentCardSkillService
	{
		List<TalentCardSkill> GetList();
		void TalentCardSkillAddBL(TalentCardSkill talentCardSkill);
		TalentCardSkill GetByID(int id);
		void TalentCardSkillDelete(TalentCardSkill talentCardSkill);
		void TalentCardSkillUpdate(TalentCardSkill talentCardSkill);
	}
}
