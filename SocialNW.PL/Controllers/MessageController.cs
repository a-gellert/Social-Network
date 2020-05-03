using AutoMapper;
using Microsoft.AspNet.Identity;
using SocialNW.BLL.DTO;
using SocialNW.BLL.Interfaces;
using SocialNW.PL.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SocialNW.PL.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public ActionResult Index()
        {

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<MessageDTO, MessageViewModel>()));
            var messages = mapper.Map<IEnumerable<MessageDTO>, List<MessageViewModel>>(_messageService.GetAll());

            return View(messages);
        }

        public ActionResult Create(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(MessageViewModel model, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<MessageViewModel, MessageDTO>()));
                    var message = mapper.Map<MessageViewModel, MessageDTO>(model);

                    var ownerId = User.Identity.GetUserId<int>();
                    _messageService.SendMessage(message, ownerId, id);
                    return RedirectToAction("GetUserMessages", new { id = ownerId });
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return View("Create", model);
        }

        public ActionResult Remove(int id)
        {
            try
            {
                _messageService.Remove(id);
                return RedirectToAction("Index", "Message");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Edit(MessageViewModel model)
        {
            try
            {
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<MessageViewModel, MessageDTO>()));
                var message = mapper.Map<MessageViewModel, MessageDTO>(model);
                _messageService.Update(message.Id, message);
                return RedirectToAction("Index", "Message");
            }
            catch (Exception)
            {
                return View("Create", model);
            }
        }

        public ActionResult HasUnreadedMessages(int id)
        {
            var countMessages = _messageService.CountUnreadedMessages(id);

            return Json(new { countMessages },
                 JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserMessages()
        {
            var messagesDto = _messageService.GetUserMessages(User.Identity.GetUserId<int>());
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<MessageDTO, MessageViewModel>()));
            var messages = mapper.Map<IEnumerable<MessageDTO>, List<MessageViewModel>>(messagesDto);

            return View(messages);
        }
    }
}