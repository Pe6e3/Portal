using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.Controllers;
using Portal.Web.ViewModels;
using System.Data;

namespace Portal.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "1,2")]
public class ChatsController : BaseController<Chat, IChatRepository>
{
    protected new readonly ILogger<BaseController<Chat, IChatRepository>> logger;
    private readonly UnitOfWork uow;
    private readonly IMapper mapper;


    public ChatsController(UnitOfWork uow, ILogger<BaseController<Chat, IChatRepository>> logger, IChatRepository repository, IMapper mapper)
             : base(uow, logger, repository)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.uow = uow;
    }

    public override async Task<IActionResult> Index()
    {
        IEnumerable<Chat> chats = await uow.ChatRep.GetAllChats();
        IEnumerable<User> allUsers = await uow.UserRep.GetAllUsers();
        ViewBag.AllUsers = allUsers;
        return View(chats);
    }

    public async Task<IActionResult> AddNewChat(string chatName)
    {
        Chat chat = new Chat();
        chat.ChatName = chatName;
        chat.CreatedAt = DateTime.Now;
        chat.ChatIMG = "1.jpg";

        await uow.ChatRep.InsertAsync(chat);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> AddUserToChat(int chatId, int userId)
    {
        bool isUserExistInChat = await uow.ChatUserRep.IsExist(chatId, userId);
        if (!isUserExistInChat)
        {
            ChatUser chatUser = new ChatUser();
            chatUser.ChatId = chatId;
            chatUser.UserId = userId;
            await uow.ChatUserRep.InsertAsync(chatUser);
            Chat chat = await uow.ChatRep.GetByIdAsync(chatId);
            chat.UserCount++;
            await uow.ChatRep.UpdateAsync(chat);
        }
        return RedirectToAction("ShowChatInfo", new { chatId });
    }

    public async Task<IActionResult> DeleteUserFromChat(int chatId, int userId)
    {
        bool isUserExistInChat = await uow.ChatUserRep.IsExist(chatId, userId);
        if (isUserExistInChat)
        {
            ChatUser chatUser = await uow.ChatUserRep.FindChatUserByUserId(userId);
            await uow.ChatUserRep.DeleteAsync(chatUser);
        }
        return RedirectToAction("ShowChatInfo", new { chatId });

    }

    public async Task<IActionResult> ShowChatInfo(int chatId)
    {
        List<ChatUser> chatUsers = await uow.ChatUserRep.GetChatInfo(chatId);
        List<ChatProfileViewModel> chatProfileVM = new List<ChatProfileViewModel>();
        mapper.Map(chatUsers, chatProfileVM);
        User user = await uow.UserRep.GetUserByLogin(User.Identity.Name);


        ViewBag.UserId = user.Id;
        ViewBag.ChatId = chatUsers.FirstOrDefault().ChatId;
        ViewBag.ChatName = chatUsers.FirstOrDefault().Chat.ChatName;
        ViewBag.ChatIMG = chatUsers.FirstOrDefault().Chat.ChatIMG;
        ViewBag.CreatedAt = chatUsers.FirstOrDefault().Chat.CreatedAt;
        ViewBag.UserCount = chatUsers.FirstOrDefault().Chat.UserCount;
        ViewBag.UserDroplist = (await uow.UserRep.ListAll("Profile")).Where(user => !chatUsers.Any(chatUser => chatUser.User.Id == user.Id)).ToList(); /*Список пользователей, которых нет в данном чате*/
        return View("ChatInfo", chatProfileVM);
    }

    public async Task<IActionResult> DeleteChat(int chatId)
    {
        Chat chat = await uow.ChatRep.GetByIdAsync(chatId);
        List<ChatUser> chatUsers = await uow.ChatUserRep.ListChatUsersOfChat(chatId);
        foreach (var chatUser in chatUsers)
            await uow.ChatUserRep.DeleteAsync(chatUser);
        await uow.ChatRep.DeleteAsync(chat);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> ShowAllChatUser() /*метод для тестирования, потом удалить*/
    {
        return View("ChatUsers", await uow.ChatUserRep.ListAll());
    }

    [HttpPost]
    public async Task<IActionResult> AddMessage(Message message)
    {
        message.SentAt = DateTime.Now;
        await uow.MessageRep.InsertAsync(message);
        return RedirectToAction("ShowChatInfo");
    }


}
