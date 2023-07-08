using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.Web.Controllers;

public class ChatController : BaseController<Chat, IChatRepository>
{
    protected new readonly ILogger<BaseController<Chat, IChatRepository>> logger;
    private readonly UnitOfWork uow;
    private readonly IMapper mapper;


    public ChatController(UnitOfWork uow, ILogger<BaseController<Chat, IChatRepository>> logger, IChatRepository repository, IMapper mapper)
             : base(uow, logger, repository)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.uow = uow;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Chat> chats = await uow.ChatRep.ListAllAsync();
        return View(chats);
    }

    public async Task AddNewChat()
    {
        Chat chat = new Chat();
        chat.ChatName = "Чат №" + chat.Id;
        chat.CreatedAt = DateTime.Now;
        chat.ChatIMG = "1.jpg";

        await uow.ChatRep.InsertAsync(chat);
    }

    public async Task AddUserToChat(int chatId, int userId)
    {
        ChatUser chatUser = new ChatUser();
    }



}
