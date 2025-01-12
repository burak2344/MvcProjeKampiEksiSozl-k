﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class MessageManager:IMessageService
	{
		IMessageDal _messageDal;

		public MessageManager(IMessageDal messageDal)
		{
			_messageDal = messageDal;
		}

		public List<Message> GetAll()
		{
			return _messageDal.List();
		}

		public List<Message> GetAllDraft(string parameter)
		{
			return _messageDal.List(x => x.SenderMail == parameter);
		}

		public List<Message> GetAllRead(string parameter)
		{
			return _messageDal.List(x => x.ReceiverMail == parameter);
		}

		public List<Message> GetAllUnRead(string parameter)
		{
			return _messageDal.List(x => x.ReceiverMail == parameter).Where(x => x.IsRead == false).ToList();

		}

		public Message GetByID(int id)
		{
			return _messageDal.Get(x => x.MessageId == id);
		}


		public List<Message> GetListInbox(string parameter)
		{
			return _messageDal.List(x => x.ReceiverMail == parameter);
		}


		public List<Message> GetListSendbox(string parameter)
		{
			return _messageDal.List(x => x.SenderMail == parameter).Where(x => x.IsDraft == false).ToList();
		}

		public List<Message> IsDraft()
		{
			return _messageDal.List(x => x.IsDraft == true);
		}

		public void MessageAddBL(Message message)
		{
			_messageDal.Insert(message);
		}

		public void MessageDelete(Message message)
		{
			_messageDal.Delete(message);
		}

		public void MessageUpdate(Message message)
		{
			_messageDal.Update(message);
		}

		public void SaveDraftAdd(Message message)
		{
			_messageDal.Insert(message);
		}
	}
}
