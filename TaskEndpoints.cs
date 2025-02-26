using Microsoft.EntityFrameworkCore;

class TaskEndpoints {
    public static void Map(RouteGroupBuilder app) {
        app.MapGet("/", async (Db db) => {
            var tasks = await db.Tasks.ToArrayAsync();

            return TypedResults.Ok(tasks);
        });

        app.MapGet("/completed", async (Db db) => {
            var tasks = await db.Tasks.Where(t => t.isCompleted).ToArrayAsync();

            return TypedResults.Ok(tasks);
        });

        app.MapGet("/uncompleted", async (Db db) => {
            var tasks = await db.Tasks.Where(t => !t.isCompleted).ToArrayAsync();

            return TypedResults.Ok(tasks);
        });

        app.MapPost("/", async (Db db, Task task) => {
            task.CreatedAt = DateTime.Now;

            db.Tasks.Add(task);
            await db.SaveChangesAsync();

            return TypedResults.Ok(task);
        });

        app.MapPut("/{id}", async (Db db, Task task, int id) => {
            var updateTask = await db.Tasks.FindAsync(id);

            if (updateTask is null) {
                return Results.NotFound();
            }

            updateTask.UpdatedAt = DateTime.Now;
            updateTask.Name = task.Name;
            updateTask.isCompleted = task.isCompleted;
            updateTask.StartAt = task.StartAt;

            await db.SaveChangesAsync();
            return Results.Ok(updateTask);
        });

        app.MapDelete("/{id}", async (Db db, int id) => {
            var deleteTask = await db.Tasks.FindAsync(id);

            if (deleteTask is null) {
                return Results.NotFound();
            }

            db.Tasks.Remove(deleteTask);
            await db.SaveChangesAsync();
            return Results.Ok();
        });
    }
}
