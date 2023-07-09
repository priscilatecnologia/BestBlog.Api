using Model;

namespace Repository
{
    public class PostRepository : BaseRepository<Post>
    {
        public PostRepository(BlogContext context) : base(context) { }
    }
}