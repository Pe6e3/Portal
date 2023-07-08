using Portal.BLL.Repositories;
using Portal.DAL.Data;
namespace Portal.BLL;

public class UnitOfWork
{
    private readonly AppDbContext _db;
    public UnitOfWork(AppDbContext db)
    {
        _db = db;
    }

    private CategoryRepository _categoryRep;
    private CommentRepository _commentRep;
    private MenuRepository _menuRep;
    private MenuItemRepository _menuItemRep;
    private PostRepository _postRep;
    private PostCategoryRepository _postCategoryRep;
    private PostCommentRepository _postCommentRep;
    private PostContentRepository _postContentRep;
    private RoleRepository _roleRep;
    private UserRepository _userRep;
    private UserProfileRepository _userProfileRep;
    private MyLoggerRepository _myLogRep;
    private ChatRepository _chatRep;
    private MessageRepository _messageRep;

    public CategoryRepository CategoryRep => _categoryRep ??= new CategoryRepository(_db);
    public CommentRepository CommentRep => _commentRep ??= new CommentRepository(_db);
    public MenuRepository MenuRep => _menuRep ??= new MenuRepository(_db);
    public MenuItemRepository MenuItemRep => _menuItemRep ??= new MenuItemRepository(_db);
    public PostRepository PostRep => _postRep ??= new PostRepository(_db);
    public PostCategoryRepository PostCategoryRep => _postCategoryRep ??= new PostCategoryRepository(_db);
    public PostCommentRepository PostCommentRep => _postCommentRep ??= new PostCommentRepository(_db);
    public PostContentRepository PostContentRep => _postContentRep ??= new PostContentRepository(_db);
    public RoleRepository RoleRep => _roleRep ??= new RoleRepository(_db);
    public UserRepository UserRep => _userRep ??= new UserRepository(_db);
    public UserProfileRepository UserProfileRep => _userProfileRep ??= new UserProfileRepository(_db);
    public MyLoggerRepository MyLoggerRep => _myLogRep ??= new MyLoggerRepository(_db);
    public ChatRepository ChatRep => _chatRep ??= new ChatRepository(_db);
    public MessageRepository MessageRep => _messageRep ??= new MessageRepository(_db);
}
