using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using Portal.Web.ViewModels;

namespace Portal.Web.Controllers;

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

    public async Task<IActionResult> AddNewChat()
    {
        Chat chat = new Chat();
        chat.ChatName = "Чат №";
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
        return RedirectToAction("Index");

    }

    public async Task<IActionResult> ShowChatInfo(int chatId)
    {
        List<ChatUser> chatUser = await uow.ChatUserRep.GetChatInfo(chatId);
        List<ChatProfileViewModel> chatProfileVM = new List<ChatProfileViewModel>();
        mapper.Map(chatUser, chatProfileVM);



        ViewBag.ChatName = chatUser.FirstOrDefault().Chat.ChatName;
        ViewBag.ChatIMG = chatUser.FirstOrDefault().Chat.ChatIMG;
        ViewBag.CreatedAt = chatUser.FirstOrDefault().Chat.CreatedAt;
        ViewBag.UserCount = chatUser.FirstOrDefault().Chat.UserCount;
        return View("ChatInfo", chatProfileVM);
    }


}
