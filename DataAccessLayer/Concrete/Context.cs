﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
	public class Context: DbContext
	{
		//About----->Proje içinde yer alan dosyanın ismi
		//Abouts ise Sql de veri tabanına	yansıyacak olan tablonun ismi
		public DbSet<About> Abouts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Content> Contents { get; set; }
		public DbSet<Heading> Headings { get; set; }
		public DbSet<Writer> Writers { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<ImageFile> ImageFiles { get; set; }
		public DbSet<Admin> Admins { get; set; }
		public DbSet<TalentCard> TalentCards { get; set; }
		public DbSet<TalentCardSkill> TalentCardSkills { get; set; }
		public DbSet<Intern> Interns { get; set; }

	}
}
