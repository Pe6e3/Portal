using Portal.BLL.Repositories;
using Portal.DAL.Data;
using Portal.DAL.Entities;

namespace Portal.BLL;

public class UnitOfWork
{
    private readonly AppDbContext db;
    public UnitOfWork(AppDbContext db)
    {
       this. db = db;
    }

    private CategoryRepository _categoryRep;
    private CommentRepository _commentRep;
    private MenuRepository _menuRep;
    private MenuItemRepository _menuItemRep;
    private PostRepository _postRep;
    private PostCategoryRepository _postCategoryRep;
    private PostContentRepository _postContentRep;
    private RoleRepository _roleRep;
    private UserRepository _userRep;
    private UserProfileRepository _userProfileRep;
    private MyLoggerRepository _myLogRep;
    private ChatRepository _chatRep;
    private ChatUserRepository _chatUserRep;
    private MessageRepository _messageRep;

    public CategoryRepository CategoryRep => _categoryRep ??= new CategoryRepository(db);
    public CommentRepository CommentRep => _commentRep ??= new CommentRepository(db);
    public MenuRepository MenuRep => _menuRep ??= new MenuRepository(db);
    public MenuItemRepository MenuItemRep => _menuItemRep ??= new MenuItemRepository(db);
    public PostRepository PostRep => _postRep ??= new PostRepository(db);
    public PostCategoryRepository PostCategoryRep => _postCategoryRep ??= new PostCategoryRepository(db);
    public PostContentRepository PostContentRep => _postContentRep ??= new PostContentRepository(db);
    public RoleRepository RoleRep => _roleRep ??= new RoleRepository(db);
    public UserRepository UserRep => _userRep ??= new UserRepository(db);
    public UserProfileRepository UserProfileRep => _userProfileRep ??= new UserProfileRepository(db);
    public MyLoggerRepository MyLoggerRep => _myLogRep ??= new MyLoggerRepository(db);
    public ChatRepository ChatRep => _chatRep ??= new ChatRepository(db);
    public ChatUserRepository ChatUserRep => _chatUserRep ??= new ChatUserRepository(db);
    public MessageRepository MessageRep => _messageRep ??= new MessageRepository(db);

}
