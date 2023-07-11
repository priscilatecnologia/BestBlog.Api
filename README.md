# BestBlog.Api

## General Report

Here has two branches in our project: '**main**' and '**feat**.' Let's consider an example of implementing new features. First, we make the necessary changes in the 'feat' branch, focusing on developing and testing the desired functionalities. Once the features are complete and stable, we can submit a Pull Request to merge these changes into the main release-ready 'main' branch. This allows for a systematic and controlled integration of new features into the main codebase.

Also included:
- A sample demonstrating TDD and **unit testing**.
- Used the **Moq library** to perform unit tests instead of integrated tests within the Controller (reposotory) class.
- Configured the Dependency Injection and also added Interfaces and **Abstraction class**
- Provided examples of how to add validations and **handle HTTP status codes**  specifically in the **CommentController** class.
- Added OpenAPI **Swagger** documentation.
- Added **docker-compose.yml** to construct **containers** to attend the application, including a **SQL Server database** image. 
- This last PR was possbile to configure the database connection. Key words: EF, Migrations, DbContext, SaveChanges, SQL Server 
- I provided an example to **demonstrate the validation approach**. Implemented the only following request validations

## Implemented Validations
- This version includes Post Model Class validations. All changes validations:
- Comment.`PostId` must be an existing post id -> CommentController class has **exemple** how to implement the *Application Services* with **roles behaviors**. It contains one more than one repository class to fit the riquiered roles
- Comment.`Content` should not have more than 120 characters -> An Example using FluentValidation
- Comment.`Author` should not have more than 30 characters
- Post.`Content` should not have more than 120 characters
- Post.`Title` should not have more than 30 characters

## Persistence layer
Implemented the empty methods to persist the data. To avoid code repetition, I introduced an abstraction class.
- `CommentRepository.GetAll` - finds all existing comments
- `CommentRepository.Get` - find the comment by id
- `CommentRepository.Create` - inserts a given comment
- `CommentRepository.Update` - updates a given comment
- `CommentRepository.Delete` - deletes a given comment
- `CommentRepository.GetByPostId` - finds all comments by post id

- `PostRepository.GetAll` - finds all existing posts
- `PostRepository.Get` - finds the post by id
- `PostRepository.Create` - inserts a given post
- `PostRepository.Update` - updates a given comment
- `PostRepository.Delete` - deletes a given comment
