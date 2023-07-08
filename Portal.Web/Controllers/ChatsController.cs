using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

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
        ChatUser chatUser = new ChatUser();
        chatUser.ChatId = chatId;
        chatUser.UserId = userId;
        await uow.ChatUserRep.InsertAsync(chatUser);
        Chat chat = await uow.ChatRep.GetByIdAsync(chatId);
        chat.UserCount++;
        await uow.ChatRep.UpdateAsync(chat);
        return RedirectToAction("Index");

    }



}
