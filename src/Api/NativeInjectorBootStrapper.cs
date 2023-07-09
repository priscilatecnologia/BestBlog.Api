using Microsoft.Extensions.DependencyInjection;
using Repository;
using System.ComponentModel.DataAnnotations;

namespace Api
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application

            // Domain - Events

            // Domain - Commands

            // Infra - Data
            services.AddScoped<BlogContext>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            // Infra - Data EventSourcing
        }
    }
}