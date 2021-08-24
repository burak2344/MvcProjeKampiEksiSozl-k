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
	public class AdminManager : IAdminService
	{
		IAdminDal _adminDal;
		IAdminService _adminService;

		public AdminManager(IAdminService adminService)
		{
			_adminService = adminService;
		}

		public AdminManager(IAdminDal adminDal)
		{
			_adminDal = adminDal;
		}

		public void AdminAddBL(Admin admin)
		{
			CheckIfAdminExists(admin);
			_adminDal.Insert(admin);
		}

		public void AdminDelete(Admin admin)
		{
			_adminDal.Delete(admin);
		}

		public void AdminUpdate(Admin admin)
		{
			_adminDal.Update(admin);
		}

		private void CheckIfAdminExists(Admin admin)
		{
			if (_adminDal.Get(x => x.AdminUserName == admin.AdminUserName) != null)
			{
				throw new Exception("Bu kullanıcı daha önce kayıt olmuştur.");
			}
		}

		public Admin GetAdmin(string mail, string password)
		{
			return _adminDal.Get(x => x.AdminUserName == mail && x.AdminPassword == password);
		}

		public Admin GetByID(int id)
		{
			return _adminDal.Get(x => x.AdminId == id);
		}

		public List<Admin> GetList()
		{
			return _adminDal.List();
		}

	
	}
}
