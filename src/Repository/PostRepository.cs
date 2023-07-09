using Model;

namespace Repository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(BlogContext context) : base(context) { }
    }
}