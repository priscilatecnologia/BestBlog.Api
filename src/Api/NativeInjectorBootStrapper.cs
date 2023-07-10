using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace Api
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Infra - Data
            services.AddScoped<BlogContext>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

        }
    }
}