using Flunt.Notifications;
using Flunt.Validations;
using minimalTodo.Models;

namespace minimalTodo.ViewModels
{
    public class CreateTodoViewModel : Notifiable<Notification>
    {
        public string Title { get; set; }
        public bool Done { get; set; } = false;

        public Todo MapTo()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNull(Title, "Informe o título da tarefa")
                .IsGreaterOrEqualsThan(Title, 5, "O Título deve conter mais de cinco caracteres.");

            AddNotifications(contract);

            return new Todo(Guid.NewGuid(), Title, Done);
        }
    }
}
